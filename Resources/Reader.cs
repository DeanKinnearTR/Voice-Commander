using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace VoiceCommander.Resources
{
    public class Reader
    {
        public static Stream EmbeddedBinaryResource<T>(string resourceName)
        {
            var assembly = typeof(T).GetTypeInfo().Assembly;
            var resourceNames = assembly.GetManifestResourceNames(); 
            var findResource = resourceNames.FirstOrDefault(q => q.EndsWith(resourceName, StringComparison.OrdinalIgnoreCase));
            return findResource == null ? null : assembly.GetManifestResourceStream(findResource);
        }
    }
}
