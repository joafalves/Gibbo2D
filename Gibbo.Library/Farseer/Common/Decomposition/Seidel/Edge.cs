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

namespace FarseerPhysics.Common.Decomposition.Seidel
{
    internal class Edge
    {
        // Pointers used for building trapezoidal map
        public Trapezoid Above;
        public float B;
        public Trapezoid Below;

        // Montone mountain points
        public HashSet<Point> MPoints;
        public Point P;
        public Point Q;

        // Slope of the line (m)
        public float Slope;


        public Edge(Point p, Point q)
        {
            P = p;
            Q = q;

            if (q.X - p.X != 0)
                Slope = (q.Y - p.Y) / (q.X - p.X);
            else
                Slope = 0;

            B = p.Y - (p.X * Slope);
            Above = null;
            Below = null;
            MPoints = new HashSet<Point>();
            MPoints.Add(p);
            MPoints.Add(q);
        }

        public bool IsAbove(Point point)
        {
            return P.Orient2D(Q, point) < 0;
        }

        public bool IsBelow(Point point)
        {
            return P.Orient2D(Q, point) > 0;
        }

        public void AddMpoint(Point point)
        {
            foreach (Point mp in MPoints)
            {
                if (!mp.Neq(point))
                    return;
            }

            MPoints.Add(point);
        }
    }
}