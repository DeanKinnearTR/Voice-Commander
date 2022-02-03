using Newtonsoft.Json;
using System.Collections.Generic;
using VoiceCommander.Types;

namespace VoiceCommander
{
    public class Repository
    {
        public static List<CommandItem> Read()
        {
            var settings = Resources.Settings.Default.UserSettings;
            return string.IsNullOrEmpty(settings) ? null : JsonConvert.DeserializeObject<List<CommandItem>>(settings);
        }

        public static void Create(List<CommandItem> items)
        {
            Resources.Settings.Default.UserSettings = JsonConvert.SerializeObject(items);
            Resources.Settings.Default.Save();
        }
    }
}
