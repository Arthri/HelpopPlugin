using HelpopPlugin.Configuration;
using Scriban;
using TShockAPI;
using TShockAPI.Hooks;

namespace HelpopPlugin
{
    public partial class Helpop
    {
        private bool _tshockReloadHooked = false;

        private ConfigurationManager _configManager;

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

        private TemplateManager _templateManager;

        public Template ReportTemplate
        {
            get => _templateManager.ReportTemplate;
            set => _templateManager.ReportTemplate = value;
        }

        private void Initialize_Config()
        {
            _configManager = new ConfigurationManager();
            _templateManager = new TemplateManager(this);

            ReloadConfig();

            Commands.ChatCommands.Add(new Command(
                Permissions.Config_Reload,
                Command_ReloadConfig,
                "helpopplugin:reload", "hopp:reload", "helpopreload"
            ));
        }

        private void Command_ReloadConfig(CommandArgs args)
        {
            TShock.Log.Info($"{args.Player.Name} initiated HelpopPlugin config reload.");

            ReloadConfig();
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
