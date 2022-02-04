using HelpopAPI.Core;
using HelpopPlugin.Configuration;
using System;
using System.Reflection;
using Terraria;
using TerrariaApi.Server;

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

        public Helpop(Main game) : base(game)
        {
        }

        /// <inheritdoc />
        public override void Initialize()
        {
            _configManager.Load();

            Initialize_Credits();
            Initialize_Redis();

            Events.OnIssue += HandleOnIssue;
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
    }
}
