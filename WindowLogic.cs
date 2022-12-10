using Terminal.Gui;
using System;

namespace XMPP
{
    public class WindowLogic
    {


        public static Window window = new Window("XMPP Client")
        {
            X = 0,
            Y = 1,

            Width = Dim.Fill(),
            Height = Dim.Fill()
        };

        public static void Execute()
        {

            Toplevel top = Application.Top;

            Console.Title = "XMPP";

            Application.Init();

            Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);

            top = Application.Top;

            MenuBarItem[] menu = new MenuBarItem[] { new MenuBarItem("Tools", new MenuItem[] {
                new MenuItem("Log In", "", DialogLogic.ShowConnectDialog),
                new MenuItem("Compose Message", "", DialogLogic.ShowMessageComposeDialog),
                new MenuItem("View Sent Messages", "", DialogLogic.ShowSentMsgs),
                new MenuItem("View Received Messages", "", DialogLogic.ShowRcvdMsgs),
                new MenuItem("About", "", () => MessageBox.Query("About", "XMPP Client\nVersion 1.0\n(C) 2022 alexander-sil"))
            }) };


            top.Add(new MenuBar(menu));

            top.Add(window);

            Application.Run();

        }
    }
}

