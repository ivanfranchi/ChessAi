using ChessAi.Application.WebApi.Controllers;
using System;

namespace ChessAi.ConsoleCaller
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            GetMeth();
            Console.ReadKey();
        }

        async static void GetMeth()
        {
            var controller = MovesController.GetInstance();

            var xx = await controller.GetMoves(default);


            Console.WriteLine("got giuis");
        }
    }
}
