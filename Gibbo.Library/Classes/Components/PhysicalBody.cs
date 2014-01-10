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
using System.ComponentModel;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using System.Runtime.Serialization;

namespace Gibbo.Library
{
    /// <summary>
    /// Physical body
    /// </summary>
    [Unique(Unique.UniqueOptions.AssignableFrom)]
    [Info("Physical Body:\nA very simple physics body that doesn't have a shape.")]
    public class PhysicalBody : ExtendedObjectComponent
    {
        #region fields

        //private float mass = 1;
        private bool fixedRotation = false;
        private float friction = 0.5f;
        private float restitution = 0.2f;
        private BodyType bodyType = BodyType.Dynamic;
        private bool isSensor = false;
        private bool isBullet = false;
        private float angularVelocity = 0;
        private float angularDamping = 0;
        private bool ignoreGravity = false;
        private float linearDamping = 0;

        #endregion

        #region properties

        /// <summary>
        /// The linear damping of the object
        /// </summary>
#if WINDOWS
        [Category("Physical Body Properties"), DisplayName("Linear Damping")]
#endif
        public float LinearDamping
        {
            get { return linearDamping; }
            set
            {
                linearDamping = value;

                if (Transform.GameObject.Body != null)
                    Transform.gameObject.Body.LinearDamping = value;
            }
        }

        /// <summary>
        /// Determines if the body is a bullet
        /// </summary>
#if WINDOWS
        [Category("Physical Body Properties"), DisplayName("Is Bullet")]
#endif
        public bool IsBullet
        {
            get { return isBullet; }
            set
            {
                isBullet = value;

                if (Transform.GameObject.Body != null)
                    Transform.gameObject.Body.IsBullet = value;
            }
        }

        /// <summary>
        /// Determines if the body should ignore gravity
        /// </summary>
#if WINDOWS
        [Category("Physical Body Properties"), DisplayName("Ignore Gravity")]
#endif
        public bool IgnoreGravity
        {
            get { return ignoreGravity; }
            set
            {
                ignoreGravity = value;

                if (Transform.GameObject.Body != null)
                    if (value)
                        Transform.GameObject.Body.GravityScale = 0;
                    else
                        Transform.GameObject.Body.GravityScale = 1;
            }
        }

        /// <summary>
        /// The angular velocity of the object
        /// </summary>
#if WINDOWS
        [Category("Physical Body Properties"), DisplayName("Angular Velocity")]
#endif
        public float AngularVelocity
        {
            get { return angularVelocity; }
            set
            {
                angularVelocity = value;

                if (Transform.GameObject.Body != null)
                    Transform.GameObject.Body.AngularVelocity = value;
            }
        }

        /// <summary>
        /// The angular damping of the object
        /// </summary>
#if WINDOWS
        [Category("Physical Body Properties"), DisplayName("Angular Damping")]
#endif
        public float AngularDamping
        {
            get { return angularDamping; }
            set
            {
                angularDamping = value;

                if (Transform.GameObject.Body != null)
                    Transform.GameObject.Body.AngularDamping = value;
            }
        }

        /// <summary>
        /// Determines if the body should behave as a sensor
        /// </summary>
#if WINDOWS
        [Category("Physical Body Properties"), DisplayName("Is Sensor")]
#endif
        public bool IsSensor
        {
            get { return isSensor; }
            set
            {
                isSensor = value;

                if (Transform.GameObject.Body != null && !SceneManager.IsEditor)
                    Transform.GameObject.Body.IsSensor = value;
            }
        }

        ///// <summary>
        ///// The mass of the body
        ///// </summary>
        //[Category("Physical Body Properties"), DisplayName("Mass")]
        //public float Mass
        //{
        //    get
        //    {
        //        return mass;
        //    }
        //    set
        //    {
        //        mass = value;

        //        if (Transform.GameObject.Body != null)
        //            Transform.GameObject.Body.Mass = value;
        //    }
        //}

        /// <summary>
        /// Determines if the body has a fixed rotation
        /// </summary>
#if WINDOWS
        [Category("Physical Body Properties"), DisplayName("Fixed Rotation")]
#endif
        public bool FixedRotation
        {
            get { return fixedRotation; }
            set
            {
                fixedRotation = value;

                if (Transform.GameObject.Body != null)
                    Transform.GameObject.Body.FixedRotation = value;
            }
        }

        /// <summary>
        /// The friction of the body
        /// </summary>
#if WINDOWS
        [Category("Physical Body Properties"), DisplayName("Friction")]
#endif
        public float Friction
        {
            get { return friction; }
            set
            {
                friction = value;

                if (Transform.GameObject.Body != null)
                    Transform.GameObject.Body.Friction = value;
            }
        }

        /// <summary>
        /// The restitution of the body
        /// </summary>
#if WINDOWS
        [Category("Physical Body Properties"), DisplayName("Restitution")]
#endif
        public float Restitution
        {
            get { return restitution; }
            set
            {
                restitution = value;

                if (Transform.GameObject.Body != null)
                    Transform.GameObject.Body.Restitution = value;
            }
        }

        /// <summary>
        /// The body type
        /// </summary>
#if WINDOWS
        [Category("Physical Body Properties"), DisplayName("Body Type")]
#endif
        public BodyType BodyType
        {
            get { return bodyType; }
            set
            {
                bodyType = value;

                if (Transform.GameObject.Body != null)
                    Transform.GameObject.Body.BodyType = value;
            }
        }

        #endregion

        #region methods

        /// <summary>
        /// Initializes the body
        /// </summary>
        public override void Initialize()
        {
            Transform.GameObject.Body = BodyFactory.CreateBody(SceneManager.ActiveScene.World);

            Transform.Position = Transform.position;
            Transform.Rotation = Transform.rotation;

            // Update the body properties:
            UpdateBodyProperties();
        }

        /// <summary>
        /// Removes the body from the world when the component is removed
        /// </summary>
        public override void Removed()
        {
            Transform.GameObject.Body = null;
        }

        /// <summary>
        /// Update the body properties
        /// </summary>
        protected virtual void UpdateBodyProperties()
        {
            if (Transform.GameObject.Body == null) return;

            Transform.GameObject.Body.BodyType = bodyType;
            Transform.GameObject.Body.Restitution = restitution;
            Transform.GameObject.Body.Friction = friction;
            Transform.GameObject.Body.FixedRotation = fixedRotation;
            if (SceneManager.IsEditor)
                Transform.GameObject.Body.IsSensor = true;
            else
                Transform.GameObject.Body.IsSensor = isSensor;
            Transform.GameObject.Body.AngularDamping = angularDamping;
            Transform.GameObject.Body.AngularVelocity = angularVelocity;
            Transform.gameObject.Body.IsBullet = isBullet;
            Transform.gameObject.Body.LinearDamping = linearDamping;

            if (ignoreGravity)
                Transform.GameObject.Body.GravityScale = 0;

            //Transform.GameObject.Body.Mass = mass;
        }

        internal virtual void ResetBody()
        {

        }

        #endregion
    }
}
