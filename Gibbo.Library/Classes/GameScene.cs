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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.ComponentModel;
using FarseerPhysics.Dynamics;
using FarseerPhysics.DebugView;
using FarseerPhysics;

#if WINDOWS
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
#endif

using System.Runtime.Serialization;
using System.Xml.Serialization;


namespace Gibbo.Library
{
    /// <summary>
    /// The Game Scene.
    /// </summary>
#if WINDOWS
    [Serializable]
#endif
    [DataContract(Namespace = "")]
    public class GameScene : IDisposable
    //#if WINDOWS
    //, ISerializable
    //#endif
    {
        #region fields

        [DataMember]
        private string name = "Game Scene";
        [DataMember]
        private Camera camera = new Camera();
        [DataMember]
        private GameObjectCollection gameObjects = new GameObjectCollection(null);
        [DataMember]
        private Color backgroundColor = Color.FromNonPremultiplied(50, 50, 50, 255);
        [DataMember]
        private Vector2 gravity = Vector2.UnitY * 10;
        [DataMember]
        private List<string> commonTags = new List<string>();

#if WINDOWS
        [NonSerialized]
#endif
        [IgnoreDataMember, XmlIgnore]
        private List<RenderView> renderViews = new List<RenderView>();

#if WINDOWS
        [NonSerialized]
#endif
        [IgnoreDataMember, XmlIgnore]
        internal List<GameObject> markedForRemoval;

#if WINDOWS
        [NonSerialized]
#endif
        [IgnoreDataMember, XmlIgnore]
        private DebugView2D debugView;


#if WINDOWS
        [NonSerialized]
#endif
        [IgnoreDataMember, XmlIgnore]
        private World world;


#if WINDOWS
        [NonSerialized]
      
#endif
        [IgnoreDataMember, XmlIgnore]
        protected ContentManager content;

#if WINDOWS
        [NonSerialized]     
#endif
        [IgnoreDataMember, XmlIgnore]
        protected SpriteBatch spriteBatch;

#if WINDOWS
        [NonSerialized]       
#endif
        [IgnoreDataMember, XmlIgnore]
        protected GraphicsDeviceManager graphics;

        #endregion

        #region properties

#if WINDOWS
        [Browsable(false)]
#endif
        public List<RenderView> RenderViews
        {
            get { return renderViews; }
            set { renderViews = value; }
        }

#if WINDOWS
        [Browsable(false)]
#endif
        public List<string> CommonTags
        {
            get { return commonTags; }
            set { commonTags = value; }
        }

        /// <summary>
        /// The active content manager
        /// </summary>
#if WINDOWS
        [Browsable(false)]
#endif
        [IgnoreDataMember, XmlIgnore]
        public ContentManager Content
        {
            get { return content; }
            set { content = value; }
        }

        /// <summary>
        /// The active spritebatch
        /// </summary>
#if WINDOWS
        [Browsable(false)]
#endif
        [IgnoreDataMember, XmlIgnore]
        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
            set { spriteBatch = value; }
        }

        /// <summary>
        /// The active graphics device manager
        /// </summary>
#if WINDOWS
        [Browsable(false)]
#endif
        [IgnoreDataMember, XmlIgnore]
        public GraphicsDeviceManager Graphics
        {
            get { return graphics; }
            set { graphics = value; }
        }

        /// <summary>
        /// The active camera on the scene
        /// </summary>
#if WINDOWS
        [Category("Scene Basic Properties")]
        [DisplayName("Camera"), Description("The active camera")]
#endif
        public Camera Camera
        {
            get { return camera; }
            set { camera = value; }
        }

        /// <summary>
        /// The layers of this scene
        /// </summary>
#if WINDOWS
        [Category("Scene Properties"), Browsable(false)]
#endif
        public GameObjectCollection GameObjects
        {
            get { return gameObjects; }
            set { gameObjects = value; }
        }

        /// <summary>
        /// The name of this scene
        /// </summary>
#if WINDOWS
        [Category("Scene Properties"), Browsable(false)]
#endif
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// The background color of this scene
        /// </summary>
#if WINDOWS
        [EditorAttribute(typeof(XNAColorEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Category("Scene Basic Properties")]
        [DisplayName("Clear Color"), Description("The clear color")]
#endif
        public Color BackgroundColor
        {
            get { return backgroundColor; }
            set { backgroundColor = value; }
        }

        /// <summary>
        /// The gravity of the world's scene
        /// </summary>
#if WINDOWS
        [Category("Scene Physics Properties")]
        [DisplayName("Gravity"), Description("Gravity")]
#endif
        public Vector2 Gravity
        {
            get { return gravity; }
            set
            {
                gravity = value;
                world.Gravity = value;
            }
        }

        /// <summary>
        /// The physics world of the scene
        /// </summary>
#if WINDOWS
        [Browsable(false)]
#endif
        [IgnoreDataMember, XmlIgnore]
        public World World
        {
            get { return world; }
            set { world = value; }
        }

        #endregion

        #region constructors

        public GameScene()
        {

        }

        //#if WINDOWS
        //        protected GameScene(SerializationInfo info, StreamingContext context)
        //        {
        //            if (info == null)
        //                throw new ArgumentNullException("info");

        //            foreach (SerializationEntry entry in info)
        //            {
        //                switch (entry.Name)
        //                {
        //                    case "name":
        //                        name = (string)entry.Value; break;
        //                    case "camera":
        //                        camera = (Camera)entry.Value; break;
        //                    case "gravity":
        //                        gravity = (Vector2)entry.Value; break;
        //                    case "backgroundColor":
        //                        backgroundColor = (Color)entry.Value; break;
        //                    case "gameObjects":
        //                        object obj = entry.Value;
        //                        if (obj.GetType().IsSubclassOf(typeof(System.Collections.CollectionBase)))
        //                        {
        //                            // handle old projects:
        //                            System.Collections.CollectionBase bx = (System.Collections.CollectionBase)obj;
        //                            this.gameObjects = new GameObjectCollection(null);
        //                            foreach (var ob in bx)
        //                            {
        //                                this.gameObjects.Add(ob as GameObject);
        //                            }
        //                        }
        //                        else
        //                        {
        //                            gameObjects = (GameObjectCollection)entry.Value;
        //                        }
        //                        break;
        //                }
        //            }
        //        }

        //        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        //        public void GetObjectData(SerializationInfo info, StreamingContext context)
        //        {
        //            if (info == null)
        //                throw new ArgumentNullException("info");

        //            info.AddValue("name", name);
        //            info.AddValue("camera", camera);
        //            info.AddValue("backgroundColor", backgroundColor);
        //            info.AddValue("gravity", gravity);
        //            info.AddValue("gameObjects", gameObjects);
        //        }
        //#endif
        #endregion

        #region methods

        /// <summary>
        /// Initializes the Game Scene.
        /// The Layers, game objects and components of this scene are initialized too.
        /// </summary>
        public virtual void Initialize()
        {
            if (renderViews == null)
                renderViews = new List<RenderView>();

            if (CommonTags == null)
                CommonTags = new List<string>();

            markedForRemoval = new List<GameObject>();

            if (world != null)
            {
                //foreach (var obj in world.BodyList)
                //    world.RemoveBody(obj);

                //world.Step(0);
            }
            else
            {
                world = new World(gravity);
            }

            debugView = new DebugView2D(world);
            debugView.RemoveFlags(DebugViewFlags.Controllers);
            debugView.LoadContent(SceneManager.GraphicsDevice, Content);

            //if(SceneManager.IsEditor)
            //    debugView.Flags = DebugViewFlags.PerformanceGraph | DebugViewFlags.DebugPanel | DebugViewFlags.Joint | DebugViewFlags.Shape;

            foreach (GameObject obj in gameObjects)
                obj.Initialize();
        }

        /// <summary>
        /// For custom scenes.
        /// Load scene's content here
        /// </summary>
        public virtual void LoadContent() { }

        /// <summary>
        /// Updates the scene's logic here.
        /// The Layers, game objects and components of this scene are updated too.
        /// </summary>
        /// <param name="gameTime">The Gametime</param>
        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < gameObjects.Count; i++)
                if (!gameObjects[i].Disabled)
                    gameObjects[i].Update(gameTime);

            if (SceneManager.IsEditor)
                world.Step(0);
            else
            {
                world.Step((float)gameTime.ElapsedGameTime.TotalMilliseconds * 0.001f);
                //Console.WriteLine((float)gameTime.ElapsedGameTime.TotalMilliseconds * 0.001f);
            }

            for (int i = markedForRemoval.Count - 1; i >= 0; i--)
            {
                markedForRemoval[i].Delete();
                markedForRemoval.RemoveAt(i);
            }
        }

        /// <summary>
        /// Draws the scene's here.
        /// The Layers, game objects and components of this scene are drawn too.
        /// </summary>
        /// <param name="gameTime">The Gametime</param>
        public void Draw(GameTime gameTime)
        {
            SceneManager.drawPass = 0;

            if (renderViews.Count == 0)
            {
                for (int i = 0; i < gameObjects.Count; i++)
                    if (!gameObjects[i].Disabled)
                        gameObjects[i].Draw(gameTime, this.SpriteBatch);

                DrawCollisionView(SceneManager.GraphicsDevice.Viewport, SceneManager.ActiveCamera);
            }
            else
            {
                // save defaults
                Viewport defaultViewport = SceneManager.GraphicsDevice.Viewport;
                Camera defaultCamera = SceneManager.ActiveCamera;

                foreach (RenderView v in renderViews)
                {
                    SceneManager.GraphicsDevice.Viewport = v.Viewport;
                    SceneManager.ActiveScene.Camera = v.Camera;
                    SceneManager.ActiveCamera = v.Camera;

                    for (int i = 0; i < gameObjects.Count; i++)
                        if (!gameObjects[i].Disabled)
                            gameObjects[i].Draw(gameTime, this.SpriteBatch);

                    if(v.Camera != null)
                        DrawCollisionView(v.Viewport, v.Camera);

                    SceneManager.drawPass++;
                }

                // load defaults
                SceneManager.GraphicsDevice.Viewport = defaultViewport;
                SceneManager.ActiveScene.camera = defaultCamera;
                SceneManager.ActiveCamera = defaultCamera;
            }         
        }

        private void DrawCollisionView(Viewport viewport, Camera camera)
        {
            if ((SceneManager.IsEditor && SceneManager.GameProject.EditorSettings.ShowCollisions)
             || (!SceneManager.IsEditor && SceneManager.GameProject.Debug && SceneManager.GameProject.EditorSettings.ShowCollisions))
            {
                var projection = Matrix.CreateOrthographicOffCenter(
                   0f,
                   ConvertUnits.ToSimUnits(viewport.Width),
                   ConvertUnits.ToSimUnits(viewport.Height), 0f, 0f,
                   1f);

                var view = Matrix.CreateTranslation(
                        -ConvertUnits.ToSimUnits(camera.Position.X),
                        -ConvertUnits.ToSimUnits(camera.Position.Y), 0.0f) *
                    Matrix.CreateRotationZ(camera.Rotation) *
                    Matrix.CreateScale(
                        new Vector3((float)camera.Zoom,
                            (float)camera.Zoom, 1)) *
                    Matrix.CreateTranslation(
                        ConvertUnits.ToSimUnits(viewport.Width / 2),
                        ConvertUnits.ToSimUnits(viewport.Height / 2), 0.0f);

                debugView.RenderDebugData(ref projection, ref view);
            }
        }

        /// <summary>
        /// Saves all of this scene object's components
        /// </summary>
        public void SaveComponentValues()
        {
            foreach (GameObject obj in gameObjects)
                obj.SaveComponentValues();
        }

        public void Dispose()
        {
            foreach (GameObject obj in this.gameObjects)
                obj.Dispose();

            GC.SuppressFinalize(this);
        }

        public override string ToString()
        {
            return this.Name;
        }

        #endregion
    }
}
