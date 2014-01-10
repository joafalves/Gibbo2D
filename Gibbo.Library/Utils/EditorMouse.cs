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
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Gibbo.Library
{
    public static class EditorMouse
    {
        private static int x, y;
        private static ButtonState leftMouseBtn, middleMouseBtn, rightMouseBtn;

        public static int Y
        {
            get { return y; }
            set { y = value; }
        }

        public static int X
        {
            get { return x; }
            set { x = value; }
        }

        public static ButtonState RightMouseBtn
        {
            get { return rightMouseBtn; }
            set { rightMouseBtn = value; }
        }

        public static ButtonState MiddleMouseBtn
        {
            get { return middleMouseBtn; }
            set { middleMouseBtn = value; }
        }

        public static ButtonState LeftMouseBtn
        {
            get { return leftMouseBtn; }
            set { leftMouseBtn = value; }
        }

        public static void UpdateState(int x, int y, bool leftMouseBtnPressed, bool middleMouseBtnPressed, bool rightMouseBtnPressed)
        {
            Vector2 worldPos = Vector2.Transform(new Vector2(x, y), Matrix.Invert(SceneManager.ActiveCamera.TransformMatrix));
            X = (int)worldPos.X;
            Y = (int)worldPos.Y;

            if (leftMouseBtnPressed)
            {
                leftMouseBtn = ButtonState.Pressed;
            }
            else
            {
                middleMouseBtn = ButtonState.Released;
                rightMouseBtn = ButtonState.Released;
            }

            if (middleMouseBtnPressed)
            {
                middleMouseBtn = ButtonState.Pressed;
            }
            else
            {
                leftMouseBtn = ButtonState.Released;
                rightMouseBtn = ButtonState.Released;
            }

            if (rightMouseBtnPressed)
            {
                rightMouseBtn = ButtonState.Pressed;
            }
            else
            {
                leftMouseBtn = ButtonState.Released;
                middleMouseBtn = ButtonState.Released;
            }
        }

        public static MouseState GetState()
        {
            return new MouseState(x, y, 0, leftMouseBtn, middleMouseBtn, rightMouseBtn, ButtonState.Released, ButtonState.Released);
        }
    }
}
