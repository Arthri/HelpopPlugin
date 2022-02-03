namespace HelpopAPI.Core
{
    /// <summary>
    /// Represents a call for help.
    /// </summary>
    public class Issue
    {
        /// <summary>
        /// Represents the issue's content.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Represents the user that issued the message.
        /// </summary>
        public IssueUser Issuer { get; }

        /// <summary>
        /// Initializes a new issue with the specified message and user.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="issuer">The issuer.</param>
        public Issue(
            string message,
            IssueUser issuer)
        {
            Message = message;
            Issuer = issuer;
        }
    }
}
