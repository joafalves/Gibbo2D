using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Gibbo.Library;

namespace Gibbo.Editor
{
    /// <summary>
    /// 
    /// </summary>
    abstract class GameControl : GraphicsDeviceControl
    {
        private GameTime gameTime;
        private Stopwatch timer;
        private TimeSpan elapsed;

        private Camera camera = new Camera();

        protected Vector2 gameMouseLastWorldPosition;
        protected Vector2 gameMouseWorldPosition;

        private float deltaFPSTime;

        internal Camera Camera
        {
            get { return camera; }
            set { camera = value; }
        }

        protected override void Initialize()
        {
            timer = Stopwatch.StartNew();

            MouseMove += GameControl_MouseMove;
            MouseDown += GameControl_MouseDown;
            MouseUp += GameControl_MouseUp;
            //Application.Idle += delegate { GameLoop(); };
        }

        void GameControl_MouseUp(object sender, MouseEventArgs e)
        {
            EditorMouse.UpdateState(e.X, e.Y,
                (e.Button == System.Windows.Forms.MouseButtons.Left),
                (e.Button == System.Windows.Forms.MouseButtons.Middle),
                (e.Button == System.Windows.Forms.MouseButtons.Right));
        }

        void GameControl_MouseDown(object sender, MouseEventArgs e)
        {
            EditorMouse.UpdateState(e.X, e.Y,
                  (e.Button == System.Windows.Forms.MouseButtons.Left),
                  (e.Button == System.Windows.Forms.MouseButtons.Middle),
                  (e.Button == System.Windows.Forms.MouseButtons.Right));
        }

        void GameControl_MouseMove(object sender, MouseEventArgs e)
        {
            EditorMouse.UpdateState(e.X, e.Y,
               (e.Button == System.Windows.Forms.MouseButtons.Left),
               (e.Button == System.Windows.Forms.MouseButtons.Middle),
               (e.Button == System.Windows.Forms.MouseButtons.Right));
        }

        protected override void Draw()
        {
            GameLoop();
            
            if (!this.Focused)
            {
                Thread.Sleep(50);
            }
            else
            {
                //Thread.Sleep(1);
            }

            Draw(gameTime);
        }

        private void GameLoop()
        {
            gameTime = new GameTime(timer.Elapsed, timer.Elapsed - elapsed);
            elapsed = timer.Elapsed;

            deltaFPSTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (deltaFPSTime > 1)
            {
                if(1 / (float)gameTime.ElapsedGameTime.TotalSeconds > 60)
                    Thread.Sleep(5);
            }
         
            if (this.Focused)
            {
                System.Drawing.Point point = System.Drawing.Point.Empty;

                point = this.PointToClient(MousePosition);

                gameMouseLastWorldPosition = new Vector2(gameMouseWorldPosition.X, gameMouseWorldPosition.Y);
                gameMouseWorldPosition = Vector2.Transform(new Vector2(point.X, point.Y), Matrix.Invert(SceneManager.ActiveCamera.TransformMatrix));

                //Invalidate();

                HandleInput();
            }
            
            Update(gameTime);    
        }

        private void HandleInput()
        {
            if (_keys != null)
            {
                EditorKeyboard.SetKeys(_keys);
            }
        }

        public void ReleaseDevice()
        {
            this.timer.Stop();
            Dispose(true);
        }

        protected abstract void Update(GameTime gameTime);
        protected abstract void Draw(GameTime gameTime);
    }
}
