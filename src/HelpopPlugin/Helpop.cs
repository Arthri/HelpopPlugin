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

        public PluginSettings PluginSettings
        {
            get => _configManager.PluginConfigFile.Settings;
            set => _configManager.PluginConfigFile.Settings = value;
        }

        public Helpop(Main game) : base(game)
        {
        }

        /// <inheritdoc />
        public override void Initialize()
        {
            _configManager.Load();

            Initialize_Credits();
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }
    }
}
