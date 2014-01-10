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
using FarseerPhysics.Common.Decomposition.CDT.Delaunay.Sweep;

namespace FarseerPhysics.Common.Decomposition.CDT
{
    internal class TriangulationPoint
    {
        // List of edges this point constitutes an upper ending point (CDT)

        public double X, Y;

        public TriangulationPoint(double x, double y)
        {
            X = x;
            Y = y;
        }

        public List<DTSweepConstraint> Edges { get; private set; }

        public float Xf
        {
            get { return (float) X; }
            set { X = value; }
        }

        public float Yf
        {
            get { return (float) Y; }
            set { Y = value; }
        }

        public bool HasEdges
        {
            get { return Edges != null; }
        }

        public override string ToString()
        {
            return "[" + X + "," + Y + "]";
        }

        public void AddEdge(DTSweepConstraint e)
        {
            if (Edges == null)
            {
                Edges = new List<DTSweepConstraint>();
            }
            Edges.Add(e);
        }
    }
}