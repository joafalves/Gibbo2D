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
using Microsoft.Xna.Framework;

namespace Gibbo.Library
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Rotates around an angle
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static Vector2 Rotate(this Vector2 vector, float value)
        {
            var sin = (float)Math.Sin(value);
            var cos = (float)Math.Cos(value);
            return new Vector2(vector.X * cos - vector.Y * sin, vector.X * sin + vector.Y * cos);
        }

        ///// <summary>
        ///// Rotates around an origin
        ///// </summary>
        ///// <param name="point"></param>
        ///// <param name="origin"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        public static Vector2 Rotate(this Vector2 vector, Vector2 origin, float value)
        {
            var nx = vector.X - origin.X;
            var ny = vector.Y - origin.Y;
            var vec = new Vector2(nx, ny);
            var sin = (float)Math.Sin(value);
            var cos = (float)Math.Cos(value);
            return new Vector2(vec.X * cos - vec.Y * sin, vec.X * sin + vec.Y * cos);
        }
    }
}
