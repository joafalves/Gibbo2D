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
using System.Runtime.Serialization;

#if WINDOWS
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
#endif

namespace Gibbo.Library
{
    /// <summary>
    /// Game Camera
    /// </summary>
#if WINDOWS
    [ExpandableObject]
    [Serializable, TypeConverter(typeof(ExpandableObjectConverter))]
#endif
    [DataContract]
    public class Camera : SystemObject
    {
        #region fields

        [DataMember]
        private float zoom;
        private Matrix transformMatrix;
        [DataMember]
        private Vector2 position;
        [DataMember]
        private float rotation = 0;

        #endregion

        #region properties

        /// <summary>
        /// 
        /// </summary>
#if WINDOWS
        [Category("Camera Properties")]
#endif
        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        /// <summary>
        /// 
        /// </summary>
#if WINDOWS
        [Category("Camera Properties")]
#endif
        public Vector2 Position
        {
            get { return position; }
            set
            {
                position = value;
            }
        }

        /// <summary>
        /// Camera zoom
        /// </summary>
        /// 
#if WINDOWS
        [Category("Camera Properties")]
#endif
        public float Zoom
        {
            get { return zoom; }
            set
            {
                if (value < 0.05f) value = 0.05f;
                zoom = value;
            }
        }

        /// <summary>
        /// The current Camera.tion matrix.
        /// </summary>
#if WINDOWS
        [Browsable(false)]
#endif
        public Matrix TransformMatrix
        {
            get
            {
                transformMatrix = CalculateTransform();

                return transformMatrix;
            }
        }

        /// <summary>
        /// The bounding box of the tile
        /// </summary>
#if WINDOWS
        [Browsable(false)]
#endif
        public Rectangle BoundingBox
        {
            get
            {
                Vector2 topLeft = Vector2.Transform(Vector2.Zero,
                    Matrix.Invert(SceneManager.ActiveCamera.TransformMatrix));

                Vector2 bottomRight = Vector2.Transform(
                    new Vector2(SceneManager.GraphicsDevice.Viewport.Width,
                        SceneManager.GraphicsDevice.Viewport.Height),
                        Matrix.Invert(SceneManager.ActiveCamera.TransformMatrix));

                return new Rectangle(
                    (int)topLeft.X,
                    (int)topLeft.Y,
                    (int)bottomRight.X - (int)topLeft.X,
                    (int)bottomRight.Y - (int)topLeft.Y);
            }
        }

        #endregion

        #region constructors

        /// <summary>
        /// Camera constructor
        /// </summary>
        public Camera()
        {
            //this.transform.GameObject = this;
            this.Position = Vector2.Zero;
            this.zoom = 1.0f;
        }

        #endregion

        #region methods

        /// <summary>
        /// Calculates the transformation matrix of the camera.
        /// </summary>
        /// <returns></returns>
        private Matrix CalculateTransform()
        {
            Vector3 scalingFactor = Vector3.One;
            if (!SceneManager.IsEditor)
            {
                // make sure we keep the real scale (user input):
                float widthScale = (float)SceneManager.GraphicsDevice.PresentationParameters.BackBufferWidth / (float)SceneManager.GameProject.Settings.ScreenWidth;
                float heightScale = (float)SceneManager.GraphicsDevice.PresentationParameters.BackBufferHeight / (float)SceneManager.GameProject.Settings.ScreenHeight;
                scalingFactor = new Vector3(widthScale, heightScale, 1);
            }

            Vector2 target = Position;

            Matrix result = Matrix.CreateTranslation(-target.X, -target.Y, 0.0f) *
                Matrix.CreateRotationZ(rotation) *
                Matrix.CreateScale(new Vector3((float)zoom, (float)zoom, 1)) *
                Matrix.CreateScale(scalingFactor) *
                Matrix.CreateTranslation(SceneManager.GraphicsDevice.Viewport.Width / 2,
                    SceneManager.GraphicsDevice.Viewport.Height / 2, 0);

            return result;
        }
         
        /// <summary>
        /// Calculates the transformation matrix of the camera for a given game object
        /// </summary>
        /// <param name="obj">The game object</param>
        /// <returns></returns>
        public Matrix ObjectTransform(GameObject obj)
        {                      
            return
                Matrix.CreateTranslation(-obj.Transform.Position.X, -obj.Transform.Position.Y, 0.0f) *
                Matrix.CreateRotationZ(obj.Transform.Rotation) *
                Matrix.CreateTranslation(obj.Transform.Position.X, obj.Transform.Position.Y, 0.0f) *
                CalculateTransform()
             ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Empty;
        }

        #endregion
    }
}
