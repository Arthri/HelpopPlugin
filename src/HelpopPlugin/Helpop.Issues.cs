using System;
using Terraria;
using TShockAPI;

namespace HelpopPlugin
{
    public partial class Helpop
    {
        private readonly HandledHandlersCollection<OnIssueEventArgs> _onIssueHandlers = new();

        /// <summary>
        /// Occurs when an issue is issued.
        /// </summary>
        /// <remarks>As opposed to <see cref="Events.OnIssue"/>, this event is always invoked on the main thread.</remarks>
        public event HandledHandler<OnIssueEventArgs> OnIssue
        {
            add
            {
                _onIssueHandlers.Add(value);
            }

            remove
            {
                _onIssueHandlers.Remove(value);
            }
        }

        private void Initialize_Issues()
        {
            Events.OnIssue += HandleOnIssue;
            OnIssue += HandleOnIssue_OnMainThread;

            AddCommand(
                Permissions.Issues_Raise,
                Command_RaiseIssue,
                "raiseissue", "issue", "helpop", "report", "sendhelp"
            );
        }

        private void HandleOnIssue(OnIssueEventArgs eventArgs)
        {
            Main.QueueMainThreadAction(() => _onIssueHandlers.Invoke(eventArgs));
        }

        private void HandleOnIssue_OnMainThread(OnIssueEventArgs eventArgs)
        {
            var issueString = new Lazy<string>(() => ReportTemplate.Render(new { Issue = eventArgs.IssuedIssue }));

            for (int i = 0; i < TShock.Players.Length; i++)
            {
                var tsPlayer = TShock.Players[i];

                if (tsPlayer.HasPermission(Permissions.Issues_See))
                {
                    tsPlayer.SendMessage(issueString.Value, PluginSettings.ReportMessageColor);
                }
            }
        }

        private void Command_RaiseIssue(CommandArgs args)
        {
            var message = string.Join(" ", args.Parameters);
            var account = args.Player.Account;
            var issuerAccount = account == null
                ? null
                : new UserAccount(account.ID, account.Name);
            var issuer = new IssueUser(args.Player.Name, args.Player.IP, args.Player.UUID, issuerAccount);
            var issue = new Issue(message, issuer)
            {
                Origin = PluginSettings.ReportOrigin,
            };

            var sendTask = RedisConnector.SendIssueAsync(issue);
            sendTask
                .ContinueWith((task) => Main.QueueMainThreadAction(() => OnSendTaskFinish(task)));

            args.Player.SendInfoMessage("Sending report...");

            void OnSendTaskFinish(Task<long> task)
            {
                if (task.IsCanceled)
                {
                    args.Player.SendErrorMessage("Report was intercepted.");
                }
                else if (task.IsFaulted)
                {
                    args.Player.SendErrorMessage("Encountered error while sending report. Please contact administrators via another channel");
                    TShock.Log.Error($"Error while sending report: {task.Exception}");
                }
                else if (!task.IsCompleted)
                {
                    args.Player.SendErrorMessage("Report was not sent.");
                }
                else
                {
                    args.Player.SendSuccessMessage("Report successfully sent");
                }
            }
        }
    }
}
