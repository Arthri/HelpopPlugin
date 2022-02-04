using LrndefLib;

namespace HelpopPlugin.Configuration
{
    /// <summary>
    /// Represents general plugin configuration options.
    /// </summary>
    public class PluginSettings
    {
        public static readonly SimpleVersion CurrentVersion = new SimpleVersion(0, 0, 1, 0);

        /// <summary>
        /// Gets or sets whether credits(to dependencies) should be shown on plugin initialization.
        /// </summary>
        public bool ShowCredits { get; set; } = true;
    }
}
