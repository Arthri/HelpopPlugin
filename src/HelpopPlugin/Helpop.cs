using HelpopPlugin.Configuration;
using Scriban;
using System;
using System.Reflection;
using System.Threading.Tasks;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;
using TShockAPI.Hooks;

namespace HelpopPlugin
{
    [ApiVersion(2, 1)]
    public partial class Helpop : TerrariaPlugin
    {
        /// <inheritdoc />
        public override string Name => typeof(Helpop).Assembly.GetCustomAttribute<AssemblyTitleAttribute>().Title;

        /// <inheritdoc />
        public override string Description => typeof(Helpop).Assembly.GetCustomAttribute<AssemblyDescriptionAttribute>().Description;

        /// <inheritdoc />
        public override Version Version => typeof(Helpop).Assembly.GetName().Version;

        /// <inheritdoc />
        public override string Author => typeof(Helpop).Assembly.GetCustomAttribute<AssemblyCompanyAttribute>().Company;

        private readonly ConfigurationManager _configManager = new ConfigurationManager();

        private TemplateManager _templateManager;

        /// <summary>
        /// Gets or sets the general plugin settings.
        /// </summary>
        public PluginSettings PluginSettings
        {
            get => _configManager.PluginConfigFile.Settings;
            set => _configManager.PluginConfigFile.Settings = value;
        }

        /// <summary>
        /// Gets or sets the Redis settings.
        /// </summary>
        public RedisSettings RedisSettings
        {
            get => _configManager.RedisConfigFile.Settings;
            set => _configManager.RedisConfigFile.Settings = value;
        }

        private readonly HandledHandlersCollection<OnIssueEventArgs> _onIssueHandlers = new HandledHandlersCollection<OnIssueEventArgs>();

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

        private bool _tshockReloadHooked = false;

        public Template ReportTemplate
        {
            get => _templateManager.ReportTemplate;
            set => _templateManager.ReportTemplate = value;
        }

        public Helpop(Main game) : base(game)
        {
        }

        /// <inheritdoc />
        public override void Initialize()
        {
            _templateManager = new TemplateManager(this);

            ReloadConfig();

            Initialize_Credits();
            Initialize_Redis();

            Events.OnIssue += HandleOnIssue;
            OnIssue += HandleOnIssue_OnMainThread;

            Commands.ChatCommands.Add(new Command(
                Permissions.Config_Reload,
                Command_ReloadConfig,
                "helpopplugin:reload", "hopp:reload", "helpopreload"
            ));
            Commands.ChatCommands.Add(new Command(
                Permissions.Issues_Raise,
                Command_RaiseIssue,
                "raiseissue", "issue", "helpop", "report", "sendhelp"
            ));
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Dispose_Redis();

                Events.OnIssue -= HandleOnIssue;
            }
            base.Dispose(disposing);
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

        private void Command_ReloadConfig(CommandArgs args)
        {
            TShock.Log.Info($"{args.Player.Name} initiated HelpopPlugin config reload.");

            ReloadConfig();
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

        private void ReloadConfig()
        {
            _configManager.Reload();
            _templateManager.Reload();

            if (_tshockReloadHooked)
            {
                if (!PluginSettings.UseTShockReload)
                {
                    GeneralHooks.ReloadEvent -= OnTShockReload;
                    _tshockReloadHooked = false;
                }
            }
            else
            {
                if (PluginSettings.UseTShockReload)
                {
                    GeneralHooks.ReloadEvent += OnTShockReload;
                    _tshockReloadHooked = true;
                }
            }
        }

        private void OnTShockReload(ReloadEventArgs args)
        {
            ReloadConfig();
        }
    }
}
