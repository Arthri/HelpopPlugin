namespace HelpopPlugin
{
    public static class Events
    {
        private static readonly HandledHandlersCollection<OnIssueEventArgs> _onIssueHandlers = new();

        /// <summary>
        /// Occurs when an issue is issued.
        /// </summary>
        public static event HandledHandler<OnIssueEventArgs> OnIssue
        {
            add
            {
                _onIssueHandlers.Add(value);
            }

            remove
            {
                _onIssueHandlers.Remove(value);
            }
        }

        public static void InvokeOnIssue(Issue issue)
        {
            var eventArgs = new OnIssueEventArgs(issue);
            _onIssueHandlers.Invoke(eventArgs);
        }
    }
}
