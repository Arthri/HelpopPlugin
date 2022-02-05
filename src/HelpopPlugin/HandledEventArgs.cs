using System;

namespace HelpopPlugin
{
    /// <summary>
    /// Represents event data with an additional <see cref="Handled"/> field to stop further handlers.
    /// </summary>
    public class HandledEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets a value indicating if proceeding handlers should not be invoked.
        /// </summary>
        public bool Handled { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="HandledEventArgs"/>.
        /// </summary>
        /// <param name="handled">A value indicating if proceeding handlers should not be invoked.</param>
        public HandledEventArgs(bool handled)
        {
            Handled = handled;
        }
    }
}
