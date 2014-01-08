using Gibbo.Library;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Gibbo.Engine.WindowsStore
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        string projectFilePath;

        string originalTitle;

        /// <summary>
        /// 
        /// </summary>
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "";

        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeSettings()
        {
            IniFile settings = new IniFile(AppDomain.CurrentDomain.BaseDirectory + "\\settings.ini");

            try
            {
                // Console
                bool showConsole = settings.IniReadValue("Console", "Visible").ToLower().Trim().Equals("true") ? true : false;
                if (showConsole)
                {
                    AllocConsole();

                    //// stdout's handle seems to always be equal to 7
                    //IntPtr defaultStdout = new IntPtr(7);
                    //IntPtr currentStdout = GetStdHandle(StdOutputHandle);

                    //if (currentStdout != defaultStdout)
                    //    // reset stdout
                    //    SetStdHandle(StdOutputHandle, defaultStdout);

                    //// reopen stdout
                    //TextWriter writer = new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true };
                    //Console.SetOut(writer);
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
            // Very important!
            Assembly objExecutingAssemblies;
            string strTempAssmbPath = "";

            objExecutingAssemblies = Assembly.GetExecutingAssembly();
            AssemblyName[] arrReferencedAssmbNames = objExecutingAssemblies.GetReferencedAssemblies();

            //Loop through the array of referenced assembly names.
            foreach (AssemblyName strAssmbName in arrReferencedAssmbNames)
            {
                //Console.WriteLine(SceneManager.GameProject.ProjectPath + "\\" + args.Name.Substring(0, args.Name.IndexOf(",")) + ".dll");
                //Check for the assembly names that have raised the "AssemblyResolve" event.
                if (strAssmbName.FullName.Substring(0, strAssmbName.FullName.IndexOf(",")) == args.Name.Substring(0, args.Name.IndexOf(",")))
                {
                    //Build the path of the assembly from where it has to be loaded.				
                    //strTempAssmbPath = "C:\\Myassemblies\\" + args.Name.Substring(0, args.Name.IndexOf(",")) + ".dll";
                    strTempAssmbPath = SceneManager.GameProject.ProjectPath + "\\" + args.Name.Substring(0, args.Name.IndexOf(",")) + ".dll";
                    break;
                }
            }

            if (strTempAssmbPath == "")
            {
                foreach (string fileName in Directory.GetFiles(SceneManager.GameProject.ProjectPath + "\\libs\\"))
                {
                    string asmName = Path.GetFileName(fileName);
                    //Console.WriteLine(asmName.Replace(".dll", "") + " == " +  args.Name.Substring(0, args.Name.IndexOf(",")));
                    if (asmName.Replace(".dll", "") == args.Name.Substring(0, args.Name.IndexOf(",")))
                    {
                        // Console.WriteLine("entrei");
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

            Console.WriteLine("Gibbo 2D - Game Engine Console");

            if (SceneManager.GameProject.Debug)
            {
                Console.WriteLine("Scene Loaded with success!");
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
                        Window.Title = originalTitle + " - FPS: " + (int)SceneManager.FPS;
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
