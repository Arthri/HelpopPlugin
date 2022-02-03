namespace HelpopAPI
{
    public class OnIssueEventArgs : HandledEventArgs
    {
        /// <summary>
        /// Gets the issue issued from the event.
        /// </summary>
        public Issue IssuedIssue { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="OnIssueEventArgs"/>.
        /// </summary>
        /// <param name="handled"><inheritdoc /></param>
        /// <param name="issuedIssue">The issue.</param>
        public OnIssueEventArgs(
            bool handled,
            Issue issuedIssue
        ) : base(
            handled
        )
        {
            IssuedIssue = issuedIssue;
        }

        /// <inheritdoc cref="OnIssueEventArgs(bool, Issue)"/>
        public OnIssueEventArgs(
            Issue issuedIssue
        ) : this(
            false,
            issuedIssue
        )
        {
        }
    }
}
