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
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Security.Cryptography;
using System.Text;
using System;

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

            //#if WINDOWS

            // The image was already loaded to memory?
            if (!textures.ContainsKey(filename) || SceneManager.IsEditor)
            {

                FileInfo aTexturePath = new FileInfo(filename);
                string encryFilename = filename + ".encry";
                FileInfo aEncryptedTexture = new FileInfo(encryFilename);

                // The file exists?
                if (!aTexturePath.Exists && !aEncryptedTexture.Exists) return null;

                if (aEncryptedTexture.Exists)
                {
                    //Through the des decryption
                    DESCryptoServiceProvider des = new DESCryptoServiceProvider();

                    //Read through the documents flow
                    FileStream fs = File.OpenRead(encryFilename);

                    //Get file binary characters
                    byte[] inputByteArray = new byte[fs.Length];

                    //Reading the stream file
                    fs.Read(inputByteArray, 0, (int)fs.Length);

                    //Close the stream
                    fs.Close();

                    // Secret Key Retrieval
                    // Open the file.
                    string projectPath = System.IO.Path.Combine(SceneManager.GameProject.ProjectPath, "Data.dat");
                    FileStream fStream = new FileStream(projectPath, FileMode.Open);

                    // Read from the stream and decrypt the data.
                    byte[] decryptedArray = Encryption.DecryptDataFromStreamWithoutEntropy(DataProtectionScope.CurrentUser, fStream, 230);
                    
                    fStream.Close();

                    //A key array
                    byte[] keyByteArray = decryptedArray; //("P0dpPz8OPwM=");

                    //Define hash variables
                    SHA1 ha = new SHA1Managed();

                    //Calculation of the specified byte group designated area hash value
                    byte[] hb = ha.ComputeHash(keyByteArray);

                    //The encryption key array
                    byte[] sKey = new byte[8];

                    //Encryption variables
                    byte[] sIV = new byte[8];

                    for (int i = 0; i < 8; i++)
                        sKey[i] = hb[i];

                    for (int i = 8; i < 16; i++)
                        sIV[i - 8] = hb[i];

                    //Access to the encryption key
                    des.Key = sKey;

                    //Encryption variables
                    des.IV = sIV;

                    MemoryStream ms = new MemoryStream();

                    CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);

                    cs.Write(inputByteArray, 0, inputByteArray.Length);

                    cs.FlushFinalBlock();

                    //fs = File.OpenWrite(filename + ".decrypt.png");

                    //foreach (byte b in ms.ToArray())
                    //{

                    //    fs.WriteByte(b);

                    //}

                    //FileStream ffs = new FileStream(filename + ".decrypt.png", FileMode.Open, FileAccess.Read);
                    Texture2D texture = Texture2D.FromStream(SceneManager.GraphicsDevice, ms);

                    fs.Close();
                    cs.Close();
                    ms.Close();
                    

                    // Store in the dictionary the loaded texture
                    textures[filename] = texture;
                }
                else
                {
                    FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
                    Texture2D texture = Texture2D.FromStream(SceneManager.GraphicsDevice, fs);
                    fs.Close();

                    // Store in the dictionary the loaded texture
                    textures[filename] = texture;
                }



                //#elif WINRT
                //                if(!MetroHelper.AppDataFileExists(filename)) return null;

                //                using (Stream stream = Windows.ApplicationModel.Package.Current.InstalledLocation.OpenStreamForReadAsync(filename).Result)
                //                {
                //                    Texture2D texture = Texture2D.FromStream(SceneManager.GraphicsDevice, stream);

                //                    textures[filename] = texture;
                //                }
                //#endif
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
