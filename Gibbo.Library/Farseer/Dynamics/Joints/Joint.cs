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
using Microsoft.Xna.Framework;

namespace FarseerPhysics.Dynamics.Joints
{
    public enum JointType
    {
        Unknown,
        Revolute,
        Prismatic,
        Distance,
        Pulley,
        //Mouse, <- We have fixed mouse
        Gear,
        Wheel,
        Weld,
        Friction,
        Rope,
        Motor,

        //FPE note: From here on and down, it is only FPE joints
        Angle,
        FixedMouse,
        FixedRevolute,
        FixedDistance,
        FixedLine,
        FixedPrismatic,
        FixedAngle,
        FixedFriction,
    }

    public enum LimitState
    {
        Inactive,
        AtLower,
        AtUpper,
        Equal,
    }

    /// <summary>
    /// A joint edge is used to connect bodies and joints together
    /// in a joint graph where each body is a node and each joint
    /// is an edge. A joint edge belongs to a doubly linked list
    /// maintained in each attached body. Each joint has two joint
    /// nodes, one for each attached body.
    /// </summary>
    public sealed class JointEdge
    {
        /// <summary>
        /// The joint.
        /// </summary>
        public Joint Joint;

        /// <summary>
        /// The next joint edge in the body's joint list.
        /// </summary>
        public JointEdge Next;

        /// <summary>
        /// Provides quick access to the other body attached.
        /// </summary>
        public Body Other;

        /// <summary>
        /// The previous joint edge in the body's joint list.
        /// </summary>
        public JointEdge Prev;
    }

    public abstract class Joint
    {
        private float _breakpoint;
        private double _breakpointSquared;

        /// <summary>
        /// Indicate if this join is enabled or not. Disabling a joint
        /// means it is still in the simulation, but inactive.
        /// </summary>
        public bool Enabled = true;

        internal JointEdge EdgeA = new JointEdge();
        internal JointEdge EdgeB = new JointEdge();
        internal bool IslandFlag;

        protected Joint()
        {
            Breakpoint = float.MaxValue;

            //Connected bodies should not collide by default
            CollideConnected = false;
        }

        protected Joint(Body bodyA, Body bodyB) : this()
        {
            //Can't connect a joint to the same body twice.
            Debug.Assert(bodyA != bodyB);

            BodyA = bodyA;
            BodyB = bodyB;
        }

        /// <summary>
        /// Constructor for fixed joint
        /// </summary>
        protected Joint(Body body) : this()
        {
            BodyA = body;
        }

        /// <summary>
        /// Gets or sets the type of the joint.
        /// </summary>
        /// <value>The type of the joint.</value>
        public JointType JointType { get; protected set; }

        /// <summary>
        /// Get the first body attached to this joint.
        /// </summary>
        public Body BodyA { get; internal set; }

        /// <summary>
        /// Get the second body attached to this joint.
        /// </summary>
        public Body BodyB { get; internal set; }

        /// <summary>
        /// Get the anchor point on bodyA in world coordinates.
        /// On some joints, this value indicate the anchor point within the world.
        /// </summary>
        public abstract Vector2 WorldAnchorA { get; set; }

        /// <summary>
        /// Get the anchor point on bodyB in world coordinates.
        /// On some joints, this value indicate the anchor point within the world.
        /// </summary>
        public abstract Vector2 WorldAnchorB { get; set; }

        /// <summary>
        /// Set the user data pointer.
        /// </summary>
        /// <value>The data.</value>
        public object UserData { get; set; }

        /// <summary>
        /// Set this flag to true if the attached bodies should collide.
        /// </summary>
        public bool CollideConnected { get; set; }

        /// <summary>
        /// The Breakpoint simply indicates the maximum Value the JointError can be before it breaks.
        /// The default value is float.MaxValue, which means it never breaks.
        /// </summary>
        public float Breakpoint
        {
            get { return _breakpoint; }
            set
            {
                _breakpoint = value;
                _breakpointSquared = _breakpoint * _breakpoint;
            }
        }

        /// <summary>
        /// Fires when the joint is broken.
        /// </summary>
        public event Action<Joint, float> Broke;

        /// <summary>
        /// Get the reaction force on body at the joint anchor in Newtons.
        /// </summary>
        /// <param name="invDt">The inverse delta time.</param>
        public abstract Vector2 GetReactionForce(float invDt);

        /// <summary>
        /// Get the reaction torque on the body at the joint anchor in N*m.
        /// </summary>
        /// <param name="invDt">The inverse delta time.</param>
        public abstract float GetReactionTorque(float invDt);

        protected void WakeBodies()
        {
            if (BodyA != null)
                BodyA.Awake = true;

            if (BodyB != null)
                BodyB.Awake = true;
        }

        /// <summary>
        /// Return true if the joint is a fixed type.
        /// </summary>
        public bool IsFixedType()
        {
            return JointType == JointType.FixedRevolute ||
                   JointType == JointType.FixedDistance ||
                   JointType == JointType.FixedPrismatic ||
                   JointType == JointType.FixedLine ||
                   JointType == JointType.FixedMouse ||
                   JointType == JointType.FixedAngle ||
                   JointType == JointType.FixedFriction;
        }

        internal abstract void InitVelocityConstraints(ref SolverData data);

        internal void Validate(float invDt)
        {
            if (!Enabled)
                return;

            float jointErrorSquared = GetReactionForce(invDt).LengthSquared();

            if (Math.Abs(jointErrorSquared) <= _breakpointSquared)
                return;

            Enabled = false;

            if (Broke != null)
                Broke(this, (float)Math.Sqrt(jointErrorSquared));
        }

        internal abstract void SolveVelocityConstraints(ref SolverData data);

        /// <summary>
        /// Solves the position constraints.
        /// </summary>
        /// <param name="data"></param>
        /// <returns>returns true if the position errors are within tolerance.</returns>
        internal abstract bool SolvePositionConstraints(ref SolverData data);
    }
}