using LrndefLib;
using Newtonsoft.Json;

namespace HelpopPlugin.Configuration
{
    /// <summary>
    /// Represents Redis connection settings.
    /// </summary>
    public class RedisSettings
    {
        /// <summary>
        /// Represents times for connecting to Redis.
        /// </summary>
        public enum ConnectionTimeEnum
        {
            /// <summary>
            /// Connects to Redis during plugin initialization(before world selection).
            /// </summary>
            Initialization,

            /// <summary>
            /// Connects to Redis when the server started.
            /// </summary>
            ServerStarted
        }

        /// <summary>
        /// Represents the current version of the settings format.
        /// </summary>
        public static readonly SimpleVersion CurrentVersion = new SimpleVersion(0, 0, 1, 0);

        /// <summary>
        /// Gets or sets the Redis config string. More information at https://stackexchange.github.io/StackExchange.Redis/Configuration
        /// </summary>
        [JsonProperty("configString")]
        public string ConfigString { get; set; } = "localhost";

        /// <summary>
        /// Gets or sets the connection time.
        /// </summary>
        [JsonProperty("connectionTime")]
        public ConnectionTimeEnum ConnectionTime { get; set; } = ConnectionTimeEnum.Initialization;
    }
}
