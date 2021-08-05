namespace HelpopAPI
{
    /// <summary>
    /// Represents the account of a <see cref="ReportUser"/>
    /// </summary>
    public class ReportAccount
    {
        /// <summary>
        /// Represents the account's ID
        /// </summary>
        public int ID { get; }

        /// <summary>
        /// Represents the account's name
        /// </summary>
        public string Name { get; }

        public ReportAccount(
            int id,
            string name)
        {
            ID = id;
            Name = name;
        }
    }
}