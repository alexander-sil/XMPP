using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace XMPP
{
    class Program
    {
        static void Main(string[] args)
        {
            SerializationLogic.Initialize("sent.txt", "received.txt");
            MessageStore.RcvdMessages = SerializationLogic.DeserializeReceivedMsgs("received.txt");
            MessageStore.SentMessages = SerializationLogic.DeserializeSentMsgs("sent.txt");

            WindowLogic.Execute();
        }
    }
}
