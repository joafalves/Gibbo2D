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


#if WINDOWS

#endif

using System.Runtime.Serialization;
using Microsoft.Xna.Framework;
using System.ComponentModel;
using Microsoft.Xna.Framework.Graphics;

namespace Gibbo.Library
{
    /// <summary>
    /// Path Object
    /// </summary>
#if WINDOWS
    [Serializable]
#endif
    [DataContract(IsReference = true)]
    [KnownType(typeof(Path))]
    public class Path : GameObject
    {
        #region fields

        [DataMember]
        private List<Vector2> points = new List<Vector2>();       

        #endregion

        #region properties

        /// <summary>
        /// The points of the path
        /// </summary>
#if WINDOWS
        [Category("Path Properties")]
        [DisplayName("Points"), Description("The points of the path")]
#endif
        public List<Vector2> Points
        {
            get { return points; }
            set { points = value; }
        }

        #endregion

        #region constructors

        #endregion

        #region methods

        /// <summary>
        /// Draws this instance
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, null, null, null, null, SceneManager.ActiveCamera.TransformMatrix);

            for (int i = points.Count - 1; i >= 0; i--)
            {
                if (i != points.Count - 1)
                    Primitives.DrawLine(spriteBatch, points[i + 1], points[i], Color.Red, 4);

                Primitives.DrawBoxFilled(spriteBatch, new Rectangle((int)points[i].X - 8, (int)points[i].Y - 8, 16, 16), Color.Red);
            }

            spriteBatch.End();
        }

        #endregion
    }
}
