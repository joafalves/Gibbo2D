using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Gibbo.Library
{
    /// <summary>
    /// This class provides some math extension methods to make some calculations easy.
    /// </summary>
    public static class MathExtension
    {
        /// <summary>
        /// Calculate basizer point based on a cubic line (4 control points)
        /// </summary>
        /// <param name="t">Current Point</param>
        /// <param name="p0">Initial Position</param>
        /// <param name="p1">Control Point 1</param>
        /// <param name="p2">Control Point 2</param>
        /// <param name="p3">End Position</param>
        /// <returns></returns>
        public static Vector2 CalculateBezierPoint(float t, Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            float uuu = uu * u;
            float ttt = tt * t;

            Vector2 p = uuu * p0; //first term
            p += 3 * uu * t * p1; //second term
            p += 3 * u * tt * p2; //third term
            p += ttt * p3; //fourth term

            return p;
        }

        /// <summary>
        /// Checks if the input is power of two
        /// </summary>
        /// <param name="value">Input value</param>
        /// <returns>The test result</returns>
        public static bool IsPowerOfTwo(Vector2 value)
        {
            return (IsPowerOfTwo((uint)value.X) && IsPowerOfTwo((uint)value.Y));
        }

        /// <summary>
        /// Checks if the input is power of two
        /// </summary>
        /// <param name="value">Input value</param>
        /// <returns>The test result</returns>
        public static bool IsPowerOfTwo(uint value)
        {
            return (value != 0) && ((value & (value - 1)) == 0);
        }

        /// <summary>
        /// Checks if the input is power of two
        /// </summary>
        /// <param name="value">Input value</param>
        /// <returns>The test result</returns>
        public static bool IsPowerOfTwo(int value)
        {
            return (value != 0) && ((value & (value - 1)) == 0);
        }

        /// <summary>
        /// Converts a radian value to the built in unit angle
        /// </summary>
        /// <param name="radians">The input value in radians</param>
        /// <returns>Unit Angle</returns>
        public static float RadiansToRotate(float radians)
        {
            return MathHelper.WrapAngle(radians);
        }

        /// <summary>
        /// Converts a degree value to the built in unit angle
        /// </summary>
        /// <param name="degrees">he input value in degrees</param>
        /// <returns>Unit Angle</returns>
        public static float DegreesToRotate(float degrees)
        {
            return MathHelper.WrapAngle(MathHelper.ToRadians(degrees));
        }

        /// <summary>
        /// Lerps a color
        /// </summary>
        /// <param name="FinalColor">The final color</param>
        /// <param name="InitialColor">The initial color</param>
        /// <param name="percentage">The percentage amount</param>
        /// <returns>The calculated color</returns>
        public static Color LerpColor(Color FinalColor, Color InitialColor, float percentage)
        {
            return new Color(
                (byte)MathHelper.Lerp(FinalColor.R, InitialColor.R, percentage),
                (byte)MathHelper.Lerp(FinalColor.G, InitialColor.G, percentage),
                (byte)MathHelper.Lerp(FinalColor.B, InitialColor.B, percentage),
                (byte)MathHelper.Lerp(FinalColor.A, InitialColor.A, percentage));
        }

        /// <summary>
        /// Angle lerp.
        /// Useful for smooth point rotations.
        /// </summary>
        /// <param name="value1">The current value</param>
        /// <param name="value2">The final value</param>
        /// <param name="lerp">The amount</param>
        /// <returns>The calculated angle</returns>
        public static float AngleLerp(float value1, float value2, float lerp)
        {
            float c, d;

            if (value2 < value1)
            {
                c = value2 + MathHelper.TwoPi;
                //c > nowrap > wraps
                d = c - value1 > value1 - value2
                    ? MathHelper.Lerp(value1, value2, lerp)
                    : MathHelper.Lerp(value1, c, lerp);

            }
            else if (value2 > value1)
            {
                c = value2 - MathHelper.TwoPi;
                //wraps > nowrap > c
                d = value2 - value1 > value1 - c
                    ? MathHelper.Lerp(value1, c, lerp)
                    : MathHelper.Lerp(value1, value2, lerp);

            }
            else { return value1; } //Same angle already

            return MathHelper.WrapAngle(d);
        }

        /// <summary>
        /// Angle lerp.
        /// Useful for smooth point rotations.
        /// </summary>
        /// <param name="value1">The current value</param>
        /// <param name="value2">The final value</param>
        /// <param name="lerp">The amount</param>
        /// <param name="error">The maximum error allowed</param>
        /// <returns>The calculated angle</returns>
        public static float AngleLerp(float value1, float value2, float lerp, float error)
        {
            float c, d;

            if (value1 - value2 > error)
            {
                c = value2 + MathHelper.TwoPi;
                //c > nowrap > wraps
                d = c - value1 > value1 - value2
                    ? MathHelper.Lerp(value1, value2, lerp)
                    : MathHelper.Lerp(value1, c, lerp);

            }
            else if (value2 - value1 > error) 
            {
                c = value2 - MathHelper.TwoPi;
                //wraps > nowrap > c
                d = value2 - value1 > value1 - c
                    ? MathHelper.Lerp(value1, c, lerp)
                    : MathHelper.Lerp(value1, value2, lerp);

            }
            else { return value1; } //Same angle already

            return MathHelper.WrapAngle(d);
        }

        /// <summary>
        /// Rounds a Vector2
        /// </summary>
        /// <param name="v">The input value</param>
        /// <returns>The calculated Vector2</returns>
        public static Vector2 Round(this Vector2 v)
        {
            return new Vector2((float)Math.Round(v.X), (float)Math.Round(v.Y));
        }

        /// <summary>
        /// Converts a Vector2 to a Point
        /// </summary>
        /// <param name="v">The input value</param>
        /// <returns>The calculated Point</returns>
        public static Point ToPoint(this Vector2 v)
        {
            return new Point((int)Math.Round(v.X), (int)Math.Round(v.Y));
        }

        /// <summary>
        /// Converts a Point to a Vector2
        /// </summary>
        /// <param name="p">The input value</param>
        /// <returns>The calculated Vector2</returns>
        public static Vector2 ToVector2(this Point p)
        {
            return new Vector2(p.X, p.Y);
        }

        /// <summary>
        /// Calculates the distance value between 2 Vector2's.
        /// </summary>
        /// <param name="v0">First Vector2</param>
        /// <param name="v">Second Vector2</param>
        /// <returns>The calculated distance</returns>
        public static float DistanceTo(this Vector2 v0, Vector2 v)
        {
            return (v - v0).Length();
        }

        /// <summary>
        /// Calculates the distance to line segments between 3 Vector2's
        /// </summary>
        /// <param name="v">First Vector2</param>
        /// <param name="a">Second Vector2</param>
        /// <param name="b">Third Vector2</param>
        /// <returns>The calculated distance</returns>
        public static float DistanceToLineSegment(this Vector2 v, Vector2 a, Vector2 b)
        {
            Vector2 x = b - a;
            x.Normalize();
            float t = Vector2.Dot(x, v - a);
            if (t < 0) return (a - v).Length();
            float d = (b - a).Length();
            if (t > d) return (b - v).Length();
            return (a + x * t - v).Length();
        }

        /// <summary>
        /// Transforms a rectangle with the given transformation matrix.
        /// </summary>
        /// <param name="r">The input rectangle</param>
        /// <param name="m">The input matrix</param>
        /// <returns>The calculated rectangle</returns>
        public static Rectangle Transform(this Rectangle r, Matrix m)
        {
            Vector2[] poly = new Vector2[2];
            poly[0] = new Vector2(r.Left, r.Top);
            poly[1] = new Vector2(r.Right, r.Bottom);
            Vector2[] newpoly = new Vector2[2];
            Vector2.Transform(poly, ref m, newpoly);

            Rectangle result = new Rectangle();
            result.Location = newpoly[0].ToPoint();
            result.Width = (int)(newpoly[1].X - newpoly[0].X);
            result.Height = (int)(newpoly[1].Y - newpoly[0].Y);
            return result;
        }

        /// <summary>
        /// Convert the Rectangle to an array of Vector2 containing its 4 corners.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="m"></param>
        /// <returns>An array of Vector2 representing the rectangle's corners starting from top/left and going clockwise.</returns>
        public static Vector2[] ToPolygon(this Rectangle r)
        {
            Vector2[] poly = new Vector2[4];
            poly[0] = new Vector2(r.Left, r.Top);
            poly[1] = new Vector2(r.Right, r.Top);
            poly[2] = new Vector2(r.Right, r.Bottom);
            poly[3] = new Vector2(r.Left, r.Bottom);
            return poly;
        }

        /// <summary>
        /// Calculates a Rectangle from to given Vector2's
        /// </summary>
        /// <param name="v1">First Vector2</param>
        /// <param name="v2">Second Vector2</param>
        /// <returns>The calculated rectangle</returns>
        public static Rectangle RectangleFromVectors(Vector2 v1, Vector2 v2)
        {
            Vector2 distance = v2 - v1;
            Rectangle result = new Rectangle();
            result.X = (int)Math.Min(v1.X, v2.X);
            result.Y = (int)Math.Min(v1.Y, v2.Y);
            result.Width = (int)Math.Abs(distance.X);
            result.Height = (int)Math.Abs(distance.Y);

            return result;
        }

        /// <summary>
        /// Linear Interpolation between two bytes
        /// </summary>
        /// <param name="a">First Byte</param>
        /// <param name="b">Second Byte</param>
        /// <param name="t">The amount</param>
        /// <returns></returns>
        public static byte LinearInterpolate(byte a, byte b, double t)
        {
            return (byte)(a * (1 - t) + b * t);
        }

        public static float LinearInterpolate(float a, float b, double t)
        {
            return (float)(a * (1 - t) + b * t);
        }

        public static Vector2 LinearInterpolate(Vector2 a, Vector2 b, double t)
        {
            return new Vector2(LinearInterpolate(a.X, b.X, t), LinearInterpolate(a.Y, b.Y, t));
        }

        public static Vector4 LinearInterpolate(Vector4 a, Vector4 b, double t)
        {
            return new Vector4(LinearInterpolate(a.X, b.X, t), LinearInterpolate(a.Y, b.Y, t), LinearInterpolate(a.Z, b.Z, t), LinearInterpolate(a.W, b.W, t));
        }

        public static Color LinearInterpolate(Color a, Color b, double t)
        {
            return new Color(LinearInterpolate(a.R, b.R, t), LinearInterpolate(a.G, b.G, t), LinearInterpolate(a.B, b.B, t), LinearInterpolate(a.A, b.A, t));
        }
    }
}
