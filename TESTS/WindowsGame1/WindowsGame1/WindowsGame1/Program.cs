using System;

namespace WindowsGame1
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            Console.WriteLine("starting");

            using (Game1 game = new Game1())
            {
                game.Run();
            }

            Console.WriteLine("end");
        }
    }
#endif
}

