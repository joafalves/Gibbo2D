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
