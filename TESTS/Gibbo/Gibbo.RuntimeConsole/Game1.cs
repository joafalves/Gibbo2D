using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gibbo.Framework;
using System.Threading;

namespace Gibbo.RuntimeConsole
{
    class Game1 : GameWindow
    {
        CircleShape circle;
		Texture texture;
		Sprite sprite;

        public Game1(VideoMode videoMode, string title, Styles style, ContextSettings settings)
            : base(videoMode, title, style, settings)
        {
           
        }

        View view = new View();
        public override void Initialize()
        {
            circle = new CircleShape(32);
            circle.FillColor = Color.Black;

			texture = new Texture ("content/rock_planet.png", new IntRect(0,0,200,200));

			sprite = new Sprite (texture);
          
    
        }

        public override void Update()
        {
            //circle.Position = new Vector2f(circle.Position.X + 1, 0);
            circle.Move(50 * GameTime.DeltaTime, 0);

            //DefaultView.Center = new Vector2f(300, 300);
           //this.DefaultView.Move(new Vector2f(200, 0));
           //this.DefaultView.Rotate(10);
           
        }

        

        public override void Draw()
        {
            Clear(Color.CornflowerBlue);

			Draw (sprite);
          
            Draw(circle);
        }
    }
}
