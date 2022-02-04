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

