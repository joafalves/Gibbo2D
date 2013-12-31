using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gibbo.Framework;

namespace Gibbo.RuntimeConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            VideoMode video = new VideoMode(800, 600);
            string title = "Demo 1";
            Styles style = Styles.Default;

            ContextSettings settings = new ContextSettings()
            {
                AntialiasingLevel = 2,
                DepthBits = 32
            };

            using (Game1 game = new Game1(video, title, style, settings))
            {
                game.Run();
            }
        }
    }
}
