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

        private readonly Recognition _engine;
        private const string ExitCommander = "Exit application";

        public Controller()
        {
            _engine = new Recognition();
            _engine.Recognized += Engine_Recognized;

            var phrases = new List<string>
            {
                ExitCommander
            };

            var read = Repository.Read();
            if (read != null)
            {
                phrases.AddRange(read.Select(item => item.Text).ToList());
            }

            _engine.AddPhrases(phrases);
        }

        private bool _listening;

        public bool Listening
        {
            get => _listening;
            set
            {
                if (_listening == value) return;
                _listening = value;
                if (value)
                {
                    _engine.StartRecognition();
                }
                else
                {
                    _engine.StopRecognition();
                }
            }
        }

        private void Engine_Recognized(string text)
        {
            try
            {
                if (text.Equals(ExitCommander, StringComparison.OrdinalIgnoreCase))
                {
                    StateChange?.Invoke(ControllerStates.ShutDown);
                    return;
                }
                var items = Repository.Read();
                var item = items.FirstOrDefault(q => q.Text.Equals(text, StringComparison.OrdinalIgnoreCase));
                if (item == null)
                {
                    StateChange?.Invoke(ControllerStates.Error);
                    return;
                }

                StateChange?.Invoke(ControllerStates.ActionStart);

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

                StateChange?.Invoke(ControllerStates.ActionComplete);
            }
            catch
            {
                StateChange?.Invoke(ControllerStates.Error);
            }
        }

        public void Dispose()
        {
            _engine?.Dispose();
        }
    }
}
