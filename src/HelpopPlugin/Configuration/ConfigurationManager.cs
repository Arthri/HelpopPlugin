using LrndefLib;
using Newtonsoft.Json;
using System.IO;

namespace HelpopPlugin.Configuration
{
    public class ConfigurationManager
    {
        /// <summary>
        /// Gets a value indicating whether or not the configs has loaded.
        /// </summary>
        public bool Loaded { get; private set; }

        /// <summary>
        /// Gets an object representing general plugin configuration.
        /// </summary>
        public VersionedConfigFile<PluginSettings> PluginConfigFile { get; private set; }

        /// <summary>
        /// Gets an object representing Redis configuration.
        /// </summary>
        public VersionedConfigFile<RedisSettings> RedisConfigFile { get; private set; }

        /// <summary>
        /// Initializes a new instance of <see cref="ConfigurationManager"/>.
        /// </summary>
        internal ConfigurationManager()
        {
        }

        /// <summary>
        /// Loads the configs.
        /// </summary>
        public void Load()
        {
            if (Loaded)
            {
                return;
            }

            PluginConfigFile = new VersionedConfigFile<PluginSettings>(
                PluginSettings.CurrentVersion
            );
            RedisConfigFile = new VersionedConfigFile<RedisSettings>(
                RedisSettings.CurrentVersion
            );

            Reload();

            Loaded = true;
        }

        /// <summary>
        /// Reloads the configs.
        /// </summary>
        public void Reload()
        {
            if (!Directory.Exists(Paths.SavePath))
            {
                Directory.CreateDirectory(Paths.SavePath);
            }

            ReadOrCreateConfig(Paths.PluginConfigPath, PluginConfigFile);
            ReadOrCreateConfig(Paths.RedisConfigPath, RedisConfigFile);
        }

        private void ReadOrCreateConfig<T>(string path, VersionedConfigFile<T> config)
            where T : new()
        {
            if (!File.Exists(path))
            {
                config.Metadata = new SettingsMetadata(
                    SettingsMetadata.CurrentMetadataVersion,
                    config.CurrentVersion);
                config.Settings = new T();
                using (var fs = new FileStream(
                    path,
                    FileMode.CreateNew,
                    FileAccess.Write,
                    FileShare.None))
                {
                    config.Write(fs, TransformWriter);
                }
            }
            else
            {
                bool incompleteSettings;
                using (var fs = new FileStream(
                    path,
                    FileMode.Open,
                    FileAccess.Read,
                    FileShare.Read))
                {
                    config.Read(fs, out incompleteSettings);
                }

                if (incompleteSettings)
                {
                    using (var wfs = new FileStream(
                        path,
                        FileMode.Create,
                        FileAccess.Write,
                        FileShare.None))
                    {
                        config.Write(wfs, TransformWriter);
                    }
                }
            }
        }

        private void TransformWriter(JsonTextWriter writer)
        {
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 4;
        }
    }
}
