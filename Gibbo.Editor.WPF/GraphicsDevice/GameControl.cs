#region Copyrights
/*
Gibbo2D - Copyright - 2013 Gibbo2D Team
Founders - Joao Alves <joao.cpp.sca@gmail.com> and Luis Fernandes <luisapidcloud@hotmail.com>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE. 
*/
#endregion
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Gibbo.Library;
using XKeys = Microsoft.Xna.Framework.Input.Keys;

namespace Gibbo.Editor.WPF
{
    /// <summary>
    /// 
    /// </summary>
    abstract class GameControl : GraphicsDeviceControl
    {
        private GameTime gameTime;
        private Stopwatch timer;
        private TimeSpan elapsed = new TimeSpan();

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
            KeyDown += GameControl_KeyDown;
            KeyUp += GameControl_KeyUp;
        }

        void GameControl_KeyUp(object sender, KeyEventArgs e)
        {
            XKeys xkey = KeyboardUtil.ToXna(e.KeyCode);
            if (_keys.Contains(xkey))
                _keys.Remove(xkey);
        }

        void GameControl_KeyDown(object sender, KeyEventArgs e)
        {
            XKeys xkey = KeyboardUtil.ToXna(e.KeyCode);
            if (!_keys.Contains(xkey))
                _keys.Add(xkey);
        }

        void GameControl_MouseUp(object sender, MouseEventArgs e)
        {
            EditorMouse.UpdateState(e.X, e.X, (e.Button == System.Windows.Forms.MouseButtons.Left), (e.Button == System.Windows.Forms.MouseButtons.Middle), (e.Button == System.Windows.Forms.MouseButtons.Right));
        }

        void GameControl_MouseDown(object sender, MouseEventArgs e)
        {
            EditorMouse.UpdateState(e.X, e.X, (e.Button == System.Windows.Forms.MouseButtons.Left), (e.Button == System.Windows.Forms.MouseButtons.Middle), (e.Button == System.Windows.Forms.MouseButtons.Right));
        }

        void GameControl_MouseMove(object sender, MouseEventArgs e)
        {
            EditorMouse.UpdateState(e.X, e.X, (e.Button == System.Windows.Forms.MouseButtons.Left), (e.Button == System.Windows.Forms.MouseButtons.Middle), (e.Button == System.Windows.Forms.MouseButtons.Right));
        }

        protected override void Draw()
        {
            GameLoop();
            
            //if (!this.Focused)
            //{
            //    //Thread.Sleep(10);
            //}
            //else
            //{
            //    //Thread.Sleep(1);
            //}

            Draw(gameTime);
        }

        private void GameLoop()
        {
            if (timer == null) return;

            gameTime = new GameTime(timer.Elapsed, timer.Elapsed - elapsed);
            elapsed = timer.Elapsed;

            deltaFPSTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (deltaFPSTime > 1)
            {
                //if (1 / (float)gameTime.ElapsedGameTime.TotalSeconds > 60)
                //    Thread.Sleep(5);
            }
         
            if (this.Focused)
            {
                System.Drawing.Point point = System.Drawing.Point.Empty;

                point = this.PointToClient(MousePosition);

                gameMouseLastWorldPosition = new Vector2(gameMouseWorldPosition.X, gameMouseWorldPosition.Y);
                gameMouseWorldPosition = Vector2.Transform(new Vector2(point.X, point.Y), Matrix.Invert(SceneManager.ActiveCamera.TransformMatrix));

                HandleInput();
            }
            
            Update(gameTime);    
        }

        private void HandleInput()
        {
            //Console.WriteLine("keys {0}", _keys.Count);

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
