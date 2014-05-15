#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace Gibbo.Engine.Windows.DirectX
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                using (var game = new Game1())
                    game.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, "error");
                Console.ReadLine();
            }

            //Console.WriteLine("DEBUG MODE, PRESS ANY KEY TO EXIT...");
            //Console.ReadKey();
        }
    }
#endif
}
