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
using System.Diagnostics;
using FarseerPhysics.Common.Decomposition.Seidel;
using Microsoft.Xna.Framework;
using Point = FarseerPhysics.Common.Decomposition.Seidel.Point;

namespace FarseerPhysics.Common.Decomposition
{
    /// <summary>
    /// Convex decomposition algorithm created by Raimund Seidel
    /// 
    /// Properties:
    /// - Decompose the polygon into trapezoids, then triangulate.
    /// - To use the trapezoid data, use ConvexPartitionTrapezoid()
    /// - Generate a lot of garbage due to incapsulation of the Poly2Tri library.
    /// - Running time is O(n log n), n = number of vertices.
    /// - Running time is almost linear for most simple polygons.
    /// - Does not care about winding order. 
    /// 
    /// For more information, see Raimund Seidel's paper "A simple and fast incremental randomized
    /// algorithm for computing trapezoidal decompositions and for triangulating polygons"
    /// 
    /// See also: "Computational Geometry", 3rd edition, by Mark de Berg et al, Chapter 6.2
    ///           "Computational Geometry in C", 2nd edition, by Joseph O'Rourke
    /// 
    /// Original code from the Poly2Tri project by Mason Green.
    /// http://code.google.com/p/poly2tri/source/browse?repo=archive#hg/scala/src/org/poly2tri/seidel
    /// 
    /// This implementation is from Dec 14, 2010
    /// </summary>
    internal static class SeidelDecomposer
    {
        /// <summary>
        /// Decompose the polygon into several smaller non-concave polygons.
        /// </summary>
        /// <param name="vertices">The polygon to decompose.</param>
        /// <param name="sheer">The sheer to use if you get bad results, try using a higher value.</param>
        /// <returns>A list of triangles</returns>
        public static List<Vertices> ConvexPartition(Vertices vertices, float sheer = 0.001f)
        {
            Debug.Assert(vertices.Count > 3);

            List<Point> compatList = new List<Point>(vertices.Count);

            foreach (Vector2 vertex in vertices)
            {
                compatList.Add(new Point(vertex.X, vertex.Y));
            }

            Triangulator t = new Triangulator(compatList, sheer);

            List<Vertices> list = new List<Vertices>();

            foreach (List<Point> triangle in t.Triangles)
            {
                Vertices outTriangles = new Vertices(triangle.Count);

                foreach (Point outTriangle in triangle)
                {
                    outTriangles.Add(new Vector2(outTriangle.X, outTriangle.Y));
                }

                list.Add(outTriangles);
            }

            return list;
        }

        /// <summary>
        /// Decompose the polygon into several smaller non-concave polygons.
        /// </summary>
        /// <param name="vertices">The polygon to decompose.</param>
        /// <param name="sheer">The sheer to use if you get bad results, try using a higher value.</param>
        /// <returns>A list of trapezoids</returns>
        public static List<Vertices> ConvexPartitionTrapezoid(Vertices vertices, float sheer = 0.001f)
        {
            List<Point> compatList = new List<Point>(vertices.Count);

            foreach (Vector2 vertex in vertices)
            {
                compatList.Add(new Point(vertex.X, vertex.Y));
            }

            Triangulator t = new Triangulator(compatList, sheer);

            List<Vertices> list = new List<Vertices>();

            foreach (Trapezoid trapezoid in t.Trapezoids)
            {
                Vertices verts = new Vertices();

                List<Point> points = trapezoid.GetVertices();
                foreach (Point point in points)
                {
                    verts.Add(new Vector2(point.X, point.Y));
                }

                list.Add(verts);
            }

            return list;
        }
    }
}