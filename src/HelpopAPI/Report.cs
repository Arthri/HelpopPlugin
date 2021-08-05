namespace HelpopAPI
{
    /// <summary>
    /// Represents a report
    /// </summary>
    public class Report
    {
        /// <summary>
        /// Represents the report
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Represents the user that made this report
        /// </summary>
        public ReportUser User { get; }

        public Report(
            string message,
            ReportUser user)
        {
            Message = message;
            User = user;
        }
    }
}
