using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;

namespace Gibbo.Library
{
    /// <summary>
    /// Bitmap Font Class
    /// </summary>
#if WINDOWS
    [Serializable]
#endif
    [DataContract]
    public class BMFont : GameObject
    {
        #region fields
        [DataMember]
        private int lineSpacing = 0;
        [DataMember]
        private Color overlayColor = Color.White;
        [DataMember]
        private string fntFilePath = string.Empty;
        [DataMember]
        private string textureFilePath = string.Empty;
        [DataMember]
        private string text = string.Empty;
        [DataMember]
        private TextAlignModes alignMode = TextAlignModes.Left;
        [DataMember]
        private Origins origin;

#if WINDOWS
        [NonSerialized]
#endif
        private FontRenderer fontRenderer;

#if WINDOWS
        [NonSerialized]
#endif
        private FontFile fontFile;

#if WINDOWS
        [NonSerialized]
#endif
        private Texture2D fontTexture;

        #endregion

        #region properties

        /// <summary>
        /// The alignment mode 
        /// </summary>
#if WINDOWS
        [Category("BMFont Properties")]
        [DisplayName("Align"), Description("The alignment mode ")]
#endif
        public TextAlignModes AlignMode
        {
            get { return alignMode; }
            set { alignMode = value; }
        }

        /// <summary>
        /// The text to be displayed
        /// </summary>
#if WINDOWS
        [Category("BMFont Properties")]
        [DisplayName("Text"), Description("The text to be displayed")]
#endif
        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        /// <summary>
        /// The relative fnt file path
        /// </summary>
#if WINDOWS
        [EditorAttribute(typeof(ContentBrowserEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Category("BMFont Properties")]
        [DisplayName("Fnt File Path"), Description("The relative fnt file path")]
#endif
        public string FntFilePath
        {
            get { return fntFilePath; }
            set
            {
                if (Path.GetExtension(value).Equals(".fnt"))
                {
                    fntFilePath = value;
                    LoadData();
                }
            }
        }


        /// <summary>
        /// The relative texture file path
        /// </summary>
        // [EditorAttribute(typeof(ContentBrowserEditor), typeof(System.Drawing.Design.UITypeEditor))]
#if WINDOWS
        [Category("BMFont Properties")]
        [DisplayName("Texture File Path"), Description("The relative texture file path")]
#endif
        public string TextureFilePath
        {
            get { return textureFilePath; }
            set
            {
                if (Path.GetExtension(value).Equals(".png"))
                {
                    textureFilePath = value;
                    LoadData();
                }
            }
        }

        /// <summary>
        /// The origin relative to the object position
        /// </summary>
        /// </summary>
#if WINDOWS
        [Category("BMFont Properties")]
        [DisplayName("Origin"), Description("The origin relative to the object position")]
#endif
        public Origins Origin
        {
            get { return origin; }
            set { origin = value; }
        }

        /// <summary>
        /// The spacing between lines
        /// </summary>
#if WINDOWS
        [Category("BMFont Properties")]
        [DisplayName("Line Spacing"), Description("The spacing between lines")]
#endif
        public int LineSpacing
        {
            get { return lineSpacing; }
            set { lineSpacing = value; }
        }

        /// <summary>
        /// The overlay color
        /// </summary>
#if WINDOWS
        [EditorAttribute(typeof(XNAColorEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Category("BMFont Properties")]
        [DisplayName("Overlay Color"), Description("The overlay color")]
#endif
        public Color OverlayColor
        {
            get { return overlayColor; }
            set { overlayColor = value; }
        }

        #endregion

        #region constructors

        #endregion

        #region methods

        /// <summary>
        /// Initializes this instance
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            LoadData();
        }

        private void LoadData()
        {
            string _fntFilePath = Path.Combine(SceneManager.GameProject.ProjectPath, fntFilePath);
            string _textureFilePath = Path.Combine(SceneManager.GameProject.ProjectPath, textureFilePath);

#if WINDOWS
            if (File.Exists(_fntFilePath) && File.Exists(_textureFilePath))
#elif WINRT
            if (MetroHelper.AppDataFileExists(_fntFilePath) && MetroHelper.AppDataFileExists(_textureFilePath))
#endif
            {
                this.fontFile = FontLoader.Load(_fntFilePath);
                this.fontTexture = TextureLoader.FromFile(_textureFilePath);

                this.fontRenderer = new FontRenderer(this.fontFile, this.fontTexture);
            }
        }

        /// <summary>
        /// Draws this instance
        /// </summary>
        /// <param name="gameTime">The gametime</param>
        /// <param name="spriteBatch">The spriteBatch</param>
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            if (this.fontRenderer != null && Visible) // && CollisionModel.CollisionBoundry.Intersects(SceneManager.ActiveCamera.BoundingBox))
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.LinearWrap, null, null, null, SceneManager.ActiveCamera.TransformMatrix);

                if (origin == Origins.TopLeft)
                {
                    this.fontRenderer.DrawText(spriteBatch, (int)Transform.Position.X, (int)Transform.Position.Y, this.text.ToString(), Transform.Scale.X, this.OverlayColor, this.lineSpacing, this.alignMode);
                }
                else
                {
                    Vector2 dest = fontRenderer.MeasureString(text, Transform.Scale.X, this.lineSpacing);
                    dest.X = Transform.Position.X - (dest.X - dest.X / 2);
                    dest.Y = Transform.Position.Y - (dest.Y - dest.Y / 2);

                    this.fontRenderer.DrawText(spriteBatch, (int)dest.X, (int)dest.Y, this.text.ToString(), Transform.Scale.X, this.OverlayColor, this.lineSpacing, this.alignMode);
                }

                spriteBatch.End();
            }
        }

        #endregion
    }
}
