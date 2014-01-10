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
    // Node for a Directed Acyclic graph (DAG)
    internal abstract class Node
    {
        protected Node LeftChild;
        public List<Node> ParentList;
        protected Node RightChild;

        protected Node(Node left, Node right)
        {
            ParentList = new List<Node>();
            LeftChild = left;
            RightChild = right;

            if (left != null)
                left.ParentList.Add(this);
            if (right != null)
                right.ParentList.Add(this);
        }

        public abstract Sink Locate(Edge s);

        // Replace a node in the graph with this node
        // Make sure parent pointers are updated
        public void Replace(Node node)
        {
            foreach (Node parent in node.ParentList)
            {
                // Select the correct node to replace (left or right child)
                if (parent.LeftChild == node)
                    parent.LeftChild = this;
                else
                    parent.RightChild = this;
            }
            ParentList.AddRange(node.ParentList);
        }
    }
}
