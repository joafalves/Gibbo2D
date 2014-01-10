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
    // Directed Acyclic graph (DAG)
    // See "Computational Geometry", 3rd edition, by Mark de Berg et al, Chapter 6.2
    internal class QueryGraph
    {
        private Node _head;

        public QueryGraph(Node head)
        {
            _head = head;
        }

        private Trapezoid Locate(Edge edge)
        {
            return _head.Locate(edge).Trapezoid;
        }

        public List<Trapezoid> FollowEdge(Edge edge)
        {
            List<Trapezoid> trapezoids = new List<Trapezoid>();
            trapezoids.Add(Locate(edge));
            int j = 0;

            while (edge.Q.X > trapezoids[j].RightPoint.X)
            {
                if (edge.IsAbove(trapezoids[j].RightPoint))
                {
                    trapezoids.Add(trapezoids[j].UpperRight);
                }
                else
                {
                    trapezoids.Add(trapezoids[j].LowerRight);
                }
                j += 1;
            }
            return trapezoids;
        }

        private void Replace(Sink sink, Node node)
        {
            if (sink.ParentList.Count == 0)
                _head = node;
            else
                node.Replace(sink);
        }

        public void Case1(Sink sink, Edge edge, Trapezoid[] tList)
        {
            YNode yNode = new YNode(edge, Sink.Isink(tList[1]), Sink.Isink(tList[2]));
            XNode qNode = new XNode(edge.Q, yNode, Sink.Isink(tList[3]));
            XNode pNode = new XNode(edge.P, Sink.Isink(tList[0]), qNode);
            Replace(sink, pNode);
        }

        public void Case2(Sink sink, Edge edge, Trapezoid[] tList)
        {
            YNode yNode = new YNode(edge, Sink.Isink(tList[1]), Sink.Isink(tList[2]));
            XNode pNode = new XNode(edge.P, Sink.Isink(tList[0]), yNode);
            Replace(sink, pNode);
        }

        public void Case3(Sink sink, Edge edge, Trapezoid[] tList)
        {
            YNode yNode = new YNode(edge, Sink.Isink(tList[0]), Sink.Isink(tList[1]));
            Replace(sink, yNode);
        }

        public void Case4(Sink sink, Edge edge, Trapezoid[] tList)
        {
            YNode yNode = new YNode(edge, Sink.Isink(tList[0]), Sink.Isink(tList[1]));
            XNode qNode = new XNode(edge.Q, yNode, Sink.Isink(tList[2]));
            Replace(sink, qNode);
        }
    }
}
