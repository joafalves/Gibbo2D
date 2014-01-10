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
//   Replaced getPolygons with attribute
// Future possibilities
//   Replace Add(Polygon) with exposed container?
//   Replace entire class with HashSet<Polygon> ?

using System.Collections.Generic;

namespace FarseerPhysics.Common.Decomposition.CDT.Polygon
{
    internal class PolygonSet
    {
        protected List<Polygon> _polygons = new List<Polygon>();

        public PolygonSet()
        {
        }

        public PolygonSet(Polygon poly)
        {
            _polygons.Add(poly);
        }

        public IEnumerable<Polygon> Polygons
        {
            get { return _polygons; }
        }

        public void Add(Polygon p)
        {
            _polygons.Add(p);
        }
    }
}