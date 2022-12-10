using System;
using Artalk.Xmpp;
using Artalk.Xmpp.Client;
using Artalk.Xmpp.Im;
using Artalk.Xmpp.Core;
using Artalk.Xmpp.Extensions.Dataforms;
using Artalk.Xmpp.Extensions;
using Terminal.Gui;
using System.Collections.Generic;
using System.Text;

namespace XMPP
{
    public class Logic
    {
        public static ArtalkXmppClient client = null;

        ~Logic()
        {
            client.Dispose();
        }

        public static void Login(string host, string username, string password, ushort port, bool tls)
        {
            client = new ArtalkXmppClient(host, username, password, port, tls);

            try
            {
                client.Connect();
                client.Message += OnMessage;
            }
            catch
            {
                MessageBox.Query("Error", $"Unable to connect to {host}", "OK");
            }

        }

        public static void SendMessage(Jid recipient, string subj, string msg)
        {

        }

        public static void OnMessage(object sender, Artalk.Xmpp.Im.MessageEventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{DateTime.Now.ToLongDateString()}");
            sb.AppendLine($"Message from {e.Message.From}");

            sb.Append("\n");

            sb.AppendLine(e.Message.Subject);

            sb.Append("\n");

            sb.AppendLine(e.Message.Body.ToString());

            sb.AppendLine();

            Console.Beep();

            string msg = sb.ToString();

            MessageBox.Query("New Message", msg, "Discard", "Save");
        }

    }
}

