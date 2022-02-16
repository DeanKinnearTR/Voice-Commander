using System.Collections.Generic;
using System.Windows.Forms;
using System;
using VoiceCommander.Resources.Icons;
using VoiceCommander.Resources.Wav;
using VoiceCommander.Types;

namespace VoiceCommander
{
    //TODO: Implement UI
    //TODO: Add caching for resources

    public partial class MainForm : Form
    {
        private readonly Controller _controller;
        public MainForm()
        {
            InitializeComponent();
            _controller = new Controller();
            _controller.StateChange += _controller_StateChange;
        }

        private void _controller_StateChange(ControllerStates state)
        {
            switch (state)
            {
                case ControllerStates.Listening:
                    NotifyIcon.Icon = Icons.GetIcon("DefaultIcon.ico");
                    Audio.PlaySound("communications_start_transmission.wav");
                    break;
                case ControllerStates.NotListening:
                    NotifyIcon.Icon = Icons.GetIcon("NotListening.ico");
                    Audio.PlaySound("communications_end_transmission.wav");
                    break;
                case ControllerStates.Error:
                    NotifyIcon.Icon = Icons.GetIcon("ErrorIcon.ico");
                    Audio.PlaySound("computer_error.wav");
                    break;
                case ControllerStates.Action:
                    NotifyIcon.Icon = Icons.GetIcon("BusyIcon.ico");
                    Audio.PlaySound("computer_work_beep.wav");
                    break;
                case ControllerStates.ShutDown:
                    ShutdowmCommander();
                    return;

            }
        }

        //From a google search, 'Chaos' is ranked as #5,000 in word frequency, therefore would be less likely to be misunderstood by recognition and sounds pretty cool lol https://en.wikipedia.org/wiki/Word_frequency_effect
        //To add application to startup, get startup path from windows-run: shell:startup
        //then add shortcut to exe in debug folder.
        //Until there is a UI, double click system tray icon to write settings below, then restart solution to update settings
        //SendKeys commands: https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.sendkeys
        private void TemporaryHack(object sender, EventArgs e)
        {
            var items = new List<CommandItem>
            {
                new CommandItem("Chaos Relax",CommandTypes.Launch, new List<string> { "https://youtu.be/BuoXZ9zqtSY", "https://youtu.be/NHRuFmwqrak", "https://youtu.be/JsEbVpTCd90" }),
                new CommandItem("Chaos Git Hub", CommandTypes.Launch, new List<string> { "https://github.com/tr?q=guinness&type=&language=" }),
                new CommandItem("Chaos Ado", CommandTypes.Launch, new List<string> { "https://dev.azure.com/tr-corp-default/DataFlow" } ),
                new CommandItem("Chaos Shared Services", CommandTypes.Launch, new List<string> { @"C:\GIT\Guinness_sharedservices-template\Guinness.SharedServices.Template\Guinness.SharedServices.Template.sln" }),
                new CommandItem("Chaos Outlook", CommandTypes.Launch, new List<string> { @"C:\Program Files\Microsoft Office\root\Office16\OUTLOOK.EXE" }),
                new CommandItem("Chaos DataCollection Addin",CommandTypes.Launch, new List<string> { @"C:\GIT\Guinness_dataflow-client\DataCollection Client.sln" }),
                new CommandItem("Chaos Good Morning",CommandTypes.Launch, new List<string> { "https://youtu.be/BuoXZ9zqtSY", "https://youtu.be/NHRuFmwqrak", "https://youtu.be/JsEbVpTCd90", @"C:\Program Files\Microsoft Office\root\Office16\OUTLOOK.EXE" }),
                new CommandItem("Chaos Face book",CommandTypes.Launch, new List<string> { "https://www.facebook.com/" }),
                new CommandItem("Chaos Twitter", CommandTypes.Launch, new List<string> { "https://twitter.com/" }),
                new CommandItem("Chaos Excel",CommandTypes.Launch, new List<string> { @"C:\Program Files\Microsoft Office\root\Office16\EXCEL.EXE" }),
                new CommandItem("Chaos Note pad plus plus", CommandTypes.Launch, new List<string> { @"C:\Program Files\Notepad++\notepad++.exe"}),
                new CommandItem("Chaos File Explorer",CommandTypes.Launch, new List<string> { @"C:\xxxUsers\0121450\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\System Tools\File Explorer" }),
                new CommandItem("Chaos Control Panel", CommandTypes.Launch, new List<string> { @"C:\Users\0121450\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\System Tools\Control Panel" }),
                new CommandItem("Chaos Command Prompt",CommandTypes.Launch, new List<string> { @"C:\Users\0121450\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\System Tools\Command Prompt" }),
                new CommandItem("Chaos Post man",CommandTypes.Launch, new List<string> { @"C:\Users\0121450\AppData\Local\Postman\Postman.exe" }),
                new CommandItem("Chaos One note", CommandTypes.Launch, new List<string> { @"C:\Program Files\Microsoft Office\root\Office16\ONENOTE.EXE" }),
                new CommandItem("Chaos Developer Tools", CommandTypes.SendKeys, new List<string> { @"^+I" }),
                new CommandItem("Chaos Open VBA", CommandTypes.SendKeys, new List<string> { @"%{F11}" }),
            };

            Repository.Create(items);
        }
    }
}

