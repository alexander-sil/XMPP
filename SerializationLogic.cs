using System;
using System.IO;

namespace XMPP
{
    public class SerializationLogic
    {
        public static void Initialize(string spath, string rpath)
        {
            if (!(File.Exists(rpath) && File.Exists(spath)))
            {
                File.Create(spath).Dispose();
                File.Create(rpath).Dispose();
            }
        }

        public static string DeserializeSentMsgs(string spath)
        {
            return File.ReadAllText(spath);
        }

        public static string DeserializeReceivedMsgs(string rpath)
        {
            return File.ReadAllText(rpath);
        }

        public static void AddSentMessage(string path, string msg, DateTime date)
        {
            File.AppendAllText(path, $"{date.ToLongDateString}\n\nMessage from You\n\n{msg}\n\n");
        }

        public static void AddReceivedMessage(string path, string msg)
        {
            File.AppendAllText(path, msg);
        }
    }
}

