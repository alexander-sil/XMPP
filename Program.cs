namespace XMPP
{
    class Program
    {
        static void Main(string[] args)
        {



            SerializationLogic.Initialize("sent.txt", "received.txt");


            WindowLogic.Execute();



        }
    }
}
