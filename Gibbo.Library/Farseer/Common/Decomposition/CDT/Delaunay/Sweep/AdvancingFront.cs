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

// Changes from the Java version
//   Removed BST code, but not all artifacts of it
// Future possibilities
//   Eliminate Add/RemoveNode ?
//   Comments comments and more comments!

using System;
using System.Text;

namespace FarseerPhysics.Common.Decomposition.CDT.Delaunay.Sweep
{
    /**
     * @author Thomas Åhlen (thahlen@gmail.com)
     */

    internal class AdvancingFront
    {
        public AdvancingFrontNode Head;
        protected AdvancingFrontNode Search;
        public AdvancingFrontNode Tail;

        public AdvancingFront(AdvancingFrontNode head, AdvancingFrontNode tail)
        {
            Head = head;
            Tail = tail;
            Search = head;
            AddNode(head);
            AddNode(tail);
        }

        public void AddNode(AdvancingFrontNode node)
        {
            //_searchTree.put(node.key, node);
        }

        public void RemoveNode(AdvancingFrontNode node)
        {
            //_searchTree.delete( node.key );
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            AdvancingFrontNode node = Head;
            while (node != Tail)
            {
                sb.Append(node.Point.X).Append("->");
                node = node.Next;
            }
            sb.Append(Tail.Point.X);
            return sb.ToString();
        }

        /// <summary>
        /// MM:  This seems to be used by LocateNode to guess a position in the implicit linked list of AdvancingFrontNodes near x
        ///      Removed an overload that depended on this being exact
        /// </summary>
        private AdvancingFrontNode FindSearchNode(double x)
        {
            // TODO: implement BST index 
            return Search;
        }

        /// <summary>
        /// We use a balancing tree to locate a node smaller or equal to given key value
        /// </summary>
        public AdvancingFrontNode LocateNode(TriangulationPoint point)
        {
            return LocateNode(point.X);
        }

        private AdvancingFrontNode LocateNode(double x)
        {
            AdvancingFrontNode node = FindSearchNode(x);
            if (x < node.Value)
            {
                while ((node = node.Prev) != null)
                    if (x >= node.Value)
                    {
                        Search = node;
                        return node;
                    }
            }
            else
            {
                while ((node = node.Next) != null)
                    if (x < node.Value)
                    {
                        Search = node.Prev;
                        return node.Prev;
                    }
            }
            return null;
        }

        /// <summary>
        /// This implementation will use simple node traversal algorithm to find a point on the front
        /// </summary>
        public AdvancingFrontNode LocatePoint(TriangulationPoint point)
        {
            double px = point.X;
            AdvancingFrontNode node = FindSearchNode(px);
            double nx = node.Point.X;

            if (px == nx)
            {
                if (point != node.Point)
                {
                    // We might have two nodes with same x value for a short time
                    if (point == node.Prev.Point)
                    {
                        node = node.Prev;
                    }
                    else if (point == node.Next.Point)
                    {
                        node = node.Next;
                    }
                    else
                    {
                        throw new Exception("Failed to find Node for given afront point");
                        //node = null;
                    }
                }
            }
            else if (px < nx)
            {
                while ((node = node.Prev) != null)
                {
                    if (point == node.Point)
                    {
                        break;
                    }
                }
            }
            else
            {
                while ((node = node.Next) != null)
                {
                    if (point == node.Point)
                    {
                        break;
                    }
                }
            }
            Search = node;
            return node;
        }
    }
}