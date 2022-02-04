using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpopPlugin.Configuration
{
    /// <summary>
    /// Contains paths used for configuration.
    /// </summary>
    public static class Paths
    {
        /// <summary>
        /// Gets the path to the folder used by plugins to store their configuration.
        /// </summary>
        public static readonly string ConfigPath = Path.GetFullPath("config");

        /// <summary>
        /// Gets the path to the folder used by this plugin to store its configuration.
        /// </summary>
        public static readonly string SavePath = Path.Combine(ConfigPath, "HelpopPlugin");

        /// <summary>
        /// Gets the path to the file used to store general plugin configuration.
        /// </summary>
        public static readonly string PluginConfigPath = Path.Combine(SavePath, "config.json");
    }
}
