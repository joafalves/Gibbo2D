using System.Diagnostics;
using System.Threading.Tasks;

namespace Gibbo.Framework
{
    public abstract class GameWindow : RenderWindow
    {
        Stopwatch gameWatch;

        public GameWindow(VideoMode videoMode, string title, Styles style, ContextSettings settings)
            : base(videoMode, title, style, settings)
        {
           	this.Closed += (object sender, System.EventArgs e) => {
				this.Close();
			};
        }

        public void Run()
        {
            SetVerticalSyncEnabled(true);

            GameTime.Elapsed = new System.TimeSpan(0);
            GameTime.Total = new System.TimeSpan(0);

            gameWatch = Stopwatch.StartNew();

            Initialize();

            while (IsOpen())
            {
                DispatchEvents();

                GameTime.Elapsed = gameWatch.Elapsed;
                GameTime.Total += gameWatch.Elapsed;

                gameWatch.Reset();
                gameWatch.Restart();

                Update();
                Draw();

                Display();
            }
        }

		public abstract void Initialize ();

		public abstract void Update ();
		
		public abstract void Draw ();
    }
}
