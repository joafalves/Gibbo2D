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
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.ComponentModel;
using System.Threading.Tasks;

#if WINDOWS
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
#endif

using System.Runtime.Serialization;

namespace Gibbo.Library
{
    /// <summary>
    /// Sprite Object
    /// </summary>
#if WINDOWS
    [Serializable]
#endif
    [DataContract(IsReference=true)]
    [KnownType(typeof(Sprite))]
    public class Sprite : GameObject
    {
        #region enumerables

        /// <summary>
        /// Display Mode
        /// </summary>
        public enum DisplayModes { None, Tile, PositionTile, Fill }

        #endregion

        #region fields
        [DataMember]
        private string imageName;
        [DataMember]
        private Origins origin = Origins.Center;
        [DataMember]
        private Color color = Color.White;
        [DataMember]
        private DisplayModes displayMode = DisplayModes.None;
        //private bool autoCollisionModel = true;
        [DataMember]
        private Rectangle sourceRectangle = Rectangle.Empty;
        [DataMember]
        private BlendModes blendMode = BlendModes.NonPremultiplied;
        [DataMember]
        private SpriteEffects spriteEffect = SpriteEffects.None;

#if WINDOWS
        [NonSerialized]
#endif
        private Texture2D texture;

#if WINDOWS
        [NonSerialized]
#endif
        private BlendState blendState;

        #endregion

        #region properties

        ///// <summary>
        ///// Automatically calculates the collision boundries based on the texture size
        ///// </summary>
        //[Category("Sprite Properties")]
        //[DisplayName("Auto Boundries"), Description("Automatically calculates the collision boundries based on the texture size")]
        //public bool AutoCollisionModel
        //{
        //    get { return autoCollisionModel; }
        //    set { autoCollisionModel = value; }
        //}

        /// <summary>
        /// Determine how this sprite should be displayed
        /// </summary>
#if WINDOWS
        [Category("Sprite Properties")]
        [DisplayName("Display Mode"), Description("Determine how this sprite should be displayed")]
#endif
        public DisplayModes DisplayMode
        {
            get { return displayMode; }
            set { displayMode = value; }
        }

        /// <summary>
        /// The center point
        /// </summary>
#if WINDOWS
        [Category("Sprite Properties")]
        [DisplayName("Origin"), Description("The center point")]
#endif
        public Origins Origin
        {
            get { return origin; }
            set { origin = value; }
        }

        /// <summary>
        /// 
        /// </summary>
#if WINDOWS
        [Category("Sprite Properties")]
        [DisplayName("Sprite Effect"), Description("Sprite effect")]
#endif
        public SpriteEffects SpriteEffect
        {
            get { return spriteEffect; }
            set { spriteEffect = value; }
        }

        /// <summary>
        /// The blending mode
        /// </summary>
#if WINDOWS
        [Category("Sprite Properties")]
        [DisplayName("Blend Mode"), Description("The blending mode")]
#endif
        public BlendModes BlendMode
        {
            get { return blendMode; }
            set { blendMode = value; this.LoadState(); }
        }

        /// <summary>
        /// The fill color
        /// </summary>
#if WINDOWS
        [Category("Sprite Properties")]
        [DisplayName("Color"), Description("The fill color")]
#endif
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        /// <summary>
        /// The relative path to the image
        /// </summary>
#if WINDOWS
        //[EditorAttribute(typeof(ContentBrowserEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Category("Sprite Properties")]
        [DisplayName("Image Path"), Description("The relative path to the image")]
#endif
        public string ImageName
        {
            get { return imageName; }
            set
            {
                imageName = value;
                LoadTexture();
            }
        }

        /// <summary>
        /// The area of the image you want to be displayed
        /// </summary>
#if WINDOWS
        [Category("Sprite Properties")]
        [DisplayName("Source Rectangle"), Description("The area of the image you want to be displayed")]
#endif
        public Rectangle SourceRectangle
        {
            get { return sourceRectangle; }
            set { sourceRectangle = value; }
        }

        /// <summary>
        /// The texture of the sprite
        /// </summary>
#if WINDOWS
        [Browsable(false)]
#endif
        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        #endregion

        #region methods

        /// <summary>
        /// Initializes this instance
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            if (texture == null && imageName != null && imageName.Trim() != "")
            {
                LoadTexture();
            }

            LoadState();
        }

        private void LoadTexture()
        {
            texture = TextureLoader.FromContent(imageName);
        }

        private void LoadState()
        {
            switch (this.blendMode)
            {
                case BlendModes.NonPremultiplied:
                    this.blendState = BlendState.NonPremultiplied;
                    break;
                case BlendModes.AlphaBlend:
                    this.blendState = BlendState.AlphaBlend;
                    break;
                case BlendModes.Additive:
                    this.blendState = BlendState.Additive;
                    break;
            }
        }

        /// <summary>
        /// Updates this instance
        /// </summary>
        /// <param name="gameTime">The gametime</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //if (autoCollisionModel && texture != null)
            //{
            //    if (displayMode == DisplayModes.None)
            //    {
            //        
            //        //if (sourceRectangle == Rectangle.Empty)
            //        //{
            //        //    this.CollisionModel.Width = texture.Width;
            //        //    this.CollisionModel.Height = texture.Height;
            //        //}
            //        //else
            //        //{
            //        //    this.CollisionModel.Width = sourceRectangle.Width;
            //        //    this.CollisionModel.Height = sourceRectangle.Height;
            //        //}
            //    }
            //    else if (displayMode == DisplayModes.Fill)
            //    {
            //        //this.Transform.Position = SceneManager.ActiveCamera.Position;
            //        //this.CollisionModel.Width = SceneManager.GameProject.Settings.ScreenWidth;
            //        //this.CollisionModel.Height = SceneManager.GameProject.Settings.ScreenHeight;
            //    }
            //}
        }

        /// <summary>
        /// Draws this instance
        /// </summary>
        /// <param name="gameTime">The gametime</param>
        /// <param name="spriteBatch">The spriteBatch</param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            if (texture != null && Visible) // && (CollisionModel.CollisionBoundry.Intersects(SceneManager.ActiveCamera.BoundingBox) || displayMode == DisplayModes.Fill || displayMode == DisplayModes.Tile || displayMode == DisplayModes.PositionTile))
            {
                Vector2 _orgx = Vector2.Zero;

                if (sourceRectangle != Rectangle.Empty)
                {
                    _orgx = new Vector2(sourceRectangle.X - (sourceRectangle.Width / 2), sourceRectangle.Y - (sourceRectangle.Height / 2));
                }
                else if (origin == Origins.Center)
                {
                    _orgx = new Vector2(texture.Width / 2, texture.Height / 2);
                }

                if (displayMode == DisplayModes.Tile && MathExtension.IsPowerOfTwo(new Vector2(texture.Width, texture.Height)))
                {
                    //Vector2 initialPosition = Vector2.Transform(Vector2.Zero, Matrix.Invert(SceneManager.ActiveCamera.TransformMatrix));
                    //Vector2 endPos = Vector2.Transform(new Vector2(SceneManager.GraphicsDevice.Viewport.Width, SceneManager.GraphicsDevice.Viewport.Height), Matrix.Invert(SceneManager.ActiveCamera.TransformMatrix));
                    //int sizeWidth = (int)(endPos.X - initialPosition.X);
                    //int sizeHeight = (int)(endPos.Y - initialPosition.Y);

                    spriteBatch.Begin(SpriteSortMode.Deferred, this.blendState, SamplerState.LinearWrap, null, null, null, SceneManager.ActiveCamera.TransformMatrix);

                    //spriteBatch.Draw(texture, new Vector2(initialPosition.X, initialPosition.Y), new Rectangle((int)SceneManager.ActiveCamera.Position.X,
                    //    (int)SceneManager.ActiveCamera.Position.Y, sizeWidth, sizeHeight), Color, 0, Vector2.Zero, 1.0f,
                    //    SpriteEffects.None, 1);

                    spriteBatch.Draw(texture, new Vector2((int)SceneManager.ActiveCamera.Position.X - SceneManager.GraphicsDevice.Viewport.Width, (int)SceneManager.ActiveCamera.Position.Y - SceneManager.GraphicsDevice.Viewport.Height), new Rectangle((int)SceneManager.ActiveCamera.Position.X,
                         (int)SceneManager.ActiveCamera.Position.Y, SceneManager.GraphicsDevice.Viewport.Width * 2, SceneManager.GraphicsDevice.Viewport.Height * 2), Color, 0, Vector2.Zero, Transform.Scale,
                         spriteEffect, 1);
                }
                else if (displayMode == DisplayModes.PositionTile)
                {
                    //Vector2 initialPosition = Vector2.Transform(Vector2.Zero, Matrix.Invert(SceneManager.ActiveCamera.TransformMatrix));
                    //Vector2 endPos = Vector2.Transform(new Vector2(SceneManager.GraphicsDevice.Viewport.Width, SceneManager.GraphicsDevice.Viewport.Height), Matrix.Invert(SceneManager.ActiveCamera.TransformMatrix));
                    //int sizeWidth = (int)(endPos.X - initialPosition.X);
                    //int sizeHeight = (int)(endPos.Y - initialPosition.Y);

                    spriteBatch.Begin(SpriteSortMode.Deferred, this.blendState, SamplerState.LinearWrap, null, null, null, SceneManager.ActiveCamera.TransformMatrix);

                    //spriteBatch.Draw(texture, new Vector2(initialPosition.X, initialPosition.Y), new Rectangle((int)Transform.Position.X,
                    //    (int)Transform.Position.Y, sizeWidth, sizeHeight), Color, 0, Vector2.Zero, 1.0f,
                    //    SpriteEffects.None, 1);

                    spriteBatch.Draw(texture, new Vector2((int)SceneManager.ActiveCamera.Position.X - SceneManager.GraphicsDevice.Viewport.Width, (int)SceneManager.ActiveCamera.Position.Y - SceneManager.GraphicsDevice.Viewport.Height), new Rectangle((int)Transform.Position.X,
                    (int)Transform.Position.Y, SceneManager.GraphicsDevice.Viewport.Width * 2, SceneManager.GraphicsDevice.Viewport.Height * 2), Color, 0, Vector2.Zero, Transform.Scale,
                    spriteEffect, 1);
                }
                else if (displayMode == DisplayModes.Fill)
                {
                    Rectangle fill = new Rectangle(
                         (int)SceneManager.ActiveScene.Camera.Position.X,
                         (int)SceneManager.ActiveScene.Camera.Position.Y,
                         (int)SceneManager.GameProject.Settings.ScreenWidth,
                         (int)SceneManager.GameProject.Settings.ScreenHeight);

                    spriteBatch.Begin(SpriteSortMode.Deferred, this.blendState, SamplerState.LinearClamp, null, null, null, SceneManager.ActiveCamera.TransformMatrix);

                    if (sourceRectangle == Rectangle.Empty)
                        spriteBatch.Draw(texture, fill, null, Color, 0, _orgx, spriteEffect, 0);
                    else
                        spriteBatch.Draw(texture, fill, sourceRectangle, Color, 0, _orgx, spriteEffect, 0);
                }
                else
                {
                    
                    spriteBatch.Begin(SpriteSortMode.Deferred, this.blendState, SamplerState.LinearClamp, null, RasterizerState.CullNone, null, SceneManager.ActiveCamera.TransformMatrix);

                    if (sourceRectangle == Rectangle.Empty)
                        spriteBatch.Draw(texture, Transform.Position, null, color, Transform.Rotation, _orgx, Transform.Scale, spriteEffect, 1);
                    else
                        spriteBatch.Draw(texture, Transform.Position, sourceRectangle, color, Transform.Rotation, _orgx, Transform.Scale, spriteEffect, 1);
                }

                spriteBatch.End();
            }
        }

        public override RotatedRectangle MeasureDimension()
        {
            if (texture != null && Body == null)
            {
               Rectangle r = new Rectangle((int)(Transform.position.X - (texture.Width / 2) * Transform.scale.X),
                    (int)(Transform.position.Y - (texture.Height / 2) * Transform.scale.Y), (int)(texture.Width * Transform.scale.X),
                    (int)(texture.Height * Transform.scale.Y));
               return new RotatedRectangle(r, Transform.Rotation);
            }
            else
            {
                return base.MeasureDimension();
            }
        }

        #endregion
    }
}