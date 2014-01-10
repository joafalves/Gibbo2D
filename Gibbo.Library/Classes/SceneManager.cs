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
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;
using System.Diagnostics;

namespace Gibbo.Library
{
    /// <summary>
    /// The Scene Manager.
    /// This class is responsable for managing the active game scene.
    /// </summary>
    public static class SceneManager
    {
        #region fields

        private static Game gameWindow;
        private static GameScene activeScene;
        private static ContentManager content;
        private static SpriteBatch spriteBatch;
        private static GraphicsDeviceManager graphics;
        private static GraphicsDevice graphicsDevice;
        private static Assembly scriptsAssembly;
        private static Camera activeCamera;

        private static bool isEditor = true;
        private static Tileset activeTileset = null;

        private static float fps = 0f;
        private static float deltaFPSTime = 0f;

        #endregion

        #region properties

        /// <summary>
        /// The active game window.
        /// Only available in the game mode
        /// </summary>
        public static Game GameWindow
        {
            get { return SceneManager.gameWindow; }
            set { SceneManager.gameWindow = value; }
        }

        /// <summary>
        /// The current frame per second rate
        /// </summary>
        public static float FPS
        {
            get { return fps; }
            set { fps = value; }
        }

        /// <summary>
        /// The active tileset
        /// </summary>
        public static Tileset ActiveTileset
        {
            get { return activeTileset; }
            set { activeTileset = value; }
        }

        /// <summary>
        /// The active camera
        /// </summary>
        public static Camera ActiveCamera
        {
            get
            {
                if (!IsEditor && ActiveScene != null)
                {
                    return ActiveScene.Camera;
                }
                else
                {
                    return SceneManager.activeCamera;
                }
            }
            set { SceneManager.activeCamera = value; }
        }

        /// <summary>
        /// Determines if we are running in editor mode
        /// </summary>
        public static bool IsEditor
        {
            get { return SceneManager.isEditor; }
            set { SceneManager.isEditor = value; }
        }

        /// <summary>
        /// Gets or sets the current game project.
        /// </summary>
        public static GibboProject GameProject { get; set; }

        /// <summary>
        /// Gets or Sets the active game scene
        /// </summary>
        public static GameScene ActiveScene
        {
            get { return activeScene; }
            set
            {
                if (value == null)
                {
                    activeScene = null;
                    return;
                }

                if (content == null)
                    throw new Exception("Cannot add scene, content manager is not assigned");

                if (spriteBatch == null)
                    throw new Exception("Cannot add scene, sprite batch is not assigned");

                if (activeScene != null)
                    activeScene.Dispose();

                activeScene = value;

                value.Content = content;
                value.SpriteBatch = spriteBatch;
                value.Graphics = graphics;

                value.Initialize();
                value.LoadContent();
     
                activeTileset = null;
            }
        }

        /// <summary>
        /// The active content manager
        /// </summary>
        public static ContentManager Content
        {
            get { return SceneManager.content; }
            set { SceneManager.content = value; }
        }

        /// <summary>
        /// The active spritebatch
        /// </summary>
        public static SpriteBatch SpriteBatch
        {
            get { return SceneManager.spriteBatch; }
            set { SceneManager.spriteBatch = value; }
        }

        /// <summary>
        /// The active graphics device manager
        /// </summary>
        public static GraphicsDeviceManager Graphics
        {
            get { return SceneManager.graphics; }
            set { SceneManager.graphics = value; }
        }

        /// <summary>
        /// The active graphics device
        /// </summary>
        public static GraphicsDevice GraphicsDevice
        {
            get { return SceneManager.graphicsDevice; }
            set { SceneManager.graphicsDevice = value; }
        }

        /// <summary>
        /// The current active scene path
        /// </summary>
        public static string ActiveScenePath { get; set; }

        /// <summary>
        /// The active scripts assembly reference
        /// </summary>
        public static Assembly ScriptsAssembly
        {
            get { return SceneManager.scriptsAssembly; }
            set { SceneManager.scriptsAssembly = value; }
        }

        #endregion

        #region methods

        /// <summary>
        /// Update of the active scene's logic.
        /// The collision engine and game input are also updated.
        /// </summary>
        /// <param name="gameTime">The gametime</param>
        public static void Update(GameTime gameTime)
        {
            GameInput.Update();

            float _fps = 1 / (float)gameTime.ElapsedGameTime.TotalSeconds;
            deltaFPSTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (deltaFPSTime > 1)
            {
                fps = _fps;
                deltaFPSTime -= 1;
            }

            if (activeScene != null)
            {
                activeScene.Update(gameTime);
            }
        }

        /// <summary>
        /// Draw the active scene
        /// </summary>
        /// <param name="gameTime">The gameTime</param>
        public static void Draw(GameTime gameTime)
        {
            if (activeScene != null)
            {
                activeScene.Draw(gameTime);
            }
        }

        /// <summary>
        /// Create a scene at the input location
        /// </summary>
        /// <param name="filename">The filename of the scene to be created</param>
        /// <returns>True if successfully created</returns>
        public static bool CreateScene(string filename)
        {
            GameScene gameScene = new GameScene();
            GibboHelper.SerializeObject(filename, gameScene);

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scenePath"></param>
        /// <returns></returns>
        public static bool LoadScene(string scenePath)
        {
            return LoadScene(scenePath, false);
        }

        /// <summary>
        /// Loads a scene to memory from a file.
        /// The active scene is updated to this one if loaded with success.
        /// </summary>
        /// <param name="scenePath">The path of the scene to load</param>
        /// <returns>True if successfully loaded</returns>
        public static bool LoadScene(string scenePath, bool saveHistory)
        {
            try
            {
#if WINDOWS
                GameScene gameScene = (GameScene)GibboHelper.DeserializeObject(scenePath);
#elif WINRT
                GameScene gameScene = (GameScene)GibboHelper.DeserializeObject(typeof(GameScene), scenePath);
#endif
                ActiveScene = gameScene;
                ActiveScenePath = scenePath;

                // Update last saved scene:
                if (GameProject != null && !scenePath.StartsWith("_") && saveHistory)
                    GameProject.EditorSettings.LastOpenScenePath = GibboHelper.MakeExclusiveRelativePath(GameProject.ProjectPath, ActiveScenePath);

                // Load with success, notify:
                return true;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("Error loading scene: " + exception.Message + "\n>" + exception.ToString());
                // Not loaded, notify:
                return false;
            }
        }

        /// <summary>
        /// Saves the active scene at its location
        /// </summary>
        /// <returns>True if scene is saved</returns>
        public static bool SaveActiveScene()
        {
            if (ActiveScene == null) return false;

            activeScene.SaveComponentValues();
            GibboHelper.SerializeObject(ActiveScenePath, ActiveScene);

            return true;
        }

        /// <summary>
        /// Saves the active scene at the input location
        /// </summary>
        /// <param name="path">The target path</param>
        /// <returns></returns>
        public static bool SaveActiveScene(string path)
        {
            if (ActiveScene == null) return false;

            GibboHelper.SerializeObject(path, ActiveScene);

            return true;
        }

        #endregion
    }
}
