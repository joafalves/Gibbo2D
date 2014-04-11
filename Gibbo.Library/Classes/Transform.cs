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
using Microsoft.Xna.Framework;
using System.ComponentModel;
using System.Collections.Generic;
using System.Dynamic;
using FarseerPhysics.Dynamics.Joints;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using FarseerPhysics.Factories;
using System.Runtime.Serialization;

#if WINDOWS
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using System.Security.Permissions;
#endif

namespace Gibbo.Library
{
    /// <summary>
    /// Transform class
    /// </summary>
#if WINDOWS
    [ExpandableObject]
    [Serializable, TypeConverter(typeof(ExpandableObjectConverter))]
#endif
    [DataContract]
    public class Transform
#if WINDOWS
        : ICloneable
#endif
    {
        #region fields
        [DataMember]
        internal GameObject gameObject;
        [DataMember]
        internal Transform parent;
        [DataMember]
        internal Vector2 position;
        [DataMember]
        internal float rotation;
        [DataMember]
        internal Vector2 scale = Vector2.One;

#if WINDOWS
        [NonSerialized]
#endif
        internal bool physicsPositionReached = true;

#if WINDOWS
        [NonSerialized]
#endif
        private Vector2 desiredPosition = Vector2.Zero;

        #endregion

        #region properties

        /// <summary>
        /// The root parent transformation
        /// </summary>
#if WINDOWS
        [Browsable(false)]
#endif
        public Transform Root
        {
            get
            {
                Transform root = this;
                while (root.parent != null)
                    root = root.parent;

                return root;
            }
        }

        /// <summary>
        /// The parent transformation
        /// </summary>
#if WINDOWS
        [Browsable(false)]
#endif
        public Transform Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        /// <summary>
        /// The game object holding this transform
        /// </summary>
#if WINDOWS
        [Browsable(false)]
#endif
        public GameObject GameObject
        {
            get { return gameObject; }
            set { gameObject = value; }
        }

        /// <summary>
        /// The relative position of the object.
        /// If the object has no parent it returns the world position.
        /// </summary>
#if WINDOWS
        [NotifyParentProperty(true)]
        [DisplayName("Position"), Description("The current position")]
#endif
        public Vector2 RelativePosition
        {
            get
            {
                if (gameObject.Body == null)
                {
                    if (parent != null)
                        return position - parent.Position;
                    else
                        return position;
                }
                else
                {
                    if (parent != null)
                        return ConvertUnits.ToDisplayUnits(gameObject.Body.Position) - parent.Position;
                    else
                        return ConvertUnits.ToDisplayUnits(gameObject.Body.Position);
                }
            }
            set
            {
                TranslateChildren(this.gameObject, value - RelativePosition, ConvertUnits.ToSimUnits(value - RelativePosition));

                if (parent != null)
                    position = value + parent.Position;
                else
                    position = value;

                if (gameObject.Body != null)
                {
                    if (parent != null)
                        desiredPosition = value + parent.Position;
                    else
                        desiredPosition = value;

                    desiredPosition = ConvertUnits.ToSimUnits(desiredPosition);

                    if (SceneManager.IsEditor || gameObject.Body.BodyType == BodyType.Static || gameObject.Body.BodyType == BodyType.Kinematic)
                    {
                        gameObject.Body.Position = desiredPosition;
                    }
                    else
                    {
                        physicsPositionReached = false;
                    }
                }
            }
        }

        /// <summary>
        /// The world position of the object
        /// </summary>
#if WINDOWS
        [Browsable(false)]
#endif
        public Vector2 Position
        {
            get
            {                
                if (gameObject != null && gameObject.Body != null)
                {
                    return ConvertUnits.ToDisplayUnits(gameObject.Body.Position);
                }
                else
                {
                    return position;
                }
            }
            set
            {
                TranslateChildren(this.gameObject, value - position, ConvertUnits.ToSimUnits(value - position));

                position = value;

                if (gameObject != null && gameObject.Body != null)
                {
                    desiredPosition = ConvertUnits.ToSimUnits(value);
                    gameObject.Body.Position = desiredPosition;
                    gameObject.Body.Awake = true;
                }
            }
        }

        private void TranslateChildren(GameObject obj, Vector2 dif, Vector2 difSim)
        {
            if (obj != null)
            {
                foreach (GameObject _obj in obj.Children)
                {
                    _obj.Transform.position += dif;

                    if (_obj.Body != null)
                    {
                        _obj.Body.Position += difSim;
                        if (!SceneManager.IsEditor)
                            _obj.Body.Awake = true; //wake the body for physics detection
                    }

                    TranslateChildren(_obj, dif, difSim);
                }
            }
        }

        /// <summary>
        /// The rotation of the object
        /// </summary>
#if WINDOWS
        [DisplayName("Rotation"), Description("The current rotation")]
#endif
        public float Rotation
        {
            get
            {                
                if (gameObject != null && gameObject.Body != null)
                {
                    //if (parent != null)
                    //    return gameObject.Body.Rotation + parent.Rotation;
                    //else
                    if (parent != null)
                        return gameObject.Body.Rotation + parent.Rotation;
                    else
                        return gameObject.Body.Rotation;
                }
                else
                {
                    if (parent != null)
                        return rotation + parent.Rotation;
                    else
                        return rotation;
                }
            }
            set
            {
                RotateChildren(this.gameObject, (value - rotation));

                rotation = value;

                if (gameObject != null && gameObject.Body != null)
                {
                    gameObject.Body.Rotation = rotation;
                    gameObject.Body.Awake = true;
                }
            }
        }

        private void RotateChildren(GameObject obj, float dif)
        {
            if (obj != null)
            {
                foreach (GameObject _obj in obj.Children)
                {
                    if (_obj.Body != null && _obj.Body.BodyType != BodyType.Dynamic)
                        _obj.Body.Rotation = _obj.Body.Rotation + _obj.Transform.rotation;

                    _obj.Transform.RelativePosition = _obj.Transform.RelativePosition.Rotate(dif);

                    RotateChildren(_obj, dif);
                }
            }
        }

        /// <summary>
        /// The scale of the object
        /// </summary>
#if WINDOWS
        [NotifyParentProperty(true)]
        [DisplayName("Scale"), Description("The current scale")]
#endif
        public Vector2 Scale
        {
            get { return scale; }
            set
            {
                if (gameObject != null)
                {
                    // scale children along
                    foreach (GameObject go in gameObject.Children)
                    {
                        go.Transform.Scale = value;
                    }
                }

                scale = value;

                if (gameObject != null && gameObject.Body != null && gameObject.physicalBody != null)
                {
                    gameObject.physicalBody.ResetBody();
                    gameObject.Body.Awake = true;
                }
            }
        }

        #endregion

        #region constructors

        public Transform()
        {

        }

#if WINDOWS
        protected Transform(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");

            gameObject = info.GetValue("gameObject", typeof(GameObject)) as GameObject;
            parent = info.GetValue("parent", typeof(Transform)) as Transform;
            position = (Vector2)info.GetValue("position", typeof(Vector2));
            rotation = (float)info.GetDouble("rotation");

            object obj = info.GetValue("scale", typeof(object));
            if (obj is Vector2)
                scale = (Vector2)info.GetValue("scale", typeof(Vector2));
            else
                scale = Vector2.One;
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");

            info.AddValue("gameObject", gameObject);
            info.AddValue("position", position);
            info.AddValue("rotation", rotation);
            info.AddValue("scale", scale);
            info.AddValue("parent", parent);
        }
#endif
        #endregion


        #region methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        public void SetPositionX(float x)
        {
            Position = new Vector2(x, Position.Y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="y"></param>
        public void SetPositionY(float y)
        {
            Position = new Vector2(Position.X, y);
        }

        /// <summary>
        /// Translates an object
        /// </summary>
        /// <param name="translation"></param>
        public void Translate(Vector2 translation)
        {
            Translate(translation.X, translation.Y);
        }

        /// <summary>
        /// Translates an object
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Translate(float x, float y)
        {
            //if (gameObject.Body == null)
            //{
            //    this.position.X += x;
            //    this.position.Y += y;
            //}
            //else
            //{
                this.Position = new Vector2(this.Position.X + x, this.Position.Y + y);
            //}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Transform DeepCopy(bool copyGameObject = false)
        {
            Transform result = Clone() as Transform;

            result.gameObject = (copyGameObject ? gameObject.Copy() : gameObject);
            result.Position = new Vector2(this.Position.X, this.Position.Y);
            result.scale = new Vector2(this.scale.X, this.scale.Y);
            result.rotation = this.rotation;

            return result;
        }

        /// <summary>
        /// Clones this object
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        /// <summary>
        /// To String
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "";
        }

        #endregion
    }
}
