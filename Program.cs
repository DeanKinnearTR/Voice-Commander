using System;
using System.Windows.Forms;
using VoiceCommander.Resources.Wav;

namespace VoiceCommander
{
    class Program
    {
        [STAThread]
        static void Main() 
        {
            Audio.PlaySound("romulan_transporter.wav");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
