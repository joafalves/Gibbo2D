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
        internal float scale = 1.0f;

#if WINDOWS
        [NonSerialized]
#endif
        internal bool physicsPositionReached = true;

#if WINDOWS
        [NonSerialized]
#endif
        internal Vector2 desiredPosition = Vector2.Zero;

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
                if (gameObject.Body == null)
                {
                    return position;
                }
                else
                {
                    return ConvertUnits.ToDisplayUnits(gameObject.Body.Position);
                }
            }
            set
            {
                TranslateChildren(this.gameObject, value - position, ConvertUnits.ToSimUnits(value - position));

                position = value;

                if (gameObject.Body != null)
                {                
                    desiredPosition = value;

                    desiredPosition = ConvertUnits.ToSimUnits(desiredPosition);

                    //if (SceneManager.IsEditor || gameObject.Body.BodyType == BodyType.Static || gameObject.Body.BodyType == BodyType.Kinematic)
                    //{
                        gameObject.Body.Position = desiredPosition;
                    //}
                    //else
                    //{
                    //    physicsPositionReached = false;
                    //}
                }
            }
        }

        private void TranslateChildren(GameObject obj, Vector2 dif, Vector2 difSim)
        {
            foreach (GameObject _obj in obj.Children)
            {
                _obj.Transform.position += dif;

                if (_obj.Body != null)
                {
                    if (SceneManager.IsEditor)
                        _obj.Body.Position += difSim;
                    else
                        desiredPosition += difSim;
                }

                TranslateChildren(_obj, dif, difSim);
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
                if (gameObject.Body == null)
                {
                    if (parent != null)
                        return rotation + parent.Rotation;
                    else
                        return rotation;
                }
                else
                {
                    //if (parent != null)
                    //    return gameObject.Body.Rotation + parent.Rotation;
                    //else
                        return gameObject.Body.Rotation;
                }
            }
            set
            {
                RotateChildren(this.gameObject, (value - rotation));

                rotation = value;

                if (gameObject.Body != null)
                    gameObject.Body.Rotation = rotation;
            }
        }

        private void RotateChildren(GameObject obj, float dif)
        {
            foreach (GameObject _obj in obj.Children)
            {
                //if (_obj.Body != null)
                //    _obj.Body.Rotation = _obj.Body.Rotation + _obj.Transform.rotation;
                
                _obj.Transform.RelativePosition = _obj.Transform.RelativePosition.Rotate(dif);

                RotateChildren(_obj, dif);
            }
        }

        /// <summary>
        /// The scale of the object
        /// </summary>
#if WINDOWS
        [NotifyParentProperty(true)]
        [DisplayName("Scale"), Description("The current scale")]
#endif
        public float Scale
        {
            get { return scale; }
            set
            {
                // Rotate children along
                foreach (GameObject go in gameObject.Children)
                {
                    go.Transform.Scale = value;
                }

                scale = value;

                if (gameObject.Body != null && gameObject.physicalBody != null)
                {
                    gameObject.physicalBody.ResetBody();
                    //Body _body = (gameObject.originalBody).DeepClone();
                    //(_body.FixtureList[0].Shape as PolygonShape).Vertices.Scale(new Vector2(value));
                    //gameObject.Body.FixtureList.RemoveAt(0);
                    //gameObject.Body.FixtureList.Add(_body.FixtureList[0]);

                    //gameObject.Body.FixtureList.Remove(gameObject.Body.FixtureList[0]);

                    //PolygonShape nshape = new PolygonShape(shape

                    //Vector2 _scale = ConvertUnits.ToSimUnits(new Vector2(value));

                    //PolygonShape shape = gameObject.Body.FixtureList[0].Shape as PolygonShape;
                    //shape.Vertices.Scale(new Vector2(scale));

                    //PolygonShape nshape = new PolygonShape
                }

                //if (gameObject.Body != null && gameObject.Body.FixtureList != null)
                //{
                //    Vector2 _scale = ConvertUnits.ToSimUnits(new Vector2(value));
                //    foreach (var v in gameObject.Body.FixtureList)
                //        if (v.Shape is PolygonShape)
                //            (v.Shape as PolygonShape).Vertices.Scale(ref _scale);
                //}
            }
        }

        #endregion

        #region methods

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
            if (gameObject.Body == null)
            {
                this.position.X += x;
                this.position.Y += y;
            }
            else
            {
                this.Position = new Vector2(this.Position.X + x, this.Position.Y + y);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Transform DeepCopy()
        {
            Transform result = Clone() as Transform; 

            result.Position = new Vector2(this.Position.X, this.Position.Y);

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
