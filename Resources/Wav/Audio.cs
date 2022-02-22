using System.Media;

namespace VoiceCommander.Resources.Wav
{
    public class Audio
    {
        public static void PlaySound(string resourceName, bool asynchronous = true)
        {
            using (var stream = Reader.EmbeddedBinaryResource<Audio>(resourceName))
            {
                using (var player = new SoundPlayer(stream))
                {
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
    }
}
