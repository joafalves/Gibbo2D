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
using FarseerPhysics.Common.Decomposition.CDT.Polygon;

namespace FarseerPhysics.Common.Decomposition.CDT.Util
{
    internal class PolygonGenerator
    {
        private static readonly Random RNG = new Random();

        private static double PI_2 = 2.0*Math.PI;

        public static Polygon.Polygon RandomCircleSweep(double scale, int vertexCount)
        {
            PolygonPoint point;
            PolygonPoint[] points;
            double radius = scale/4;

            points = new PolygonPoint[vertexCount];
            for (int i = 0; i < vertexCount; i++)
            {
                do
                {
                    if (i%250 == 0)
                    {
                        radius += scale/2*(0.5 - RNG.NextDouble());
                    }
                    else if (i%50 == 0)
                    {
                        radius += scale/5*(0.5 - RNG.NextDouble());
                    }
                    else
                    {
                        radius += 25*scale/vertexCount*(0.5 - RNG.NextDouble());
                    }
                    radius = radius > scale/2 ? scale/2 : radius;
                    radius = radius < scale/10 ? scale/10 : radius;
                } while (radius < scale/10 || radius > scale/2);
                point = new PolygonPoint(radius*Math.Cos((PI_2*i)/vertexCount),
                                         radius*Math.Sin((PI_2*i)/vertexCount));
                points[i] = point;
            }
            return new Polygon.Polygon(points);
        }

        public static Polygon.Polygon RandomCircleSweep2(double scale, int vertexCount)
        {
            PolygonPoint point;
            PolygonPoint[] points;
            double radius = scale/4;

            points = new PolygonPoint[vertexCount];
            for (int i = 0; i < vertexCount; i++)
            {
                do
                {
                    radius += scale/5*(0.5 - RNG.NextDouble());
                    radius = radius > scale/2 ? scale/2 : radius;
                    radius = radius < scale/10 ? scale/10 : radius;
                } while (radius < scale/10 || radius > scale/2);
                point = new PolygonPoint(radius*Math.Cos((PI_2*i)/vertexCount),
                                         radius*Math.Sin((PI_2*i)/vertexCount));
                points[i] = point;
            }
            return new Polygon.Polygon(points);
        }
    }
}