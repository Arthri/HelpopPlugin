using System.Collections;
using System.Collections.Generic;
using TShockAPI;

namespace HelpopPlugin
{
    public class CommandList : IList<Command>
    {
        private readonly List<Command> _commands;

        /// <inheritdoc />
        public CommandList()
        {
            _commands = new();
        }

        ~CommandList()
        {
            for (int i = 0; i < _commands.Count; i++)
            {
                var command = _commands[i];
                Commands.ChatCommands.Remove(command);
            }
        }

        /// <inheritdoc />
        public Command this[int index]
        {
            get => _commands[index];

            set
            {
                Commands.ChatCommands.Add(value);
                Commands.ChatCommands.Remove(_commands[index]);
                _commands[index] = value;
            }
        }

        /// <inheritdoc />
        public int Count => _commands.Count;

        /// <inheritdoc />
        public bool IsReadOnly => false;

        /// <inheritdoc />
        public void Add(Command item)
        {
            _commands.Add(item);
            Commands.ChatCommands.Add(item);
        }

        /// <inheritdoc />
        public void Insert(int index, Command item)
        {
            Commands.ChatCommands.Add(item);
            _commands.Insert(index, item);
        }

        /// <inheritdoc />
        public bool Contains(Command item)
        {
            return _commands.Contains(item);
        }

        /// <inheritdoc />
        public int IndexOf(Command item)
        {
            return _commands.IndexOf(item);
        }

        /// <inheritdoc />
        public void CopyTo(Command[] array, int arrayIndex)
        {
            _commands.CopyTo(array, arrayIndex);
        }

        /// <inheritdoc />
        public bool Remove(Command item)
        {
            var removed = _commands.Remove(item);
            if (removed)
            {
                Commands.ChatCommands.Remove(item);
            }

            return removed;
        }

        /// <inheritdoc />
        public void RemoveAt(int index)
        {
            Commands.ChatCommands.Remove(_commands[index]);
            _commands.RemoveAt(index);
        }

        /// <inheritdoc />
        public void Clear()
        {
            for (int i = 0; i < _commands.Count; i++)
            {
                var command = _commands[i];
                Commands.ChatCommands.Remove(command);
            }
            _commands.Clear();
        }

        /// <inheritdoc />
        public IEnumerator<Command> GetEnumerator()
        {
            return _commands.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _commands.GetEnumerator();
        }
    }
}
