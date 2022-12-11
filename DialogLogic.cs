using System;
using System.IO;
using System.Text;
using Terminal.Gui;
using Artalk.Xmpp;
using static Terminal.Gui.View;

namespace XMPP
{
    public class DialogLogic
    {
        #region Dialogs

        public static Window _ConnectDialog = new Window(new Rect(7, 7, 80, 19), "Establish XMPP Server Connection");

        public static Window _MessageComposeDialog = new Window(new Rect(7, 7, 80, 16), "Compose Message");

        #endregion

        #region ConnectDialogControls

        public static TextField _ConnectDialogHostField = new TextField(2, 2, 25, "");

        public static TextField _ConnectDialogJIDAddressField = new TextField(2, 4, 25, "");

        public static TextField _ConnectDialogPasswordField = new TextField(2, 6, 25, "");

        public static TextField _ConnectDialogPortField = new TextField(2, 8, 25, "5222");

        public static CheckBox _ConnectDialogTLSBox = new CheckBox(2, 10, "Use TLS");

        public static Label _ConnectDialogHostLabel = new Label(29, 2, "XMPP Hostname");

        public static Label _ConnectDialogJIDAddressLabel = new Label(29, 4, "XMPP Username");

        public static Label _ConnectDialogPasswordLabel = new Label(29, 6, "XMPP Password");

        public static Label _ConnectDialogPortLabel = new Label(29, 8, "XMPP Port");

        public static Button _ConnectDialogYesButton = new Button(2, 14, "OK");

        public static Button _ConnectDialogNoButton = new Button(2, 15, "Cancel");

        #endregion

        #region MessageComposeDialogControls

        public static TextField _MessageComposeDialogReceiverJIDAddressField = new TextField(2, 2, 25, "");

        public static TextView _MessageComposeDialogMessageField = new TextView(new Rect(2, 4, 25, 6));

        public static Label _MessageComposeDialogReceiverJIDAddressLabel = new Label(29, 2, "Receiver JID Address");

        public static Label _MessageComposeDialogMessageLabel = new Label(29, 4, "Message Body");

        public static Button _MessageComposeDialogYesButton = new Button(2, 12, "OK");

        public static Button _MessageComposeDialogNoButton = new Button(2, 13, "Cancel");

        #endregion

        #region Methods

        public static void ShowSentMsgs()
        {
            string text = SerializationLogic.DeserializeSentMsgs("sent.txt");

            MessageBox.Query("Sent Messages", text, "OK");

            WindowLogic.window.SetFocus();
        }

        public static void ShowRcvdMsgs()
        {
            string text = SerializationLogic.DeserializeReceivedMsgs("received.txt");

            MessageBox.Query("Received Messages", text, "OK");

            WindowLogic.window.SetFocus();
        }

        public static void ShowConnectDialog()
        {

            _ConnectDialogHostField.MouseEnter += ((MouseEventArgs e) => _ConnectDialogHostField.SetFocus());
            _ConnectDialogJIDAddressField.MouseEnter += ((MouseEventArgs e) => _ConnectDialogJIDAddressField.SetFocus());
            _ConnectDialogPasswordField.MouseEnter += ((MouseEventArgs e) => _ConnectDialogPasswordField.SetFocus());
            _ConnectDialogPortField.MouseEnter += ((MouseEventArgs e) => _ConnectDialogPortField.SetFocus());

            _ConnectDialogYesButton.MouseEnter += ((MouseEventArgs e) => _ConnectDialogYesButton.SetFocus());
            _ConnectDialogNoButton.MouseEnter += ((MouseEventArgs e) => _ConnectDialogNoButton.SetFocus());

            _ConnectDialog.Add(_ConnectDialogHostField);
            _ConnectDialog.Add(_ConnectDialogJIDAddressField);
            _ConnectDialog.Add(_ConnectDialogPasswordField);
            _ConnectDialog.Add(_ConnectDialogPortField);
            _ConnectDialog.Add(_ConnectDialogTLSBox);

            _ConnectDialog.Add(_ConnectDialogHostLabel);
            _ConnectDialog.Add(_ConnectDialogJIDAddressLabel);
            _ConnectDialog.Add(_ConnectDialogPasswordLabel);
            _ConnectDialog.Add(_ConnectDialogPortLabel);

            _ConnectDialog.Add(_ConnectDialogYesButton);
            _ConnectDialog.Add(_ConnectDialogNoButton);

            WindowLogic.window.Add(_ConnectDialog);



            _ConnectDialogYesButton.Clicked += ShowConnectDialogYes;
            _ConnectDialogNoButton.Clicked += ShowConnectDialogNo;

            _ConnectDialog.SetFocus();
        }
        private static void ShowConnectDialogNo()
        {
            _ConnectDialogHostField.Text = "";
            _ConnectDialogJIDAddressField.Text = "";
            _ConnectDialogPasswordField.Text = "";
            _ConnectDialogPortField.Text = "5222";
            _ConnectDialogTLSBox.Checked = false;

            _ConnectDialog.RemoveAll();

            WindowLogic.window.Remove(_ConnectDialog);

            _ConnectDialogYesButton.Clicked -= ShowConnectDialogYes;
            _ConnectDialogNoButton.Clicked -= ShowConnectDialogNo;

            WindowLogic.window.SetFocus();
        }

        private static void ShowConnectDialogYes()
        {
            string host = (string)_ConnectDialogHostField.Text;
            string jid = (string)_ConnectDialogJIDAddressField.Text;
            string pass = (string)_ConnectDialogPasswordField.Text;
            string port = (string)_ConnectDialogPortField.Text;
            bool tls = _ConnectDialogTLSBox.Checked;

            _ConnectDialogHostField.Text = "";
            _ConnectDialogJIDAddressField.Text = "";
            _ConnectDialogPasswordField.Text = "";
            _ConnectDialogPortField.Text = "5222";
            _ConnectDialogTLSBox.Checked = false;

            _ConnectDialogYesButton.Clicked -= ShowConnectDialogYes;
            _ConnectDialogNoButton.Clicked -= ShowConnectDialogNo;

            _ConnectDialog.RemoveAll();

            WindowLogic.window.Remove(_ConnectDialog);

            if (ushort.TryParse(port, out ushort check))
            {
                Logic.Login(host, jid, pass, check, tls);
            }
            else
            {
                MessageBox.Query("Error", "Invalid port number format", "OK");
            }

        }

        public static void ShowMessageComposeDialog()
        {

            _MessageComposeDialogReceiverJIDAddressField.MouseEnter += ((MouseEventArgs e) => _MessageComposeDialogReceiverJIDAddressField.SetFocus());
            _MessageComposeDialogMessageField.MouseEnter += ((MouseEventArgs e) => _MessageComposeDialogMessageField.SetFocus());

            _MessageComposeDialogYesButton.MouseEnter += ((MouseEventArgs e) => _MessageComposeDialogYesButton.SetFocus());
            _MessageComposeDialogNoButton.MouseEnter += ((MouseEventArgs e) => _MessageComposeDialogNoButton.SetFocus());

            _MessageComposeDialog.Add(_MessageComposeDialogReceiverJIDAddressField);
            _MessageComposeDialog.Add(_MessageComposeDialogMessageField);

            _MessageComposeDialog.Add(_MessageComposeDialogReceiverJIDAddressLabel);
            _MessageComposeDialog.Add(_MessageComposeDialogMessageLabel);

            _MessageComposeDialog.Add(_MessageComposeDialogYesButton);
            _MessageComposeDialog.Add(_MessageComposeDialogNoButton);

            WindowLogic.window.Add(_MessageComposeDialog);



            _MessageComposeDialogYesButton.Clicked += ShowMessageComposeDialogYes;
            _MessageComposeDialogNoButton.Clicked += ShowMessageComposeDialogNo;

            _ConnectDialog.SetFocus();
        }

        private static void ShowMessageComposeDialogNo()
        {
            _MessageComposeDialogReceiverJIDAddressField.Text = "";
            _MessageComposeDialogMessageField.Text = "";

            _MessageComposeDialog.RemoveAll();

            WindowLogic.window.Remove(_MessageComposeDialog);

            _MessageComposeDialogYesButton.Clicked -= ShowMessageComposeDialogYes;
            _MessageComposeDialogNoButton.Clicked -= ShowMessageComposeDialogNo;

            WindowLogic.window.SetFocus();
        }

        private static void ShowMessageComposeDialogYes()
        {
            string jid = (string)_MessageComposeDialogReceiverJIDAddressField.Text;
            string msg = (string)_MessageComposeDialogMessageField.Text;

            _MessageComposeDialogReceiverJIDAddressField.Text = "";
            _MessageComposeDialogMessageField.Text = "";

            _MessageComposeDialogYesButton.Clicked -= ShowMessageComposeDialogYes;
            _MessageComposeDialogNoButton.Clicked -= ShowMessageComposeDialogNo;

            _MessageComposeDialog.RemoveAll();

            WindowLogic.window.Remove(_MessageComposeDialog);

            try
            {
                Jid check = new Jid(jid);

                Logic.SendMessage(check, msg);

                DateTime date = DateTime.Now;

                SerializationLogic.AddSentMessage("sent.txt", msg, date);

            }
            catch (Exception ex)
            {
                MessageBox.Query("Error", $"Unable to send the specified message.\nThe exception is:\n{ex.Message}", "OK");
            }
        }

        #endregion

    }
}


