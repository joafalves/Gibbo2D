namespace Gibbo.Editor
{
    partial class SceneTreeViewControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SceneTreeViewControl));
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("No scene in memory");
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LayerContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addGameObjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.emptyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.spriteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.animatedSpriteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.bMTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.tilesetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.audioObjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addFromStateToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.moveUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.objectContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addComponentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addGameObjectToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.emptyToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.spriteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.animatedSpriteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.tilesetToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.audioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addFromStateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.savePrefabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadStateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.componentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertiesStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.rootContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.treeView = new Gibbo.Editor.DragDropTreeView();
            this.bMFontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.contextMenuStrip.SuspendLayout();
            this.LayerContextMenuStrip.SuspendLayout();
            this.objectContextMenuStrip.SuspendLayout();
            this.rootContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "hierarchy.png");
            this.imageList.Images.SetKeyName(1, "picture.png");
            this.imageList.Images.SetKeyName(2, "game.png");
            this.imageList.Images.SetKeyName(3, "game_empty.png");
            this.imageList.Images.SetKeyName(4, "game_anime.png");
            this.imageList.Images.SetKeyName(5, "game_audio.png");
            this.imageList.Images.SetKeyName(6, "game_tileset.png");
            this.imageList.Images.SetKeyName(7, "game_text.png");
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(114, 26);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Image = global::Gibbo.Editor.Properties.Resources.refresh;
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // LayerContextMenuStrip
            // 
            this.LayerContextMenuStrip.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.LayerContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addGameObjectToolStripMenuItem,
            this.addFromStateToolStripMenuItem1,
            this.toolStripSeparator1,
            this.pasteToolStripMenuItem,
            this.removeToolStripMenuItem,
            this.toolStripSeparator2,
            this.renameToolStripMenuItem,
            this.toolStripSeparator3,
            this.moveUpToolStripMenuItem,
            this.moveDownToolStripMenuItem});
            this.LayerContextMenuStrip.Name = "LayerContextMenuStrip";
            this.LayerContextMenuStrip.Size = new System.Drawing.Size(169, 176);
            // 
            // addGameObjectToolStripMenuItem
            // 
            this.addGameObjectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.emptyToolStripMenuItem,
            this.toolStripSeparator9,
            this.spriteToolStripMenuItem,
            this.animatedSpriteToolStripMenuItem,
            this.toolStripSeparator14,
            this.bMTextToolStripMenuItem,
            this.toolStripSeparator12,
            this.tilesetToolStripMenuItem,
            this.toolStripSeparator10,
            this.audioObjectToolStripMenuItem});
            this.addGameObjectToolStripMenuItem.Image = global::Gibbo.Editor.Properties.Resources.game;
            this.addGameObjectToolStripMenuItem.Name = "addGameObjectToolStripMenuItem";
            this.addGameObjectToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.addGameObjectToolStripMenuItem.Text = "Add Game Object";
            // 
            // emptyToolStripMenuItem
            // 
            this.emptyToolStripMenuItem.Image = global::Gibbo.Editor.Properties.Resources.game_empty;
            this.emptyToolStripMenuItem.Name = "emptyToolStripMenuItem";
            this.emptyToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.emptyToolStripMenuItem.Text = "Empty";
            this.emptyToolStripMenuItem.Click += new System.EventHandler(this.emptyToolStripMenuItem_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(156, 6);
            // 
            // spriteToolStripMenuItem
            // 
            this.spriteToolStripMenuItem.Image = global::Gibbo.Editor.Properties.Resources.game;
            this.spriteToolStripMenuItem.Name = "spriteToolStripMenuItem";
            this.spriteToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.spriteToolStripMenuItem.Text = "Sprite";
            this.spriteToolStripMenuItem.Click += new System.EventHandler(this.spriteToolStripMenuItem_Click);
            // 
            // animatedSpriteToolStripMenuItem
            // 
            this.animatedSpriteToolStripMenuItem.Image = global::Gibbo.Editor.Properties.Resources.game_anime;
            this.animatedSpriteToolStripMenuItem.Name = "animatedSpriteToolStripMenuItem";
            this.animatedSpriteToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.animatedSpriteToolStripMenuItem.Text = "Animated Sprite";
            this.animatedSpriteToolStripMenuItem.Click += new System.EventHandler(this.animatedSpriteToolStripMenuItem_Click);
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            this.toolStripSeparator14.Size = new System.Drawing.Size(156, 6);
            // 
            // bMTextToolStripMenuItem
            // 
            this.bMTextToolStripMenuItem.Image = global::Gibbo.Editor.Properties.Resources.game_text;
            this.bMTextToolStripMenuItem.Name = "bMTextToolStripMenuItem";
            this.bMTextToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.bMTextToolStripMenuItem.Text = "BM Font";
            this.bMTextToolStripMenuItem.Click += new System.EventHandler(this.bMFontToolStripMenuItem_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(156, 6);
            // 
            // tilesetToolStripMenuItem
            // 
            this.tilesetToolStripMenuItem.Image = global::Gibbo.Editor.Properties.Resources.game_tileset;
            this.tilesetToolStripMenuItem.Name = "tilesetToolStripMenuItem";
            this.tilesetToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.tilesetToolStripMenuItem.Text = "Tileset";
            this.tilesetToolStripMenuItem.Click += new System.EventHandler(this.tilesetToolStripMenuItem_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(156, 6);
            // 
            // audioObjectToolStripMenuItem
            // 
            this.audioObjectToolStripMenuItem.Image = global::Gibbo.Editor.Properties.Resources.game_audio;
            this.audioObjectToolStripMenuItem.Name = "audioObjectToolStripMenuItem";
            this.audioObjectToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.audioObjectToolStripMenuItem.Text = "Audio";
            this.audioObjectToolStripMenuItem.Click += new System.EventHandler(this.audioObjectToolStripMenuItem_Click);
            // 
            // addFromStateToolStripMenuItem1
            // 
            this.addFromStateToolStripMenuItem1.Image = global::Gibbo.Editor.Properties.Resources.folder;
            this.addFromStateToolStripMenuItem1.Name = "addFromStateToolStripMenuItem1";
            this.addFromStateToolStripMenuItem1.Size = new System.Drawing.Size(168, 22);
            this.addFromStateToolStripMenuItem1.Text = "Add From State...";
            this.addFromStateToolStripMenuItem1.Click += new System.EventHandler(this.addFromStateToolStripMenuItem1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(165, 6);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Image = global::Gibbo.Editor.Properties.Resources.paste;
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.pasteToolStripMenuItem.Text = "Paste Object";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Image = global::Gibbo.Editor.Properties.Resources.delete;
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.removeToolStripMenuItem.Text = "Delete Layer";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(165, 6);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Image = global::Gibbo.Editor.Properties.Resources.rename;
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.renameToolStripMenuItem.Text = "Rename";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(165, 6);
            // 
            // moveUpToolStripMenuItem
            // 
            this.moveUpToolStripMenuItem.Image = global::Gibbo.Editor.Properties.Resources.up;
            this.moveUpToolStripMenuItem.Name = "moveUpToolStripMenuItem";
            this.moveUpToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.moveUpToolStripMenuItem.Text = "Move Up";
            this.moveUpToolStripMenuItem.Click += new System.EventHandler(this.moveUpToolStripMenuItem_Click);
            // 
            // moveDownToolStripMenuItem
            // 
            this.moveDownToolStripMenuItem.Image = global::Gibbo.Editor.Properties.Resources.down;
            this.moveDownToolStripMenuItem.Name = "moveDownToolStripMenuItem";
            this.moveDownToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.moveDownToolStripMenuItem.Text = "Move Down";
            this.moveDownToolStripMenuItem.Click += new System.EventHandler(this.moveDownToolStripMenuItem_Click);
            // 
            // objectContextMenuStrip
            // 
            this.objectContextMenuStrip.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.objectContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addComponentToolStripMenuItem,
            this.addGameObjectToolStripMenuItem1,
            this.addFromStateToolStripMenuItem,
            this.toolStripSeparator5,
            this.savePrefabToolStripMenuItem,
            this.loadStateToolStripMenuItem,
            this.toolStripSeparator7,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.toolStripMenuItem3,
            this.removeToolStripMenuItem1,
            this.toolStripSeparator8,
            this.componentsToolStripMenuItem,
            this.propertiesStripMenuItem,
            this.renameToolStripMenuItem1,
            this.toolStripSeparator4,
            this.toolStripMenuItem1,
            this.toolStripMenuItem2});
            this.objectContextMenuStrip.Name = "LayerContextMenuStrip";
            this.objectContextMenuStrip.Size = new System.Drawing.Size(169, 358);
            this.objectContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.objectContextMenuStrip_Opening);
            // 
            // addComponentToolStripMenuItem
            // 
            this.addComponentToolStripMenuItem.Image = global::Gibbo.Editor.Properties.Resources.component;
            this.addComponentToolStripMenuItem.Name = "addComponentToolStripMenuItem";
            this.addComponentToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.addComponentToolStripMenuItem.Text = "Add Component";
            // 
            // addGameObjectToolStripMenuItem1
            // 
            this.addGameObjectToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.emptyToolStripMenuItem1,
            this.toolStripSeparator6,
            this.spriteToolStripMenuItem1,
            this.animatedSpriteToolStripMenuItem1,
            this.toolStripSeparator11,
            this.bMFontToolStripMenuItem,
            this.toolStripSeparator15,
            this.tilesetToolStripMenuItem1,
            this.toolStripSeparator13,
            this.audioToolStripMenuItem});
            this.addGameObjectToolStripMenuItem1.Image = global::Gibbo.Editor.Properties.Resources.game;
            this.addGameObjectToolStripMenuItem1.Name = "addGameObjectToolStripMenuItem1";
            this.addGameObjectToolStripMenuItem1.Size = new System.Drawing.Size(168, 22);
            this.addGameObjectToolStripMenuItem1.Text = "Add Game Object";
            // 
            // emptyToolStripMenuItem1
            // 
            this.emptyToolStripMenuItem1.Image = global::Gibbo.Editor.Properties.Resources.game_empty;
            this.emptyToolStripMenuItem1.Name = "emptyToolStripMenuItem1";
            this.emptyToolStripMenuItem1.Size = new System.Drawing.Size(159, 22);
            this.emptyToolStripMenuItem1.Text = "Empty";
            this.emptyToolStripMenuItem1.Click += new System.EventHandler(this.emptyToolStripMenuItem1_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(156, 6);
            // 
            // spriteToolStripMenuItem1
            // 
            this.spriteToolStripMenuItem1.Image = global::Gibbo.Editor.Properties.Resources.game;
            this.spriteToolStripMenuItem1.Name = "spriteToolStripMenuItem1";
            this.spriteToolStripMenuItem1.Size = new System.Drawing.Size(159, 22);
            this.spriteToolStripMenuItem1.Text = "Sprite";
            this.spriteToolStripMenuItem1.Click += new System.EventHandler(this.spriteToolStripMenuItem1_Click);
            // 
            // animatedSpriteToolStripMenuItem1
            // 
            this.animatedSpriteToolStripMenuItem1.Image = global::Gibbo.Editor.Properties.Resources.game_anime;
            this.animatedSpriteToolStripMenuItem1.Name = "animatedSpriteToolStripMenuItem1";
            this.animatedSpriteToolStripMenuItem1.Size = new System.Drawing.Size(159, 22);
            this.animatedSpriteToolStripMenuItem1.Text = "Animated Sprite";
            this.animatedSpriteToolStripMenuItem1.Click += new System.EventHandler(this.animatedSpriteToolStripMenuItem1_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(156, 6);
            // 
            // tilesetToolStripMenuItem1
            // 
            this.tilesetToolStripMenuItem1.Image = global::Gibbo.Editor.Properties.Resources.game_tileset;
            this.tilesetToolStripMenuItem1.Name = "tilesetToolStripMenuItem1";
            this.tilesetToolStripMenuItem1.Size = new System.Drawing.Size(159, 22);
            this.tilesetToolStripMenuItem1.Text = "Tileset";
            this.tilesetToolStripMenuItem1.Click += new System.EventHandler(this.tilesetToolStripMenuItem1_Click);
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(156, 6);
            // 
            // audioToolStripMenuItem
            // 
            this.audioToolStripMenuItem.Image = global::Gibbo.Editor.Properties.Resources.game_audio;
            this.audioToolStripMenuItem.Name = "audioToolStripMenuItem";
            this.audioToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.audioToolStripMenuItem.Text = "Audio";
            this.audioToolStripMenuItem.Click += new System.EventHandler(this.audioToolStripMenuItem_Click);
            // 
            // addFromStateToolStripMenuItem
            // 
            this.addFromStateToolStripMenuItem.Name = "addFromStateToolStripMenuItem";
            this.addFromStateToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.addFromStateToolStripMenuItem.Text = "Add From State...";
            this.addFromStateToolStripMenuItem.Click += new System.EventHandler(this.addFromStateToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(165, 6);
            // 
            // savePrefabToolStripMenuItem
            // 
            this.savePrefabToolStripMenuItem.Image = global::Gibbo.Editor.Properties.Resources.save;
            this.savePrefabToolStripMenuItem.Name = "savePrefabToolStripMenuItem";
            this.savePrefabToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.savePrefabToolStripMenuItem.Text = "Save State...";
            this.savePrefabToolStripMenuItem.Click += new System.EventHandler(this.savePrefabToolStripMenuItem_Click);
            // 
            // loadStateToolStripMenuItem
            // 
            this.loadStateToolStripMenuItem.Name = "loadStateToolStripMenuItem";
            this.loadStateToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.loadStateToolStripMenuItem.Text = "Load State...";
            this.loadStateToolStripMenuItem.Click += new System.EventHandler(this.loadStateToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(165, 6);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Image = global::Gibbo.Editor.Properties.Resources.cut;
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.cutToolStripMenuItem.Text = "Cut";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Image = global::Gibbo.Editor.Properties.Resources.copy;
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Image = global::Gibbo.Editor.Properties.Resources.paste;
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(168, 22);
            this.toolStripMenuItem3.Text = "Paste";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // removeToolStripMenuItem1
            // 
            this.removeToolStripMenuItem1.Image = global::Gibbo.Editor.Properties.Resources.delete;
            this.removeToolStripMenuItem1.Name = "removeToolStripMenuItem1";
            this.removeToolStripMenuItem1.Size = new System.Drawing.Size(168, 22);
            this.removeToolStripMenuItem1.Text = "Delete";
            this.removeToolStripMenuItem1.Click += new System.EventHandler(this.removeToolStripMenuItem1_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(165, 6);
            // 
            // componentsToolStripMenuItem
            // 
            this.componentsToolStripMenuItem.Image = global::Gibbo.Editor.Properties.Resources.component_item;
            this.componentsToolStripMenuItem.Name = "componentsToolStripMenuItem";
            this.componentsToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.componentsToolStripMenuItem.Text = "Components";
            this.componentsToolStripMenuItem.Click += new System.EventHandler(this.componentsToolStripMenuItem_Click);
            // 
            // propertiesStripMenuItem
            // 
            this.propertiesStripMenuItem.Image = global::Gibbo.Editor.Properties.Resources.properties;
            this.propertiesStripMenuItem.Name = "propertiesStripMenuItem";
            this.propertiesStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.propertiesStripMenuItem.Text = "Properties";
            this.propertiesStripMenuItem.Click += new System.EventHandler(this.propertiesStripMenuItem_Click);
            // 
            // renameToolStripMenuItem1
            // 
            this.renameToolStripMenuItem1.Image = global::Gibbo.Editor.Properties.Resources.rename;
            this.renameToolStripMenuItem1.Name = "renameToolStripMenuItem1";
            this.renameToolStripMenuItem1.Size = new System.Drawing.Size(168, 22);
            this.renameToolStripMenuItem1.Text = "Rename";
            this.renameToolStripMenuItem1.Click += new System.EventHandler(this.renameToolStripMenuItem1_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(165, 6);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Image = global::Gibbo.Editor.Properties.Resources.up;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(168, 22);
            this.toolStripMenuItem1.Text = "Move Up";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Image = global::Gibbo.Editor.Properties.Resources.down;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(168, 22);
            this.toolStripMenuItem2.Text = "Move Down";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // rootContextMenuStrip
            // 
            this.rootContextMenuStrip.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.rootContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addLayerToolStripMenuItem});
            this.rootContextMenuStrip.Name = "rootContextMenuStrip";
            this.rootContextMenuStrip.Size = new System.Drawing.Size(128, 26);
            // 
            // addLayerToolStripMenuItem
            // 
            this.addLayerToolStripMenuItem.Image = global::Gibbo.Editor.Properties.Resources.add;
            this.addLayerToolStripMenuItem.Name = "addLayerToolStripMenuItem";
            this.addLayerToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.addLayerToolStripMenuItem.Text = "Add Layer";
            this.addLayerToolStripMenuItem.Click += new System.EventHandler(this.addLayerToolStripMenuItem_Click);
            // 
            // treeView
            // 
            this.treeView.BackColor = System.Drawing.Color.White;
            this.treeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView.ContextMenuStrip = this.contextMenuStrip;
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.treeView.FullRowSelect = true;
            this.treeView.HotTracking = true;
            this.treeView.ImageIndex = 0;
            this.treeView.ImageList = this.imageList;
            this.treeView.Indent = 15;
            this.treeView.ItemHeight = 18;
            this.treeView.LabelEdit = true;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView";
            treeNode2.ImageKey = "(default)";
            treeNode2.Name = "Node0";
            treeNode2.Text = "No scene in memory";
            this.treeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2});
            this.treeView.SelectedImageIndex = 0;
            this.treeView.ShowLines = false;
            this.treeView.ShowRootLines = false;
            this.treeView.Size = new System.Drawing.Size(415, 374);
            this.treeView.TabIndex = 2;
            this.treeView.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeView_AfterLabelEdit);
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            this.treeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseClick);
            this.treeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseDoubleClick);
            // 
            // bMFontToolStripMenuItem
            // 
            this.bMFontToolStripMenuItem.Image = global::Gibbo.Editor.Properties.Resources.game_text;
            this.bMFontToolStripMenuItem.Name = "bMFontToolStripMenuItem";
            this.bMFontToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.bMFontToolStripMenuItem.Text = "BMFont";
            this.bMFontToolStripMenuItem.Click += new System.EventHandler(this.bMFontToolStripMenuItem_Click_1);
            // 
            // toolStripSeparator15
            // 
            this.toolStripSeparator15.Name = "toolStripSeparator15";
            this.toolStripSeparator15.Size = new System.Drawing.Size(156, 6);
            // 
            // SceneTreeViewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeView);
            this.Name = "SceneTreeViewControl";
            this.Size = new System.Drawing.Size(415, 374);
            this.contextMenuStrip.ResumeLayout(false);
            this.LayerContextMenuStrip.ResumeLayout(false);
            this.objectContextMenuStrip.ResumeLayout(false);
            this.rootContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DragDropTreeView treeView;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip LayerContextMenuStrip;
        private System.Windows.Forms.ContextMenuStrip objectContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem addGameObjectToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip rootContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem addLayerToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem moveUpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveDownToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem addGameObjectToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem propertiesStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addComponentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem componentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem savePrefabToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem loadStateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addFromStateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addFromStateToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem emptyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spriteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem emptyToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem spriteToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem animatedSpriteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem animatedSpriteToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripMenuItem audioObjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripMenuItem audioToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripMenuItem tilesetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tilesetToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
        private System.Windows.Forms.ToolStripMenuItem bMTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bMFontToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;

    }
}
