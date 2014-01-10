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
using System;
using System.Collections.Generic;

namespace FarseerPhysics.Common.Decomposition.CDT.Util
{
    internal class PointGenerator
    {
        private static readonly Random RNG = new Random();

        public static List<TriangulationPoint> UniformDistribution(int n, double scale)
        {
            List<TriangulationPoint> points = new List<TriangulationPoint>();
            for (int i = 0; i < n; i++)
            {
                points.Add(new TriangulationPoint(scale*(0.5 - RNG.NextDouble()), scale*(0.5 - RNG.NextDouble())));
            }
            return points;
        }

        public static List<TriangulationPoint> UniformGrid(int n, double scale)
        {
            double x = 0;
            double size = scale/n;
            double halfScale = 0.5*scale;

            List<TriangulationPoint> points = new List<TriangulationPoint>();
            for (int i = 0; i < n + 1; i++)
            {
                x = halfScale - i*size;
                for (int j = 0; j < n + 1; j++)
                {
                    points.Add(new TriangulationPoint(x, halfScale - j*size));
                }
            }
            return points;
        }
    }
}