using ChessAi.ConsoleCaller.HttpGate;
using System;

namespace ChessAi.ConsoleCaller
{
    class Program
    {
        static RequestManager _requestManager;

        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            _requestManager = new RequestManager();

            var s = "";
            while (s != "q")
            {
                GetMethod(_requestManager);
                s = Console.ReadLine();
            }
        }

        static void GetMethod(RequestManager rm)
        {
            try
            {
                var xx = rm.SendGETRequest("https://localhost:44336/moves/fen", "", "", false);


                Console.WriteLine("got giuis");
            }
            catch
            {
                
            }
        }
    }
}
