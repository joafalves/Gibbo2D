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
namespace FarseerPhysics.Common.Decomposition.CDT.Delaunay.Sweep
{
    internal class DTSweepConstraint : TriangulationConstraint
    {
        /// <summary>
        /// Give two points in any order. Will always be ordered so
        /// that q.y > p.y and q.x > p.x if same y value 
        /// </summary>
        public DTSweepConstraint(TriangulationPoint p1, TriangulationPoint p2)
        {
            P = p1;
            Q = p2;
            if (p1.Y > p2.Y)
            {
                Q = p1;
                P = p2;
            }
            else if (p1.Y == p2.Y)
            {
                if (p1.X > p2.X)
                {
                    Q = p1;
                    P = p2;
                }
                else if (p1.X == p2.X)
                {
                    //                logger.info( "Failed to create constraint {}={}", p1, p2 );
                    //                throw new DuplicatePointException( p1 + "=" + p2 );
                    //                return;
                }
            }
            Q.AddEdge(this);
        }
    }
}