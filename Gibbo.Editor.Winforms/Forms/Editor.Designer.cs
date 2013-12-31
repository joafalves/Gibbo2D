using System.Windows.Forms;
namespace Gibbo.Editor
{
    partial class Editor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Editor));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.saveProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSceneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deployToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
            this.projectSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sceneSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearTextureBufferToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.debugGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.commandsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertToCollisionModelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotate180ºToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.rotate90ºToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotate90ºToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.rotate45ºToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotate45ºToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scaleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.numericScaleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.visualScriptingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pluginsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.documentationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.webSiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.kryptonNavigator1 = new FlatTabControl.FlatTabControl();
            this.kryptonPage1 = new System.Windows.Forms.TabPage();
            this.kryptonPage2 = new System.Windows.Forms.TabPage();
            this.kryptonNavigator3 = new FlatTabControl.FlatTabControl();
            this.kryptonPage4 = new System.Windows.Forms.TabPage();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.kryptonNavigator2 = new FlatTabControl.FlatTabControl();
            this.kryptonPage3 = new System.Windows.Forms.TabPage();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.displayPanel = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.selectStripBtn = new System.Windows.Forms.ToolStripButton();
            this.moveStripButton = new System.Windows.Forms.ToolStripButton();
            this.rotateStripButton = new System.Windows.Forms.ToolStripButton();
            this.scaleStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.undoBtn = new System.Windows.Forms.ToolStripButton();
            this.redoBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.showGridBtn = new System.Windows.Forms.ToolStripButton();
            this.gridSnappingBtn = new System.Windows.Forms.ToolStripButton();
            this.showCollisionsButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.centerCameraObjectStripButton = new System.Windows.Forms.ToolStripButton();
            this.zoomLabel = new System.Windows.Forms.ToolStripLabel();
            this.zoomCombo = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.tabControl1 = new FlatTabControl.FlatTabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.tilesetPencilBtn = new System.Windows.Forms.ToolStripButton();
            this.tilesetRectangleBtn = new System.Windows.Forms.ToolStripButton();
            this.tilesetEraserBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.tilesetAddRowBtn = new System.Windows.Forms.ToolStripButton();
            this.tilesetAddColumnBtn = new System.Windows.Forms.ToolStripButton();
            this.tilesetRemoveRowBtn = new System.Windows.Forms.ToolStripButton();
            this.tilesetRemoveColumnBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.shiftLeftBtn = new System.Windows.Forms.ToolStripButton();
            this.shiftUpBtn = new System.Windows.Forms.ToolStripButton();
            this.shiftRightBtn = new System.Windows.Forms.ToolStripButton();
            this.shiftDownBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.Updater = new System.Windows.Forms.Timer(this.components);
            this.projectTreeView = new Gibbo.Editor.FolderTreeViewControl();
            this.sceneTreeView = new Gibbo.Editor.SceneTreeViewControl();
            this.sceneEditorControl1 = new Gibbo.Editor.SceneEditorControl();
            this.brushControl1 = new Gibbo.Editor.BrushControl();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.kryptonNavigator1.SuspendLayout();
            this.kryptonPage1.SuspendLayout();
            this.kryptonPage2.SuspendLayout();
            this.kryptonNavigator3.SuspendLayout();
            this.kryptonPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.kryptonNavigator2.SuspendLayout();
            this.kryptonPage3.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.displayPanel.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(230)))), ((int)(((byte)(232)))));
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.commandsToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.pluginsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(963, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newProjectToolStripMenuItem,
            this.loadProjectToolStripMenuItem,
            this.toolStripSeparator5,
            this.saveProjectToolStripMenuItem,
            this.saveSceneToolStripMenuItem,
            this.deployToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newProjectToolStripMenuItem
            // 
            this.newProjectToolStripMenuItem.Image = global::Gibbo.Editor.Properties.Resources._new;
            this.newProjectToolStripMenuItem.Name = "newProjectToolStripMenuItem";
            this.newProjectToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.newProjectToolStripMenuItem.Text = "New Project";
            this.newProjectToolStripMenuItem.Click += new System.EventHandler(this.newProjectToolStripMenuItem_Click);
            // 
            // loadProjectToolStripMenuItem
            // 
            this.loadProjectToolStripMenuItem.Image = global::Gibbo.Editor.Properties.Resources.folder;
            this.loadProjectToolStripMenuItem.Name = "loadProjectToolStripMenuItem";
            this.loadProjectToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.loadProjectToolStripMenuItem.Text = "Load Project";
            this.loadProjectToolStripMenuItem.Click += new System.EventHandler(this.loadProjectToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(176, 6);
            // 
            // saveProjectToolStripMenuItem
            // 
            this.saveProjectToolStripMenuItem.Image = global::Gibbo.Editor.Properties.Resources.save;
            this.saveProjectToolStripMenuItem.Name = "saveProjectToolStripMenuItem";
            this.saveProjectToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveProjectToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.saveProjectToolStripMenuItem.Text = "Save Project";
            this.saveProjectToolStripMenuItem.Click += new System.EventHandler(this.saveProjectToolStripMenuItem_Click);
            // 
            // saveSceneToolStripMenuItem
            // 
            this.saveSceneToolStripMenuItem.Name = "saveSceneToolStripMenuItem";
            this.saveSceneToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.saveSceneToolStripMenuItem.Text = "Save Scene";
            this.saveSceneToolStripMenuItem.Click += new System.EventHandler(this.saveSceneToolStripMenuItem_Click);
            // 
            // deployToolStripMenuItem
            // 
            this.deployToolStripMenuItem.Name = "deployToolStripMenuItem";
            this.deployToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.deployToolStripMenuItem.Text = "Deploy...";
            this.deployToolStripMenuItem.Click += new System.EventHandler(this.deployToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(176, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripSeparator17,
            this.projectSettingsToolStripMenuItem,
            this.sceneSettingsToolStripMenuItem});
            this.editToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            this.editToolStripMenuItem.DropDownOpening += new System.EventHandler(this.editToolStripMenuItem_DropDownOpening);
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Image = global::Gibbo.Editor.Properties.Resources.undo_2;
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Image = global::Gibbo.Editor.Properties.Resources.redo_2;
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.redoToolStripMenuItem.Text = "Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
            // 
            // toolStripSeparator17
            // 
            this.toolStripSeparator17.Name = "toolStripSeparator17";
            this.toolStripSeparator17.Size = new System.Drawing.Size(154, 6);
            // 
            // projectSettingsToolStripMenuItem
            // 
            this.projectSettingsToolStripMenuItem.Image = global::Gibbo.Editor.Properties.Resources.system1;
            this.projectSettingsToolStripMenuItem.Name = "projectSettingsToolStripMenuItem";
            this.projectSettingsToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.projectSettingsToolStripMenuItem.Text = "Project Settings";
            this.projectSettingsToolStripMenuItem.Click += new System.EventHandler(this.projectSettingsToolStripMenuItem_Click);
            // 
            // sceneSettingsToolStripMenuItem
            // 
            this.sceneSettingsToolStripMenuItem.Name = "sceneSettingsToolStripMenuItem";
            this.sceneSettingsToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.sceneSettingsToolStripMenuItem.Text = "Scene Settings";
            this.sceneSettingsToolStripMenuItem.Click += new System.EventHandler(this.sceneSettingsToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.compileToolStripMenuItem,
            this.clearTextureBufferToolStripMenuItem,
            this.toolStripSeparator7,
            this.debugGameToolStripMenuItem});
            this.viewToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.viewToolStripMenuItem.Text = "&Run";
            // 
            // compileToolStripMenuItem
            // 
            this.compileToolStripMenuItem.Image = global::Gibbo.Editor.Properties.Resources.compile;
            this.compileToolStripMenuItem.Name = "compileToolStripMenuItem";
            this.compileToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.compileToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.compileToolStripMenuItem.Text = "Reload Scripts";
            this.compileToolStripMenuItem.Click += new System.EventHandler(this.compileToolStripMenuItem_Click);
            // 
            // clearTextureBufferToolStripMenuItem
            // 
            this.clearTextureBufferToolStripMenuItem.Name = "clearTextureBufferToolStripMenuItem";
            this.clearTextureBufferToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.clearTextureBufferToolStripMenuItem.Text = "Clear Texture Buffer";
            this.clearTextureBufferToolStripMenuItem.Click += new System.EventHandler(this.clearTextureBufferToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(175, 6);
            // 
            // debugGameToolStripMenuItem
            // 
            this.debugGameToolStripMenuItem.Image = global::Gibbo.Editor.Properties.Resources.play;
            this.debugGameToolStripMenuItem.Name = "debugGameToolStripMenuItem";
            this.debugGameToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.debugGameToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.debugGameToolStripMenuItem.Text = "Debug Game";
            this.debugGameToolStripMenuItem.Click += new System.EventHandler(this.debugGameToolStripMenuItem_Click);
            // 
            // commandsToolStripMenuItem
            // 
            this.commandsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectionToolStripMenuItem,
            this.rotateToolStripMenuItem,
            this.scaleToolStripMenuItem});
            this.commandsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.commandsToolStripMenuItem.Name = "commandsToolStripMenuItem";
            this.commandsToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.commandsToolStripMenuItem.Text = "Commands";
            this.commandsToolStripMenuItem.DropDownOpening += new System.EventHandler(this.commandsToolStripMenuItem_DropDownOpening);
            // 
            // selectionToolStripMenuItem
            // 
            this.selectionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.convertToCollisionModelToolStripMenuItem,
            this.toolStripSeparator10,
            this.selectAllToolStripMenuItem,
            this.clearSelectionToolStripMenuItem});
            this.selectionToolStripMenuItem.Name = "selectionToolStripMenuItem";
            this.selectionToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.selectionToolStripMenuItem.Text = "Selection";
            // 
            // convertToCollisionModelToolStripMenuItem
            // 
            this.convertToCollisionModelToolStripMenuItem.Name = "convertToCollisionModelToolStripMenuItem";
            this.convertToCollisionModelToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.convertToCollisionModelToolStripMenuItem.Text = "To Collision Block";
            this.convertToCollisionModelToolStripMenuItem.Click += new System.EventHandler(this.convertToCollisionModelToolStripMenuItem_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(165, 6);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.selectAllToolStripMenuItem.Text = "Select All";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // clearSelectionToolStripMenuItem
            // 
            this.clearSelectionToolStripMenuItem.Name = "clearSelectionToolStripMenuItem";
            this.clearSelectionToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.clearSelectionToolStripMenuItem.Text = "Clear Selection";
            this.clearSelectionToolStripMenuItem.Click += new System.EventHandler(this.clearSelectionToolStripMenuItem_Click);
            // 
            // rotateToolStripMenuItem
            // 
            this.rotateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rotate180ºToolStripMenuItem,
            this.toolStripSeparator13,
            this.rotate90ºToolStripMenuItem,
            this.rotate90ºToolStripMenuItem1,
            this.toolStripSeparator11,
            this.rotate45ºToolStripMenuItem,
            this.rotate45ºToolStripMenuItem1,
            this.toolStripSeparator12,
            this.resetToolStripMenuItem});
            this.rotateToolStripMenuItem.Name = "rotateToolStripMenuItem";
            this.rotateToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.rotateToolStripMenuItem.Text = "Rotate";
            // 
            // rotate180ºToolStripMenuItem
            // 
            this.rotate180ºToolStripMenuItem.Name = "rotate180ºToolStripMenuItem";
            this.rotate180ºToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.rotate180ºToolStripMenuItem.Text = "Rotate 180º";
            this.rotate180ºToolStripMenuItem.Click += new System.EventHandler(this.rotate180ºToolStripMenuItem_Click);
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(135, 6);
            // 
            // rotate90ºToolStripMenuItem
            // 
            this.rotate90ºToolStripMenuItem.Name = "rotate90ºToolStripMenuItem";
            this.rotate90ºToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.rotate90ºToolStripMenuItem.Text = "Rotate 90º ";
            this.rotate90ºToolStripMenuItem.Click += new System.EventHandler(this.rotate90ºToolStripMenuItem_Click);
            // 
            // rotate90ºToolStripMenuItem1
            // 
            this.rotate90ºToolStripMenuItem1.Name = "rotate90ºToolStripMenuItem1";
            this.rotate90ºToolStripMenuItem1.Size = new System.Drawing.Size(138, 22);
            this.rotate90ºToolStripMenuItem1.Text = "Rotate -90º ";
            this.rotate90ºToolStripMenuItem1.Click += new System.EventHandler(this.rotate90ºToolStripMenuItem1_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(135, 6);
            // 
            // rotate45ºToolStripMenuItem
            // 
            this.rotate45ºToolStripMenuItem.Name = "rotate45ºToolStripMenuItem";
            this.rotate45ºToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.rotate45ºToolStripMenuItem.Text = "Rotate 45º";
            this.rotate45ºToolStripMenuItem.Click += new System.EventHandler(this.rotate45ºToolStripMenuItem_Click);
            // 
            // rotate45ºToolStripMenuItem1
            // 
            this.rotate45ºToolStripMenuItem1.Name = "rotate45ºToolStripMenuItem1";
            this.rotate45ºToolStripMenuItem1.Size = new System.Drawing.Size(138, 22);
            this.rotate45ºToolStripMenuItem1.Text = "Rotate -45º";
            this.rotate45ºToolStripMenuItem1.Click += new System.EventHandler(this.rotate45ºToolStripMenuItem1_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(135, 6);
            // 
            // resetToolStripMenuItem
            // 
            this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            this.resetToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.resetToolStripMenuItem.Text = "Reset";
            this.resetToolStripMenuItem.Click += new System.EventHandler(this.resetToolStripMenuItem_Click);
            // 
            // scaleToolStripMenuItem
            // 
            this.scaleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.numericScaleToolStripMenuItem});
            this.scaleToolStripMenuItem.Name = "scaleToolStripMenuItem";
            this.scaleToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.scaleToolStripMenuItem.Text = "Scale";
            // 
            // numericScaleToolStripMenuItem
            // 
            this.numericScaleToolStripMenuItem.Name = "numericScaleToolStripMenuItem";
            this.numericScaleToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.numericScaleToolStripMenuItem.Text = "Numeric Scale...";
            this.numericScaleToolStripMenuItem.Click += new System.EventHandler(this.numericScaleToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.visualScriptingToolStripMenuItem});
            this.toolsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // visualScriptingToolStripMenuItem
            // 
            this.visualScriptingToolStripMenuItem.Name = "visualScriptingToolStripMenuItem";
            this.visualScriptingToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.visualScriptingToolStripMenuItem.Text = "Visual Scripting";
            this.visualScriptingToolStripMenuItem.Click += new System.EventHandler(this.visualScriptingToolStripMenuItem_Click);
            // 
            // pluginsToolStripMenuItem
            // 
            this.pluginsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pluginsToolStripMenuItem.Name = "pluginsToolStripMenuItem";
            this.pluginsToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.pluginsToolStripMenuItem.Text = "&Plugins";
            this.pluginsToolStripMenuItem.Visible = false;
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.documentationToolStripMenuItem,
            this.toolStripSeparator8,
            this.aboutToolStripMenuItem,
            this.webSiteToolStripMenuItem,
            this.toolStripSeparator9,
            this.checkForUpdatesToolStripMenuItem});
            this.helpToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // documentationToolStripMenuItem
            // 
            this.documentationToolStripMenuItem.Image = global::Gibbo.Editor.Properties.Resources.help;
            this.documentationToolStripMenuItem.Name = "documentationToolStripMenuItem";
            this.documentationToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.documentationToolStripMenuItem.Text = "Documentation";
            this.documentationToolStripMenuItem.Click += new System.EventHandler(this.documentationToolStripMenuItem_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(167, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // webSiteToolStripMenuItem
            // 
            this.webSiteToolStripMenuItem.Name = "webSiteToolStripMenuItem";
            this.webSiteToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.webSiteToolStripMenuItem.Text = "Web site";
            this.webSiteToolStripMenuItem.Click += new System.EventHandler(this.webSiteToolStripMenuItem_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(167, 6);
            // 
            // checkForUpdatesToolStripMenuItem
            // 
            this.checkForUpdatesToolStripMenuItem.Image = global::Gibbo.Editor.Properties.Resources.refresh__2_;
            this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.checkForUpdatesToolStripMenuItem.Text = "Check for Updates";
            this.checkForUpdatesToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdatesToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(230)))), ((int)(((byte)(232)))));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 540);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(963, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 2;
            // 
            // statusLabel
            // 
            this.statusLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold);
            this.statusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.statusLabel.Image = global::Gibbo.Editor.Properties.Resources.info;
            this.statusLabel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(133, 17);
            this.statusLabel.Text = "Welcome to Gibbo 2D";
            this.statusLabel.Click += new System.EventHandler(this.statusLabel_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(230)))), ((int)(((byte)(232)))));
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(1);
            this.splitContainer1.Size = new System.Drawing.Size(963, 516);
            this.splitContainer1.SplitterDistance = 260;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 3;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(1, 1);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.kryptonNavigator1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.kryptonNavigator3);
            this.splitContainer2.Size = new System.Drawing.Size(258, 514);
            this.splitContainer2.SplitterDistance = 227;
            this.splitContainer2.TabIndex = 1;
            // 
            // kryptonNavigator1
            // 
            this.kryptonNavigator1.Controls.Add(this.kryptonPage1);
            this.kryptonNavigator1.Controls.Add(this.kryptonPage2);
            this.kryptonNavigator1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonNavigator1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonNavigator1.Location = new System.Drawing.Point(0, 0);
            this.kryptonNavigator1.myBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(230)))), ((int)(((byte)(232)))));
            this.kryptonNavigator1.Name = "kryptonNavigator1";
            this.kryptonNavigator1.SelectedIndex = 0;
            this.kryptonNavigator1.Size = new System.Drawing.Size(258, 227);
            this.kryptonNavigator1.TabIndex = 2;
            this.kryptonNavigator1.Text = "kryptonNavigator1";
            // 
            // kryptonPage1
            // 
            this.kryptonPage1.BackColor = System.Drawing.Color.White;
            this.kryptonPage1.Controls.Add(this.projectTreeView);
            this.kryptonPage1.Location = new System.Drawing.Point(4, 25);
            this.kryptonPage1.MinimumSize = new System.Drawing.Size(43, 43);
            this.kryptonPage1.Name = "kryptonPage1";
            this.kryptonPage1.Size = new System.Drawing.Size(250, 198);
            this.kryptonPage1.TabIndex = 0;
            this.kryptonPage1.Text = "Project Explorer";
            // 
            // kryptonPage2
            // 
            this.kryptonPage2.BackColor = System.Drawing.Color.White;
            this.kryptonPage2.Controls.Add(this.sceneTreeView);
            this.kryptonPage2.Location = new System.Drawing.Point(4, 25);
            this.kryptonPage2.MinimumSize = new System.Drawing.Size(43, 43);
            this.kryptonPage2.Name = "kryptonPage2";
            this.kryptonPage2.Size = new System.Drawing.Size(250, 198);
            this.kryptonPage2.TabIndex = 1;
            this.kryptonPage2.Text = "Scene Explorer";
            // 
            // kryptonNavigator3
            // 
            this.kryptonNavigator3.Controls.Add(this.kryptonPage4);
            this.kryptonNavigator3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonNavigator3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonNavigator3.Location = new System.Drawing.Point(0, 0);
            this.kryptonNavigator3.myBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(230)))), ((int)(((byte)(232)))));
            this.kryptonNavigator3.Name = "kryptonNavigator3";
            this.kryptonNavigator3.SelectedIndex = 0;
            this.kryptonNavigator3.Size = new System.Drawing.Size(258, 283);
            this.kryptonNavigator3.TabIndex = 3;
            this.kryptonNavigator3.Text = "kryptonNavigator3";
            // 
            // kryptonPage4
            // 
            this.kryptonPage4.BackColor = System.Drawing.Color.White;
            this.kryptonPage4.Controls.Add(this.propertyGrid1);
            this.kryptonPage4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonPage4.Location = new System.Drawing.Point(4, 25);
            this.kryptonPage4.MinimumSize = new System.Drawing.Size(43, 43);
            this.kryptonPage4.Name = "kryptonPage4";
            this.kryptonPage4.Size = new System.Drawing.Size(250, 254);
            this.kryptonPage4.TabIndex = 0;
            this.kryptonPage4.Text = "Property Editor";
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGrid1.BackColor = System.Drawing.SystemColors.Window;
            this.propertyGrid1.CategoryForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.propertyGrid1.CommandsBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(230)))), ((int)(((byte)(232)))));
            this.propertyGrid1.CommandsForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.propertyGrid1.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold);
            this.propertyGrid1.HelpBackColor = System.Drawing.Color.White;
            this.propertyGrid1.HelpForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.propertyGrid1.LineColor = System.Drawing.Color.White;
            this.propertyGrid1.Location = new System.Drawing.Point(-1, -1);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.propertyGrid1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.propertyGrid1.Size = new System.Drawing.Size(253, 260);
            this.propertyGrid1.TabIndex = 3;
            this.propertyGrid1.ViewForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.propertyGrid1.Click += new System.EventHandler(this.propertyGrid1_Click);
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer3.Location = new System.Drawing.Point(1, 1);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.kryptonNavigator2);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer3.Size = new System.Drawing.Size(698, 514);
            this.splitContainer3.SplitterDistance = 386;
            this.splitContainer3.SplitterWidth = 3;
            this.splitContainer3.TabIndex = 5;
            // 
            // kryptonNavigator2
            // 
            this.kryptonNavigator2.Controls.Add(this.kryptonPage3);
            this.kryptonNavigator2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonNavigator2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonNavigator2.Location = new System.Drawing.Point(0, 0);
            this.kryptonNavigator2.myBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(230)))), ((int)(((byte)(232)))));
            this.kryptonNavigator2.Name = "kryptonNavigator2";
            this.kryptonNavigator2.SelectedIndex = 0;
            this.kryptonNavigator2.Size = new System.Drawing.Size(386, 514);
            this.kryptonNavigator2.TabIndex = 4;
            this.kryptonNavigator2.Text = "kryptonNavigator2";
            // 
            // kryptonPage3
            // 
            this.kryptonPage3.BackColor = System.Drawing.Color.White;
            this.kryptonPage3.Controls.Add(this.toolStripContainer1);
            this.kryptonPage3.Location = new System.Drawing.Point(4, 25);
            this.kryptonPage3.MinimumSize = new System.Drawing.Size(43, 43);
            this.kryptonPage3.Name = "kryptonPage3";
            this.kryptonPage3.Size = new System.Drawing.Size(378, 485);
            this.kryptonPage3.TabIndex = 0;
            this.kryptonPage3.Text = "Scene View";
            // 
            // toolStripContainer1
            // 
            this.toolStripContainer1.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.displayPanel);
            this.toolStripContainer1.ContentPanel.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(378, 460);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(378, 485);
            this.toolStripContainer1.TabIndex = 2;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.BackColor = System.Drawing.Color.White;
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            this.toolStripContainer1.TopToolStripPanel.Padding = new System.Windows.Forms.Padding(3);
            // 
            // displayPanel
            // 
            this.displayPanel.Controls.Add(this.sceneEditorControl1);
            this.displayPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.displayPanel.Location = new System.Drawing.Point(0, 0);
            this.displayPanel.Name = "displayPanel";
            this.displayPanel.Size = new System.Drawing.Size(378, 460);
            this.displayPanel.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectStripBtn,
            this.moveStripButton,
            this.rotateStripButton,
            this.scaleStripButton,
            this.toolStripSeparator3,
            this.undoBtn,
            this.redoBtn,
            this.toolStripSeparator2,
            this.showGridBtn,
            this.gridSnappingBtn,
            this.showCollisionsButton,
            this.toolStripSeparator4,
            this.toolStripButton2,
            this.centerCameraObjectStripButton,
            this.zoomLabel,
            this.zoomCombo,
            this.toolStripSeparator6,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Margin = new System.Windows.Forms.Padding(2);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(368, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // selectStripBtn
            // 
            this.selectStripBtn.AutoSize = false;
            this.selectStripBtn.Checked = true;
            this.selectStripBtn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.selectStripBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.selectStripBtn.Image = global::Gibbo.Editor.Properties.Resources.cursor;
            this.selectStripBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.selectStripBtn.Name = "selectStripBtn";
            this.selectStripBtn.Size = new System.Drawing.Size(22, 22);
            this.selectStripBtn.Text = "Select Tool";
            this.selectStripBtn.ToolTipText = "Select Tool\r\nPress Q";
            this.selectStripBtn.Click += new System.EventHandler(this.selectStripBtn_Click);
            // 
            // moveStripButton
            // 
            this.moveStripButton.AutoSize = false;
            this.moveStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveStripButton.Image = global::Gibbo.Editor.Properties.Resources.move;
            this.moveStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveStripButton.Name = "moveStripButton";
            this.moveStripButton.Size = new System.Drawing.Size(22, 22);
            this.moveStripButton.Text = "Move";
            this.moveStripButton.ToolTipText = "Move\r\nPress E";
            this.moveStripButton.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // rotateStripButton
            // 
            this.rotateStripButton.AutoSize = false;
            this.rotateStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.rotateStripButton.Image = global::Gibbo.Editor.Properties.Resources.refresh1;
            this.rotateStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.rotateStripButton.Name = "rotateStripButton";
            this.rotateStripButton.Size = new System.Drawing.Size(22, 22);
            this.rotateStripButton.Text = "Rotate";
            this.rotateStripButton.ToolTipText = "Rotate\r\nPress R \r\nPress Z for snapping";
            this.rotateStripButton.Click += new System.EventHandler(this.rotateStripButton_Click);
            // 
            // scaleStripButton
            // 
            this.scaleStripButton.AutoSize = false;
            this.scaleStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.scaleStripButton.Image = global::Gibbo.Editor.Properties.Resources.resize2;
            this.scaleStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.scaleStripButton.Name = "scaleStripButton";
            this.scaleStripButton.Size = new System.Drawing.Size(22, 22);
            this.scaleStripButton.Text = "Scale";
            this.scaleStripButton.ToolTipText = "Scale \r\nPress T";
            this.scaleStripButton.Click += new System.EventHandler(this.scaleStripButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // undoBtn
            // 
            this.undoBtn.AutoSize = false;
            this.undoBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.undoBtn.Image = global::Gibbo.Editor.Properties.Resources.undo_2;
            this.undoBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.undoBtn.Name = "undoBtn";
            this.undoBtn.Size = new System.Drawing.Size(22, 22);
            this.undoBtn.Text = "Undo";
            this.undoBtn.ToolTipText = "Undo\r\nPress Cntrl + Z";
            this.undoBtn.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // redoBtn
            // 
            this.redoBtn.AutoSize = false;
            this.redoBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.redoBtn.Image = global::Gibbo.Editor.Properties.Resources.redo_2;
            this.redoBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.redoBtn.Name = "redoBtn";
            this.redoBtn.Size = new System.Drawing.Size(22, 22);
            this.redoBtn.Text = "Redo";
            this.redoBtn.ToolTipText = "Redo\r\nPress Cntrl + Y";
            this.redoBtn.Click += new System.EventHandler(this.redoBtn_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // showGridBtn
            // 
            this.showGridBtn.AutoSize = false;
            this.showGridBtn.CheckOnClick = true;
            this.showGridBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.showGridBtn.Image = global::Gibbo.Editor.Properties.Resources.grid;
            this.showGridBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showGridBtn.Name = "showGridBtn";
            this.showGridBtn.Size = new System.Drawing.Size(22, 22);
            this.showGridBtn.Text = "Show Grid";
            this.showGridBtn.ToolTipText = "Show Grid\r\nShortcut Key: G";
            this.showGridBtn.Click += new System.EventHandler(this.showGridBtn_Click);
            // 
            // gridSnappingBtn
            // 
            this.gridSnappingBtn.AutoSize = false;
            this.gridSnappingBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.gridSnappingBtn.Image = global::Gibbo.Editor.Properties.Resources.snap;
            this.gridSnappingBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.gridSnappingBtn.Name = "gridSnappingBtn";
            this.gridSnappingBtn.Size = new System.Drawing.Size(22, 22);
            this.gridSnappingBtn.Text = "Grid Snapping";
            this.gridSnappingBtn.Click += new System.EventHandler(this.gridSnappingBtn_Click);
            // 
            // showCollisionsButton
            // 
            this.showCollisionsButton.AutoSize = false;
            this.showCollisionsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.showCollisionsButton.Image = global::Gibbo.Editor.Properties.Resources.collision;
            this.showCollisionsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showCollisionsButton.Name = "showCollisionsButton";
            this.showCollisionsButton.Size = new System.Drawing.Size(22, 22);
            this.showCollisionsButton.Text = "Show Collision Models";
            this.showCollisionsButton.Click += new System.EventHandler(this.showCollisionsButton_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.AutoSize = false;
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::Gibbo.Editor.Properties.Resources.camera;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(22, 22);
            this.toolStripButton2.Text = "Reset Camera Position";
            this.toolStripButton2.ToolTipText = "Reset Camera Position\r\nShortcut Key: C";
            this.toolStripButton2.Click += new System.EventHandler(this.centerCameraBtn_Click);
            // 
            // centerCameraObjectStripButton
            // 
            this.centerCameraObjectStripButton.AutoSize = false;
            this.centerCameraObjectStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.centerCameraObjectStripButton.Image = global::Gibbo.Editor.Properties.Resources.camera_object;
            this.centerCameraObjectStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.centerCameraObjectStripButton.Name = "centerCameraObjectStripButton";
            this.centerCameraObjectStripButton.Size = new System.Drawing.Size(22, 22);
            this.centerCameraObjectStripButton.Text = "Center Camera on Selected Object";
            this.centerCameraObjectStripButton.ToolTipText = "Center Camera on Selected Object\r\nShortcut Key: O";
            this.centerCameraObjectStripButton.Click += new System.EventHandler(this.centerCameraObjectStripButton_Click);
            // 
            // zoomLabel
            // 
            this.zoomLabel.Name = "zoomLabel";
            this.zoomLabel.Size = new System.Drawing.Size(45, 22);
            this.zoomLabel.Text = "Zoom: ";
            this.zoomLabel.Visible = false;
            // 
            // zoomCombo
            // 
            this.zoomCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.zoomCombo.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.zoomCombo.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.zoomCombo.Name = "zoomCombo";
            this.zoomCombo.Size = new System.Drawing.Size(119, 21);
            this.zoomCombo.Visible = false;
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.AutoSize = false;
            this.toolStripButton1.Image = global::Gibbo.Editor.Properties.Resources.play;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(96, 22);
            this.toolStripButton1.Text = "Debug Game";
            this.toolStripButton1.Click += new System.EventHandler(this.debugGameToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.myBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(230)))), ((int)(((byte)(232)))));
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(309, 514);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.White;
            this.tabPage2.Controls.Add(this.brushControl1);
            this.tabPage2.Controls.Add(this.toolStrip2);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(301, 485);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Tileset Container";
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackColor = System.Drawing.Color.White;
            this.toolStrip2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tilesetPencilBtn,
            this.tilesetRectangleBtn,
            this.tilesetEraserBtn,
            this.toolStripSeparator14,
            this.tilesetAddRowBtn,
            this.tilesetAddColumnBtn,
            this.tilesetRemoveRowBtn,
            this.tilesetRemoveColumnBtn,
            this.toolStripSeparator15,
            this.shiftLeftBtn,
            this.shiftUpBtn,
            this.shiftRightBtn,
            this.shiftDownBtn,
            this.toolStripSeparator16,
            this.toolStripButton3});
            this.toolStrip2.Location = new System.Drawing.Point(3, 3);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip2.Size = new System.Drawing.Size(295, 25);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // tilesetPencilBtn
            // 
            this.tilesetPencilBtn.Checked = true;
            this.tilesetPencilBtn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tilesetPencilBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tilesetPencilBtn.Image = global::Gibbo.Editor.Properties.Resources.pencil;
            this.tilesetPencilBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tilesetPencilBtn.Name = "tilesetPencilBtn";
            this.tilesetPencilBtn.Size = new System.Drawing.Size(23, 22);
            this.tilesetPencilBtn.Text = "Pencil Tool";
            this.tilesetPencilBtn.Click += new System.EventHandler(this.tilesetPencilBtn_Click);
            // 
            // tilesetRectangleBtn
            // 
            this.tilesetRectangleBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tilesetRectangleBtn.Image = global::Gibbo.Editor.Properties.Resources.area;
            this.tilesetRectangleBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tilesetRectangleBtn.Name = "tilesetRectangleBtn";
            this.tilesetRectangleBtn.Size = new System.Drawing.Size(23, 22);
            this.tilesetRectangleBtn.Text = "Rectangle Tool";
            this.tilesetRectangleBtn.Click += new System.EventHandler(this.tilesetRectangleBtn_Click);
            // 
            // tilesetEraserBtn
            // 
            this.tilesetEraserBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tilesetEraserBtn.Image = global::Gibbo.Editor.Properties.Resources.delete_cell;
            this.tilesetEraserBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tilesetEraserBtn.Name = "tilesetEraserBtn";
            this.tilesetEraserBtn.Size = new System.Drawing.Size(23, 22);
            this.tilesetEraserBtn.Text = "Eraser Tool";
            this.tilesetEraserBtn.Click += new System.EventHandler(this.tilesetEraserBtn_Click);
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            this.toolStripSeparator14.Size = new System.Drawing.Size(6, 25);
            // 
            // tilesetAddRowBtn
            // 
            this.tilesetAddRowBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tilesetAddRowBtn.Image = global::Gibbo.Editor.Properties.Resources.add_row;
            this.tilesetAddRowBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tilesetAddRowBtn.Name = "tilesetAddRowBtn";
            this.tilesetAddRowBtn.Size = new System.Drawing.Size(23, 22);
            this.tilesetAddRowBtn.Text = "Add Row Tool";
            this.tilesetAddRowBtn.Click += new System.EventHandler(this.tilesetAddRowBtn_Click);
            // 
            // tilesetAddColumnBtn
            // 
            this.tilesetAddColumnBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tilesetAddColumnBtn.Image = global::Gibbo.Editor.Properties.Resources.add_column;
            this.tilesetAddColumnBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tilesetAddColumnBtn.Name = "tilesetAddColumnBtn";
            this.tilesetAddColumnBtn.Size = new System.Drawing.Size(23, 22);
            this.tilesetAddColumnBtn.Text = "Add Column Tool";
            this.tilesetAddColumnBtn.Click += new System.EventHandler(this.tilesetAddColumnBtn_Click);
            // 
            // tilesetRemoveRowBtn
            // 
            this.tilesetRemoveRowBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tilesetRemoveRowBtn.Image = global::Gibbo.Editor.Properties.Resources.remove_row_2;
            this.tilesetRemoveRowBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tilesetRemoveRowBtn.Name = "tilesetRemoveRowBtn";
            this.tilesetRemoveRowBtn.Size = new System.Drawing.Size(23, 22);
            this.tilesetRemoveRowBtn.Text = "Remove Row Tool";
            this.tilesetRemoveRowBtn.Click += new System.EventHandler(this.tilesetRemoveRowBtn_Click);
            // 
            // tilesetRemoveColumnBtn
            // 
            this.tilesetRemoveColumnBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tilesetRemoveColumnBtn.Image = global::Gibbo.Editor.Properties.Resources.remove_column_2;
            this.tilesetRemoveColumnBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tilesetRemoveColumnBtn.Name = "tilesetRemoveColumnBtn";
            this.tilesetRemoveColumnBtn.Size = new System.Drawing.Size(23, 22);
            this.tilesetRemoveColumnBtn.Text = "Remove Column Tool";
            this.tilesetRemoveColumnBtn.Click += new System.EventHandler(this.tilesetRemoveColumnBtn_Click);
            // 
            // toolStripSeparator15
            // 
            this.toolStripSeparator15.Name = "toolStripSeparator15";
            this.toolStripSeparator15.Size = new System.Drawing.Size(6, 25);
            // 
            // shiftLeftBtn
            // 
            this.shiftLeftBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.shiftLeftBtn.Image = global::Gibbo.Editor.Properties.Resources.grid_shift_left;
            this.shiftLeftBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.shiftLeftBtn.Name = "shiftLeftBtn";
            this.shiftLeftBtn.Size = new System.Drawing.Size(23, 22);
            this.shiftLeftBtn.Text = "Shift Left";
            this.shiftLeftBtn.Click += new System.EventHandler(this.shiftLeftBtn_Click);
            // 
            // shiftUpBtn
            // 
            this.shiftUpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.shiftUpBtn.Image = global::Gibbo.Editor.Properties.Resources.grid_shift_up;
            this.shiftUpBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.shiftUpBtn.Name = "shiftUpBtn";
            this.shiftUpBtn.Size = new System.Drawing.Size(23, 22);
            this.shiftUpBtn.Text = "Shift Up";
            this.shiftUpBtn.Click += new System.EventHandler(this.shiftUpBtn_Click);
            // 
            // shiftRightBtn
            // 
            this.shiftRightBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.shiftRightBtn.Image = global::Gibbo.Editor.Properties.Resources.grid_shift_right;
            this.shiftRightBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.shiftRightBtn.Name = "shiftRightBtn";
            this.shiftRightBtn.Size = new System.Drawing.Size(23, 22);
            this.shiftRightBtn.Text = "Shift Right";
            this.shiftRightBtn.Click += new System.EventHandler(this.shiftRightBtn_Click);
            // 
            // shiftDownBtn
            // 
            this.shiftDownBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.shiftDownBtn.Image = global::Gibbo.Editor.Properties.Resources.grid_shift_down;
            this.shiftDownBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.shiftDownBtn.Name = "shiftDownBtn";
            this.shiftDownBtn.Size = new System.Drawing.Size(23, 22);
            this.shiftDownBtn.Text = "Shift Down";
            this.shiftDownBtn.Click += new System.EventHandler(this.shiftDownBtn_Click);
            // 
            // toolStripSeparator16
            // 
            this.toolStripSeparator16.Name = "toolStripSeparator16";
            this.toolStripSeparator16.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = global::Gibbo.Editor.Properties.Resources.grid_close;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "Exit Tileset Mode";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // Updater
            // 
            this.Updater.Enabled = true;
            this.Updater.Tick += new System.EventHandler(this.Updater_Tick);
            // 
            // projectTreeView
            // 
            this.projectTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.projectTreeView.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.projectTreeView.Location = new System.Drawing.Point(0, 0);
            this.projectTreeView.Name = "projectTreeView";
            this.projectTreeView.SelectedNode = null;
            this.projectTreeView.Size = new System.Drawing.Size(250, 198);
            this.projectTreeView.TabIndex = 0;
            // 
            // sceneTreeView
            // 
            this.sceneTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sceneTreeView.Location = new System.Drawing.Point(0, 0);
            this.sceneTreeView.Name = "sceneTreeView";
            this.sceneTreeView.SelectedNode = null;
            this.sceneTreeView.Size = new System.Drawing.Size(250, 198);
            this.sceneTreeView.TabIndex = 0;
            // 
            // sceneEditorControl1
            // 
            this.sceneEditorControl1.BackColor = System.Drawing.Color.Black;
            this.sceneEditorControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sceneEditorControl1.LeftMouseKeyDown = false;
            this.sceneEditorControl1.LeftMouseKeyPressed = false;
            this.sceneEditorControl1.Location = new System.Drawing.Point(0, 0);
            this.sceneEditorControl1.Margin = new System.Windows.Forms.Padding(0);
            this.sceneEditorControl1.MiddleMouseKeyDown = false;
            this.sceneEditorControl1.Name = "sceneEditorControl1";
            this.sceneEditorControl1.SelectionArea = ((Microsoft.Xna.Framework.Rectangle)(resources.GetObject("sceneEditorControl1.SelectionArea")));
            this.sceneEditorControl1.Size = new System.Drawing.Size(378, 460);
            this.sceneEditorControl1.TabIndex = 0;
            this.sceneEditorControl1.VSync = false;
            this.sceneEditorControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.sceneEditorControl1_MouseDown_1);
            this.sceneEditorControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.sceneEditorControl1_MouseMove);
            this.sceneEditorControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.sceneEditorControl1_MouseUp);
            // 
            // brushControl1
            // 
            this.brushControl1.AutoScroll = true;
            this.brushControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.brushControl1.BrushSizeX = 32;
            this.brushControl1.BrushSizeY = 32;
            this.brushControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.brushControl1.Location = new System.Drawing.Point(3, 28);
            this.brushControl1.Name = "brushControl1";
            this.brushControl1.Size = new System.Drawing.Size(295, 454);
            this.brushControl1.TabIndex = 0;
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(230)))), ((int)(((byte)(232)))));
            this.ClientSize = new System.Drawing.Size(963, 562);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Editor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gibbo 2D";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Editor_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Editor_FormClosed);
            this.Load += new System.EventHandler(this.Editor_Load);
            this.Shown += new System.EventHandler(this.Editor_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.kryptonNavigator1.ResumeLayout(false);
            this.kryptonPage1.ResumeLayout(false);
            this.kryptonPage2.ResumeLayout(false);
            this.kryptonNavigator3.ResumeLayout(false);
            this.kryptonPage4.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.kryptonNavigator2.ResumeLayout(false);
            this.kryptonPage3.ResumeLayout(false);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.displayPanel.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem newProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.Timer Updater;
        private System.Windows.Forms.ToolStripMenuItem compileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem debugGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearTextureBufferToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSceneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem documentationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private FlatTabControl.FlatTabControl kryptonNavigator1;
        private TabPage kryptonPage1;
        private TabPage kryptonPage2;
        private TabPage kryptonPage3;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.Panel displayPanel;
        private SceneEditorControl sceneEditorControl1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton selectStripBtn;
        private System.Windows.Forms.ToolStripButton moveStripButton;
        private System.Windows.Forms.ToolStripButton rotateStripButton;
        private System.Windows.Forms.ToolStripButton scaleStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton undoBtn;
        private System.Windows.Forms.ToolStripButton redoBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton showGridBtn;
        private System.Windows.Forms.ToolStripButton gridSnappingBtn;
        private System.Windows.Forms.ToolStripButton showCollisionsButton;
        private FolderTreeViewControl projectTreeView;
        private SceneTreeViewControl sceneTreeView;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton centerCameraObjectStripButton;
        private System.Windows.Forms.ToolStripLabel zoomLabel;
        private System.Windows.Forms.ToolStripComboBox zoomCombo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private TabPage kryptonPage4;
        public System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.ToolStripMenuItem deployToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem webSiteToolStripMenuItem;
        private ToolStripMenuItem checkForUpdatesToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator9;
        private ToolStripMenuItem commandsToolStripMenuItem;
        private ToolStripMenuItem selectionToolStripMenuItem;
        private ToolStripMenuItem convertToCollisionModelToolStripMenuItem;
        private ToolStripMenuItem selectAllToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator10;
        private ToolStripMenuItem clearSelectionToolStripMenuItem;
        private ToolStripMenuItem rotateToolStripMenuItem;
        private ToolStripMenuItem scaleToolStripMenuItem;
        private ToolStripMenuItem rotate90ºToolStripMenuItem;
        private ToolStripMenuItem rotate90ºToolStripMenuItem1;
        private ToolStripSeparator toolStripSeparator11;
        private ToolStripMenuItem rotate45ºToolStripMenuItem;
        private ToolStripMenuItem rotate45ºToolStripMenuItem1;
        private ToolStripMenuItem numericScaleToolStripMenuItem;
        private ToolStripMenuItem rotate180ºToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator13;
        private ToolStripSeparator toolStripSeparator12;
        private ToolStripMenuItem resetToolStripMenuItem;
        private ToolStripMenuItem pluginsToolStripMenuItem;
        private SplitContainer splitContainer3;
        private TabPage tabPage2;
        private BrushControl brushControl1;
        private ToolStrip toolStrip2;
        private ToolStripButton tilesetPencilBtn;
        private ToolStripButton tilesetRectangleBtn;
        private ToolStripButton tilesetEraserBtn;
        private ToolStripSeparator toolStripSeparator14;
        private ToolStripButton tilesetAddColumnBtn;
        private ToolStripButton tilesetAddRowBtn;
        private ToolStripButton tilesetRemoveRowBtn;
        private ToolStripButton tilesetRemoveColumnBtn;
        private ToolStripSeparator toolStripSeparator15;
        private ToolStripButton shiftLeftBtn;
        private ToolStripButton shiftUpBtn;
        private ToolStripButton shiftRightBtn;
        private ToolStripButton shiftDownBtn;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem visualScriptingToolStripMenuItem;
        private FlatTabControl.FlatTabControl kryptonNavigator2;
        private FlatTabControl.FlatTabControl kryptonNavigator3;
        private FlatTabControl.FlatTabControl tabControl1;
        private ToolStripSeparator toolStripSeparator16;
        private ToolStripButton toolStripButton3;
        private ToolStripSeparator toolStripSeparator17;
        private ToolStripMenuItem projectSettingsToolStripMenuItem;
        private ToolStripMenuItem sceneSettingsToolStripMenuItem;
    }
}

