using System.Collections.Generic;

namespace VoiceCommander.Types
{
    public class CommandItem
    {
        public CommandItem(string text, CommandTypes commandType, List<string> actions)
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
