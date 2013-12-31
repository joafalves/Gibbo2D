using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Gibbo.Library;
using System.Reflection;
using ComponentFactory.Krypton.Toolkit;
using System.Deployment.Application;
using Microsoft.Xna.Framework;

namespace Gibbo.Editor
{
    public partial class Editor : KryptonForm
    {
        #region dll imports

        #endregion

        #region fields

        private bool shown = false;
        private EditorModes lastEditorMode = EditorModes.Select;

        private string pluginsPath = AppDomain.CurrentDomain.BaseDirectory + "\\Plugins";
        private Dictionary<string, Assembly> plugins = new Dictionary<string, Assembly>();

        #endregion

        #region constructors

        /// <summary>
        /// 
        /// </summary>
        public Editor()
        {
            InitializeComponent();
            Initialize();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectPath"></param>
        public Editor(string projectPath)
        {
            InitializeComponent();
            Initialize();

            LoadProject(projectPath);
        }

        #endregion

        #region methods

        private void Initialize()
        {
            EditorHandler.PropertyPage = kryptonPage4;
            EditorHandler.PropertyGrid = this.propertyGrid1;
            EditorHandler.SceneTreeView = this.sceneTreeView;
            EditorHandler.UnDoRedo = new UndoRedo();
            EditorHandler.StatusLabel = statusLabel;
            EditorHandler.BrushControl = this.brushControl1;
            EditorHandler.EditorSplitterContainer = this.splitContainer3;

            this.splitContainer3.Panel2Collapsed = true;

            LoadPlugins();

            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
        }

        /// <summary>
        /// Loads all available plugins to the editor.
        /// </summary>
        private void LoadPlugins()
        {
            if (!Directory.Exists(pluginsPath))
                Directory.CreateDirectory(pluginsPath);

            Assembly assembly = Assembly.GetExecutingAssembly();

            foreach (string filename in Directory.GetFiles(pluginsPath))
            {
                // The file has a .dll extension?
                if (Path.GetExtension(filename).ToLower().Equals(".dll"))
                {
                    plugins[filename] = Assembly.LoadFrom(filename);
                }
            }

            if (plugins.Count > 0)
            {
                pluginsToolStripMenuItem.Visible = true;

                for (int i = 0; i < plugins.Count; i++)
                {
                    Type[] types = plugins.ElementAt(i).Value.GetTypes();

                    foreach (Type type in types)
                    {
                        if (type.GetInterfaces().Contains(typeof(IPlugin)))
                        {
                            IPlugin plugin = (IPlugin)Activator.CreateInstance(type);
                            plugin.Initialize();

                            ToolStripMenuItem item = new ToolStripMenuItem();
                            item.Text = plugin.Name;
                            item.Tag = plugin;
                            item.Click += pluginItem_Click;

                            pluginsToolStripMenuItem.DropDownItems.Add(item);

                            //Console.WriteLine("plugin: " + type.FullName);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void ResetUI()
        {
            propertyGrid1.SelectedObject = null;
            kryptonPage4.Text = "Property Editor";
            projectTreeView.CreateView(SceneManager.GameProject.ProjectPath);

            //Reset Zoom ComboBox
            zoomCombo.Items.Clear();
            for (int i = 25; i <= 300; i += 25)
            {
                zoomCombo.Items.Add(i.ToString() + "%");
            }
            zoomCombo.SelectedText = "100%";

            //SceneManager.ActiveScene = null;

            // OPTIONAL?: Reset other objects
        }

        /// <summary>
        /// Updates the UI of this form
        /// </summary>
        public void UpdateUI()
        {
            if (SceneManager.GameProject != null)
            {
                string sceneName = (SceneManager.ActiveScene != null) ? sceneName = Path.GetFileNameWithoutExtension(SceneManager.ActiveScenePath) : "";
                this.Text = string.Format("{0} - {1} - {2}", Properties.Resources.DisplayName,
                    SceneManager.GameProject.ProjectName, sceneName);

                showGridBtn.Checked = SceneManager.GameProject.EditorSettings.ShowGrid;
                gridSnappingBtn.Checked = SceneManager.GameProject.EditorSettings.SnapToGrid;
                showCollisionsButton.Checked = SceneManager.GameProject.EditorSettings.ShowCollisions;

               // Console.WriteLine(sceneEditorControl1.EditorMode + ":" + lastEditorMode);

                if (sceneEditorControl1.EditorMode != lastEditorMode)
                {
                    selectStripBtn.Checked = false;
                    moveStripButton.Checked = false;
                    scaleStripButton.Checked = false;
                    rotateStripButton.Checked = false;

                    if (sceneEditorControl1.EditorMode == EditorModes.Select)
                    {
                        selectStripBtn.Checked = true;
                    }
                    else if (sceneEditorControl1.EditorMode == EditorModes.Move)
                    {
                        moveStripButton.Checked = true;
                    }
                    else if (sceneEditorControl1.EditorMode == EditorModes.Rotate)
                    {
                        rotateStripButton.Checked = true;
                    }
                    else if (sceneEditorControl1.EditorMode == EditorModes.Scale)
                    {
                        scaleStripButton.Checked = true;
                    }

                    lastEditorMode = sceneEditorControl1.EditorMode;
                }

                //if (sceneEditorControl1.Camera.Zoom != lastZoom)
                //{
                //    lastZoom = sceneEditorControl1.Camera.Zoom;
                //    float zoom = (float)Math.Round(sceneEditorControl1.Camera.Zoom * 10) * 10.0f;
                //    zoomCombo.Text = zoom.ToString() + "%";
                //}
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void OpenScene()
        {
            // TODO: implement a safe open scene (save current, etc..)

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open scene";
            ofd.Filter = @"(*.scene)|*.scene";

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (!SceneManager.LoadScene(ofd.FileName))
                {
                    KryptonMessageBox.Show("Ups!\nCannot load Scene", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    EditorHandler.ChangeSelectedObject(SceneManager.ActiveScene);
                    EditorHandler.SceneTreeView.CreateView();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Reload()
        {
            ResetUI();
            UpdateUI();

            SceneManager.Content = new Microsoft.Xna.Framework.Content.ContentManager(sceneEditorControl1.Services, SceneManager.GameProject.ProjectPath);
            SceneManager.ActiveScene = null;
        }

        /// <summary>
        /// Loads a saved project
        /// </summary>
        public bool LoadProject()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open project";
            ofd.Filter = @"(*.gibbo)|*.gibbo";

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                LoadProject(ofd.FileName);
                EditorCommands.AddToProjectHistory(ofd.FileName);
            }

            EditorCommands.ShowOutputMessage("Project loaded with sucess");

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private bool LoadProject(string filename)
        {
            try
            {
                if (File.Exists(filename))
                {
                    SceneManager.GameProject = GibboProject.Load(filename);

                    File.Copy("Gibbo.Library.dll", SceneManager.GameProject.ProjectPath + "\\Gibbo.Library.dll", true);
                    File.Copy("Project Templates\\Gibbo.Engine.Windows.exe", SceneManager.GameProject.ProjectPath + "\\Gibbo.Engine.Windows.exe", true);
                    GibboHelper.CopyDirectory("Project Templates\\libs", SceneManager.GameProject.ProjectPath + "", true);
                    
                    SceneManager.ActiveScene = null;
                    EditorHandler.SceneTreeView.CreateView();

                    //CompileScripts(true);        
                    CompilerForm cf = new CompilerForm();
                    bool success = cf.ShowDialog() == System.Windows.Forms.DialogResult.Yes ? true : false;

                    Reload();

                    if (success)
                    {
                        LoadLastScene();
                        EditorCommands.ShowOutputMessage("Project loaded with sucess");

                        kryptonNavigator1.SelectedIndex = 1;
                    }
                    else
                    {
                        EditorCommands.ShowOutputMessage("Project loaded with script errors");
                    }

                    EditorHandler.Settings = new IniFile(SceneManager.GameProject.ProjectPath + "\\settings.ini");

                    return true;
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show("Invalid File\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool LoadLastScene()
        {
            if (SceneManager.GameProject == null || !shown) return false;

            if (!SceneManager.GameProject.EditorSettings.LastOpenScenePath.Trim().Equals(string.Empty) &&
                      File.Exists(SceneManager.GameProject.EditorSettings.LastOpenScenePath))
            {
                SceneManager.LoadScene(SceneManager.GameProject.EditorSettings.LastOpenScenePath);
                EditorHandler.ChangeSelectedObject(Gibbo.Library.SceneManager.ActiveScene);
                EditorHandler.SelectedGameObjects = new List<GameObject>();
                EditorHandler.SceneTreeView.CreateView();



                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateTransformToolsCheckState()
        {
            selectStripBtn.Checked = false;
            moveStripButton.Checked = false;
            rotateStripButton.Checked = false;
            scaleStripButton.Checked = false;

            switch (sceneEditorControl1.EditorMode)
            {
                case EditorModes.Select:
                    selectStripBtn.Checked = true;
                    break;
                case EditorModes.Move:
                    moveStripButton.Checked = true;
                    break;
                case EditorModes.Rotate:
                    rotateStripButton.Checked = true;
                    break;
                case EditorModes.Scale:
                    scaleStripButton.Checked = true;
                    break;
            }
        }

        private void UpdateTilesetModes()
        {
            tilesetPencilBtn.Checked = false;
            tilesetRectangleBtn.Checked = false;
            tilesetEraserBtn.Checked = false;
            tilesetAddColumnBtn.Checked = false;
            tilesetRemoveColumnBtn.Checked = false;
            tilesetAddRowBtn.Checked = false;
            tilesetRemoveRowBtn.Checked = false;

            switch (sceneEditorControl1.TilesetMode)
            {
                case TilesetModes.Pencil:
                    tilesetPencilBtn.Checked = true;
                    break;
                case TilesetModes.Rectangle:
                    tilesetRectangleBtn.Checked = true;
                    break;
                case TilesetModes.Eraser:
                    tilesetEraserBtn.Checked = true;
                    break;
                case TilesetModes.AddColumn:
                    tilesetAddColumnBtn.Checked = true;
                    break;
                case TilesetModes.RemoveColumn:
                    tilesetRemoveColumnBtn.Checked = true;
                    break;
                case TilesetModes.AddRow:
                    tilesetAddRowBtn.Checked = true;
                    break;
                case TilesetModes.RemoveRow:
                    tilesetRemoveRowBtn.Checked = true;
                    break;
            }
        }

        #endregion

        #region events

        private void projectSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ProjectSettings().ShowDialog();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            EditorHandler.SelectedGameObjects.Clear();
            EditorHandler.ChangeSelectedObjects();
        }

        private void Editor_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            //TODO: implement safe exit

        }

        private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewProject np = new NewProject();

            // A project was created?
            if (np.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
            {
                //Reload();
                LoadProject(np.ProjectPath);
                EditorCommands.AddToProjectHistory(np.ProjectPath);
            }
        }

        private void loadProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadProject();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Editor_Load(object sender, EventArgs e)
        {

            //Wi2PlayController.Start();
        }

        static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            // Very important! 
            //return SceneManager.ScriptsAssembly;

            //This handler is called only when the common language runtime tries to bind to the assembly and fails.

            //Retrieve the list of referenced assemblies in an array of AssemblyName.
            Assembly objExecutingAssemblies;
            string strTempAssmbPath = "";

            objExecutingAssemblies = Assembly.GetExecutingAssembly();
            AssemblyName[] arrReferencedAssmbNames = objExecutingAssemblies.GetReferencedAssemblies();

            //Loop through the array of referenced assembly names.
            foreach (AssemblyName strAssmbName in arrReferencedAssmbNames)
            {
                //Console.WriteLine("search: " + SceneManager.GameProject.ProjectPath + "\\" + args.Name.Substring(0, args.Name.IndexOf(",")) + ".dll");
                //Console.WriteLine("aa: " + args.Name.Substring(0, args.Name.IndexOf(",")));
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
                    //Console.WriteLine("search: " + asmName.Replace(".dll", "") + " == " + args.Name.Substring(0, args.Name.IndexOf(",")));
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

        // For each GraphicsDeviceControl instance that you want to respond to input, call its
        // public-facing ProcessKeyMessage method.

        protected override bool ProcessKeyPreview(ref Message m)
        {
            sceneEditorControl1.ProcessKeyMessage(ref m);
            return base.ProcessKeyPreview(ref m);
        }

        private void saveProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorCommands.SaveProject();
            EditorCommands.SaveScene(false);
        }

        private void openSceneBtn_Click(object sender, EventArgs e)
        {
            OpenScene();
        }

        private void saveSceneBtn_Click(object sender, EventArgs e)
        {
            EditorCommands.SaveScene(false);
        }

        private void showGridBtn_Click(object sender, EventArgs e)
        {
            SceneManager.GameProject.EditorSettings.ShowGrid = showGridBtn.Checked;
        }

        private void Updater_Tick(object sender, EventArgs e)
        {
            UpdateUI();
           // this.Invalidate();
        }

        private void centerCameraBtn_Click(object sender, EventArgs e)
        {
            sceneEditorControl1.CenterCamera();
        }

        private void zoomCombo_TextChanged(object sender, EventArgs e)
        {

        }

        private void zoomCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            float zoom = float.Parse(zoomCombo.Text.Substring(0, zoomCombo.Text.Length - 1));
            sceneEditorControl1.Camera.Zoom = zoom / 100;
        }

        private void centerCameraObjectStripButton_Click(object sender, EventArgs e)
        {
            sceneEditorControl1.CenterCameraObject();
        }

        private void sceneEditorControl1_MouseDown(object sender, MouseEventArgs e)
        {
            sceneEditorControl1.Focus();
        }

        private void undoBtn_Click(object sender, EventArgs e)
        {
            EditorHandler.UnDoRedo.Undo(1);
        }

        private void redoBtn_Click(object sender, EventArgs e)
        {
            EditorHandler.UnDoRedo.Redo(1);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            sceneEditorControl1.EditorMode = EditorModes.Move;
            UpdateTransformToolsCheckState();
        }

        private void selectStripBtn_Click(object sender, EventArgs e)
        {
            sceneEditorControl1.EditorMode = EditorModes.Select;
            UpdateTransformToolsCheckState();
        }

        private void rotateStripButton_Click(object sender, EventArgs e)
        {
            sceneEditorControl1.EditorMode = EditorModes.Rotate;
            UpdateTransformToolsCheckState();
        }

        private void scaleStripButton_Click(object sender, EventArgs e)
        {
            sceneEditorControl1.EditorMode = EditorModes.Scale;
            UpdateTransformToolsCheckState();
        }

        private void gridSnappingBtn_Click(object sender, EventArgs e)
        {
            SceneManager.GameProject.EditorSettings.SnapToGrid = !SceneManager.GameProject.EditorSettings.SnapToGrid;
            gridSnappingBtn.Checked = SceneManager.GameProject.EditorSettings.SnapToGrid;
        }

        private void compileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //CompileScripts(true);
            //ScriptsBuilder.ReloadScripts();
            CompilerForm cf = new CompilerForm();
            cf.ShowDialog();
        }

        private void clearTextureBufferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextureLoader.Clear();
        }

        private void debugGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorCommands.DebugGame();
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            EditorCommands.DebugGame();
        }

        private void showCollisionsButton_Click(object sender, EventArgs e)
        {
            SceneManager.GameProject.EditorSettings.ShowCollisions = !SceneManager.GameProject.EditorSettings.ShowCollisions;
            showCollisionsButton.Checked = SceneManager.GameProject.EditorSettings.ShowCollisions;
        }

        private void saveSceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SceneManager.SaveActiveScene();
            EditorCommands.ShowOutputMessage("Game Scene Saved");
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorHandler.UnDoRedo.Undo(1);
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorHandler.UnDoRedo.Redo(1);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog();
        }

        private void documentationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("documentation.chm");
        }

        private void projectExplorer_Click(object sender, EventArgs e)
        {

        }

        private void sceneEditorControl1_MouseMove(object sender, MouseEventArgs e)
        {
            sceneEditorControl1.SetMousePosition(new Microsoft.Xna.Framework.Vector2(e.X, e.Y));
        }

        private void Editor_Shown(object sender, EventArgs e)
        {
            shown = true;
            LoadLastScene();
        }

        private void sceneEditorControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                sceneEditorControl1.LeftMouseKeyDown = false;
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Middle)
            {
                sceneEditorControl1.MiddleMouseKeyDown = false;
            }
        }

        private void sceneEditorControl1_MouseDown_1(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                sceneEditorControl1.LeftMouseKeyDown = true;
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Middle)
            {
                sceneEditorControl1.MiddleMouseKeyDown = true;
            }
        }

        private void deployToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //new ProjectDeploy().ShowDialog();
            KryptonMessageBox.Show("Not yet available");
        }

        private void statusLabel_Click(object sender, EventArgs e)
        {
            EditorHandler.OutputWindow.Visible = true;
            EditorHandler.OutputWindow.BringToFront();
        }

        private void webSiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.dragon-scale-studios.com/gibbo");
        }

        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                ApplicationDeployment updateCheck = ApplicationDeployment.CurrentDeployment;
                UpdateCheckInfo info = updateCheck.CheckForDetailedUpdate();

                if (info.UpdateAvailable)
                {
                    if (KryptonMessageBox.Show("There is a new build available [" + info.AvailableVersion.ToString() + "]\nCurrent Version [" + updateCheck.CurrentVersion.ToString() + "]\n\nDo you want download?", "Gibbo 2D Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        //    updateCheck.Update();

                        //    KryptonMessageBox.Show("The application has been updated, and will now restart.", "Gibbo 2D Software");

                        //    Application.Restart();
                        System.Diagnostics.Process.Start("http://www.dragon-scale-studios.com/downloads/gibbo/setup.exe");
                    }
                }
                else
                {
                    KryptonMessageBox.Show("There is no update available.", "Gibbo 2D Software");
                }
            }
            else
            {
                KryptonMessageBox.Show("This option is not available for DEBUG builds.", "Gibbo 2D Software");
            }
        }

        private void commandsToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            if (SceneManager.ActiveScene == null)
            {
                selectionToolStripMenuItem.Enabled = false;
                rotateToolStripMenuItem.Enabled = false;
                scaleToolStripMenuItem.Enabled = false;
            }
            else
            {
                selectionToolStripMenuItem.Enabled = true;
                rotateToolStripMenuItem.Enabled = true;
                scaleToolStripMenuItem.Enabled = true;
            }
        }

        private void convertToCollisionModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sceneEditorControl1.SelectionArea != Rectangle.Empty)
            {
                GameObject gameObject = new GameObject();
                gameObject.Name = "Collision Block";

                gameObject.Transform.Position = new Vector2(sceneEditorControl1.SelectionArea.X + sceneEditorControl1.SelectionArea.Width / 2, sceneEditorControl1.SelectionArea.Y + sceneEditorControl1.SelectionArea.Height / 2);
                gameObject.CollisionModel.Width = sceneEditorControl1.SelectionArea.Width;
                gameObject.CollisionModel.Height = sceneEditorControl1.SelectionArea.Height;

                if (SceneManager.ActiveScene.Layers == null || SceneManager.ActiveScene.Layers.Count == 0)
                {
                    SceneManager.ActiveScene.Layers.Add(new Layer());
                }

                SceneManager.ActiveScene.Layers[SceneManager.ActiveScene.Layers.Count - 1].GameObjects.Add(gameObject);

                sceneTreeView.CreateView();

                EditorHandler.SelectedGameObjects = new List<GameObject>();
                EditorHandler.SelectedGameObjects.Add(gameObject);
            }
            else
            {
                KryptonMessageBox.Show("No valid selection provided, please make a screen selection before executing this command", "Error");
            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorHandler.SelectedGameObjects = GameObject.GetAllGameObjects();
            EditorHandler.ChangeSelectedObjects();
        }

        private void clearSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorHandler.SelectedGameObjects = new List<GameObject>();
        }

        private void rotate90ºToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (GameObject gameObject in EditorHandler.SelectedGameObjects)
            {
                EditorHandler.UnDoRedo.InsertInUnDoRedoForRotate(gameObject.Transform.Rotation + (float)Math.PI / 2,
                    gameObject.Transform.Rotation, gameObject);

                gameObject.Transform.Rotation += (float)Math.PI / 2;
            }
        }

        private void rotate90ºToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            foreach (GameObject gameObject in EditorHandler.SelectedGameObjects)
            {
                EditorHandler.UnDoRedo.InsertInUnDoRedoForRotate(gameObject.Transform.Rotation - (float)Math.PI / 2,
                    gameObject.Transform.Rotation, gameObject);

                gameObject.Transform.Rotation -= (float)Math.PI / 2;
            }
        }

        private void rotate45ºToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (GameObject gameObject in EditorHandler.SelectedGameObjects)
            {
                EditorHandler.UnDoRedo.InsertInUnDoRedoForRotate(gameObject.Transform.Rotation + (float)Math.PI / 4,
                    gameObject.Transform.Rotation, gameObject);

                gameObject.Transform.Rotation += (float)Math.PI / 4;
            }
        }

        private void rotate45ºToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            foreach (GameObject gameObject in EditorHandler.SelectedGameObjects)
            {
                EditorHandler.UnDoRedo.InsertInUnDoRedoForRotate(gameObject.Transform.Rotation - (float)Math.PI / 4,
                    gameObject.Transform.Rotation, gameObject);

                gameObject.Transform.Rotation -= (float)Math.PI / 4;
            }
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (GameObject gameObject in EditorHandler.SelectedGameObjects)
            {
                EditorHandler.UnDoRedo.InsertInUnDoRedoForRotate(0,
                    gameObject.Transform.Rotation, gameObject);

                gameObject.Transform.Rotation = 0;
            }
        }

        private void rotate180ºToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (GameObject gameObject in EditorHandler.SelectedGameObjects)
            {
                EditorHandler.UnDoRedo.InsertInUnDoRedoForRotate(gameObject.Transform.Rotation + (float)Math.PI,
                            gameObject.Transform.Rotation, gameObject);

                gameObject.Transform.Rotation += (float)Math.PI;
            }
        }

        private void numericScaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (EditorHandler.SelectedGameObjects.Count > 0)
            {
                new NumericScale().ShowDialog();
            }
            else
            {
                KryptonMessageBox.Show("No object selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// When a plugin item is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void pluginItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            IPlugin plugin = (IPlugin)item.Tag;

            PluginContext context = new PluginContext();

            // OPTIONAL: add context options here:


            plugin.PerformAction(context);
        }

        private void tilesetPencilBtn_Click(object sender, EventArgs e)
        {
            sceneEditorControl1.TilesetMode = TilesetModes.Pencil;
            UpdateTilesetModes();
        }

        private void tilesetRectangleBtn_Click(object sender, EventArgs e)
        {
            sceneEditorControl1.TilesetMode = TilesetModes.Rectangle;
            UpdateTilesetModes();
        }

        private void tilesetEraserBtn_Click(object sender, EventArgs e)
        {
            sceneEditorControl1.TilesetMode = TilesetModes.Eraser;
            UpdateTilesetModes();
        }

        private void tilesetAddRowBtn_Click(object sender, EventArgs e)
        {
            sceneEditorControl1.TilesetMode = TilesetModes.AddRow;
            UpdateTilesetModes();
        }

        private void tilesetAddColumnBtn_Click(object sender, EventArgs e)
        {
            sceneEditorControl1.TilesetMode = TilesetModes.AddColumn;
            UpdateTilesetModes();
        }

        private void tilesetRemoveRowBtn_Click(object sender, EventArgs e)
        {
            sceneEditorControl1.TilesetMode = TilesetModes.RemoveRow;
            UpdateTilesetModes();
        }

        private void tilesetRemoveColumnBtn_Click(object sender, EventArgs e)
        {
            sceneEditorControl1.TilesetMode = TilesetModes.RemoveColumn;
            UpdateTilesetModes();
        }

        private void shiftDownBtn_Click(object sender, EventArgs e)
        {
            Tile[,] _tiles = (EditorHandler.SelectedGameObjects[0] as Tileset).DeepCopy();

            (EditorHandler.SelectedGameObjects[0] as Tileset).ShiftDown(1);

            TilesetCommand tc = new TilesetCommand((EditorHandler.SelectedGameObjects[0] as Tileset).Tiles, _tiles, (EditorHandler.SelectedGameObjects[0] as Tileset));
            EditorHandler.UnDoRedo.InsertUndoRedo(tc);
        }

        private void shiftRightBtn_Click(object sender, EventArgs e)
        {
            Tile[,] _tiles = (EditorHandler.SelectedGameObjects[0] as Tileset).DeepCopy();

            (EditorHandler.SelectedGameObjects[0] as Tileset).ShiftRight(1);

            TilesetCommand tc = new TilesetCommand((EditorHandler.SelectedGameObjects[0] as Tileset).Tiles, _tiles, (EditorHandler.SelectedGameObjects[0] as Tileset));
            EditorHandler.UnDoRedo.InsertUndoRedo(tc);
        }

        private void shiftUpBtn_Click(object sender, EventArgs e)
        {
            Tile[,] _tiles = (EditorHandler.SelectedGameObjects[0] as Tileset).DeepCopy();

            (EditorHandler.SelectedGameObjects[0] as Tileset).ShiftUp(1);

            TilesetCommand tc = new TilesetCommand((EditorHandler.SelectedGameObjects[0] as Tileset).Tiles, _tiles, (EditorHandler.SelectedGameObjects[0] as Tileset));
            EditorHandler.UnDoRedo.InsertUndoRedo(tc);
        }

        private void shiftLeftBtn_Click(object sender, EventArgs e)
        {
            Tile[,] _tiles = (EditorHandler.SelectedGameObjects[0] as Tileset).DeepCopy();

            (EditorHandler.SelectedGameObjects[0] as Tileset).ShiftLeft(1);

            TilesetCommand tc = new TilesetCommand((EditorHandler.SelectedGameObjects[0] as Tileset).Tiles, _tiles, (EditorHandler.SelectedGameObjects[0] as Tileset));
            EditorHandler.UnDoRedo.InsertUndoRedo(tc);
        }

        private void propertyGrid1_Click(object sender, EventArgs e)
        {

        }

        private void visualScriptingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KryptonMessageBox.Show("under construction");
            //new VisualScriptingWindow().Show();
        }

        private void sceneSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SceneManager.ActiveScene != null)
                EditorHandler.ChangeSelectedObject(SceneManager.ActiveScene);
        }

        private void editToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            if (SceneManager.ActiveScene == null)
                sceneSettingsToolStripMenuItem.Enabled = false;
            else
                sceneSettingsToolStripMenuItem.Enabled = true;
        }

        #endregion
    }
}
