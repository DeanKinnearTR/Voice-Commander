using System;
using System.Collections.Generic;
using System.Speech.Recognition;

namespace VoiceCommander
{
    public class Recognition : IDisposable
    {
        public event Action<string> Recognized;
        private readonly SpeechRecognitionEngine _engine;
        private readonly GrammarBuilder _builder;
        private Grammar _grammar;

        public Recognition()
        {
            _engine = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));
            _builder = new GrammarBuilder();

            _engine.SpeechRecognized += recognizer_SpeechRecognized;
            _engine.SetInputToDefaultAudioDevice();           
        }

        public void AddPhrases(List<string> items)
        {            
            var choices = new Choices(items.ToArray());
            _builder.Append(choices);
            _grammar = new Grammar(_builder);
            _engine.LoadGrammar(_grammar);
        }

        public void StartRecognition()
        {
            _engine.RecognizeAsync(RecognizeMode.Multiple);
        }

        public void StopRecognition()
        {
            _engine.RecognizeAsyncStop();
        }

        private void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Recognized?.Invoke(e.Result.Text);
        }

        public void Dispose()
        {
            _engine?.Dispose();
        }
    }
}
