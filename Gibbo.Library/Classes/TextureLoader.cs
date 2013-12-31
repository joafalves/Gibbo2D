using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gibbo.Library
{
    /// <summary>
    /// This class is responsable to manage the textures loading.
    /// Textures are loaded to memory for quick usage, if you load the same texture twice it will use the same space in memory in order to preserve resources.
    /// If you want to clear the cache in runtime, execute the TextureLoader.Clear() method.
    /// </summary>
    public static class TextureLoader
    {
        #region fields

        private static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        private static Dictionary<Color, Texture2D> colorTextures = new Dictionary<Color, Texture2D>();

        #endregion

        #region methods

        /// <summary>
        /// Loads a texture from a file.
        /// The filename is relative to the game project path.
        /// </summary>
        /// <param name="filename">The filename of the texture to load</param>
        /// <returns>The loaded texture, null if not loaded</returns>
        public static Texture2D FromContent(string filename)
        {
            if (SceneManager.GameProject == null) return null;

#if WINDOWS
            return FromFile(SceneManager.GameProject.ProjectPath + "//" + filename);
#elif WINRT
            return FromFile(filename);
#endif
        }

        /// <summary>
        /// Loads a texture from a file.
        /// </summary>
        /// <param name="filename">The filename of the texture to load</param>
        /// <returns>The loaded texture, null if not loaded</returns>
        public static Texture2D FromFile(string filename)
        {
            if (SceneManager.GraphicsDevice == null) return null;

            // The image was already loaded to memory?
            if (!textures.ContainsKey(filename))
            {
#if WINDOWS
                FileInfo aTexturePath = new FileInfo(filename);

                // The file exists?
                if (!aTexturePath.Exists) return null;

                FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
                Texture2D texture = Texture2D.FromStream(SceneManager.GraphicsDevice, fs);
                fs.Close();

                // Store in the dictionary the loaded texture
                textures[filename] = texture;
#elif WINRT
                if(!MetroHelper.AppDataFileExists(filename)) return null;

                using (Stream stream = Windows.ApplicationModel.Package.Current.InstalledLocation.OpenStreamForReadAsync(filename).Result)
                {
                    Texture2D texture = Texture2D.FromStream(SceneManager.GraphicsDevice, stream);

                    textures[filename] = texture;
                }
#endif
            }

            return textures[filename];
        }

        /// <summary>
        /// Returns a texture from a given color
        /// </summary>
        /// <param name="color">Color</param>
        /// <returns>The texture created</returns>
        public static Texture2D GetColorTexture(Color color)
        {
            if (SceneManager.GraphicsDevice == null) return null;
            
            // The color texture was already loaded to memory?
            if (!colorTextures.ContainsKey(color))
            {
                Texture2D texture = new Texture2D(SceneManager.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
                texture.SetData(new[] { color });

                colorTextures[color] = texture;
            }

            return colorTextures[color];
        }

        /// <summary>
        /// Clears the texture's cache
        /// </summary>
        public static void Clear()
        {
            textures.Clear();
            colorTextures.Clear();
        }

        #endregion
    }
}
