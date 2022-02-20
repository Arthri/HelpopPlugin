using LrndefLib;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace HelpopPlugin.Configuration
{
    /// <summary>
    /// Represents general plugin configuration options.
    /// </summary>
    public class PluginSettings
    {
        /// <summary>
        /// Represents the current version of the settings format.
        /// </summary>
        public static readonly SimpleVersion CurrentVersion = new(0, 0, 1, 0);

        /// <summary>
        /// Gets or sets whether credits(to dependencies) should be shown on plugin initialization.
        /// </summary>
        [JsonProperty("showCredits")]
        public bool ShowCredits { get; set; } = true;

        /// <summary>
        /// Gets or sets the current server's name when sending reports.
        ///
        /// For example, with the value "<c>Server1</c>", the report message can use it like so: <c>[Report] from Server1: ...</c>
        /// </summary>
        [JsonProperty("reportOrigin")]
        public string ReportOrigin { get; set; }

        /// <summary>
        /// Gets or sets the color of the report message.
        /// </summary>
        [JsonIgnore]
        public Color ReportMessageColor { get; set; } = new Color(0xDB, 0xA2, 0x29);

        /// <summary>
        /// Gets or sets the color of the report message.
        /// </summary>
        /// <remarks>The value should be in RGBA decimal/integer format.</remarks>
        [JsonProperty("reportMessageColor")]
        public uint ReportMessageColorPacked
        {
            get
            {
                var packedValue = ReportMessageColor.PackedValue;
                return (packedValue >> 24)
                      | ((packedValue & 0x00FF) >> 8)
                      | ((packedValue & 0x0000FF) << 8)
                      | ((packedValue & 0x000000FF) << 24);
            }

            set => ReportMessageColor = new Color(
                    (value >> 24)
                  | ((value & 0x00FF) >> 8)
                  | ((value & 0x0000FF) << 8)
                  | ((value & 0x000000FF) << 24)
            );
        }

        /// <summary>
        /// Gets or sets whether the plugin should react to TShock's config reload function.
        /// </summary>
        [JsonProperty("useTShockReload")]
        public bool UseTShockReload { get; set; } = true;
    }
}
