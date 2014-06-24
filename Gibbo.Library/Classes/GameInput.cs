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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;

namespace Gibbo.Library
{
    /// <summary>
    /// Handles the game input of the game
    /// </summary>
    public static class GameInput
    {
        #region fields

        private static KeyboardState lastKeyboardState;
        private static KeyboardState keyboardState;

        private static MouseState mouseState;
        private static MouseState lastMouseState;

        private static Dictionary<PlayerIndex, GamePadState> gamePadState = new Dictionary<PlayerIndex, GamePadState>();
        private static Dictionary<PlayerIndex, GamePadState> lastGamePadState = new Dictionary<PlayerIndex, GamePadState>();

        // TODO: implement other input types

        #endregion

        #region properties

        /// <summary>
        /// The bounding box of the mouse.
        /// Uses the mouse position as reference
        /// </summary>
        public static Rectangle MouseBoundingBox
        {
            get
            {
                Vector2 worldPosition = Vector2.Transform(new Vector2(mouseState.X, mouseState.Y), Matrix.Invert(SceneManager.ActiveCamera.TransformMatrix));
                return new Rectangle((int)worldPosition.X, (int)worldPosition.Y, 1, 1);
            }
        }

        /// <summary>
        /// The position of the mouse in the scene.
        /// </summary>
        public static Vector2 MousePosition
        {
            get
            {
                if (SceneManager.IsEditor)
                    return new Vector2(mouseState.X, mouseState.Y);
                else
                    return Vector2.Transform(new Vector2(mouseState.X, mouseState.Y), Matrix.Invert(SceneManager.ActiveCamera.TransformMatrix));
            }
        }

        /// <summary>
        /// The mouse transformation matrix
        /// </summary>
        public static Matrix MouseTransform
        {
            get
            {
               return
                    Matrix.CreateTranslation(new Vector3(-new Vector2(1.0f / 2.0f, 1.0f / 2.0f), 0.0f)) *
                    Matrix.CreateScale(SceneManager.ActiveCamera.Zoom) *
                    Matrix.CreateTranslation(new Vector3(mouseState.X, mouseState.Y, 0.0f));
            }
        }

        /// <summary>
        /// The current keyboard state.
        /// </summary>
        public static KeyboardState KeyboardState
        {
            get { return GameInput.keyboardState; }
        }

        /// <summary>
        /// The last keyboard state.
        /// You can use this to know how was the last state of the keyboard
        /// </summary>
        public static KeyboardState LastKeyboardState
        {
            get { return GameInput.lastKeyboardState; }
        }

        /// <summary>
        /// The current mouse state.
        /// </summary>
        public static MouseState MouseState
        {
            get { return GameInput.mouseState; }
        }

        /// <summary>
        /// The last mouse state.
        /// You can use this to know how was the last state of the mouse
        /// </summary>
        public static MouseState LastMouseState
        {
            get { return GameInput.lastMouseState; }
        }

        /// <summary>
        /// The current game pads states
        /// </summary>
        public static Dictionary<PlayerIndex, GamePadState> GamePadState
        {
            get { return GameInput.gamePadState; }
        }

        /// <summary>
        /// The last game pads states.
        /// You can use this to know how was the last states of the game pads
        /// </summary>
        public static Dictionary<PlayerIndex, GamePadState> LastGamePadState
        {
            get { return GameInput.lastGamePadState; }
        }

        #endregion

        #region constructor

        static GameInput()
        {
            foreach (PlayerIndex index in Enum.GetValues(typeof(PlayerIndex)))
            {
                lastGamePadState[index] = GamePad.GetState(index);
                gamePadState[index] = GamePad.GetState(index);
            }
        }

        #endregion

        #region general methods

        /// <summary>
        /// Updates the logic of the Game Input.
        /// Input states are updated here.
        /// </summary>
        public static void Update()
        {
            // Update last keyboard state and update the current keyboard state
            lastKeyboardState = keyboardState;

            if (SceneManager.IsEditor)
                keyboardState = EditorKeyboard.GetState();
            else
                keyboardState = Keyboard.GetState();

            // Update mouse state
            lastMouseState = mouseState;

            if(SceneManager.IsEditor)
               mouseState = EditorMouse.GetState();
            else
               mouseState = Mouse.GetState();
         
            // Update game pad state
            foreach (PlayerIndex index in Enum.GetValues(typeof(PlayerIndex)))
            {
                lastGamePadState[index] = gamePadState[index];
                gamePadState[index] = GamePad.GetState(index);
            }

            MouseCollisionEvents();
        }

        private static void MouseCollisionEvents()
        {
            if (SceneManager.ActiveScene != null)
            {
                Fixture detected = SceneManager.ActiveScene.World.TestPoint(ConvertUnits.ToSimUnits(MousePosition));

                if (detected != null)
                {
                    if (!detected.Body.GameObject.mouseOver)
                        detected.Body.GameObject.OnMouseEnter();

                    detected.Body.GameObject.mouseOver = true;
                    detected.Body.GameObject.OnMouseMove();

                    bool mouseDown = false;

                    /* left button down? */
                    if (GameInput.MouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                    {
                        detected.Body.GameObject.OnMouseDown(MouseEventButton.LeftButton);
                        mouseDown = true;
                    }

                    /* right button down? */
                    if (GameInput.MouseState.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                    {
                        detected.Body.GameObject.OnMouseDown(MouseEventButton.RightButton);
                        mouseDown = true;
                    }

                    /* middle button down? */
                    if (GameInput.MouseState.MiddleButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                    {
                        detected.Body.GameObject.OnMouseDown(MouseEventButton.MiddleButton);
                        mouseDown = true;
                    }

                    /* left button click? */
                    if (GameInput.IsMouseKeyPressed(MouseEventButton.LeftButton))
                    {
                        detected.Body.GameObject.OnMouseClick(MouseEventButton.LeftButton);
                        mouseDown = true;
                    }

                    /* right button click? */
                    if (GameInput.IsMouseKeyPressed(MouseEventButton.RightButton))
                    {
                        detected.Body.GameObject.OnMouseClick(MouseEventButton.RightButton);
                        mouseDown = true;
                    }

                    /* middle button click? */
                    if (GameInput.IsMouseKeyPressed(MouseEventButton.MiddleButton))
                    {
                        detected.Body.GameObject.OnMouseClick(MouseEventButton.MiddleButton);
                        mouseDown = true;
                    }

                    if (mouseDown)
                    {
                        detected.Body.GameObject.mouseDown = true;
                    }
                    else
                    {
                        if (detected.Body.GameObject.mouseDown)
                        {
                            detected.Body.GameObject.mouseDown = false;
                            detected.Body.GameObject.OnMouseUp();
                        }
                    }
                }
            }
        }

        #endregion 

        #region mouse methods

        /// <summary>
        /// Check if the param button is being pressed
        /// </summary>
        /// <param name="button">The button you want to test</param>
        /// <returns></returns>
        public static bool IsMouseKeyPressed(MouseEventButton button)
        {
            if (button == MouseEventButton.LeftButton)
            {
                return mouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Released;
            }
            else if (button == MouseEventButton.RightButton)
            {
                return mouseState.RightButton == ButtonState.Pressed && lastMouseState.RightButton == ButtonState.Released;
            }
            else if (button == MouseEventButton.MiddleButton)
            {
                return mouseState.MiddleButton == ButtonState.Pressed && lastMouseState.MiddleButton == ButtonState.Released;
            }

            return false;
        }

        /// <summary>
        ///  Check if the param button is down
        /// </summary>
        /// <param name="button">The button you want to test</param>
        /// <returns></returns>
        public static bool IsMouseKeyDown(MouseEventButton button)
        {
            if (button == MouseEventButton.LeftButton)
            {
                return mouseState.LeftButton == ButtonState.Pressed;
            }
            else if (button == MouseEventButton.RightButton)
            {
                return mouseState.RightButton == ButtonState.Pressed;
            }
            else if (button == MouseEventButton.MiddleButton)
            {
                return mouseState.MiddleButton == ButtonState.Pressed;
            }

            return false;
        }

        /// <summary>
        ///  Check if the param button is up
        /// </summary>
        /// <param name="button">The button you want to test</param>
        /// <returns></returns>
        public static bool IsMouseKeyUp(MouseEventButton button)
        {
            if (button == MouseEventButton.LeftButton)
            {
                return mouseState.LeftButton == ButtonState.Released;
            }
            else if (button == MouseEventButton.RightButton)
            {
                return mouseState.RightButton == ButtonState.Released;
            }
            else if (button == MouseEventButton.MiddleButton)
            {
                return mouseState.MiddleButton == ButtonState.Released;
            }

            return false;
        }

        #endregion

        #region keyboard methods

        /// <summary>
        /// Check if the param key is being pressed
        /// </summary>
        /// <param name="Key">The key you want to test</param>
        /// <returns></returns>
        public static bool IsKeyPressed(Keys Key)
        {
            return keyboardState.IsKeyDown(Key) && lastKeyboardState.IsKeyUp(Key);
        }

        /// <summary>
        /// Check if the param key is down
        /// </summary>
        /// <param name="key">The key you want to test</param>
        /// <returns></returns>
        static public bool IsKeyDown(Keys key)
        {
            return keyboardState.IsKeyDown(key);
        }

        /// <summary>
        /// Check if the param key is up
        /// </summary>
        /// <param name="key">The key you want to test</param>
        /// <returns></returns>
        static public bool IsKeyUp(Keys key)
        {
            return keyboardState.IsKeyUp(key);
        }

        #endregion
    }
}
