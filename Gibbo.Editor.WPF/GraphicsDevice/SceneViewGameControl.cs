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
using Gibbo.Editor.Model;
using Gibbo.Library;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using FarseerPhysics.Dynamics;

namespace Gibbo.Editor.WPF
{
    internal enum EditorModes { Select, Move, Rotate, Scale }
    internal enum TilesetModes { Pencil, Rectangle, Eraser, AddColumn, AddRow, RemoveColumn, RemoveRow }

    class SceneViewGameControl : GameControl
    {
        #region fields

        private const int HANDLER_SIZE = 24;
        private const int AXIS_OFFSET = 65;
        private const int AXIS_OPT_SIZE = 18;

        private bool usingYAxis = false;
        private bool usingXAxis = false;
        private bool hoverYAxis = false;
        private bool hoverXAxis = false;

        private ContentManager content;
        private SpriteBatch spriteBatch;

        private FontRenderer bmFontRenderer;

        private Vector2 mouseWorldPosition;
        private EditorModes editorMode = EditorModes.Select;
        private TilesetModes tilesetMode = TilesetModes.Pencil;

        private Tile[,] memTiles;

        private bool objectHandled;
        private bool initialized = false;
        private bool lastLeftKeyState; // true = down, false = up

        private float delta;

        private Vector2 selectionStart;
        private Vector2 selectionEnd;
        private Rectangle selectionArea;
        private bool selectionStarted = false;

        private List<GameObject> sceneGameObjects = new List<GameObject>();
        private Dictionary<string, Texture2D> objectIcons = new Dictionary<string, Texture2D>();

        private bool panStarted;
        private Vector2 panStart;
        private Vector2 panMouseLastPos;

        private bool tilesetDragStarted = false;
        private Vector2 tilesetMouseDownPos;
        private Rectangle tilesetSelectedArea;

        private bool leftMouseKeyDown;

        private bool mouseDragStarted = false;
        private Vector2 mouseClickPosition = Vector2.Zero;
        private Vector2 mouseLastPosition = Vector2.Zero;

        private Dictionary<GameObject, Transform> beforeState = new Dictionary<GameObject, Transform>();

        #endregion

        #region properties

        public Rectangle SelectionArea
        {
            get { return selectionArea; }
            set { selectionArea = value; }
        }

        public bool TileSetMode
        {
            get
            {
                return (EditorHandler.SelectedGameObjects.Count == 1 && EditorHandler.SelectedGameObjects[0] is Tileset);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool LeftMouseKeyDown
        {
            get { return leftMouseKeyDown; }
            set
            {
                leftMouseKeyDown = value;
            }
        }

        public bool LeftMouseKeyPressed { get; set; }
        public bool MiddleMouseKeyDown { get; set; }

        /// <summary>
        /// 
        /// </summary>

        internal EditorModes EditorMode
        {
            get { return editorMode; }
            set { editorMode = value; }
        }

        internal TilesetModes TilesetMode
        {
            get { return tilesetMode; }
            set { tilesetMode = value; }
        }

        public ServiceContainer ServiceContainer
        {
            get { return Services; }
        }

        public Rectangle MouseBoundingBox
        {
            get { return new Rectangle((int)mouseWorldPosition.X, (int)mouseWorldPosition.Y, 1, 1); }
        }

        #endregion

        #region constructors

        /// <summary>
        /// 
        /// </summary>
        public SceneViewGameControl()
        {

        }

        #endregion

        #region methods

        /// <summary>
        /// 
        /// </summary>
        protected override void Initialize()
        {
            if (!initialized)
            {
                base.Initialize();

                content = new ContentManager(Services, "Content");
                spriteBatch = new SpriteBatch(GraphicsDevice);

                //SceneManager.Content = content;
                SceneManager.SpriteBatch = spriteBatch;
                SceneManager.GraphicsDevice = GraphicsDevice;
                SceneManager.ActiveCamera = this.Camera;

                EditorHandler.SelectedGameObjects = new List<GameObject>();

                // Load ObjectIcons
                objectIcons["AudioObject"] = TextureLoader.FromFile(AppDomain.CurrentDomain.BaseDirectory + "\\Gibbo.Content\\audio.png");

                FontFile fontFile = FontLoader.Load(AppDomain.CurrentDomain.BaseDirectory + "\\Gibbo.Content\\editorBMFont.fnt");
                Texture2D fontTexture = TextureLoader.FromFile(AppDomain.CurrentDomain.BaseDirectory + "\\Gibbo.Content\\editorBMFont_0.png");
                bmFontRenderer = new FontRenderer(fontFile, fontTexture);

                LoadContent();

                this.CenterCamera();

                initialized = true;

                this.MouseWheel += SceneEditorControl_MouseWheel;
                this.MouseDown += SceneViewGameControl_MouseDown;
                this.MouseUp += SceneViewGameControl_MouseUp;
                this.MouseMove += SceneViewGameControl_MouseMove;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private void LoadContent()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        protected override void Draw(GameTime gameTime)
        {
            if (SceneManager.ActiveScene != null)
            {
                GraphicsDevice.Clear(SceneManager.ActiveScene.BackgroundColor);

                DrawEditorBottom();

                SceneManager.Draw(gameTime);

                DrawEditorTop();

                if (Properties.Settings.Default.ShowDebugView)
                {
                    string text = string.Format("Camera Position: (X: {0}, Y: {1})", SceneManager.ActiveCamera.Position.X, SceneManager.ActiveCamera.Position.Y);

                    spriteBatch.Begin();

                    bmFontRenderer.DrawText(spriteBatch, new Vector2(10, 8), text, Color.White);

                    //if(this.Focused)
                    //    bmFontRenderer.DrawText(spriteBatch, new Vector2(10, 26), "FPS: " + SceneManager.FPS.ToString("0"), Color.White);

                    spriteBatch.End();
                }
            }
            else
            {
                GraphicsDevice.Clear(Color.FromNonPremultiplied(50, 50, 50, 255));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void DrawObjectIcons()
        {
            foreach (GameObject gameObject in sceneGameObjects)
            {
                if (objectIcons.ContainsKey(gameObject.GetType().Name))
                {
                    //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, null, null, null, null, SceneManager.ActiveCamera.TransformMatrix);
                    spriteBatch.Draw(objectIcons[gameObject.GetType().Name], gameObject.Transform.Position, null, Color.White, 0, new Vector2(objectIcons[gameObject.GetType().Name].Width / 2, objectIcons[gameObject.GetType().Name].Height / 2), 1, SpriteEffects.None, 1);
                    //spriteBatch.End();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void DrawEditorBottom()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        private void DrawEditorTop()
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);

            if (TileSetMode && SceneManager.GameProject.EditorSettings.ShowGrid)
            {
                DrawGrid((EditorHandler.SelectedGameObjects[0] as Tileset).TileWidth,
                    (EditorHandler.SelectedGameObjects[0] as Tileset).TileHeight,
                    0, 0,
                    (EditorHandler.SelectedGameObjects[0] as Tileset).Width / 2,
                    (EditorHandler.SelectedGameObjects[0] as Tileset).Height / 2,
                    //(EditorHandler.SelectedGameObjects[0] as Tileset).OffsetX,
                    //(EditorHandler.SelectedGameObjects[0] as Tileset).OffsetY,
                    SceneManager.GameProject.EditorSettings.GridColor);
            }
            else
                if (SceneManager.GameProject.EditorSettings.ShowGrid)
                {
                    DrawGrid(
                        SceneManager.GameProject.EditorSettings.GridSpacing,
                        SceneManager.GameProject.EditorSettings.GridSpacing,
                        0, 0,
                        SceneManager.GameProject.EditorSettings.GridNumberOfLines / 2,
                        SceneManager.GameProject.EditorSettings.GridNumberOfLines / 2,
                        SceneManager.GameProject.EditorSettings.GridColor);
                }

            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, null, null, null, null, SceneManager.ActiveCamera.TransformMatrix);

            if (Properties.Settings.Default.ShowDebugView)
                DrawObjectIcons();

            if (TileSetMode)
            {
                // snap to the tileset grid
                //Vector2 drawPosition = new Vector2()
                //{
                //    X = ((EditorHandler.SelectedGameObjects[0] as Tileset).TileWidth * (int)Math.Round(((mouseWorldPosition.X - (EditorHandler.SelectedGameObjects[0] as Tileset).OffsetX - EditorHandler.BrushControl.CurrentSelectionXNA.Width / 2) / (EditorHandler.SelectedGameObjects[0] as Tileset).TileWidth))),
                //    Y = ((EditorHandler.SelectedGameObjects[0] as Tileset).TileHeight * (int)Math.Round(((mouseWorldPosition.Y - (EditorHandler.SelectedGameObjects[0] as Tileset).OffsetY - EditorHandler.BrushControl.CurrentSelectionXNA.Height / 2) / (EditorHandler.SelectedGameObjects[0] as Tileset).TileHeight)))
                //};

                //drawPosition.X += (EditorHandler.SelectedGameObjects[0] as Tileset).OffsetX;
                //drawPosition.Y += (EditorHandler.SelectedGameObjects[0] as Tileset).OffsetY;

                if (tilesetMode == TilesetModes.Pencil)
                {
                    if ((EditorHandler.SelectedGameObjects[0] as Tileset).Texture != null)
                    {
                        spriteBatch.Draw((EditorHandler.SelectedGameObjects[0] as Tileset).Texture, SnapToTilesetGrid(mouseWorldPosition), EditorHandler.TilesetBrushControl.CurrentSelectionXNA, Color.White);
                    }
                }
                else if (tilesetMode == TilesetModes.Rectangle)
                {
                    if (tilesetSelectedArea != Rectangle.Empty)
                    {
                        //Rectangle drawRectangle = new Rectangle()
                        //{
                        //    X = (int)((EditorHandler.SelectedGameObjects[0] as Tileset).TileWidth * (int)Math.Floor((((float)tilesetSelectedArea.X - (EditorHandler.SelectedGameObjects[0] as Tileset).OffsetX - EditorHandler.BrushControl.CurrentSelectionXNA.Width / 2) / (EditorHandler.SelectedGameObjects[0] as Tileset).TileWidth))),
                        //    Y = (int)((EditorHandler.SelectedGameObjects[0] as Tileset).TileHeight * (int)Math.Floor((((float)tilesetSelectedArea.Y - (EditorHandler.SelectedGameObjects[0] as Tileset).OffsetY - EditorHandler.BrushControl.CurrentSelectionXNA.Height / 2) / (EditorHandler.SelectedGameObjects[0] as Tileset).TileHeight))),
                        //    Width = tilesetSelectedArea.Width,
                        //    Height = tilesetSelectedArea.Height
                        //};

                        //drawRectangle.X += (EditorHandler.SelectedGameObjects[0] as Tileset).OffsetX;
                        //drawRectangle.Y += (EditorHandler.SelectedGameObjects[0] as Tileset).OffsetY;

                        Primitives.DrawBoxFilled(spriteBatch, tilesetSelectedArea, new Color(65, 115, 175, 140));
                        Primitives.DrawBox(spriteBatch, tilesetSelectedArea, new Color(23, 55, 95, 140), 2);
                    }
                }
                else if (tilesetMode == TilesetModes.Eraser)
                {
                    if (tilesetSelectedArea != Rectangle.Empty)
                    {
                        Primitives.DrawBoxFilled(spriteBatch, tilesetSelectedArea, new Color(217, 0, 0, 140));
                        Primitives.DrawBox(spriteBatch, tilesetSelectedArea, new Color(217, 0, 0, 180), 2);
                    }
                }
                else if (tilesetMode == TilesetModes.AddColumn)
                {
                    Vector2 ps = SnapToTilesetGrid(mouseWorldPosition);
                    float tBorder = Vector2.Transform(new Vector2(0, 0), Matrix.Invert(SceneManager.ActiveCamera.TransformMatrix)).Y;
                    float bBorder = Vector2.Transform(new Vector2(0, Height), Matrix.Invert(SceneManager.ActiveCamera.TransformMatrix)).Y;

                    Rectangle dispRect = new Rectangle()
                    {
                        X = (int)ps.X,
                        Y = (int)tBorder,
                        Width = (EditorHandler.SelectedGameObjects[0] as Tileset).TileWidth,
                        Height = (int)bBorder - (int)tBorder
                    };

                    Primitives.DrawBoxFilled(spriteBatch, dispRect, new Color(65, 115, 175, 140));
                }
                else if (tilesetMode == TilesetModes.RemoveColumn)
                {
                    Vector2 ps = SnapToTilesetGrid(mouseWorldPosition);
                    float tBorder = Vector2.Transform(new Vector2(0, 0), Matrix.Invert(SceneManager.ActiveCamera.TransformMatrix)).Y;
                    float bBorder = Vector2.Transform(new Vector2(0, Height), Matrix.Invert(SceneManager.ActiveCamera.TransformMatrix)).Y;

                    Rectangle dispRect = new Rectangle()
                    {
                        X = (int)ps.X,
                        Y = (int)tBorder,
                        Width = (EditorHandler.SelectedGameObjects[0] as Tileset).TileWidth,
                        Height = (int)bBorder - (int)tBorder
                    };

                    Primitives.DrawBoxFilled(spriteBatch, dispRect, new Color(217, 0, 0, 140));
                }
                else if (tilesetMode == TilesetModes.AddRow)
                {
                    Vector2 ps = SnapToTilesetGrid(mouseWorldPosition);
                    float lBorder = Vector2.Transform(new Vector2(0, 0), Matrix.Invert(SceneManager.ActiveCamera.TransformMatrix)).X;
                    float rBorder = Vector2.Transform(new Vector2(Width, 0), Matrix.Invert(SceneManager.ActiveCamera.TransformMatrix)).X;

                    Rectangle dispRect = new Rectangle()
                    {
                        X = (int)lBorder,
                        Y = (int)ps.Y,
                        Width = (int)rBorder - (int)lBorder,
                        Height = (EditorHandler.SelectedGameObjects[0] as Tileset).TileHeight
                    };

                    Primitives.DrawBoxFilled(spriteBatch, dispRect, new Color(65, 115, 175, 140));
                }
                else if (tilesetMode == TilesetModes.RemoveRow)
                {
                    Vector2 ps = SnapToTilesetGrid(mouseWorldPosition);
                    float lBorder = Vector2.Transform(new Vector2(0, 0), Matrix.Invert(SceneManager.ActiveCamera.TransformMatrix)).X;
                    float rBorder = Vector2.Transform(new Vector2(Width, 0), Matrix.Invert(SceneManager.ActiveCamera.TransformMatrix)).X;

                    Rectangle dispRect = new Rectangle()
                    {
                        X = (int)lBorder,
                        Y = (int)ps.Y,
                        Width = (int)rBorder - (int)lBorder,
                        Height = (EditorHandler.SelectedGameObjects[0] as Tileset).TileHeight
                    };

                    Primitives.DrawBoxFilled(spriteBatch, dispRect, new Color(217, 0, 0, 140));
                }
            }

            spriteBatch.End();

            if (EditorHandler.SelectedGameObjects.Count > 0)
            {
                DrawCurrentObjectHandler();
            }

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, null, null, null, null, SceneManager.ActiveCamera.TransformMatrix);

            if (editorMode == EditorModes.Select && selectionStart != selectionEnd)
            {
                Primitives.DrawBoxFilled(spriteBatch, selectionArea, new Color(65, 115, 175, 140));
                Primitives.DrawBox(spriteBatch, selectionArea, new Color(23, 55, 95, 140), 2);
            }

            //if (spanStarted)
            //    Primitives.DrawLine(spriteBatch, spanStart, mouseWorldPosition, new Color(0, 0, 0, 180), 2);

            spriteBatch.End();

            spriteBatch.Begin();

            if (Properties.Settings.Default.ShowDebugView)
                DrawSceneCamera();

            spriteBatch.End();
        }

        /// <summary>
        /// 
        /// </summary>
        private void DrawCurrentObjectHandler()
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, null, null, null, null, SceneManager.ActiveCamera.TransformMatrix);

            foreach (GameObject gameObject in EditorHandler.SelectedGameObjects)
            {
                if (!(gameObject is Tileset))
                {
                    Vector2 spos = gameObject.Transform.Position;

                    //If you dont want to use the spritebatch of the Camera.tion matrix, multiply the position like shown bellow:
                    //Vector2.Transform(EditorHandler.SelectedGameObject.Transform.Position, SceneManager.ActiveCamera.TransformMatrix);

                    // Draw center arrow
                    Vector2 start = new Vector2(spos.X, spos.Y - HANDLER_SIZE);
                    Vector2 end = new Vector2(spos.X, spos.Y + HANDLER_SIZE);

                    //start = new Vector2(spos.X - HANDLER_SIZE, spos.Y);
                    //end = new Vector2(spos.X + HANDLER_SIZE, spos.Y);
                    //Primitives.DrawLine(spriteBatch, start, end, Color.Yellow, 2);

                    if (editorMode == EditorModes.Move || editorMode == EditorModes.Scale)
                    {
                        // Y axis:
                        {
                            start = new Vector2(spos.X, spos.Y - AXIS_OFFSET);
                            end = new Vector2(spos.X, spos.Y);
                            Primitives.DrawLine(spriteBatch, start, end, Color.Green, 4);

                            if (editorMode == EditorModes.Move)
                            {
                                start = new Vector2(spos.X - AXIS_OPT_SIZE / 2, spos.Y - AXIS_OFFSET + AXIS_OPT_SIZE / 2);
                                end = new Vector2(spos.X + 1, spos.Y - AXIS_OFFSET);
                                Primitives.DrawLine(spriteBatch, start, end, Color.Green, 4);

                                start = new Vector2(spos.X + AXIS_OPT_SIZE / 2, spos.Y - AXIS_OFFSET + AXIS_OPT_SIZE / 2);
                                end = new Vector2(spos.X - 1, spos.Y - AXIS_OFFSET);
                                Primitives.DrawLine(spriteBatch, start, end, Color.Green, 4);


                            }
                            else if (editorMode == EditorModes.Scale)
                            {
                                Primitives.DrawBoxFilled(spriteBatch,
                                    new Rectangle((int)spos.X - AXIS_OPT_SIZE / 2, (int)spos.Y - AXIS_OFFSET - AXIS_OPT_SIZE / 2, AXIS_OPT_SIZE, AXIS_OPT_SIZE)
                                    , Color.Green);
                            }

                            if (usingYAxis || GameInput.IsKeyDown(Keys.Y))
                            {
                                float tBorder = Vector2.Transform(new Vector2(0, 0), Matrix.Invert(SceneManager.ActiveCamera.TransformMatrix)).Y;
                                float bBorder = Vector2.Transform(new Vector2(0, Height), Matrix.Invert(SceneManager.ActiveCamera.TransformMatrix)).Y;

                                start = new Vector2(spos.X, tBorder);
                                end = new Vector2(spos.X, bBorder);

                                Primitives.DrawLine(spriteBatch, start, end, Color.CornflowerBlue, 2);
                            }
                            else if (usingXAxis || GameInput.IsKeyDown(Keys.X))
                            {
                                float lBorder = Vector2.Transform(new Vector2(0, 0), Matrix.Invert(SceneManager.ActiveCamera.TransformMatrix)).X;
                                float rBorder = Vector2.Transform(new Vector2(Width, 0), Matrix.Invert(SceneManager.ActiveCamera.TransformMatrix)).X;

                                start = new Vector2(lBorder, spos.Y);
                                end = new Vector2(rBorder, spos.Y);

                                Primitives.DrawLine(spriteBatch, start, end, Color.CornflowerBlue, 2);
                            }
                        }

                        // X axis:
                        {
                            start = new Vector2(spos.X + AXIS_OFFSET, spos.Y);
                            end = new Vector2(spos.X, spos.Y);
                            Primitives.DrawLine(spriteBatch, start, end, Color.Red, 4);

                            if (editorMode == EditorModes.Move)
                            {
                                start = new Vector2(spos.X + AXIS_OFFSET - AXIS_OPT_SIZE / 2, spos.Y - AXIS_OPT_SIZE / 2);
                                end = new Vector2(spos.X + AXIS_OFFSET, spos.Y + 1);
                                Primitives.DrawLine(spriteBatch, start, end, Color.Red, 4);

                                start = new Vector2(spos.X + AXIS_OFFSET - AXIS_OPT_SIZE / 2, spos.Y + AXIS_OPT_SIZE / 2);
                                end = new Vector2(spos.X + AXIS_OFFSET, spos.Y - 1);
                                Primitives.DrawLine(spriteBatch, start, end, Color.Red, 4);
                            }
                            else if (editorMode == EditorModes.Scale)
                            {
                                Primitives.DrawBoxFilled(spriteBatch,
                                    new Rectangle((int)spos.X + AXIS_OFFSET - AXIS_OPT_SIZE / 2, (int)spos.Y - AXIS_OPT_SIZE / 2, AXIS_OPT_SIZE, AXIS_OPT_SIZE)
                                    , Color.Red);
                            }
                        }
                    }

                    // Draw Transform Box 
                    Rectangle box = new Rectangle((int)spos.X - HANDLER_SIZE / 2, (int)spos.Y - HANDLER_SIZE / 2, HANDLER_SIZE, HANDLER_SIZE);
                    Primitives.DrawBox(spriteBatch, box, Color.Yellow, 2);

                    Primitives.DrawBoxFilled(spriteBatch, new Rectangle((int)spos.X - 4, (int)spos.Y - 4, 8, 8), Color.Yellow);

                    spriteBatch.End();

                    // Draw Measure Box
                    Rectangle measure = gameObject.MeasureDimension().CollisionRectangle;
                    if (measure.Width > HANDLER_SIZE * 2 && measure.Height > HANDLER_SIZE * 2)
                    {
                        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, null, null, null, null, SceneManager.ActiveCamera.ObjectTransform(gameObject));
                        Primitives.DrawBox(spriteBatch, measure, Color.Yellow, 3);
                        spriteBatch.End();
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void DrawGrid(int gridSizeX, int gridSizeY, int offsetX, int offsetY, int maxX, int maxY, Color color)
        {
            //int max = SceneManager.GameProject.EditorSettings.GridNumberOfLines / 2;

            // Draw Vertical Lines
            for (int x = 0; x <= maxX; x++)
            {
                Vector2 start = Vector2.Transform(new Vector2(x * gridSizeX + offsetX, -maxY * gridSizeY), SceneManager.ActiveCamera.TransformMatrix);
                Vector2 end = Vector2.Transform(new Vector2(x * gridSizeX + offsetX, maxY * gridSizeY), SceneManager.ActiveCamera.TransformMatrix);

                Primitives.DrawLine(spriteBatch, start, end, color, SceneManager.GameProject.EditorSettings.GridThickness);

                start = Vector2.Transform(new Vector2(-x * gridSizeX + offsetX, -maxY * gridSizeY), SceneManager.ActiveCamera.TransformMatrix);
                end = Vector2.Transform(new Vector2(-x * gridSizeX + offsetX, maxY * gridSizeY), SceneManager.ActiveCamera.TransformMatrix);

                Primitives.DrawLine(spriteBatch, start, end, color, SceneManager.GameProject.EditorSettings.GridThickness);
            }

            // Draw Horizontal Lines
            for (int y = 0; y <= maxY; y++)
            {
                Vector2 start = Vector2.Transform(new Vector2(-maxX * gridSizeX, y * gridSizeY + offsetY), SceneManager.ActiveCamera.TransformMatrix);
                Vector2 end = Vector2.Transform(new Vector2(maxX * gridSizeX, y * gridSizeY + offsetY), SceneManager.ActiveCamera.TransformMatrix);

                Primitives.DrawLine(spriteBatch, start, end, color, SceneManager.GameProject.EditorSettings.GridThickness);

                start = Vector2.Transform(new Vector2(-maxX * gridSizeX, -y * gridSizeY + offsetY), SceneManager.ActiveCamera.TransformMatrix);
                end = Vector2.Transform(new Vector2(maxX * gridSizeX, -y * gridSizeY + offsetY), SceneManager.ActiveCamera.TransformMatrix);

                Primitives.DrawLine(spriteBatch, start, end, color, SceneManager.GameProject.EditorSettings.GridThickness);
            }

            // Draw Center Lines
            Vector2 px, py;
            px = Vector2.Transform(new Vector2(-maxX * gridSizeX, offsetY), SceneManager.ActiveCamera.TransformMatrix);
            py = Vector2.Transform(new Vector2(maxX * gridSizeX, offsetY), SceneManager.ActiveCamera.TransformMatrix);
            Primitives.DrawLine(spriteBatch, px, py, color, SceneManager.GameProject.EditorSettings.GridThickness * 3);
            px = Vector2.Transform(new Vector2(offsetX, -maxY * gridSizeY), SceneManager.ActiveCamera.TransformMatrix);
            py = Vector2.Transform(new Vector2(offsetX, maxY * gridSizeY), SceneManager.ActiveCamera.TransformMatrix);
            Primitives.DrawLine(spriteBatch, px, py, color, SceneManager.GameProject.EditorSettings.GridThickness * 3);
        }

        /// <summary>
        /// 
        /// </summary>
        private void DrawSceneCamera()
        {
            Vector2 vertex = new Vector2((-SceneManager.GameProject.Settings.ScreenWidth / 2) + SceneManager.ActiveScene.Camera.Position.X,
                (-SceneManager.GameProject.Settings.ScreenHeight / 2) + SceneManager.ActiveScene.Camera.Position.Y);

            vertex = Vector2.Transform(vertex, SceneManager.ActiveCamera.TransformMatrix);

            string text = string.Format("Scene Camera ({0}x{1})", SceneManager.GameProject.Settings.ScreenWidth, SceneManager.GameProject.Settings.ScreenHeight);

            bmFontRenderer.DrawText(spriteBatch, new Vector2(vertex.X, vertex.Y - 25), text, Color.Yellow);
            //spriteBatch.DrawString(hudFont, text, new Vector2(vertex.X, vertex.Y - 20), Color.Yellow, 0.0f, Vector2.Zero, 0.8f, SpriteEffects.None, 1);
            Primitives.DrawBox(spriteBatch, new Rectangle((int)vertex.X, (int)vertex.Y, (int)(SceneManager.GameProject.Settings.ScreenWidth * Camera.Zoom), (int)(SceneManager.GameProject.Settings.ScreenHeight * SceneManager.ActiveCamera.Zoom)), Color.Black, 4);
            Primitives.DrawBox(spriteBatch, new Rectangle((int)vertex.X, (int)vertex.Y, (int)(SceneManager.GameProject.Settings.ScreenWidth * Camera.Zoom), (int)(SceneManager.GameProject.Settings.ScreenHeight * SceneManager.ActiveCamera.Zoom)), Color.Yellow, 2);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="elapsedTime"></param>
        protected override void Update(GameTime gameTime)
        {
            if (SceneManager.GameProject != null)
            {
                GlobalInput();
            }

            if (SceneManager.ActiveScene != null)
            {
                if (this.Focused)
                {
                    // Update list of gameObjects:
                    sceneGameObjects = GameObject.GetAllGameObjects();

                    Input(gameTime);
                }
            }

            SceneManager.Update(gameTime);

            mouseLastPosition = new Vector2(mouseWorldPosition.X, mouseWorldPosition.Y);
        }

        /// <summary>
        /// 
        /// </summary>
        private void GlobalInput()
        {
            //if (GameInput.IsKeyPressed(Keys.F6))
            //{
            //    CompilerWindow cf = new CompilerWindow();
            //    cf.Show();
            //}

            if (TileSetMode)
            {
                if (GameInput.IsKeyDown(Keys.Escape))
                {
                    EditorHandler.SelectedGameObjects.Clear();
                    EditorHandler.ChangeSelectedObjects();
                }
            }

            //if (GameInput.IsKeyPressed(Keys.F5))
            //    EditorCommands.DebugGame();

            if (this.Focused)
            {
                if (GameInput.IsKeyPressed(Keys.Delete))
                {
                    if (EditorHandler.SelectedGameObjects.Count > 0 &&
                        MessageBox.Show("Are you sure you want to delete the selected game object(s)?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        foreach (GameObject gameObject in EditorHandler.SelectedGameObjects)
                        {
                            GameObject.Remove(gameObject);
                        }

                        EditorHandler.SceneTreeView.CreateView();
                        EditorHandler.SelectedGameObjects = new List<GameObject>();
                        EditorHandler.ChangeSelectedObjects();
                    }
                }
                else if (GameInput.IsKeyPressed(Keys.Q))
                {
                    editorMode = EditorModes.Select;
                }
                else if (GameInput.IsKeyPressed(Keys.E))
                {
                    editorMode = EditorModes.Move;
                }
                else if (GameInput.IsKeyPressed(Keys.R))
                {
                    editorMode = EditorModes.Rotate;
                }
                else if (GameInput.IsKeyPressed(Keys.T))
                {
                    editorMode = EditorModes.Scale;
                }
                else if (GameInput.IsKeyPressed(Keys.G))
                {
                    SceneManager.GameProject.EditorSettings.ShowGrid = !SceneManager.GameProject.EditorSettings.ShowGrid;
                }
            }
        }
        private bool leftMouseKeyStateChanged = false;
        /// <summary>
        /// 
        /// </summary>
        public void Input(GameTime gameTime)
        {
            delta = (float)gameTime.ElapsedGameTime.Milliseconds;
            if (GameInput.IsKeyDown(Keys.LeftShift)) delta *= 3;

            //Console.WriteLine(MousePosition.X);

            if (leftMouseKeyDown)
            {
                if (!mouseDragStarted)
                {
                    //mouseClickPosition = mouseWorldPosition;
                    mouseDragStarted = true;
                }
            }
            else
            {
                mouseDragStarted = false;
            }

            // Camera Movement
            if ((GameInput.IsKeyDown(Keys.W)) && GameInput.IsKeyUp(Keys.LeftControl)) SceneManager.ActiveCamera.Position += (new Vector2(0, -delta));
            if ((GameInput.IsKeyDown(Keys.S)) && GameInput.IsKeyUp(Keys.LeftControl)) SceneManager.ActiveCamera.Position += (new Vector2(0, +delta));
            if ((GameInput.IsKeyDown(Keys.A)) && GameInput.IsKeyUp(Keys.LeftControl)) SceneManager.ActiveCamera.Position += (new Vector2(-delta, 0));
            if ((GameInput.IsKeyDown(Keys.D)) && GameInput.IsKeyUp(Keys.LeftControl)) SceneManager.ActiveCamera.Position += (new Vector2(+delta, 0));

            if (GameInput.IsKeyPressed(Keys.Down))
                foreach (GameObject obj in EditorHandler.SelectedGameObjects)
                    obj.Transform.Translate(Vector2.UnitY);
            else if (GameInput.IsKeyPressed(Keys.Up))
                foreach (GameObject obj in EditorHandler.SelectedGameObjects)
                    obj.Transform.Translate(-Vector2.UnitY);
            else if (GameInput.IsKeyPressed(Keys.Right))
                foreach (GameObject obj in EditorHandler.SelectedGameObjects)
                    obj.Transform.Translate(Vector2.UnitX);

            else if (GameInput.IsKeyPressed(Keys.Left))
                foreach (GameObject obj in EditorHandler.SelectedGameObjects)
                    obj.Transform.Translate(-Vector2.UnitX);


            // Camera Zoom
            if (GameInput.IsKeyDown(Keys.OemPlus)) SceneManager.ActiveCamera.Zoom += 0.01f;
            if (GameInput.IsKeyDown(Keys.OemMinus)) SceneManager.ActiveCamera.Zoom -= 0.01f;
            if (GameInput.IsKeyDown(Keys.D0)) SceneManager.ActiveCamera.Zoom = 1.0f;

            if (leftMouseKeyDown && !lastLeftKeyState)
            {
                LeftMouseKeyPressed = true;
            }
            else
            {
                LeftMouseKeyPressed = false;
            }

            if (lastLeftKeyState != leftMouseKeyDown)
            {
                leftMouseKeyStateChanged = true;
            }

            if (leftMouseKeyDown && leftMouseKeyStateChanged)
            {
                mouseClickPosition = new Vector2(mouseWorldPosition.X, mouseWorldPosition.Y);
                leftMouseKeyStateChanged = false;
            }

            lastLeftKeyState = leftMouseKeyDown;

            // mouse span
            if (MiddleMouseKeyDown)
            {
                if (!panStarted)
                {
                    panStarted = true;
                    panStart = mouseWorldPosition;
                }
                else
                {
                    if (panMouseLastPos != mouseWorldPosition)
                    {
                        Vector2 alpha = mouseWorldPosition - panStart;

                        SceneManager.ActiveCamera.Position = new Vector2()
                        {
                            X = (int)(SceneManager.ActiveCamera.Position.X - alpha.X),
                            Y = (int)(SceneManager.ActiveCamera.Position.Y - alpha.Y)
                        };

                        panMouseLastPos = mouseWorldPosition;
                    }
                }
            }
            else
            {
                panStart = Vector2.Zero;
                panStarted = false;
            }

            if (GameInput.IsKeyDown(Keys.O)) CenterCameraObject();
            else if (GameInput.IsKeyPressed(Keys.C) && !GameInput.IsKeyDown(Keys.LeftControl)) CenterCamera();

            if (GameInput.IsKeyDown(Keys.LeftControl) && GameInput.IsKeyPressed(Keys.Z)) EditorHandler.UnDoRedo.Undo(1);
            if (GameInput.IsKeyDown(Keys.LeftControl) && GameInput.IsKeyPressed(Keys.Y)) EditorHandler.UnDoRedo.Redo(1);


            if (GameInput.IsKeyDown(Keys.LeftControl) && GameInput.IsKeyPressed(Keys.C)) EditorCommands.CopySelectedObjects();
            if (GameInput.IsKeyDown(Keys.LeftControl) && GameInput.IsKeyPressed(Keys.V)) EditorCommands.PasteSelectedObjects();

            //if (GameInput.IsKeyDown(Keys.LeftControl) && GameInput.IsKeyDown(Keys.C))
            //{
            //    if (EditorHandler.SelectedGameObjects.Count > 0)
            //    {
            //        Clipboard.SetData("GameObject", EditorHandler.SelectedGameObjects);
            //    }
            //}

            UpdateCurrentCursor();

            // the selected game object is a tileset?
            if (TileSetMode)
            {
                TilesetTool();
            }
            else
            {
                if (editorMode == EditorModes.Select)
                    SelectTool();

                SelectedObjectInput();
            }

            //if (GameInput.IsKeyDown(Keys.LeftControl) && GameInput.IsKeyPressed(Keys.S)) SceneManager.SaveActiveScene();
        }

        void SceneEditorControl_MouseWheel(object sender, MouseEventArgs e)
        {
            int mwheeldelta = e.Delta;
            if (mwheeldelta > 0)
            {
                float zoom = (float)Math.Round(SceneManager.ActiveCamera.Zoom * 100f) + 10.0f;
                SceneManager.ActiveCamera.Zoom = zoom / 100f;
            }
            if (mwheeldelta < 0)
            {
                float zoom = (float)Math.Round(SceneManager.ActiveCamera.Zoom * 100f) - 10.0f;
                SceneManager.ActiveCamera.Zoom = zoom / 100f;
            }
        }

        private void TilesetTool()
        {
            if (tilesetMode == TilesetModes.Pencil)
            {
                if (LeftMouseKeyDown)
                {
                    if (!tilesetDragStarted)
                    {
                        memTiles = (EditorHandler.SelectedGameObjects[0] as Tileset).DeepCopy();
                    }

                    (EditorHandler.SelectedGameObjects[0] as Tileset).PlaceTiles(EditorHandler.TilesetBrushControl.CurrentSelectionXNA, SnapToTilesetGrid(mouseWorldPosition));

                    tilesetDragStarted = true;
                }
                else
                {
                    if (tilesetDragStarted)
                    {
                        TilesetCommand tc = new TilesetCommand((EditorHandler.SelectedGameObjects[0] as Tileset).Tiles, memTiles, (EditorHandler.SelectedGameObjects[0] as Tileset));
                        EditorHandler.UnDoRedo.InsertUndoRedo(tc);

                        tilesetDragStarted = false;
                    }
                }
            }
            else if (tilesetMode == TilesetModes.Rectangle)
            {
                if (LeftMouseKeyDown)
                {
                    if (tilesetDragStarted)
                    {
                        Vector2 releasePos = SnapToTilesetGrid(mouseWorldPosition);
                        tilesetSelectedArea = MathExtension.RectangleFromVectors(tilesetMouseDownPos, releasePos);

                        tilesetSelectedArea = new Rectangle()
                        {
                            X = tilesetSelectedArea.X,
                            Y = tilesetSelectedArea.Y,
                            Width = tilesetSelectedArea.Width + (EditorHandler.SelectedGameObjects[0] as Tileset).TileWidth,
                            Height = tilesetSelectedArea.Height + (EditorHandler.SelectedGameObjects[0] as Tileset).TileHeight
                        };
                    }
                    else
                    {
                        tilesetDragStarted = true;
                        tilesetMouseDownPos = SnapToTilesetGrid(mouseWorldPosition);
                    }
                }
                else
                {
                    if (tilesetDragStarted)
                    {
                        if (EditorHandler.TilesetBrushControl.CurrentSelectionXNA != Rectangle.Empty)
                        {
                            Tile[,] _tiles = (EditorHandler.SelectedGameObjects[0] as Tileset).DeepCopy();

                            (EditorHandler.SelectedGameObjects[0] as Tileset).PlaceTiles(EditorHandler.TilesetBrushControl.CurrentSelectionXNA, tilesetSelectedArea);

                            TilesetCommand tc = new TilesetCommand((EditorHandler.SelectedGameObjects[0] as Tileset).Tiles, _tiles, (EditorHandler.SelectedGameObjects[0] as Tileset));
                            EditorHandler.UnDoRedo.InsertUndoRedo(tc);
                        }
                    }

                    tilesetSelectedArea = Rectangle.Empty;
                    tilesetDragStarted = false;
                }
            }
            else if (tilesetMode == TilesetModes.Eraser)
            {
                if (LeftMouseKeyDown)
                {
                    if (tilesetDragStarted)
                    {
                        Vector2 releasePos = SnapToTilesetGrid(mouseWorldPosition);
                        tilesetSelectedArea = MathExtension.RectangleFromVectors(tilesetMouseDownPos, releasePos);

                        tilesetSelectedArea = new Rectangle()
                        {
                            X = tilesetSelectedArea.X,
                            Y = tilesetSelectedArea.Y,
                            Width = tilesetSelectedArea.Width + (EditorHandler.SelectedGameObjects[0] as Tileset).TileWidth,
                            Height = tilesetSelectedArea.Height + (EditorHandler.SelectedGameObjects[0] as Tileset).TileHeight
                        };
                    }
                    else
                    {
                        tilesetDragStarted = true;
                        tilesetMouseDownPos = SnapToTilesetGrid(mouseWorldPosition);
                    }
                }
                else
                {
                    if (tilesetDragStarted)
                    {
                        Tile[,] _tiles = (EditorHandler.SelectedGameObjects[0] as Tileset).DeepCopy();

                        (EditorHandler.SelectedGameObjects[0] as Tileset).RemoveTiles(tilesetSelectedArea);

                        TilesetCommand tc = new TilesetCommand((EditorHandler.SelectedGameObjects[0] as Tileset).Tiles, _tiles, (EditorHandler.SelectedGameObjects[0] as Tileset));
                        EditorHandler.UnDoRedo.InsertUndoRedo(tc);
                    }

                    tilesetSelectedArea = Rectangle.Empty;
                    tilesetDragStarted = false;
                }
            }
            else if (tilesetMode == TilesetModes.AddColumn)
            {
                if (LeftMouseKeyPressed)
                {
                    Tile[,] _tiles = (EditorHandler.SelectedGameObjects[0] as Tileset).DeepCopy();

                    (EditorHandler.SelectedGameObjects[0] as Tileset).AddColumn((int)mouseWorldPosition.X);

                    TilesetCommand tc = new TilesetCommand((EditorHandler.SelectedGameObjects[0] as Tileset).Tiles, _tiles, (EditorHandler.SelectedGameObjects[0] as Tileset));
                    EditorHandler.UnDoRedo.InsertUndoRedo(tc);
                }
            }
            else if (tilesetMode == TilesetModes.AddRow)
            {
                if (LeftMouseKeyPressed)
                {
                    Tile[,] _tiles = (EditorHandler.SelectedGameObjects[0] as Tileset).DeepCopy();

                    (EditorHandler.SelectedGameObjects[0] as Tileset).AddRow((int)mouseWorldPosition.Y);

                    TilesetCommand tc = new TilesetCommand((EditorHandler.SelectedGameObjects[0] as Tileset).Tiles, _tiles, (EditorHandler.SelectedGameObjects[0] as Tileset));
                    EditorHandler.UnDoRedo.InsertUndoRedo(tc);
                }
            }
            else if (tilesetMode == TilesetModes.RemoveColumn)
            {
                if (LeftMouseKeyPressed)
                {
                    Tile[,] _tiles = (EditorHandler.SelectedGameObjects[0] as Tileset).DeepCopy();

                    (EditorHandler.SelectedGameObjects[0] as Tileset).RemoveColumn((int)mouseWorldPosition.X);

                    TilesetCommand tc = new TilesetCommand((EditorHandler.SelectedGameObjects[0] as Tileset).Tiles, _tiles, (EditorHandler.SelectedGameObjects[0] as Tileset));
                    EditorHandler.UnDoRedo.InsertUndoRedo(tc);
                }
            }
            else if (tilesetMode == TilesetModes.RemoveRow)
            {
                if (LeftMouseKeyPressed)
                {
                    Tile[,] _tiles = (EditorHandler.SelectedGameObjects[0] as Tileset).DeepCopy();

                    (EditorHandler.SelectedGameObjects[0] as Tileset).RemoveRow((int)mouseWorldPosition.Y);

                    TilesetCommand tc = new TilesetCommand((EditorHandler.SelectedGameObjects[0] as Tileset).Tiles, _tiles, (EditorHandler.SelectedGameObjects[0] as Tileset));
                    EditorHandler.UnDoRedo.InsertUndoRedo(tc);
                }
            }
        }

        private void SelectTool()
        {
            if (LeftMouseKeyDown)
            {
                if (!selectionStarted)
                {
                    if (SceneManager.GameProject.EditorSettings.SnapToGrid)
                    {
                        selectionStart = SnapToGrid(new Vector2(mouseWorldPosition.X, mouseWorldPosition.Y));
                    }
                    else
                    {
                        selectionStart = new Vector2(mouseWorldPosition.X, mouseWorldPosition.Y);
                    }

                    selectionStarted = true;
                }
                else
                {
                    if (SceneManager.GameProject.EditorSettings.SnapToGrid)
                    {
                        selectionEnd = SnapToGrid(new Vector2(mouseWorldPosition.X + SceneManager.GameProject.EditorSettings.GridSpacing, mouseWorldPosition.Y + SceneManager.GameProject.EditorSettings.GridSpacing));
                    }
                    else
                    {
                        selectionEnd = new Vector2(mouseWorldPosition.X, mouseWorldPosition.Y);
                    }

                    //Console.WriteLine(selectionStart + ":" + selectionEnd);

                    selectionArea = MathExtension.RectangleFromVectors(selectionStart, selectionEnd);
                }

                CheckSelectCollision();
            }
            else
            {
                if (selectionStarted)
                {
                    if (!GameInput.IsKeyDown(Keys.LeftControl))
                        selectionArea = Rectangle.Empty;

                    if (EditorHandler.SelectedGameObjects.Count > 0)
                    {
                        // EditorHandler.PropertyGrid.SelectedObjects = EditorHandler.SelectedGameObjects.ToArray();
                        EditorHandler.ChangeSelectedObjects();
                    }
                    else
                    {
                        //EditorHandler.PropertyGrid.SelectedObject = null;
                        //EditorHandler.PropertyPage.Text = "Nothing selected";
                        EditorHandler.ChangeSelectedObjects();
                    }
                    //EditorHandler.ChangeSelectedObject(EditorHandler.SelectedGameObjects[0]);
                }

                selectionStarted = false;
            }
        }

        private void CheckSelectCollision()
        {
            if (!GameInput.IsKeyDown(Keys.LeftControl))
                EditorHandler.SelectedGameObjects = new List<GameObject>();

            foreach (GameObject gameObject in sceneGameObjects)
            {
                if (gameObject.Visible && gameObject.Selectable && selectionArea.Intersects(gameObject.MeasureDimension())) //new Rectangle((int)gameObject.Transform.Position.X, (int)gameObject.Transform.Position.Y, 1, 1)))
                {
                    if (selectionArea.Width <= 2 && selectionArea.Height <= 2 && !GameInput.IsKeyDown(Keys.LeftControl))
                    {
                        EditorHandler.SelectedGameObjects.Clear();
                    }

                    if (!EditorHandler.SelectedGameObjects.Contains(gameObject))
                        EditorHandler.SelectedGameObjects.Add(gameObject);
                }
            }

            Fixture detected = SceneManager.ActiveScene.World.TestPoint(ConvertUnits.ToSimUnits(mouseWorldPosition));
            if (detected != null)
            {
                if (detected.Body.GameObject.Visible && detected.Body.GameObject.Selectable && !EditorHandler.SelectedGameObjects.Contains(detected.Body.GameObject))
                    EditorHandler.SelectedGameObjects.Add(detected.Body.GameObject);
            }

            EditorHandler.SceneTreeView.SelectionUpdate();
        }

        private void UpdateCurrentCursor()
        {
            if (panStarted)
            {
                Cursor = Cursors.Hand;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Vector2 SnapToGrid(Vector2 input)
        {
            Vector2 result = Vector2.Zero;
            result.X = SceneManager.GameProject.EditorSettings.GridSpacing * (int)Math.Floor(input.X / SceneManager.GameProject.EditorSettings.GridSpacing);
            result.Y = SceneManager.GameProject.EditorSettings.GridSpacing * (int)Math.Floor(input.Y / SceneManager.GameProject.EditorSettings.GridSpacing);

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Vector2 SnapToTilesetGrid(Vector2 input)
        {
            Vector2 result = input;
            result.X = (EditorHandler.SelectedGameObjects[0] as Tileset).TileWidth * (int)Math.Floor(result.X / (EditorHandler.SelectedGameObjects[0] as Tileset).TileWidth);
            result.Y = (EditorHandler.SelectedGameObjects[0] as Tileset).TileHeight * (int)Math.Floor(result.Y / (EditorHandler.SelectedGameObjects[0] as Tileset).TileHeight);

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        private void InsertUndoRedo()
        {
            switch (editorMode)
            {
                case EditorModes.Move:
                    foreach (GameObject gameObject in EditorHandler.SelectedGameObjects)
                    {
                        EditorHandler.UnDoRedo.InsertInUnDoRedoForMove(gameObject.Transform.Position,
                            beforeState[gameObject].Position, gameObject);
                    }
                    break;
                case EditorModes.Scale:
                    foreach (GameObject gameObject in EditorHandler.SelectedGameObjects)
                    {
                        EditorHandler.UnDoRedo.InsertInUnDoRedoForScale(gameObject.Transform.Scale,
                            beforeState[gameObject].Scale, gameObject);
                    }
                    break;
                case EditorModes.Rotate:
                    foreach (GameObject gameObject in EditorHandler.SelectedGameObjects)
                    {
                        EditorHandler.UnDoRedo.InsertInUnDoRedoForRotate(gameObject.Transform.Rotation,
                            beforeState[gameObject].Rotation, gameObject);
                    }
                    break;
            }

            beforeState.Clear();
        }

        private void CheckCursor()
        {
            switch (editorMode)
            {
                case EditorModes.Move:
                    if (hoverXAxis)
                        Cursor = Cursors.NoMoveHoriz;
                    else if (hoverYAxis)
                        Cursor = Cursors.NoMoveVert;
                    else
                        Cursor = Cursors.NoMove2D;
                    break;
                case EditorModes.Rotate:
                    Cursor = Cursors.NoMove2D;
                    break;
                case EditorModes.Scale:
                    if (hoverXAxis)
                        Cursor = Cursors.NoMoveHoriz;
                    else if (hoverYAxis)
                        Cursor = Cursors.NoMoveVert;
                    else
                        Cursor = Cursors.NoMove2D;
                    break;
                default:
                    Cursor = Cursors.Default;
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void HandleTransformations()
        {
            if (editorMode == EditorModes.Move)
            {
                foreach (GameObject gameObject in EditorHandler.SelectedGameObjects)
                {
                    if (GameInput.IsKeyDown(Keys.X) || usingXAxis)
                    {
                        gameObject.Transform.Position =
                                new Vector2(gameObject.Transform.Position.X + (mouseWorldPosition.X - mouseLastPosition.X), gameObject.Transform.Position.Y);

                    }
                    else if (GameInput.IsKeyDown(Keys.Y) || usingYAxis)
                    {
                        gameObject.Transform.Position =
                            new Vector2(gameObject.Transform.Position.X, gameObject.Transform.Position.Y + (mouseWorldPosition.Y - mouseLastPosition.Y));
                    }
                    else
                    {
                        gameObject.Transform.Position += mouseWorldPosition - mouseLastPosition;
                    }
                }
            }
            else if (editorMode == EditorModes.Rotate)
            {
                foreach (GameObject gameObject in EditorHandler.SelectedGameObjects)
                {
                    // value relative to the selected object [0] position
                    float value = (float)Math.Atan2(mouseWorldPosition.Y - EditorHandler.SelectedGameObjects[0].Transform.Position.Y, mouseWorldPosition.X - EditorHandler.SelectedGameObjects[0].Transform.Position.X);

                    if (GameInput.IsKeyDown(Keys.Z))
                    {
                        if (value < -((float)Math.PI / 4.0f) && value > -((float)Math.PI / 4.0f) * 3)
                        {
                            // Top Snap
                            gameObject.Transform.Rotation = -(float)Math.PI / 2.0f;
                        }
                        else if (value > ((float)Math.PI / 4.0f) && value < ((float)Math.PI / 4.0f) * 3)
                        {
                            // Bottom Snap
                            gameObject.Transform.Rotation = (float)Math.PI / 2.0f;
                        }
                        else if (value > -((float)Math.PI / 4.0f) && value < ((float)Math.PI / 4.0f))
                        {
                            // Right Snap
                            gameObject.Transform.Rotation = 0.0f;
                        }
                        else
                        {
                            // Left Snap
                            gameObject.Transform.Rotation = (float)Math.PI;
                        }
                    }
                    else
                    {
                        gameObject.Transform.Rotation = value;
                    }
                }
            }
            else if (editorMode == EditorModes.Scale)
            {
                // todo: proportional scale
                foreach (GameObject gameObject in EditorHandler.SelectedGameObjects)
                {
                    // SCALE A = ?
                    // POSI = X (X => primeiro clique) -- 100%
                    // POSA = SCALE A
                    if (usingYAxis)
                    {
                        gameObject.Transform.Scale = new Vector2(gameObject.Transform.Scale.X, Math.Abs(((gameObject.Transform.Position.Y - mouseWorldPosition.Y) * beforeState[gameObject].Scale.Y / (gameObject.Transform.Position.Y - mouseClickPosition.Y))));
                    }
                    else if (usingXAxis)
                    {
                        gameObject.Transform.Scale = new Vector2(Math.Abs((gameObject.Transform.Position.X - mouseWorldPosition.X) * beforeState[gameObject].Scale.X / (gameObject.Transform.Position.X - mouseClickPosition.X)), gameObject.Transform.Scale.Y);
                    }
                    else
                    {
                        float dx = (gameObject.Transform.Position.X - mouseWorldPosition.X) - (gameObject.Transform.Position.X - mouseClickPosition.X);
                        float dy = (gameObject.Transform.Position.Y - mouseWorldPosition.Y) - (gameObject.Transform.Position.Y - mouseClickPosition.Y);
                        float h = (dx * dx) + (dy * dy);

                        gameObject.Transform.Scale = new Vector2(Math.Abs((gameObject.Transform.Position.X - mouseWorldPosition.X) * beforeState[gameObject].Scale.X / (gameObject.Transform.Position.X - mouseClickPosition.X)),
                            Math.Abs(((gameObject.Transform.Position.Y - mouseWorldPosition.Y) * beforeState[gameObject].Scale.Y / (gameObject.Transform.Position.Y - mouseClickPosition.Y))));
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void SelectedObjectInput()
        {
            if (EditorHandler.SelectedGameObjects.Count == 0)
            {
                if (!panStarted)
                    Cursor = Cursors.Default;

                return;
            }

            foreach (GameObject gameObject in EditorHandler.SelectedGameObjects)
            {
                Vector2 spos = gameObject.Transform.Position;

                Rectangle selectedObjectBoundingBox =
                       new Rectangle((int)spos.X - HANDLER_SIZE / 2, (int)spos.Y - HANDLER_SIZE / 2, HANDLER_SIZE, HANDLER_SIZE);

                Rectangle YAxisBoundingBox = new Rectangle((int)spos.X - AXIS_OPT_SIZE / 2, (int)spos.Y - AXIS_OFFSET - AXIS_OPT_SIZE / 2, AXIS_OPT_SIZE, AXIS_OFFSET - (HANDLER_SIZE / 2));
                Rectangle XAxisBoundingBox = new Rectangle((int)spos.X + (HANDLER_SIZE / 2), (int)spos.Y - AXIS_OPT_SIZE / 2, AXIS_OFFSET - (HANDLER_SIZE / 2) + AXIS_OPT_SIZE / 2, AXIS_OPT_SIZE);

                bool intersects = false;

                if (!objectHandled && !panStarted)
                {
                    hoverXAxis = false;
                    hoverYAxis = false;

                    // The mouse is intersecting the selected object?
                    if (MouseBoundingBox.Intersects(YAxisBoundingBox))
                    {
                        intersects = true;
                        hoverYAxis = true;
                        CheckCursor(); //

                        if (leftMouseKeyDown)
                        {
                            usingYAxis = true;

                            SaveState();

                            objectHandled = true;
                        }
                    }
                    else if (MouseBoundingBox.Intersects(XAxisBoundingBox))
                    {
                        intersects = true;
                        hoverXAxis = true;
                        CheckCursor(); //

                        if (leftMouseKeyDown)
                        {
                            usingXAxis = true;

                            SaveState();

                            objectHandled = true;
                        }
                    }
                    else if ((MouseBoundingBox.Intersects(selectedObjectBoundingBox) || MouseBoundingBox.Intersects(gameObject.MeasureDimension())) && gameObject.Selectable)
                    {
                        intersects = true;

                        CheckCursor();

                        if (leftMouseKeyDown)
                        {
                            // Save the current transform for Undo / Redo purposes
                            SaveState();

                            objectHandled = true;
                        }
                    }
                }
                else if (panStarted)
                {
                    Cursor = Cursors.Hand;
                }

                if (!leftMouseKeyDown)
                {
                    if (objectHandled)
                    {
                        // Save Undo / Redo history
                        InsertUndoRedo();

                        // Snap to grid?
                        if (SceneManager.GameProject.EditorSettings.SnapToGrid)
                        {
                            gameObject.Transform.Position =
                                SnapToGrid(gameObject.Transform.Position);
                        }
                    }

                    if (!intersects && !panStarted)
                        Cursor = Cursors.Default;

                    objectHandled = false;
                    usingYAxis = false;
                    usingXAxis = false;
                }
            }

            if (objectHandled)
            {
                HandleTransformations();
            }
        }

        private void SaveState()
        {
            foreach (GameObject gameObject in EditorHandler.SelectedGameObjects)
            {
                beforeState[gameObject] = (Transform)gameObject.Transform.DeepCopy();
                beforeState[gameObject].GameObject = new GameObject();
            }
        }

        void SceneViewGameControl_MouseMove(object sender, MouseEventArgs e)
        {
            SetMousePosition(new Microsoft.Xna.Framework.Vector2(e.X, e.Y));
        }

        void SceneViewGameControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                LeftMouseKeyDown = false;
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Middle)
            {
                MiddleMouseKeyDown = false;
            }
        }

        void SceneViewGameControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                LeftMouseKeyDown = true;
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Middle)
            {
                MiddleMouseKeyDown = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void CenterCamera()
        {
            SceneManager.ActiveCamera.Position = new Vector2(0, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        public void CenterCameraObject()
        {
            if (EditorHandler.SelectedGameObjects.Count > 0)
            {
                SceneManager.ActiveCamera.Position =
                    new Vector2(EditorHandler.SelectedGameObjects[0].Transform.Position.X,
                    EditorHandler.SelectedGameObjects[0].Transform.Position.Y);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pos"></param>
        public void SetMousePosition(Vector2 pos)
        {
            controlPosition = pos;
            mouseWorldPosition = Vector2.Transform(pos, Matrix.Invert(SceneManager.ActiveCamera.TransformMatrix));
        }

        Vector2 controlPosition;

        #endregion
    }
}
