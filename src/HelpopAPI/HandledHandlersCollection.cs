using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace HelpopAPI
{
    /// <summary>
    /// Represents an event handler that can be skipped depending on the value of <see cref="HandledEventArgs.Handled"/>.
    /// </summary>
    /// <param name="eventArgs">The event args.</param>
    public delegate void HandledHandler<T>(T eventArgs)
        where T : HandledEventArgs;

    /// <summary>
    /// Represents a collection of <see cref="HandledHandler{T}"/> handlers.
    /// </summary>
    public class HandledHandlersCollection<T> : IList<HandledHandler<T>>
        where T : HandledEventArgs
    {
        private record HandlerEntry
        {
            /// <summary>
            /// Gets the handler.
            /// </summary>
            public HandledHandler<T> Handler { get; }

            /// <summary>
            /// Gets a value indicating if this handler should always run, regardless of <see cref="HandledEventArgs.Handled"/>.
            /// </summary>
            public bool AlwaysRun { get; }

            /// <summary>
            /// Initializes a new entry.
            /// </summary>
            /// <param name="handler">The handler.</param>
            /// <param name="alwaysRun">A value indicating if this handler should always run.</param>
            public HandlerEntry(
                HandledHandler<T> handler,
                bool alwaysRun
            )
            {
                Handler = handler;
                AlwaysRun = alwaysRun;
            }
        }

        private readonly List<HandlerEntry> _handlers;

        #region List Properties

        /// <inheritdoc />
        public HandledHandler<T> this[int index]
        {
            get => _handlers[index].Handler;
        }

        HandledHandler<T> IList<HandledHandler<T>>.this[int index]
        {
            get => _handlers[index].Handler;

            set => _handlers[index] = new HandlerEntry(value, false);
        }

        /// <inheritdoc />
        public int Count => _handlers.Count;

        /// <inheritdoc />
        public bool IsReadOnly => false;

        #endregion

        #region List Methods

        /// <inheritdoc />
        public void Add(HandledHandler<T> item)
        {
            Add(item, false);
        }

        /// <summary>
        /// Adds a handler to the collection along with the specified properties.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="alwaysRun"></param>
        public void Add(HandledHandler<T> item, bool alwaysRun)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            _handlers.Add(new HandlerEntry(item, alwaysRun));
        }

        /// <inheritdoc />
        public void Insert(int index, HandledHandler<T> item)
        {
            Insert(index, item, false);
        }

        /// <summary>
        /// Inserts a handler to the collection at the specified index along with the specified properties.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        /// <param name="alwaysRun"></param>
        public void Insert(int index, HandledHandler<T> item, bool alwaysRun)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            _handlers.Insert(index, new HandlerEntry(item, alwaysRun));
        }

        /// <inheritdoc />
        public bool Contains(HandledHandler<T> item)
        {
            if (item is null)
            {
                return false;
            }

            var comparer = EqualityComparer<HandledHandler<T>>.Default;
            for (int i = 0; i < _handlers.Count; i++)
            {
                var entry = _handlers[i];
                if (comparer.Equals(entry.Handler, item))
                {
                    return true;
                }
            }

            return false;
        }

        /// <inheritdoc />
        public int IndexOf(HandledHandler<T> item)
        {
            if (item is null)
            {
                return -1;
            }

            var comparer = EqualityComparer<HandledHandler<T>>.Default;
            for (int i = 0; i < _handlers.Count; i++)
            {
                var entry = _handlers[i];
                if (comparer.Equals(entry.Handler, item))
                {
                    return i;
                }
            }

            return -1;
        }

        /// <inheritdoc />
        public void CopyTo(HandledHandler<T>[] array, int arrayIndex)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc />
        public bool Remove(HandledHandler<T> item)
        {
            var index = IndexOf(item);
            if (index >= 0)
            {
                RemoveAt(index);
                return true;
            }

            return false;
        }

        /// <inheritdoc />
        public void RemoveAt(int index)
        {
            _handlers.RemoveAt(index);
        }

        /// <inheritdoc />
        public void Clear()
        {
            _handlers.Clear();
        }

        /// <inheritdoc />
        public IEnumerator<HandledHandler<T>> GetEnumerator()
        {
            for (int i = 0; i < _handlers.Count; i++)
            {
                yield return _handlers[i].Handler;
            }
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        public void Invoke(T args)
        {
            int i = 0;
            while (i < _handlers.Count && !args.Handled)
            {
                var entry = _handlers[i];

                entry.Handler(args);

                i++;
            }
            while (i < _handlers.Count)
            {
                var entry = _handlers[i];

                if (entry.AlwaysRun)
                {
                    entry.Handler(args);
                }

                i++;
            }
        }
    }
}
