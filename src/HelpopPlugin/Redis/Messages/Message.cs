namespace HelpopPlugin
{
    /// <summary>
    /// Represents a message sent to or received from Redis.
    /// </summary>
    public abstract class Message
    {
        /// <summary>
        /// The source of the message. Used for hosts ignoring messages they sent.
        /// </summary>
        public string Source { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="Message"/>.
        /// </summary>
        /// <param name="source">The message's source.</param>
        public Message(string source)
        {
            Source = source;
        }
    }
}
