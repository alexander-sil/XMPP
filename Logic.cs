using Artalk.Xmpp;
using Artalk.Xmpp.Client;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Terminal.Gui;
using Artalk.Xmpp.Im;

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
            catch (Exception ex)
            {
                client = null;
                MessageBox.Query("Error", $"Unable to connect to {host}.\nThe error message is:\n{ex.Message}", "OK");
            }

        }


        public static void SendMessage(Jid recipient, string subj, string msg)
        {
            if ((client != null) && client.Connected)
            {
                client.SendMessage(recipient, msg, subj);
            }
            else
            {
                MessageBox.Query("Error", "Not connected. Aborting message send procedure", "OK");
            }

            WindowLogic.window.SetFocus();
        }

        public static void OnMessage(object sender, Artalk.Xmpp.Im.MessageEventArgs e)
        {
            
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{DateTime.Now.ToLongDateString()}");
            sb.AppendLine($"Message from {e.Message.From}");

            sb.Append("\n");

            sb.AppendLine(e.Message.Body.ToString());

            sb.AppendLine();

            string msg = sb.ToString();

            int answer = MessageBox.Query("New Message", msg, "Save", "Discard");

            if (answer == 0)
            {
                SerializationLogic.AddReceivedMessage("received.txt", msg);
            }

            Application.Top.RemoveAll();

            WindowLogic.Execute();

            client.Message -= OnMessage;

            client.Message += OnMessage;
            

        }
    }
}

