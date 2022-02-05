using Newtonsoft.Json;

namespace HelpopPlugin.Redis
{
    /// <summary>
    /// Represents an issue that originated from Redis.
    /// </summary>
    public record RedisIssue : Issue
    {
        /// <summary>
        /// Gets the issue's origin server's identifier.
        /// </summary>
        public string OriginServerID { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="RedisIssue"/>.
        /// </summary>
        /// <param name="message"><inheritdoc /></param>
        /// <param name="issuer"><inheritdoc /></param>
        /// <param name="originServerID">The origin server's ID.</param>
        [JsonConstructor]
        public RedisIssue(
            string message,
            IssueUser issuer,
            string originServerID)
        : base(
            message,
            issuer)
        {
            OriginServerID = originServerID;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="RedisIssue"/>
        /// </summary>
        /// <param name="original"><inheritdoc /></param>
        /// <param name="originServerID">The origin server's ID.</param>
        public RedisIssue(Issue original, string originServerID) : base(original)
        {
            OriginServerID = originServerID;
        }
    }
}
