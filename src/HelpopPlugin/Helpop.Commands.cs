using System.Collections.Generic;
using TShockAPI;

namespace HelpopPlugin
{
    public partial class Helpop
    {
        private CommandList _commands;

        private void Initialize_CommandList()
        {
            _commands = new CommandList();
        }

        private void Dispose_CommandList()
        {
            _commands = null;
        }

        private Command AddCommand(List<string> permissions, CommandDelegate cmd, params string[] names)
        {
            var command = new Command(permissions, cmd, names);
            _commands.Add(command);
            return command;
        }

        private Command AddCommand(string permission, CommandDelegate cmd, params string[] names)
        {
            var command = new Command(permission, cmd, names);
            _commands.Add(command);
            return command;
        }

        private Command AddCommand(CommandDelegate cmd, params string[] names)
        {
            var command = new Command(cmd, names);
            _commands.Add(command);
            return command;
        }

        private bool RemoveCommand(Command command)
        {
            return _commands.Remove(command);
        }
    }
}
