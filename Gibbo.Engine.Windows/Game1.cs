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
#region Using Statements
using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Gibbo.Library;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace Gibbo.Engine.Windows
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
#if WINDOWS
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        const int SW_HIDE = 0;
        const int SW_SHOW = 5;
        // P/Invoke required:
        private const UInt32 StdOutputHandle = 0xFFFFFFF5;
        [DllImport("kernel32.dll")]
        private static extern IntPtr GetStdHandle(UInt32 nStdHandle);
        [DllImport("kernel32.dll")]
        private static extern void SetStdHandle(UInt32 nStdHandle, IntPtr handle);
        [DllImport("kernel32")]
        static extern bool AllocConsole();
        TimeSpan elapsedTime = TimeSpan.Zero;
#endif

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        string projectFilePath;

        string originalTitle;

        private static int fpsCount = 0;
        private static int fps = 0;
        private static float deltaFPSTime = 0f;

        /// <summary>
        /// 
        /// </summary>
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            //graphics.SynchronizeWithVerticalRetrace = false;
            Content.RootDirectory = "";

            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeSettings()
        {
            //AllocConsole();           
            IniFile settings = new IniFile(AppDomain.CurrentDomain.BaseDirectory + "\\settings.ini");

            try
            {
                // Console
                bool showConsole = settings.IniReadValue("Console", "Visible").ToLower().Trim().Equals("true") ? true : false;
                if (!showConsole)
                {
                    var handle = GetConsoleWindow();

                    // Hide
                    ShowWindow(handle, SW_HIDE);
                }

                // Cursor
                bool showCursor = settings.IniReadValue("Mouse", "Visible").ToLower().Trim().Equals("true") ? true : false;
                if (showCursor)
                    IsMouseVisible = true;

                graphics.PreferredBackBufferWidth = Convert.ToInt32(settings.IniReadValue("Window", "Width").Trim());
                graphics.PreferredBackBufferHeight = Convert.ToInt32(settings.IniReadValue("Window", "Height").Trim());
               
                // Full Screen
                bool fullScreen = settings.IniReadValue("Window", "StartFullScreen").ToLower().Trim().Equals("true") ? true : false;
                if (fullScreen)
                {
                    graphics.ToggleFullScreen();
                }
                else
                {
                    Type type = typeof(OpenTKGameWindow);
                    System.Reflection.FieldInfo field = type.GetField("window", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    OpenTK.GameWindow window = (OpenTK.GameWindow)field.GetValue(Window);

                    window.X = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / 2 - graphics.PreferredBackBufferWidth / 2;
                    window.Y = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height / 2 - graphics.PreferredBackBufferHeight / 2;
                }

                // Update Settings
                graphics.ApplyChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // Search for the project file
            foreach (string filePath in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory))
            {
                if (Path.GetExtension(filePath).ToLower().Equals(".gibbo"))
                {
                    projectFilePath = filePath;
                    break;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            Assembly objExecutingAssemblies;
            string strTempAssmbPath = "";

            objExecutingAssemblies = Assembly.GetExecutingAssembly();
            AssemblyName[] arrReferencedAssmbNames = objExecutingAssemblies.GetReferencedAssemblies();

            //Loop through the array of referenced assembly names.
            foreach (AssemblyName strAssmbName in arrReferencedAssmbNames)
            {
                //Check for the assembly names that have raised the "AssemblyResolve" event.
                if (strAssmbName.FullName.Substring(0, strAssmbName.FullName.IndexOf(",")) == args.Name.Substring(0, args.Name.IndexOf(",")))
                {
                    //Build the path of the assembly from where it has to be loaded.				
                    strTempAssmbPath = SceneManager.GameProject.ProjectPath + "\\" + args.Name.Substring(0, args.Name.IndexOf(",")) + ".dll";
                    break;
                }
            }

            if (strTempAssmbPath == "")
            {
                foreach (string fileName in Directory.GetFiles(SceneManager.GameProject.ProjectPath + "\\libs\\"))
                {
                    string asmName = Path.GetFileName(fileName);
                    if (asmName.Replace(".dll", "") == args.Name.Substring(0, args.Name.IndexOf(",")))
                    {
                        strTempAssmbPath = SceneManager.GameProject.ProjectPath + "\\libs\\" + asmName;
                        break;
                    }
                }
            }

            if (strTempAssmbPath == "")
            {
                return SceneManager.ScriptsAssembly;
            }

            //Load and return the loaded assembly.
            return Assembly.LoadFrom(strTempAssmbPath);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            InitializeSettings();

            SceneManager.ScriptsAssembly = Assembly.LoadFile(Environment.CurrentDirectory + "\\libs\\Scripts.dll");

            if (!File.Exists(projectFilePath)) Exit();

            SceneManager.GameProject = GibboProject.Load(projectFilePath);
            SceneManager.GameWindow = this;

            //IsFixedTimeStep = (SceneManager.GameProject.ProjectSettings.VSyncEnabled) ? true : false;

            if (SceneManager.GameProject.Debug)
            {
                Console.WriteLine("Gibbo 2D - Game Engine Console");
                Console.WriteLine("Scene loaded successfully!");
                Console.WriteLine("Path: " + SceneManager.GameProject.ProjectPath);
                Console.WriteLine("Assembly path " + SceneManager.ScriptsAssembly.Location.ToString());
            }

            Window.Title = SceneManager.GameProject.ProjectName;
            originalTitle = SceneManager.GameProject.ProjectName;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            if (!File.Exists(SceneManager.GameProject.ProjectPath + "\\" + SceneManager.GameProject.SceneStartPath)) Exit();

            SceneManager.Content = Content;
            SceneManager.SpriteBatch = spriteBatch;
            SceneManager.GraphicsDevice = GraphicsDevice;
            SceneManager.Graphics = graphics;
            SceneManager.IsEditor = false;

            if (!SceneManager.LoadScene(SceneManager.GameProject.SceneStartPath))
                Exit();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            // Audio.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            try
            {
                if (SceneManager.ActiveScene != null)
                {
                    SceneManager.Update(gameTime);

                    if (SceneManager.GameProject.Debug)
                    {
                        deltaFPSTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (deltaFPSTime >= 1)
                        {
                            fps = fpsCount;
                            fpsCount = 0;
                            deltaFPSTime = 0;

                            Window.Title = originalTitle + " - FPS: " + fps.ToString();
                        }
                        else
                        {
                            fpsCount++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ups: " + ex.ToString() + "\nTarget:>" + ex.TargetSite);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            if (SceneManager.ActiveScene != null)
            {
                GraphicsDevice.Clear(SceneManager.ActiveScene.BackgroundColor);
            }
            else
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);
            }

            try
            {
                if (SceneManager.ActiveScene != null)
                {
                    SceneManager.Draw(gameTime);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ups: " + ex.ToString() + "\nTarget:>" + ex.TargetSite);
            }

            base.Draw(gameTime);
        }
    }
}