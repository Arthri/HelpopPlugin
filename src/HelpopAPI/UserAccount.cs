namespace HelpopAPI
{
    /// <summary>
    /// Represents the account of a <see cref="IssueUser"/>.
    /// </summary>
    public class UserAccount
    {
        /// <summary>
        /// Represents the account's ID.
        /// </summary>
        public int ID { get; }

        /// <summary>
        /// Represents the account's name.
        /// </summary>
        public string Name { get; }

        public UserAccount(
            int id,
            string name)
        {
            ID = id;
            Name = name;
        }
    }
}
