using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Terminal.Gui;

namespace XMPP
{
    class Program
    {
        static void Main(string[] args)
        {
            SerializationLogic.Initialize("sent.txt", "received.txt");
            MessageStore.RcvdMessages = SerializationLogic.DeserializeReceivedMsgs("received.txt");
            MessageStore.SentMessages = SerializationLogic.DeserializeSentMsgs("sent.txt");

        mark:

            try
            {
                WindowLogic.Execute();
            }
            catch (Exception ex)
            {
                MessageBox.Query("Error", ex.Message, "OK");
                goto mark;
            }
        }
    }
}
