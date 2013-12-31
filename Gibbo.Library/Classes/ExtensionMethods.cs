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
