using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Gibbo.Library
{
    /// <summary>
    /// Font Renderer
    /// </summary>
    public class FontRenderer
    {
        #region fields

        private Dictionary<char, FontChar> _characterMap;
        private FontFile _fontFile;
        private Texture2D _texture;

        #endregion

        #region properties

        /// <summary>
        /// 
        /// </summary>
        /// 
#if WINDOWS
        [Browsable(false)]
#endif
        public FontFile FontFile
        {
            get { return _fontFile; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
#if WINDOWS
        [Browsable(false)]
#endif
        public Texture2D Texture
        {
            get { return _texture; }
        }

        #endregion

        #region constructors

        /// <summary>
        /// Instantiates this instance
        /// </summary>
        /// <param name="fontFile">The font file</param>
        /// <param name="fontTexture">The font texture</param>
        public FontRenderer(FontFile fontFile, Texture2D fontTexture)
        {
            _fontFile = fontFile;
            _texture = fontTexture;
            _characterMap = new Dictionary<char, FontChar>();

            foreach (var fontCharacter in _fontFile.Chars)
            {
                char c = (char)fontCharacter.ID;
                _characterMap.Add(c, fontCharacter);
            }
        }

        #endregion

        #region methods

        /// <summary>
        /// Draws Text
        /// </summary>
        /// <param name="spriteBatch">The spritebatch</param>
        /// <param name="position">The position</param>
        /// <param name="text">The text to be drawn</param>
        public void DrawText(SpriteBatch spriteBatch, Vector2 position, string text)
        {
            DrawText(spriteBatch, (int)position.X, (int)position.Y, text, 1, Color.White, 0, TextAlignModes.Left);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="position"></param>
        /// <param name="text"></param>
        /// <param name="color"></param>
        public void DrawText(SpriteBatch spriteBatch, Vector2 position, string text, Color color)
        {
            DrawText(spriteBatch, (int)position.X, (int)position.Y, text, 1, color, 0, TextAlignModes.Left);
        }

        /// 
        /// <summary>
        /// Draws Text
        /// </summary>
        /// <param name="spriteBatch">The spritebatch</param>
        /// <param name="position">The position</param>
        /// <param name="text">The text to be drawn</param>
        /// <param name="scale">The scale</param>
        public void DrawText(SpriteBatch spriteBatch, Vector2 position, string text, float scale)
        {
            DrawText(spriteBatch, (int)position.X, (int)position.Y, text, scale, Color.White, 0, TextAlignModes.Left);
        }

        /// <summary>
        /// Draws Text
        /// </summary>
        /// <param name="spriteBatch">The spritebatch</param>
        /// <param name="x">Position X</param>
        /// <param name="y">Position Y</param>
        /// <param name="text">The text to be drawn</param>
        public void DrawText(SpriteBatch spriteBatch, int x, int y, string text)
        {
            DrawText(spriteBatch, x, y, text, 1, Color.White, 0, TextAlignModes.Left);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="text"></param>
        /// <param name="scale"></param>
        /// <param name="color"></param>
        /// <param name="lineSpacing"></param>
        public void DrawText(SpriteBatch spriteBatch, int x, int y, string text, float scale, Color color, int lineSpacing, TextAlignModes alignMode)
        {
            string[] lines = text.Split(new string[] { @"\n" }, StringSplitOptions.RemoveEmptyEntries);
            Vector2 size = MeasureString(lines, scale, lineSpacing);

            int dx = x;
            int dy = y;

            foreach (string line in lines)
            {
                float maxHeight = 0;

                if (alignMode == TextAlignModes.Center)
                {
                    Vector2 lineSize = MeasureString(line, scale);
                    dx = (int)(dx + (size.X / 2 - lineSize.X / 2));
                }

                foreach (char c in line)
                {
                    FontChar fc;

                    if (_characterMap.TryGetValue(c, out fc))
                    {
                        var sourceRectangle = new Rectangle(fc.X, fc.Y, fc.Width, fc.Height);
                        var destination = new Rectangle(dx + fc.XOffset, dy + fc.YOffset, (int)(fc.Width * scale), (int)(fc.Height * scale));

                        spriteBatch.Draw(_texture, destination, sourceRectangle, color);
                        dx += (int)(fc.XAdvance * scale);

                        if (fc.Height * scale > maxHeight)
                        {
                            maxHeight = fc.Height * scale;
                        }
                    }
                }

                dx = x;
                dy += (int)maxHeight + lineSpacing;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="scale"></param>
        /// <param name="lineSpacing"></param>
        /// <returns></returns>
        public Vector2 MeasureString(string text, float scale, int lineSpacing)
        {
            string[] lines = text.Split(new string[] { @"\n" }, StringSplitOptions.RemoveEmptyEntries);
            return MeasureString(lines, scale, lineSpacing);
        }

        /// <summary>
        /// Measure all lines resulting in the max size
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="scale"></param>
        /// <param name="lineSpacing"></param>
        /// <returns></returns>
        public Vector2 MeasureString(string[] lines, float scale, int lineSpacing)
        {
            float maxWidth = 0, maxHeight = 0;

            foreach (string line in lines)
            {
                Vector2 size = MeasureString(line, scale);

                if (size.X > maxWidth)
                    maxWidth = size.X;

                maxHeight += size.Y + lineSpacing;
            }

            return new Vector2(maxWidth, maxHeight);
        }

        /// <summary>
        /// Measure a string (width and height)
        /// </summary>
        /// <returns></returns>
        private Vector2 MeasureString(string text, float scale)
        {
            float maxWidth = 0;
            float maxHeight = 0;

            foreach (char c in text)
            {
                FontChar fc;

                if (_characterMap.TryGetValue(c, out fc))
                {
                    maxWidth += (int)(fc.XAdvance * scale);

                    if (fc.Height * scale > maxHeight)
                    {
                        maxHeight = fc.Height * scale;
                    }
                }
            }

            return new Vector2(maxWidth, maxHeight);
        }

        #endregion
    }
}
