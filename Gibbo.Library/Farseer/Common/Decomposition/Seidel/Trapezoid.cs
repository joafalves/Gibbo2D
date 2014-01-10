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
    internal class Trapezoid
    {
        public Edge Bottom;
        public bool Inside;
        public Point LeftPoint;

        // Neighbor pointers
        public Trapezoid LowerLeft;
        public Trapezoid LowerRight;

        public Point RightPoint;
        public Sink Sink;

        public Edge Top;
        public Trapezoid UpperLeft;
        public Trapezoid UpperRight;

        public Trapezoid(Point leftPoint, Point rightPoint, Edge top, Edge bottom)
        {
            LeftPoint = leftPoint;
            RightPoint = rightPoint;
            Top = top;
            Bottom = bottom;
            UpperLeft = null;
            UpperRight = null;
            LowerLeft = null;
            LowerRight = null;
            Inside = true;
            Sink = null;
        }

        // Update neighbors to the left
        public void UpdateLeft(Trapezoid ul, Trapezoid ll)
        {
            UpperLeft = ul;
            if (ul != null) ul.UpperRight = this;
            LowerLeft = ll;
            if (ll != null) ll.LowerRight = this;
        }

        // Update neighbors to the right
        public void UpdateRight(Trapezoid ur, Trapezoid lr)
        {
            UpperRight = ur;
            if (ur != null) ur.UpperLeft = this;
            LowerRight = lr;
            if (lr != null) lr.LowerLeft = this;
        }

        // Update neighbors on both sides
        public void UpdateLeftRight(Trapezoid ul, Trapezoid ll, Trapezoid ur, Trapezoid lr)
        {
            UpperLeft = ul;
            if (ul != null) ul.UpperRight = this;
            LowerLeft = ll;
            if (ll != null) ll.LowerRight = this;
            UpperRight = ur;
            if (ur != null) ur.UpperLeft = this;
            LowerRight = lr;
            if (lr != null) lr.LowerLeft = this;
        }

        // Recursively trim outside neighbors
        public void TrimNeighbors()
        {
            if (Inside)
            {
                Inside = false;
                if (UpperLeft != null) UpperLeft.TrimNeighbors();
                if (LowerLeft != null) LowerLeft.TrimNeighbors();
                if (UpperRight != null) UpperRight.TrimNeighbors();
                if (LowerRight != null) LowerRight.TrimNeighbors();
            }
        }

        // Determines if this point lies inside the trapezoid
        public bool Contains(Point point)
        {
            return (point.X > LeftPoint.X && point.X < RightPoint.X && Top.IsAbove(point) && Bottom.IsBelow(point));
        }

        public List<Point> GetVertices()
        {
            List<Point> verts = new List<Point>(4);
            verts.Add(LineIntersect(Top, LeftPoint.X));
            verts.Add(LineIntersect(Bottom, LeftPoint.X));
            verts.Add(LineIntersect(Bottom, RightPoint.X));
            verts.Add(LineIntersect(Top, RightPoint.X));
            return verts;
        }

        private Point LineIntersect(Edge edge, float x)
        {
            float y = edge.Slope * x + edge.B;
            return new Point(x, y);
        }

        // Add points to monotone mountain
        public void AddPoints()
        {
            if (LeftPoint != Bottom.P)
            {
                Bottom.AddMpoint(LeftPoint);
            }
            if (RightPoint != Bottom.Q)
            {
                Bottom.AddMpoint(RightPoint);
            }
            if (LeftPoint != Top.P)
            {
                Top.AddMpoint(LeftPoint);
            }
            if (RightPoint != Top.Q)
            {
                Top.AddMpoint(RightPoint);
            }
        }
    }
}