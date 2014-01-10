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
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Gibbo.Library
{
    /// <summary>
    /// Class responsable to draw 2D primitives
    /// </summary>
    public static class Primitives
    {
        #region fields

        private static Texture2D pixel;

        // TODO: implement other primitives (circle, ..)

        #endregion

        #region properties



        #endregion

        #region constructors

        /// <summary>
        /// The dafault constructor
        /// </summary>
        static Primitives()
        {
            pixel = new Texture2D(SceneManager.GraphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White });
        }

        #endregion

        #region methods

        /// <summary>
        /// Draw a pixel at the input location
        /// </summary>
        /// <param name="sb">The spritebatch</param>
        /// <param name="x">Position X</param>
        /// <param name="y">Position Y</param>
        /// <param name="c">Color</param>
        public static void DrawPixel(SpriteBatch sb, int x, int y, Color c)
        {
            sb.Draw(pixel, new Vector2(x, y), c);
        }

        /// <summary>
        /// Draw a box at the input location
        /// </summary>
        /// <param name="sb">The spritebatch</param>
        /// <param name="r">Destination rectangle</param>
        /// <param name="c">Color</param>
        /// <param name="linewidth">Border Width</param>
        public static void DrawBox(SpriteBatch sb, Rectangle r, Color c, int linewidth)
        {
            DrawLine(sb, r.Left, r.Top, r.Right, r.Top, c, linewidth);
            DrawLine(sb, r.Right, r.Y, r.Right, r.Bottom, c, linewidth);
            DrawLine(sb, r.Right, r.Bottom, r.Left, r.Bottom, c, linewidth);
            DrawLine(sb, r.Left, r.Bottom, r.Left, r.Top, c, linewidth);
        }

        /// <summary>
        /// Draw a box at the input location
        /// </summary>
        /// <param name="sb">The spritebatch</param>
        /// <param name="upperLeft">Upper left position</param>
        /// <param name="lowerRight">Lower right position</param>
        /// <param name="c">Color</param>
        public static void DrawBox(SpriteBatch sb, Vector2 upperLeft, Vector2 lowerRight, Color c, int linewidth)
        {
            Rectangle r = MathExtension.RectangleFromVectors(upperLeft, lowerRight);
            DrawLine(sb, r.Left, r.Top, r.Right, r.Top, c, linewidth);
            DrawLine(sb, r.Right, r.Y, r.Right, r.Bottom, c, linewidth);
            DrawLine(sb, r.Right, r.Bottom, r.Left, r.Bottom, c, linewidth);
            DrawLine(sb, r.Left, r.Bottom, r.Left, r.Top, c, linewidth);
        }

        /// <summary>
        /// Draw a filled box at the input location
        /// </summary>
        /// <param name="sb">The spritebatch</param>
        /// <param name="x">Position X</param>
        /// <param name="y">Position Y</param>
        /// <param name="w">Width</param>
        /// <param name="h">Height</param>
        /// <param name="c">Color</param>
        public static void DrawBoxFilled(SpriteBatch sb, float x, float y, float w, float h, Color c)
        {
            sb.Draw(pixel, new Rectangle((int)x, (int)y, (int)w, (int)h), c);
        }

        /// <summary>
        /// Draw a filled box at the input location
        /// </summary>
        /// <param name="sb">The spritebatch</param>
        /// <param name="upperLeft">Upper left position</param>
        /// <param name="lowerRight">Lower right position</param>
        /// <param name="c">Color</param>
        public static void DrawBoxFilled(SpriteBatch sb, Vector2 upperLeft, Vector2 lowerRight, Color c)
        {
            Rectangle r = MathExtension.RectangleFromVectors(upperLeft, lowerRight);
            sb.Draw(pixel, r, c);
        }

        /// <summary>
        /// Draw a filled box at the input location
        /// </summary>
        /// <param name="sb">The spritebatch</param>
        /// <param name="r">Destination rectangle</param>
        /// <param name="c">Color</param>
        public static void DrawBoxFilled(SpriteBatch sb, Rectangle r, Color c)
        {
            sb.Draw(pixel, r, c);
        }

        /// <summary>
        /// Draw a line at the input location
        /// </summary>
        /// <param name="sb">The spritebatch</param>
        /// <param name="x1">Position X1</param>
        /// <param name="y1">Position Y1</param>
        /// <param name="x2">Position X2</param>
        /// <param name="y2">Position Y2</param>
        /// <param name="c">Color</param>
        /// <param name="linewidth">Line Width</param>
        public static void DrawLine(SpriteBatch sb, float x1, float y1, float x2, float y2, Color c, int linewidth)
        {
            Vector2 v = new Vector2(x2 - x1, y2 - y1);
            float rot = (float)Math.Atan2(y2 - y1, x2 - x1);
            sb.Draw(pixel, new Vector2(x1, y1), new Rectangle(1, 1, 1, linewidth), c, rot,
                new Vector2(0, linewidth / 2), new Vector2(v.Length(), 1), SpriteEffects.None, 0);
        }

        /// <summary>
        /// Draw a line at the input location
        /// </summary>
        /// <param name="sb">The spritebatch</param>
        /// <param name="startpos">Start position</param>
        /// <param name="endpos">End position</param>
        /// <param name="c">Color</param>
        /// <param name="linewidth">Line Width</param>
        public static void DrawLine(SpriteBatch sb, Vector2 startpos, Vector2 endpos, Color c, int linewidth)
        {
            DrawLine(sb, startpos.X, startpos.Y, endpos.X, endpos.Y, c, linewidth);
        }

        #endregion
    }
}
