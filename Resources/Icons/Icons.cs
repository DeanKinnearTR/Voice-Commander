using System.Drawing;

namespace VoiceCommander.Resources.Icons
{
    public class Icons
    {
        public static Icon GetIcon(string resourceName)
        {
            using (var stream = Reader.EmbeddedBinaryResource<Icons>(resourceName))
            {
                return new Icon(stream);
            }
        }
    }
}
