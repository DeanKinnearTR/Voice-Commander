using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System;
using VoiceCommander.Resources.Icons;
using VoiceCommander.Resources.Wav;
using VoiceCommander.Types;

namespace VoiceCommander
{
    public partial class MainForm : Form
    {
        private Recognition _engine;
        private EngineStates _engineState = EngineStates.Idle;

        public MainForm()
        {
            InitializeComponent();
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
            _engineState = EngineStates.Listening;
        }

        private void Engine_Recognized(string text)
        {
            try
            {
                if (text.Equals("Stop Listening", StringComparison.OrdinalIgnoreCase))
                {
                    _engineState = EngineStates.NotListening;
                    NotifyIcon.Icon = Icons.GetIcon("NotListening.ico");
                    Audio.PlaySound("communications_end_transmission.wav");
                    InitializeEngine(PhraseTypes.Start);
                    return;
                }
                if (text.Equals("Start Listening", StringComparison.OrdinalIgnoreCase))
                {
                    _engineState = EngineStates.Listening;
                    NotifyIcon.Icon = Icons.GetIcon("DefaultIcon.ico");
                    Audio.PlaySound("communications_start_transmission.wav");
                    InitializeEngine(PhraseTypes.All);
                    return;
                }
                if (text.Equals("Exit Commander", StringComparison.OrdinalIgnoreCase))
                {
                    ExitCommander();
                    return;
                }

                if (_engineState == EngineStates.NotListening) return;

                var items = Repository.Read();
                var item = items.FirstOrDefault(q => q.Text.Equals(text, StringComparison.OrdinalIgnoreCase));
                if (item == null)
                {
                    SetErrorState();
                    return;
                }

                NotifyIcon.Icon = Icons.GetIcon("BusyIcon.ico");
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

                Audio.PlaySound("computer_work_beep.wav");
                NotifyIcon.Icon = Icons.GetIcon("DefaultIcon.ico");
            }
            catch
            {
                SetErrorState();
            }
        }

        private void SetErrorState()
        {
            _engineState = EngineStates.Error;
            NotifyIcon.Icon = Icons.GetIcon("ErrorIcon.ico");
            Audio.PlaySound("computer_error.wav");
        }

        //To add application to startup, get startup path from windows-run: shell:startup
        //then add shortcut to exe in debug folder.
        //Until there is a UI, double click system tray icon to write settings below, then restart solution to update settings
        //SendKeys commands: https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.sendkeys
        private void button1_Click(object sender, EventArgs e)
        {
            var items = new List<CommandItem>
            {
                new CommandItem(CommandTypes.Launch, new List<string> { "https://youtu.be/BuoXZ9zqtSY", "https://youtu.be/NHRuFmwqrak", "https://youtu.be/JsEbVpTCd90" }, "Relax"),
                new CommandItem(CommandTypes.Launch, new List<string> { "https://github.com/tr?q=guinness&type=&language=" }, "Git Hub"),
                new CommandItem(CommandTypes.Launch, new List<string> { "https://dev.azure.com/tr-corp-default/DataFlow" }, "Ado"),
                new CommandItem(CommandTypes.Launch, new List<string> { @"C:\GIT\Guinness_sharedservices-template\Guinness.SharedServices.Template\Guinness.SharedServices.Template.sln" }, "Shared Services"),
                new CommandItem(CommandTypes.Launch, new List<string> { @"C:\Program Files\Microsoft Office\root\Office16\OUTLOOK.EXE" }, "Outlook"),
                new CommandItem(CommandTypes.Launch, new List<string> { @"C:\GIT\Guinness_dataflow-client\DataCollection Client.sln" }, "DataCollection Addin"),
                new CommandItem(CommandTypes.Launch, new List<string> { "https://youtu.be/BuoXZ9zqtSY", "https://youtu.be/NHRuFmwqrak", "https://youtu.be/JsEbVpTCd90", @"C:\Program Files\Microsoft Office\root\Office16\OUTLOOK.EXE" }, "Good Morning"),
                new CommandItem(CommandTypes.Launch, new List<string> { "https://www.facebook.com/" }, "Face book"),
                new CommandItem(CommandTypes.Launch, new List<string> { "https://twitter.com/" }, "Twitter"),
                new CommandItem(CommandTypes.Launch, new List<string> { @"C:\Program Files\Microsoft Office\root\Office16\EXCEL.EXE" }, "Excel"),
                new CommandItem(CommandTypes.Launch, new List<string> { @"C:\Program Files\Notepad++\notepad++.exe"}, "Note pad plus plus"),
                new CommandItem(CommandTypes.Launch, new List<string> { @"C:\xxxUsers\0121450\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\System Tools\File Explorer" }, "File Explorer"),
                new CommandItem(CommandTypes.Launch, new List<string> { @"C:\Users\0121450\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\System Tools\Control Panel" }, "Control Panel"),
                new CommandItem(CommandTypes.Launch, new List<string> { @"C:\Users\0121450\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\System Tools\Command Prompt" }, "Command Prompt"),
                new CommandItem(CommandTypes.Launch, new List<string> { @"C:\Users\0121450\AppData\Local\Postman\Postman.exe" }, "Post man"),
                new CommandItem(CommandTypes.Launch, new List<string> { @"C:\Program Files\Microsoft Office\root\Office16\ONENOTE.EXE" }, "One note"),
                new CommandItem(CommandTypes.SendKeys, new List<string> { @"^+I" }, "Developer Tools"),
                new CommandItem(CommandTypes.SendKeys, new List<string> { @"%{F11}" }, "Open VBA"),
            };

            Repository.Create(items);
        }
    }
}

