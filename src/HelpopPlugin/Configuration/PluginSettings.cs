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
        public static readonly SimpleVersion CurrentVersion = new SimpleVersion(0, 0, 1, 0);

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
            get => (ReportMessageColor.PackedValue & 0x00FF00FF)
                | ((ReportMessageColor.packedValue & 0x0000FF) << 16)
                | ((ReportMessageColor.packedValue & 0xFF) >> 16);

            set => ReportMessageColor = new Color(
                   (value & 0x00FF00FF)
                | ((value & 0xFF) >> 16)
                | ((value & 0x0000FF) << 16)
            );
        }
    }
}
