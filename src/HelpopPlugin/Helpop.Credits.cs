using System.Diagnostics;
using TerrariaApi.Server;
using TShockAPI;

namespace HelpopPlugin
{
    public partial class Helpop
    {
        public static readonly string Credits = @"HelpopPlugin uses Open Source components. You can find the source code of their open source projects along with license information below. We acknowledge and are grateful to these developers for their contributions to open source.

StackExchange.Redis https://stackexchange.github.io/StackExchange.Redis/
Copyright (c) 2014 Stack Exchange
MIT License https://raw.githubusercontent.com/StackExchange/StackExchange.Redis/main/LICENSE

LrndefLib https://github.com/Arthri/LrndefLib
Copyright 2021 Arthri
MIT-0 License https://raw.githubusercontent.com/Arthri/LrndefLib/master/LICENSE";

        private void Initialize_Credits()
        {
            if (PluginSettings.ShowCredits)
            {
                ServerApi.LogWriter.PluginWriteLine(this, Credits, TraceLevel.Info);
            }

            AddCommand(Command_Credits, "helpopplugin:credits", "helpop:credits");
        }

        private void Command_Credits(CommandArgs args)
        {
            args.Player.SendInfoMessage(Credits);
        }
    }
}
