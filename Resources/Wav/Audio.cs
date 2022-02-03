using System.Media;

namespace VoiceCommander.Resources.Wav
{
    public class Audio
    {
        public static void PlaySound(string resourceName, bool asynchronous = true)
        {
            var player = new SoundPlayer(Reader.EmbeddedBinaryResource<Audio>(resourceName));
            if (asynchronous)
            {
                player.Play();
            }
            else
            {
                player.PlaySync();
            }            
        }
    }
}
