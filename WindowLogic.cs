using Terminal.Gui;
using System;

namespace XMPP
{
    public class WindowLogic
    {
        public static Label label = new Label("  ");

        public static MenuBarItem[] menu = new MenuBarItem[] { new MenuBarItem("Tools", new MenuItem[] {
                new MenuItem("Log In", "", DialogLogic.ShowConnectDialog),
                new MenuItem("Compose Message", "", DialogLogic.ShowMessageComposeDialog, () => (Logic.client != null)),
                new MenuItem("Toggle Sent Logging", "", () => { if (Logic.LogSentMsgs == true) { Logic.LogSentMsgs = false; } else { Logic.LogSentMsgs = true; }; }, () => true),
                new MenuItem("Toggle Received Logging", "", () => { if (Logic.LogRcvdMsgs == true) { Logic.LogRcvdMsgs = false; } else { Logic.LogRcvdMsgs = true; }; }, () => true),
                new MenuItem("View Sent Messages", "", DialogLogic.ShowSentMsgs),
                new MenuItem("View Received Messages", "", DialogLogic.ShowRcvdMsgs),
                new MenuItem("About", "", () => MessageBox.Query("About", "XMPP Client\nVersion 1.0\n(C) 2022 alexander-sil", "OK"))
            }) };

        public static Window window = new Window("XMPP Client")
        {
            X = 0,
            Y = 1,

            Width = Dim.Fill(),
            Height = Dim.Fill()
        };

        public static void Execute()
        {
            while (true)
            {
                Toplevel top = Application.Top;

                Console.Title = "XMPP";

                Application.Init();

                Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
                Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);

                top = Application.Top;

                top.Add(new MenuBar(menu));

                top.Add(window);

                window.Add(label);

                Application.Run();

                Logic.client.Close();
            }
        }
    }
}

