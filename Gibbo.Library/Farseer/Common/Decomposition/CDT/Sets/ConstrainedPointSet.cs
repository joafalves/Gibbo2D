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

namespace FarseerPhysics.Common.Decomposition.CDT.Sets
{
    /*
     * Extends the PointSet by adding some Constraints on how it will be triangulated<br>
     * A constraint defines an edge between two points in the set, these edges can not
     * be crossed. They will be enforced triangle edges after a triangulation.
     * <p>
     * 
     * 
     * @author Thomas Åhlén, thahlen@gmail.com
     */

    internal class ConstrainedPointSet : PointSet
    {
        private List<TriangulationPoint> _constrainedPointList;

        public ConstrainedPointSet(List<TriangulationPoint> points, int[] index)
            : base(points)
        {
            EdgeIndex = index;
        }

        /**
         * 
         * @param points - A list of all points in PointSet
         * @param constraints - Pairs of two points defining a constraint, all points <b>must</b> be part of given PointSet!
         */

        public ConstrainedPointSet(List<TriangulationPoint> points, IEnumerable<TriangulationPoint> constraints)
            : base(points)
        {
            _constrainedPointList = new List<TriangulationPoint>();
            _constrainedPointList.AddRange(constraints);
        }

        public int[] EdgeIndex { get; private set; }

        public override TriangulationMode TriangulationMode
        {
            get { return TriangulationMode.Constrained; }
        }

        public override void PrepareTriangulation(TriangulationContext tcx)
        {
            base.PrepareTriangulation(tcx);
            if (_constrainedPointList != null)
            {
                TriangulationPoint p1, p2;
                List<TriangulationPoint>.Enumerator iterator = _constrainedPointList.GetEnumerator();
                while (iterator.MoveNext())
                {
                    p1 = iterator.Current;
                    iterator.MoveNext();
                    p2 = iterator.Current;
                    tcx.NewConstraint(p1, p2);
                }
            }
            else
            {
                for (int i = 0; i < EdgeIndex.Length; i += 2)
                {
                    // XXX: must change!!
                    tcx.NewConstraint(Points[EdgeIndex[i]], Points[EdgeIndex[i + 1]]);
                }
            }
        }

        /**
         * TODO: TO BE IMPLEMENTED!
         * Peforms a validation on given input<br>
         * 1. Check's if there any constraint edges are crossing or collinear<br>
         * 2. 
         * @return
         */

        public bool isValid()
        {
            return true;
        }
    }
}