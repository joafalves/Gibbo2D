namespace Gibbo.Editor
{
    partial class FolderTreeViewControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FolderTreeViewControl));
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.directoryContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.createNewItemToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.addGameSceneMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.renameToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.treeView = new Gibbo.Editor.DragDropTreeView();
            this.directoryContextMenu.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.fileContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "folder.png");
            this.imageList.Images.SetKeyName(1, "new.png");
            this.imageList.Images.SetKeyName(2, "image.png");
            this.imageList.Images.SetKeyName(3, "text_file.png");
            this.imageList.Images.SetKeyName(4, "system.png");
            this.imageList.Images.SetKeyName(5, "csharp1.png");
            this.imageList.Images.SetKeyName(6, "scene.png");
            this.imageList.Images.SetKeyName(7, "solution2.png");
            // 
            // directoryContextMenu
            // 
            this.directoryContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createNewItemToolStripMenuItem1,
            this.createDirectoryToolStripMenuItem,
            this.toolStripSeparator1,
            this.renameToolStripMenuItem,
            this.removeDirectoryToolStripMenuItem});
            this.directoryContextMenu.Name = "directoryContextMenu";
            this.directoryContextMenu.Size = new System.Drawing.Size(145, 98);
            // 
            // createNewItemToolStripMenuItem1
            // 
            this.createNewItemToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addGameSceneMenuItem,
            this.toolStripSeparator2,
            this.cScriptToolStripMenuItem});
            this.createNewItemToolStripMenuItem1.Image = global::Gibbo.Editor.Properties.Resources.add;
            this.createNewItemToolStripMenuItem1.Name = "createNewItemToolStripMenuItem1";
            this.createNewItemToolStripMenuItem1.Size = new System.Drawing.Size(144, 22);
            this.createNewItemToolStripMenuItem1.Text = "Create...";
            // 
            // addGameSceneMenuItem
            // 
            this.addGameSceneMenuItem.Image = global::Gibbo.Editor.Properties.Resources.scene;
            this.addGameSceneMenuItem.Name = "addGameSceneMenuItem";
            this.addGameSceneMenuItem.Size = new System.Drawing.Size(139, 22);
            this.addGameSceneMenuItem.Text = "Game Scene";
            this.addGameSceneMenuItem.Click += new System.EventHandler(this.addGameSceneMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(136, 6);
            this.toolStripSeparator2.Visible = false;
            // 
            // cScriptToolStripMenuItem
            // 
            this.cScriptToolStripMenuItem.Image = global::Gibbo.Editor.Properties.Resources.csharp1;
            this.cScriptToolStripMenuItem.Name = "cScriptToolStripMenuItem";
            this.cScriptToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.cScriptToolStripMenuItem.Text = "C# Script";
            this.cScriptToolStripMenuItem.Visible = false;
            this.cScriptToolStripMenuItem.Click += new System.EventHandler(this.cScriptToolStripMenuItem_Click);
            // 
            // createDirectoryToolStripMenuItem
            // 
            this.createDirectoryToolStripMenuItem.Image = global::Gibbo.Editor.Properties.Resources.add_folder;
            this.createDirectoryToolStripMenuItem.Name = "createDirectoryToolStripMenuItem";
            this.createDirectoryToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.createDirectoryToolStripMenuItem.Text = "Create Folder";
            this.createDirectoryToolStripMenuItem.Click += new System.EventHandler(this.createDirectoryToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(141, 6);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Image = global::Gibbo.Editor.Properties.Resources.rename;
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.renameToolStripMenuItem.Text = "Rename";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
            // 
            // removeDirectoryToolStripMenuItem
            // 
            this.removeDirectoryToolStripMenuItem.Image = global::Gibbo.Editor.Properties.Resources.delete;
            this.removeDirectoryToolStripMenuItem.Name = "removeDirectoryToolStripMenuItem";
            this.removeDirectoryToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.removeDirectoryToolStripMenuItem.Text = "Remove";
            this.removeDirectoryToolStripMenuItem.Click += new System.EventHandler(this.removeDirectoryToolStripMenuItem_Click);
            // 
            // contextMenuStrip
            // 
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
            // fileContextMenu
            // 
            this.fileContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.toolStripSeparator3,
            this.renameToolStripMenuItem1,
            this.deleteToolStripMenuItem});
            this.fileContextMenu.Name = "fileContextMenu";
            this.fileContextMenu.Size = new System.Drawing.Size(118, 76);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = global::Gibbo.Editor.Properties.Resources.folder;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(114, 6);
            // 
            // renameToolStripMenuItem1
            // 
            this.renameToolStripMenuItem1.Image = global::Gibbo.Editor.Properties.Resources.rename;
            this.renameToolStripMenuItem1.Name = "renameToolStripMenuItem1";
            this.renameToolStripMenuItem1.Size = new System.Drawing.Size(117, 22);
            this.renameToolStripMenuItem1.Text = "Rename";
            this.renameToolStripMenuItem1.Click += new System.EventHandler(this.renameToolStripMenuItem1_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = global::Gibbo.Editor.Properties.Resources.delete;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
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
            this.treeView.SelectedImageIndex = 0;
            this.treeView.ShowLines = false;
            this.treeView.ShowRootLines = false;
            this.treeView.Size = new System.Drawing.Size(414, 325);
            this.treeView.TabIndex = 1;
            this.treeView.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeView_BeforeLabelEdit);
            this.treeView.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeView_AfterLabelEdit);
            this.treeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView_BeforeExpand);
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            this.treeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseClick_1);
            this.treeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseDoubleClick);
            // 
            // FolderTreeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeView);
            this.Name = "FolderTreeView";
            this.Size = new System.Drawing.Size(414, 325);
            this.directoryContextMenu.ResumeLayout(false);
            this.contextMenuStrip.ResumeLayout(false);
            this.fileContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ContextMenuStrip directoryContextMenu;
        private System.Windows.Forms.ToolStripMenuItem createDirectoryToolStripMenuItem;
        private DragDropTreeView treeView;
        private System.Windows.Forms.ToolStripMenuItem removeDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createNewItemToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem cScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addGameSceneMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ContextMenuStrip fileContextMenu;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
    }
}
