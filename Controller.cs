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
        private const string StartListening = "Chaos start listening";
        private const string StopListening = "Chaos stop listening";
        private const string ExitCommander = "Chaos exit application";

        public Controller()
        {
            InitializeEngine(PhraseTypes.All);
        }

        private void InitializeEngine(PhraseTypes phraseType)
        {
            _engine?.Dispose();

            _engine = new Recognition();
            _engine.Recognized += Engine_Recognized;

            var phrases = new List<string>
            {
                StartListening
            };

            if (phraseType == PhraseTypes.All)
            {
                phrases.Add(StopListening);
                phrases.Add(ExitCommander);
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
                if (text.Equals(StopListening, StringComparison.OrdinalIgnoreCase))
                {
                    if (_engineState == ControllerStates.NotListening) return;
                    StateChange?.Invoke(_engineState = ControllerStates.NotListening);
                    InitializeEngine(PhraseTypes.StartOnly);
                    return;
                }

                if (text.Equals(StartListening, StringComparison.OrdinalIgnoreCase))
                {
                    if (_engineState == ControllerStates.Listening) return;
                    StateChange?.Invoke(_engineState = ControllerStates.Listening);
                    InitializeEngine(PhraseTypes.All);
                    return;
                }

                if (text.Equals(ExitCommander, StringComparison.OrdinalIgnoreCase))
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

                StateChange?.Invoke(_engineState = ControllerStates.Action);
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

                StateChange?.Invoke(_engineState = ControllerStates.Listening);
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
