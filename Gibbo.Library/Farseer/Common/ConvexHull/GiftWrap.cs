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
using Microsoft.Xna.Framework;

namespace FarseerPhysics.Common.ConvexHull
{
    /// <summary>
    /// Giftwrap convex hull algorithm.
    /// O(nh) time complexity, where n is the number of points and h is the number of points on the convex hull.
    /// 
    /// See http://en.wikipedia.org/wiki/Gift_wrapping_algorithm for more details.
    /// </summary>
    public static class GiftWrap
    {
        //Extracted from Box2D

        /// <summary>
        /// Returns the convex hull from the given vertices.
        /// </summary>
        /// <param name="vertices">The vertices.</param>
        public static Vertices GetConvexHull(Vertices vertices)
        {
            if (vertices.Count <= 3)
                return vertices;

            // Find the right most point on the hull
            int i0 = 0;
            float x0 = vertices[0].X;
            for (int i = 1; i < vertices.Count; ++i)
            {
                float x = vertices[i].X;
                if (x > x0 || (x == x0 && vertices[i].Y < vertices[i0].Y))
                {
                    i0 = i;
                    x0 = x;
                }
            }

            int[] hull = new int[vertices.Count];
            int m = 0;
            int ih = i0;

            for (; ; )
            {
                hull[m] = ih;

                int ie = 0;
                for (int j = 1; j < vertices.Count; ++j)
                {
                    if (ie == ih)
                    {
                        ie = j;
                        continue;
                    }

                    Vector2 r = vertices[ie] - vertices[hull[m]];
                    Vector2 v = vertices[j] - vertices[hull[m]];
                    float c = MathUtils.Cross(ref r, ref v);
                    if (c < 0.0f)
                    {
                        ie = j;
                    }

                    // Collinearity check
                    if (c == 0.0f && v.LengthSquared() > r.LengthSquared())
                    {
                        ie = j;
                    }
                }

                ++m;
                ih = ie;

                if (ie == i0)
                {
                    break;
                }
            }

            Vertices result = new Vertices(m);

            // Copy vertices.
            for (int i = 0; i < m; ++i)
            {
                result.Add(vertices[hull[i]]);
            }
            return result;
        }
    }
}