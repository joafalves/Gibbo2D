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
using System.Diagnostics;
using FarseerPhysics.Common;
using Microsoft.Xna.Framework;

namespace FarseerPhysics.Collision.Shapes
{
    /// <summary>
    /// A circle shape.
    /// </summary>
    public class CircleShape : Shape
    {
        internal Vector2 _position;

        /// <summary>
        /// Create a new circle with the desired radius and density.
        /// </summary>
        /// <param name="radius">The radius of the circle.</param>
        /// <param name="density">The density of the circle.</param>
        public CircleShape(float radius, float density)
            : base(density)
        {
            Debug.Assert(radius >= 0);
            Debug.Assert(density >= 0);

            ShapeType = ShapeType.Circle;
            _position = Vector2.Zero;
            Radius = radius; // The Radius property cache 2radius and calls ComputeProperties(). So no need to call ComputeProperties() here.
        }

        internal CircleShape()
            : base(0)
        {
            ShapeType = ShapeType.Circle;
            _radius = 0.0f;
            _position = Vector2.Zero;
        }

        public override int ChildCount
        {
            get { return 1; }
        }

        /// <summary>
        /// Get or set the position of the circle
        /// </summary>
        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;
                ComputeProperties(); //TODO: Optimize here
            }
        }

        public override bool TestPoint(ref Transform transform, ref Vector2 point)
        {
            Vector2 center = transform.p + MathUtils.Mul(transform.q, Position);
            Vector2 d = point - center;
            return Vector2.Dot(d, d) <= _2radius;
        }

        public override bool RayCast(out RayCastOutput output, ref RayCastInput input, ref Transform transform, int childIndex)
        {
            // Collision Detection in Interactive 3D Environments by Gino van den Bergen
            // From Section 3.1.2
            // x = s + a * r
            // norm(x) = radius

            output = new RayCastOutput();

            Vector2 position = transform.p + MathUtils.Mul(transform.q, Position);
            Vector2 s = input.Point1 - position;
            float b = Vector2.Dot(s, s) - _2radius;

            // Solve quadratic equation.
            Vector2 r = input.Point2 - input.Point1;
            float c = Vector2.Dot(s, r);
            float rr = Vector2.Dot(r, r);
            float sigma = c * c - rr * b;

            // Check for negative discriminant and short segment.
            if (sigma < 0.0f || rr < Settings.Epsilon)
            {
                return false;
            }

            // Find the point of intersection of the line with the circle.
            float a = -(c + (float)Math.Sqrt(sigma));

            // Is the intersection point on the segment?
            if (0.0f <= a && a <= input.MaxFraction * rr)
            {
                a /= rr;
                output.Fraction = a;

                //TODO: Check results here
                output.Normal = s + a * r;
                output.Normal.Normalize();
                return true;
            }

            return false;
        }

        public override void ComputeAABB(out AABB aabb, ref Transform transform, int childIndex)
        {
            Vector2 p = transform.p + MathUtils.Mul(transform.q, Position);
            aabb.LowerBound = new Vector2(p.X - Radius, p.Y - Radius);
            aabb.UpperBound = new Vector2(p.X + Radius, p.Y + Radius);
        }

        protected override sealed void ComputeProperties()
        {
            float area = Settings.Pi * _2radius;
            MassData.Area = area;
            MassData.Mass = Density * area;
            MassData.Centroid = Position;

            // inertia about the local origin
            MassData.Inertia = MassData.Mass * (0.5f * _2radius + Vector2.Dot(Position, Position));
        }

        public override float ComputeSubmergedArea(ref Vector2 normal, float offset, ref Transform xf, out Vector2 sc)
        {
            sc = Vector2.Zero;

            Vector2 p = MathUtils.Mul(ref xf, Position);
            float l = -(Vector2.Dot(normal, p) - offset);
            if (l < -Radius + Settings.Epsilon)
            {
                //Completely dry
                return 0;
            }
            if (l > Radius)
            {
                //Completely wet
                sc = p;
                return Settings.Pi * _2radius;
            }

            //Magic
            float l2 = l * l;
            float area = _2radius * (float)((Math.Asin(l / Radius) + Settings.Pi / 2) + l * Math.Sqrt(_2radius - l2));
            float com = -2.0f / 3.0f * (float)Math.Pow(_2radius - l2, 1.5f) / area;

            sc.X = p.X + normal.X * com;
            sc.Y = p.Y + normal.Y * com;

            return area;
        }

        /// <summary>
        /// Compare the circle to another circle
        /// </summary>
        /// <param name="shape">The other circle</param>
        /// <returns>True if the two circles are the same size and have the same position</returns>
        public bool CompareTo(CircleShape shape)
        {
            return (Radius == shape.Radius && Position == shape.Position);
        }

        public override Shape Clone()
        {
            CircleShape clone = new CircleShape();
            clone.ShapeType = ShapeType;
            clone._radius = Radius;
            clone._2radius = _2radius; //FPE note: We also copy the cache
            clone._density = _density;
            clone._position = _position;
            clone.MassData = MassData;
            return clone;
        }
    }
}