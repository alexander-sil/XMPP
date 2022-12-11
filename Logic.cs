using Artalk.Xmpp;
using Artalk.Xmpp.Client;
using System;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;
using Terminal.Gui;

namespace XMPP
{
    public class Logic
    {
        public static ArtalkXmppClient client = null;

        public static bool LogRcvdMsgs = true;

        public static bool LogSentMsgs = true;

        // TODO add cancellation reset on next receive
        
        public static bool Cancel = false;

        public static uint UnreadCount = 0;

        public static List<string> UnreadMessages = new List<string>();

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

                WindowLogic.label.Text = $" {UnreadCount} New Messages";

                EventHandler<Artalk.Xmpp.Im.MessageEventArgs> eventHandler = OnMessage;

                client.Message += (s, e) => Task.Run(() => OnMessage(s, e));

            }
            catch (Exception ex)
            {
                client = null;
                MessageBox.Query("Error", $"Unable to connect to {host}.\nThe error message is:\n{ex.Message}", "OK");
            }

            WindowLogic.label.SetFocus();
        }


        public static void SendMessage(Jid recipient, string msg)
        {
            if ((client != null) && client.Connected)
            {
                client.SendMessage(recipient, msg, string.Empty);
            }
            else
            {
                MessageBox.Query("Error", "Not connected. Aborting message send procedure", "OK");
            }

            WindowLogic.label.SetFocus();
        }

        public static void OnMessage(object sender, Artalk.Xmpp.Im.MessageEventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{DateTime.Now.ToString()}");
            sb.AppendLine($"Message from {e.Message.From}");

            sb.Append("\n");

            sb.AppendLine(e.Message.Body.ToString());

            sb.AppendLine();

            string msg = sb.ToString();

            if (LogRcvdMsgs)
            {
                SerializationLogic.AddReceivedMessage("received.txt", msg);
            }

            Task.Run(() => LabelClickHandler(msg));

            WindowLogic.label.SetFocus();
        }

        public static void LabelClickHandler(string msg)
        {
            UnreadCount++;
            WindowLogic.label.Clicked += Label_Clicked;
            WindowLogic.label.Text = $" [{UnreadCount} New Messages - CLICK HERE]";
            UnreadMessages.Add(msg);
        }

        private static void Label_Clicked()
        {
            StringBuilder sb = new StringBuilder();

            foreach (string item in UnreadMessages)
            {
                sb.AppendLine(item);
            }

            

            UnreadMessages.Clear();

            UnreadCount = 0;

            WindowLogic.label.Text = $" {UnreadCount} New Messages";

            WindowLogic.label.Clicked -= Label_Clicked;

            ShowUnreadMessages(sb.ToString(), ref Cancel);
        }

        public static void ShowUnreadMessages(string msg, ref bool cancel)
        {
            if (!cancel)
            {
                MessageBox.Query("Unread Messages", msg, "OK");
            }

            cancel = true;
        }
    }
}

