using System;
using System.Collections.Generic;
using System.Speech.Recognition;

namespace VoiceCommander
{
    public class Recognition : IDisposable
    {
        public event Action<string> Recognized;
        private readonly SpeechRecognitionEngine _engine;

        public Recognition(List<string> phrases)
        {
            _engine = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));
            var choices = new Choices(phrases.ToArray());
            var builder = new GrammarBuilder();
            builder.Append(choices);
            var grammar = new Grammar(builder);
            _engine.LoadGrammar(grammar);
            _engine.SpeechRecognized += Recognizer_SpeechRecognized;
            _engine.SetInputToDefaultAudioDevice();           
        }

        public void StartRecognition()
        {
            _engine.RecognizeAsync(RecognizeMode.Multiple);
        }

        public void StopRecognition()
        {
            _engine.RecognizeAsyncStop();
        }

        private void Recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Recognized?.Invoke(e.Result.Text);
        }

        public void Dispose()
        {
            _engine?.Dispose();
        }
    }
}
