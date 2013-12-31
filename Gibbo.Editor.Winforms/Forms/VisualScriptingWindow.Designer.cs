namespace Gibbo.Editor
{
    partial class VisualScriptingWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VisualScriptingWindow));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.flatTabControl2 = new FlatTabControl.FlatTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.visualScriptsComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.flatTabControl1 = new FlatTabControl.FlatTabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.treeViewAdv1 = new Aga.Controls.Tree.TreeViewAdv();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.onSceneStartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.visualScripting1 = new Gibbo.Editor.VisualScripting();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.flatTabControl2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.flatTabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.flatTabControl2);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(3, 3, 0, 3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.flatTabControl1);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.splitContainer1.Panel2Collapsed = true;
            this.splitContainer1.Size = new System.Drawing.Size(930, 533);
            this.splitContainer1.SplitterDistance = 686;
            this.splitContainer1.TabIndex = 1;
            // 
            // flatTabControl2
            // 
            this.flatTabControl2.Controls.Add(this.tabPage1);
            this.flatTabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flatTabControl2.Location = new System.Drawing.Point(3, 3);
            this.flatTabControl2.myBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(221)))), ((int)(((byte)(222)))));
            this.flatTabControl2.Name = "flatTabControl2";
            this.flatTabControl2.SelectedIndex = 0;
            this.flatTabControl2.Size = new System.Drawing.Size(927, 527);
            this.flatTabControl2.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.visualScripting1);
            this.tabPage1.Controls.Add(this.toolStrip1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(919, 498);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Visual Script Editor";
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onSceneStartToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(152, 26);
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.visualScriptsComboBox,
            this.toolStripLabel1,
            this.toolStripButton2,
            this.toolStripSeparator1,
            this.toolStripButton3});
            this.toolStrip1.Location = new System.Drawing.Point(3, 3);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(913, 28);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = global::Gibbo.Editor.Properties.Resources.edit_file;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(88, 25);
            this.toolStripButton1.Text = "Create New";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // visualScriptsComboBox
            // 
            this.visualScriptsComboBox.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.visualScriptsComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.visualScriptsComboBox.Name = "visualScriptsComboBox";
            this.visualScriptsComboBox.Size = new System.Drawing.Size(180, 28);
            this.visualScriptsComboBox.SelectedIndexChanged += new System.EventHandler(this.visualScriptsComboBox_SelectedIndexChanged);
            this.visualScriptsComboBox.TextChanged += new System.EventHandler(this.visualScriptsComboBox_TextChanged);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel1.Image = global::Gibbo.Editor.Properties.Resources.visual_Script;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(129, 25);
            this.toolStripLabel1.Text = "Active Visual Script: ";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = global::Gibbo.Editor.Properties.Resources.delete_page;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(103, 25);
            this.toolStripButton2.Text = "Delete Current";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 28);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Image = global::Gibbo.Editor.Properties.Resources.camera;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(99, 25);
            this.toolStripButton3.Text = "Reset Camera";
            this.toolStripButton3.ToolTipText = "Reset Camera Position";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // flatTabControl1
            // 
            this.flatTabControl1.Controls.Add(this.tabPage3);
            this.flatTabControl1.Controls.Add(this.tabPage4);
            this.flatTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flatTabControl1.Location = new System.Drawing.Point(0, 3);
            this.flatTabControl1.myBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(230)))), ((int)(((byte)(232)))));
            this.flatTabControl1.Name = "flatTabControl1";
            this.flatTabControl1.SelectedIndex = 0;
            this.flatTabControl1.Size = new System.Drawing.Size(93, 94);
            this.flatTabControl1.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.White;
            this.tabPage3.Controls.Add(this.treeViewAdv1);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(85, 65);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Visual Scripts";
            // 
            // treeViewAdv1
            // 
            this.treeViewAdv1.BackColor = System.Drawing.SystemColors.Window;
            this.treeViewAdv1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeViewAdv1.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeViewAdv1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewAdv1.DragDropMarkColor = System.Drawing.Color.Black;
            this.treeViewAdv1.LineColor = System.Drawing.SystemColors.ControlDark;
            this.treeViewAdv1.Location = new System.Drawing.Point(3, 3);
            this.treeViewAdv1.Model = null;
            this.treeViewAdv1.Name = "treeViewAdv1";
            this.treeViewAdv1.SelectedNode = null;
            this.treeViewAdv1.Size = new System.Drawing.Size(79, 59);
            this.treeViewAdv1.TabIndex = 0;
            this.treeViewAdv1.Text = "visualScriptsTreeView";
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.White;
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(85, 65);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "Variables";
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "visual_Script.png");
            // 
            // onSceneStartToolStripMenuItem
            // 
            this.onSceneStartToolStripMenuItem.Name = "onSceneStartToolStripMenuItem";
            this.onSceneStartToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.onSceneStartToolStripMenuItem.Text = "On Scene Start";
            this.onSceneStartToolStripMenuItem.Click += new System.EventHandler(this.onSceneStartToolStripMenuItem_Click);
            // 
            // visualScripting1
            // 
            this.visualScripting1.ActiveVisualScript = null;
            this.visualScripting1.BackColor = System.Drawing.Color.Black;
            this.visualScripting1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.visualScripting1.ContextMenuStrip = this.contextMenuStrip;
            this.visualScripting1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.visualScripting1.LeftMouseKeyDown = false;
            this.visualScripting1.Location = new System.Drawing.Point(3, 31);
            this.visualScripting1.MiddleMouseKeyDown = false;
            this.visualScripting1.Name = "visualScripting1";
            this.visualScripting1.Size = new System.Drawing.Size(913, 464);
            this.visualScripting1.TabIndex = 0;
            this.visualScripting1.VSync = false;
            this.visualScripting1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.visualScripting1_MouseDown_1);
            this.visualScripting1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.visualScripting1_MouseUp);
            // 
            // VisualScriptingWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(230)))), ((int)(((byte)(232)))));
            this.ClientSize = new System.Drawing.Size(930, 533);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "VisualScriptingWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Visual Scripting Tool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VisualScriptingWindow_FormClosing);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.flatTabControl2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.contextMenuStrip.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.flatTabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private VisualScripting visualScripting1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private FlatTabControl.FlatTabControl flatTabControl1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private FlatTabControl.FlatTabControl flatTabControl2;
        private System.Windows.Forms.TabPage tabPage1;
        private Aga.Controls.Tree.TreeViewAdv treeViewAdv1;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripComboBox visualScriptsComboBox;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripMenuItem onSceneStartToolStripMenuItem;
    }
}