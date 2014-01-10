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
using Microsoft.Xna.Framework;

namespace FarseerPhysics.Dynamics
{
    /// <summary>
    /// This is an internal structure.
    /// </summary>
    public struct TimeStep
    {
        /// <summary>
        /// Time step (Delta time)
        /// </summary>
        public float dt;

        /// <summary>
        /// dt * inv_dt0
        /// </summary>
        public float dtRatio;

        /// <summary>
        /// Inverse time step (0 if dt == 0).
        /// </summary>
        public float inv_dt;
    }

    /// This is an internal structure.
    public struct Position
    {
        public Vector2 c;
        public float a;
    }

    /// This is an internal structure.
    public struct Velocity
    {
        public Vector2 v;
        public float w;
    }

    /// Solver Data
    public struct SolverData
    {
        public TimeStep step;
        public Position[] positions;
        public Velocity[] velocities;
    }
}