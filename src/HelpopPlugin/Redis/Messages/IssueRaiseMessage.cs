using Newtonsoft.Json;

namespace HelpopPlugin.Redis.Messages
{
    /// <summary>
    /// Represents an issue raise message.
    /// </summary>
    public class IssueRaiseMessage : Message
    {
        /// <summary>
        /// Represents the raised issue.
        /// </summary>
        public RedisIssue IssuedIssue { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="IssueRaiseMessage"/>.
        /// </summary>
        /// <param name="source"><inheritdoc /></param>
        /// <param name="issuedIssue">The issued issue.</param>
        [JsonConstructor]
        public IssueRaiseMessage(
            string source,
            RedisIssue issuedIssue
        ) : base(
            source
        )
        {
            IssuedIssue = issuedIssue;
        }
    }
}
