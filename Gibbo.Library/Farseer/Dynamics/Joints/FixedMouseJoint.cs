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
using System.Diagnostics;
using FarseerPhysics.Common;
using Microsoft.Xna.Framework;

namespace FarseerPhysics.Dynamics.Joints
{
    // p = attached point, m = mouse point
    // C = p - m
    // Cdot = v
    //      = v + cross(w, r)
    // J = [I r_skew]
    // Identity used:
    // w k % (rx i + ry j) = w * (-ry i + rx j)

    /// <summary>
    /// A mouse joint is used to make a point on a body track a
    /// specified world point. This a soft constraint with a maximum
    /// force. This allows the constraint to stretch and without
    /// applying huge forces.
    /// NOTE: this joint is not documented in the manual because it was
    /// developed to be used in the testbed. If you want to learn how to
    /// use the mouse joint, look at the testbed.
    /// </summary>
    public class FixedMouseJoint : Joint
    {
        private Vector2 _target;
        private float _frequency;
        private float _dampingRatio;
        private float _beta;

        // Solver shared
        private Vector2 _impulse;
        private float _maxForce;
        private float _gamma;

        // Solver temp
        private int _indexA;
        private Vector2 _rA;
        private Vector2 _localCenterA;
        private float _invMassA;
        private float _invIA;
        private Mat22 _mass;
        private Vector2 _C;

        /// <summary>
        /// This requires a world target point,
        /// tuning parameters, and the time step.
        /// </summary>
        /// <param name="body">The body.</param>
        /// <param name="anchor">The target.</param>
        public FixedMouseJoint(Body body, Vector2 anchor)
            : base(body)
        {
            JointType = JointType.FixedMouse;
            Frequency = 5.0f;
            DampingRatio = 0.7f;
            MaxForce = 1000 * body.Mass;

            Debug.Assert(anchor.IsValid());

            _target = anchor;
            LocalAnchorA = MathUtils.MulT(BodyA._xf, anchor);
        }

        /// <summary>
        /// The local anchor point on BodyA
        /// </summary>
        public Vector2 LocalAnchorA { get; set; }

        public override Vector2 WorldAnchorA
        {
            get { return BodyA.GetWorldPoint(LocalAnchorA); }
            set { LocalAnchorA = BodyA.GetLocalPoint(value); }
        }

        public override Vector2 WorldAnchorB
        {
            get { return _target; }
            set
            {
                WakeBodies();
                _target = value;
            }
        }

        /// <summary>
        /// The maximum constraint force that can be exerted
        /// to move the candidate body. Usually you will express
        /// as some multiple of the weight (multiplier * mass * gravity).
        /// </summary>
        public float MaxForce
        {
            get { return _maxForce; }
            set
            {
                Debug.Assert(MathUtils.IsValid(value) && value >= 0.0f);
                _maxForce = value;
            }
        }

        /// <summary>
        /// The response speed.
        /// </summary>
        public float Frequency
        {
            get { return _frequency; }
            set
            {
                Debug.Assert(MathUtils.IsValid(value) && value >= 0.0f);
                _frequency = value;
            }
        }

        /// <summary>
        /// The damping ratio. 0 = no damping, 1 = critical damping.
        /// </summary>
        public float DampingRatio
        {
            get { return _dampingRatio; }
            set
            {
                Debug.Assert(MathUtils.IsValid(value) && value >= 0.0f);
                _dampingRatio = value;
            }
        }

        public override Vector2 GetReactionForce(float invDt)
        {
            return invDt * _impulse;
        }

        public override float GetReactionTorque(float invDt)
        {
            return invDt * 0.0f;
        }

        internal override void InitVelocityConstraints(ref SolverData data)
        {
            _indexA = BodyA.IslandIndex;
            _localCenterA = BodyA._sweep.LocalCenter;
            _invMassA = BodyA._invMass;
            _invIA = BodyA._invI;

            Vector2 cA = data.positions[_indexA].c;
            float aA = data.positions[_indexA].a;
            Vector2 vA = data.velocities[_indexA].v;
            float wA = data.velocities[_indexA].w;

            Rot qA = new Rot(aA);

            float mass = BodyA.Mass;

            // Frequency
            float omega = 2.0f * Settings.Pi * Frequency;

            // Damping coefficient
            float d = 2.0f * mass * DampingRatio * omega;

            // Spring stiffness
            float k = mass * (omega * omega);

            // magic formulas
            // gamma has units of inverse mass.
            // beta has units of inverse time.
            float h = data.step.dt;
            Debug.Assert(d + h * k > Settings.Epsilon);
            _gamma = h * (d + h * k);
            if (_gamma != 0.0f)
            {
                _gamma = 1.0f / _gamma;
            }

            _beta = h * k * _gamma;

            // Compute the effective mass matrix.
            _rA = MathUtils.Mul(qA, LocalAnchorA - _localCenterA);
            // K    = [(1/m1 + 1/m2) * eye(2) - skew(r1) * invI1 * skew(r1) - skew(r2) * invI2 * skew(r2)]
            //      = [1/m1+1/m2     0    ] + invI1 * [r1.Y*r1.Y -r1.X*r1.Y] + invI2 * [r1.Y*r1.Y -r1.X*r1.Y]
            //        [    0     1/m1+1/m2]           [-r1.X*r1.Y r1.X*r1.X]           [-r1.X*r1.Y r1.X*r1.X]
            Mat22 K = new Mat22();
            K.ex.X = _invMassA + _invIA * _rA.Y * _rA.Y + _gamma;
            K.ex.Y = -_invIA * _rA.X * _rA.Y;
            K.ey.X = K.ex.Y;
            K.ey.Y = _invMassA + _invIA * _rA.X * _rA.X + _gamma;

            _mass = K.Inverse;

            _C = cA + _rA - _target;
            _C *= _beta;

            // Cheat with some damping
            wA *= 0.98f;

            if (Settings.EnableWarmstarting)
            {
                _impulse *= data.step.dtRatio;
                vA += _invMassA * _impulse;
                wA += _invIA * MathUtils.Cross(_rA, _impulse);
            }
            else
            {
                _impulse = Vector2.Zero;
            }

            data.velocities[_indexA].v = vA;
            data.velocities[_indexA].w = wA;
        }

        internal override void SolveVelocityConstraints(ref SolverData data)
        {
            Vector2 vA = data.velocities[_indexA].v;
            float wA = data.velocities[_indexA].w;

            // Cdot = v + cross(w, r)
            Vector2 Cdot = vA + MathUtils.Cross(wA, _rA);
            Vector2 impulse = MathUtils.Mul(ref _mass, -(Cdot + _C + _gamma * _impulse));

            Vector2 oldImpulse = _impulse;
            _impulse += impulse;
            float maxImpulse = data.step.dt * MaxForce;
            if (_impulse.LengthSquared() > maxImpulse * maxImpulse)
            {
                _impulse *= maxImpulse / _impulse.Length();
            }
            impulse = _impulse - oldImpulse;

            vA += _invMassA * impulse;
            wA += _invIA * MathUtils.Cross(_rA, impulse);

            data.velocities[_indexA].v = vA;
            data.velocities[_indexA].w = wA;
        }

        internal override bool SolvePositionConstraints(ref SolverData data)
        {
            return true;
        }
    }
}