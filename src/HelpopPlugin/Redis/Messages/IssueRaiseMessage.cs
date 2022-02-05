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
        public Issue IssuedIssue { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="IssueRaiseMessage"/>.
        /// </summary>
        /// <param name="source"><inheritdoc /></param>
        /// <param name="issue">The issued issue.</param>
        public IssueRaiseMessage(string source, Issue issue) : base(source)
        {
            IssuedIssue = issue;
        }
    }
}
