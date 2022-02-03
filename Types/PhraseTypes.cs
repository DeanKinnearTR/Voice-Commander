using System;

namespace VoiceCommander.Types
{
    [Flags]
    public enum PhraseTypes
    {
        None = 0,
        Stop = 1,
        Start = 2,
        Repository = 4,
        ExitCommander = 8,
        All = Stop | Start | Repository | ExitCommander,
    }
}
