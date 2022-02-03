using System.Collections.Generic;

namespace VoiceCommander.Types
{
    public class CommandItem
    {
        public CommandItem(CommandTypes commandType, List<string> actions, string text )
        {
            CommandType = commandType;
            Actions = actions;
            Text = text;
        }

        public CommandTypes CommandType { get; set; }
        public List<string> Actions { get; set; }
        public string Text { get; set; }
    }
}
