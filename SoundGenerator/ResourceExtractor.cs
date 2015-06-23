using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace SoundGenerator
{
    public static class ResourceExtractor
    {
        public static void ExtractResourceToFile(String resourceName, String filename)
        {
            if (!File.Exists(filename))
                using (Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
                using (FileStream fs = new FileStream(filename, FileMode.Create))
                {
                    byte[] b = new byte[s.Length];
                    s.Read(b, 0, b.Length);
                    fs.Write(b, 0, b.Length);
                }
        }

        public static String ReadResourceAsString(String resourceName)
        {
            String value = "";
            byte[] rawBytes;
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                rawBytes = new byte[stream.Length];
                stream.Read(rawBytes, 0, rawBytes.Length);
            }
            value = ASCIIEncoding.ASCII.GetString(rawBytes);
            return value;
        }
    }
}
