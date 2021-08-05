namespace HelpopAPI
{
    /// <summary>
    /// Represents a user that made a report
    /// </summary>
    public class ReportUser
    {
        /// <summary>
        /// Represents the user's character name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Represents the user's IP
        /// </summary>
        public string IP { get; }

        /// <summary>
        /// Represents the user's UUID
        /// </summary>
        public string UUID { get; }

        /// <summary>
        /// Represents the user's server account
        /// </summary>
        public ReportAccount UserAccount { get; }

        public ReportUser(
            string name,
            string ip,
            string uuid,
            ReportAccount userAccount)
        {
            Name = name;
            IP = ip;
            UUID = uuid;
            UserAccount = userAccount;
        }
    }
}