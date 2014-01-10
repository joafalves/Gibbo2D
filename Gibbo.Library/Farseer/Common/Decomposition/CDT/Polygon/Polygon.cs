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
//   Polygon constructors sprused up, checks for 3+ polys
//   Naming of everything
//   getTriangulationMode() -> TriangulationMode { get; }
//   Exceptions replaced
// Future possibilities
//   We have a lot of Add/Clear methods -- we may prefer to just expose the container
//   Some self-explanitory methods may deserve commenting anyways

using System;
using System.Collections.Generic;
using System.Linq;
using FarseerPhysics.Common.Decomposition.CDT.Delaunay;

namespace FarseerPhysics.Common.Decomposition.CDT.Polygon
{
    internal class Polygon : Triangulatable
    {
        protected List<Polygon> _holes;
        protected PolygonPoint _last;
        protected List<TriangulationPoint> _points = new List<TriangulationPoint>();
        protected List<TriangulationPoint> _steinerPoints;
        protected List<DelaunayTriangle> _triangles;

        /// <summary>
        /// Create a polygon from a list of at least 3 points with no duplicates.
        /// </summary>
        /// <param name="points">A list of unique points</param>
        public Polygon(IList<PolygonPoint> points)
        {
            if (points.Count < 3) throw new ArgumentException("List has fewer than 3 points", "points");

            // Lets do one sanity check that first and last point hasn't got same position
            // Its something that often happen when importing polygon data from other formats
            if (points[0].Equals(points[points.Count - 1])) points.RemoveAt(points.Count - 1);

            _points.AddRange(points.Cast<TriangulationPoint>());
        }

        /// <summary>
        /// Create a polygon from a list of at least 3 points with no duplicates.
        /// </summary>
        /// <param name="points">A list of unique points.</param>
        public Polygon(IEnumerable<PolygonPoint> points) : this((points as IList<PolygonPoint>) ?? points.ToArray())
        {
        }

        public Polygon()
        {
        }

        public IList<Polygon> Holes
        {
            get { return _holes; }
        }

        #region Triangulatable Members

        public TriangulationMode TriangulationMode
        {
            get { return TriangulationMode.Polygon; }
        }

        public IList<TriangulationPoint> Points
        {
            get { return _points; }
        }

        public IList<DelaunayTriangle> Triangles
        {
            get { return _triangles; }
        }

        public void AddTriangle(DelaunayTriangle t)
        {
            _triangles.Add(t);
        }

        public void AddTriangles(IEnumerable<DelaunayTriangle> list)
        {
            _triangles.AddRange(list);
        }

        public void ClearTriangles()
        {
            if (_triangles != null) _triangles.Clear();
        }

        /// <summary>
        /// Creates constraints and populates the context with points
        /// </summary>
        /// <param name="tcx">The context</param>
        public void PrepareTriangulation(TriangulationContext tcx)
        {
            if (_triangles == null)
            {
                _triangles = new List<DelaunayTriangle>(_points.Count);
            }
            else
            {
                _triangles.Clear();
            }

            // Outer constraints
            for (int i = 0; i < _points.Count - 1; i++)
            {
                tcx.NewConstraint(_points[i], _points[i + 1]);
            }
            tcx.NewConstraint(_points[0], _points[_points.Count - 1]);
            tcx.Points.AddRange(_points);

            // Hole constraints
            if (_holes != null)
            {
                foreach (Polygon p in _holes)
                {
                    for (int i = 0; i < p._points.Count - 1; i++)
                    {
                        tcx.NewConstraint(p._points[i], p._points[i + 1]);
                    }
                    tcx.NewConstraint(p._points[0], p._points[p._points.Count - 1]);
                    tcx.Points.AddRange(p._points);
                }
            }

            if (_steinerPoints != null)
            {
                tcx.Points.AddRange(_steinerPoints);
            }
        }

        #endregion

        public void AddSteinerPoint(TriangulationPoint point)
        {
            if (_steinerPoints == null)
            {
                _steinerPoints = new List<TriangulationPoint>();
            }
            _steinerPoints.Add(point);
        }

        public void AddSteinerPoints(List<TriangulationPoint> points)
        {
            if (_steinerPoints == null)
            {
                _steinerPoints = new List<TriangulationPoint>();
            }
            _steinerPoints.AddRange(points);
        }

        public void ClearSteinerPoints()
        {
            if (_steinerPoints != null)
            {
                _steinerPoints.Clear();
            }
        }

        /// <summary>
        /// Add a hole to the polygon.
        /// </summary>
        /// <param name="poly">A subtraction polygon fully contained inside this polygon.</param>
        public void AddHole(Polygon poly)
        {
            if (_holes == null) _holes = new List<Polygon>();
            _holes.Add(poly);
            // XXX: tests could be made here to be sure it is fully inside
            //        addSubtraction( poly.getPoints() );
        }

        /// <summary>
        /// Inserts newPoint after point.
        /// </summary>
        /// <param name="point">The point to insert after in the polygon</param>
        /// <param name="newPoint">The point to insert into the polygon</param>
        public void InsertPointAfter(PolygonPoint point, PolygonPoint newPoint)
        {
            // Validate that 
            int index = _points.IndexOf(point);
            if (index == -1)
                throw new ArgumentException(
                    "Tried to insert a point into a Polygon after a point not belonging to the Polygon", "point");
            newPoint.Next = point.Next;
            newPoint.Previous = point;
            point.Next.Previous = newPoint;
            point.Next = newPoint;
            _points.Insert(index + 1, newPoint);
        }

        /// <summary>
        /// Inserts list (after last point in polygon?)
        /// </summary>
        /// <param name="list"></param>
        public void AddPoints(IEnumerable<PolygonPoint> list)
        {
            PolygonPoint first;
            foreach (PolygonPoint p in list)
            {
                p.Previous = _last;
                if (_last != null)
                {
                    p.Next = _last.Next;
                    _last.Next = p;
                }
                _last = p;
                _points.Add(p);
            }
            first = (PolygonPoint) _points[0];
            _last.Next = first;
            first.Previous = _last;
        }

        /// <summary>
        /// Adds a point after the last in the polygon.
        /// </summary>
        /// <param name="p">The point to add</param>
        public void AddPoint(PolygonPoint p)
        {
            p.Previous = _last;
            p.Next = _last.Next;
            _last.Next = p;
            _points.Add(p);
        }

        /// <summary>
        /// Removes a point from the polygon.
        /// </summary>
        /// <param name="p"></param>
        public void RemovePoint(PolygonPoint p)
        {
            PolygonPoint next, prev;

            next = p.Next;
            prev = p.Previous;
            prev.Next = next;
            next.Previous = prev;
            _points.Remove(p);
        }
    }
}