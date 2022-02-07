using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using VoiceCommander.Types;

namespace VoiceCommander
{
    public class Controller : IDisposable
    {
        public event Action<ControllerStates> StateChange;

        private Recognition _engine;
        private ControllerStates _engineState;

        public Controller()
        {
            InitializeEngine(PhraseTypes.All);
        }

        private void InitializeEngine(PhraseTypes phraseType)
        {
            _engine?.Dispose();

            _engine = new Recognition();
            _engine.Recognized += Engine_Recognized;

            var phrases = new List<string>();
            if (phraseType.HasFlag(PhraseTypes.Stop))
            {
                phrases.Add("Stop Listening");
            }

            if (phraseType.HasFlag(PhraseTypes.Start))
            {
                phrases.Add("Start Listening");
            }

            if (phraseType.HasFlag(PhraseTypes.ExitCommander))
            {
                phrases.Add("Exit Commander");
            }

            if (phraseType.HasFlag(PhraseTypes.Repository))
            {
                var read = Repository.Read();
                if (read != null)
                {
                    phrases.AddRange(read.Select(item => item.Text));
                }
            }

            _engine.AddPhrases(phrases);
            _engine.StartRecognition();
        }

        private void Engine_Recognized(string text)
        {
            try
            {
                if (text.Equals("Stop Listening", StringComparison.OrdinalIgnoreCase))
                {
                    StateChange?.Invoke(_engineState = ControllerStates.NotListening);
                    InitializeEngine(PhraseTypes.Start);
                    return;
                }

                if (text.Equals("Start Listening", StringComparison.OrdinalIgnoreCase))
                {
                    StateChange?.Invoke(_engineState = ControllerStates.Listening);
                    InitializeEngine(PhraseTypes.All);
                    return;
                }

                if (text.Equals("Exit Commander", StringComparison.OrdinalIgnoreCase))
                {
                    StateChange?.Invoke(_engineState = ControllerStates.ShutDown);
                    return;
                }

                if (_engineState == ControllerStates.NotListening) return;

                var items = Repository.Read();
                var item = items.FirstOrDefault(q => q.Text.Equals(text, StringComparison.OrdinalIgnoreCase));
                if (item == null)
                {
                    StateChange?.Invoke(_engineState = ControllerStates.Error);
                    return;
                }

                StateChange?.Invoke(ControllerStates.Action);
                switch (item.CommandType)
                {
                    case CommandTypes.Launch:
                        {
                            foreach (var action in item.Actions)
                            {
                                System.Diagnostics.Process.Start(action);
                            }

                            break;
                        }
                    case CommandTypes.SendKeys:
                        {
                            foreach (var action in item.Actions)
                            {
                                SendKeys.Send(action);
                            }

                            break;
                        }
                }

                StateChange?.Invoke(ControllerStates.Listening);
            }
            catch
            {
                StateChange?.Invoke(_engineState = ControllerStates.Error);
            }
        }

        public void Dispose()
        {
            _engine?.Dispose();
        }
    }
}
