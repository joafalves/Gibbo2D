using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Gibbo.Library.Classes
{
    /// <summary>
    /// 
    /// </summary>
    public class BMFontLoader
    {
        #region fields

        private static Dictionary<string, FontRenderer> fontRenderers = new Dictionary<string, FontRenderer>();

        #endregion

        #region constructors

        /// <summary>
        /// The available font renderers
        /// </summary>
        public static Dictionary<string, FontRenderer> FontRenderers
        {
            get { return BMFontLoader.fontRenderers; }
        }

        #endregion

        #region methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fntFilePath"></param>
        /// <param name="fontTexturePath"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static FontRenderer AddBMFont(string fntFilePath, string fontTexturePath, string name)
        {
            if(!fontRenderers.ContainsKey(name))
            {
                string _fntFilePath = Path.Combine(SceneManager.GameProject.ProjectPath, fntFilePath);
                string _textureFilePath = Path.Combine(SceneManager.GameProject.ProjectPath, fontTexturePath);

#if WINDOWS
                if (File.Exists(_fntFilePath) && File.Exists(_textureFilePath))
#elif WINRT
                if(MetroHelper.AppDataFileExists(_fntFilePath) && MetroHelper.AppDataFileExists(_textureFilePath))
#endif
                {
                    FontFile fontFile = FontLoader.Load(_fntFilePath);
                    Texture2D fontTexture = TextureLoader.FromFile(_textureFilePath);

                    FontRenderer fr = new FontRenderer(fontFile, fontTexture);
                    fontRenderers[name] = fr;

                    return fr;
                }
            }

            return null;
        }

        /// <summary>
        /// Clears the renderers cache
        /// </summary>
        public static void Clear()
        {
            fontRenderers.Clear();
        }

        #endregion
    }
}
