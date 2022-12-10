using System;
using System.IO;

namespace XMPP
{
    public class SerializationLogic
    {
        public static void Initialize(string path)
        {
            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
            }
        }

        public static string DeserializeSentMsgs(string path)
        {
            return File.ReadAllText(path);
        }

        public static void AddSentMessage(string path)
        {
            File.AppendAllText(path, $"Message from {}");
        }
    }
}

