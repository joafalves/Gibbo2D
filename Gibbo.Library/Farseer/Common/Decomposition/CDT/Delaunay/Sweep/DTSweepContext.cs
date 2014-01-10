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
    /**
     * 
     * @author Thomas Åhlén, thahlen@gmail.com
     *
     */

    internal class DTSweepContext : TriangulationContext
    {
        // Inital triangle factor, seed triangle will extend 30% of 
        // PointSet width to both left and right.
        private const float ALPHA = 0.3f;

        public DTSweepBasin Basin = new DTSweepBasin();
        public DTSweepEdgeEvent EdgeEvent = new DTSweepEdgeEvent();

        private DTSweepPointComparator _comparator = new DTSweepPointComparator();
        public AdvancingFront aFront;

        public DTSweepContext()
        {
            Clear();
        }

        public TriangulationPoint Head { get; set; }
        public TriangulationPoint Tail { get; set; }

        public void RemoveFromList(DelaunayTriangle triangle)
        {
            Triangles.Remove(triangle);
            // TODO: remove all neighbor pointers to this triangle
            //        for( int i=0; i<3; i++ )
            //        {
            //            if( triangle.neighbors[i] != null )
            //            {
            //                triangle.neighbors[i].clearNeighbor( triangle );
            //            }
            //        }
            //        triangle.clearNeighbors();
        }

        public void MeshClean(DelaunayTriangle triangle)
        {
            MeshCleanReq(triangle);
        }

        private void MeshCleanReq(DelaunayTriangle triangle)
        {
            if (triangle != null && !triangle.IsInterior)
            {
                triangle.IsInterior = true;
                Triangulatable.AddTriangle(triangle);
                for (int i = 0; i < 3; i++)
                {
                    if (!triangle.EdgeIsConstrained[i])
                    {
                        MeshCleanReq(triangle.Neighbors[i]);
                    }
                }
            }
        }

        public override void Clear()
        {
            base.Clear();
            Triangles.Clear();
        }

        public void AddNode(AdvancingFrontNode node)
        {
            //        Console.WriteLine( "add:" + node.key + ":" + System.identityHashCode(node.key));
            //        m_nodeTree.put( node.getKey(), node );
            aFront.AddNode(node);
        }

        public void RemoveNode(AdvancingFrontNode node)
        {
            //        Console.WriteLine( "remove:" + node.key + ":" + System.identityHashCode(node.key));
            //        m_nodeTree.delete( node.getKey() );
            aFront.RemoveNode(node);
        }

        public AdvancingFrontNode LocateNode(TriangulationPoint point)
        {
            return aFront.LocateNode(point);
        }

        public void CreateAdvancingFront()
        {
            AdvancingFrontNode head, tail, middle;
            // Initial triangle
            DelaunayTriangle iTriangle = new DelaunayTriangle(Points[0], Tail, Head);
            Triangles.Add(iTriangle);

            head = new AdvancingFrontNode(iTriangle.Points[1]);
            head.Triangle = iTriangle;
            middle = new AdvancingFrontNode(iTriangle.Points[0]);
            middle.Triangle = iTriangle;
            tail = new AdvancingFrontNode(iTriangle.Points[2]);

            aFront = new AdvancingFront(head, tail);
            aFront.AddNode(middle);

            // TODO: I think it would be more intuitive if head is middles next and not previous
            //       so swap head and tail
            aFront.Head.Next = middle;
            middle.Next = aFront.Tail;
            middle.Prev = aFront.Head;
            aFront.Tail.Prev = middle;
        }

        /// <summary>
        /// Try to map a node to all sides of this triangle that don't have 
        /// a neighbor.
        /// </summary>
        public void MapTriangleToNodes(DelaunayTriangle t)
        {
            AdvancingFrontNode n;
            for (int i = 0; i < 3; i++)
            {
                if (t.Neighbors[i] == null)
                {
                    n = aFront.LocatePoint(t.PointCW(t.Points[i]));
                    if (n != null)
                    {
                        n.Triangle = t;
                    }
                }
            }
        }

        public override void PrepareTriangulation(Triangulatable t)
        {
            base.PrepareTriangulation(t);

            double xmax, xmin;
            double ymax, ymin;

            xmax = xmin = Points[0].X;
            ymax = ymin = Points[0].Y;

            // Calculate bounds. Should be combined with the sorting
            foreach (TriangulationPoint p in Points)
            {
                if (p.X > xmax)
                    xmax = p.X;
                if (p.X < xmin)
                    xmin = p.X;
                if (p.Y > ymax)
                    ymax = p.Y;
                if (p.Y < ymin)
                    ymin = p.Y;
            }

            double deltaX = ALPHA*(xmax - xmin);
            double deltaY = ALPHA*(ymax - ymin);
            TriangulationPoint p1 = new TriangulationPoint(xmax + deltaX, ymin - deltaY);
            TriangulationPoint p2 = new TriangulationPoint(xmin - deltaX, ymin - deltaY);

            Head = p1;
            Tail = p2;

            //        long time = System.nanoTime();
            // Sort the points along y-axis
            Points.Sort(_comparator);
            //        logger.info( "Triangulation setup [{}ms]", ( System.nanoTime() - time ) / 1e6 );
        }


        public void FinalizeTriangulation()
        {
            Triangulatable.AddTriangles(Triangles);
            Triangles.Clear();
        }

        public override TriangulationConstraint NewConstraint(TriangulationPoint a, TriangulationPoint b)
        {
            return new DTSweepConstraint(a, b);
        }

        #region Nested type: DTSweepBasin

        public class DTSweepBasin
        {
            public AdvancingFrontNode bottomNode;
            public bool leftHighest;
            public AdvancingFrontNode leftNode;
            public AdvancingFrontNode rightNode;
            public double width;
        }

        #endregion

        #region Nested type: DTSweepEdgeEvent

        public class DTSweepEdgeEvent
        {
            public DTSweepConstraint ConstrainedEdge;
            public bool Right;
        }

        #endregion
    }
}