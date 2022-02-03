namespace HelpopAPI
{
    /// <summary>
    /// Represents a user.
    /// </summary>
    public class IssueUser
    {
        /// <summary>
        /// Represents the user's display name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Represents the user's IP.
        /// </summary>
        public string IP { get; }

        /// <summary>
        /// Represents the user's UUID.
        /// </summary>
        public string UUID { get; }

        /// <summary>
        /// Represents the user's server account.
        /// </summary>
        public UserAccount Account { get; }

        /// <summary>
        /// Initializes a new instance of a user.
        /// </summary>
        /// <param name="name">The user's displayname.</param>
        /// <param name="ip">The user's IP.</param>
        /// <param name="uuid">The user's UUID.</param>
        /// <param name="account">The user's account.</param>
        public IssueUser(
            string name,
            string ip,
            string uuid,
            UserAccount account)
        {
            Name = name;
            IP = ip;
            UUID = uuid;
            Account = account;
        }
    }
}
