namespace Gibbo.Editor
{
    partial class ProjectSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectSettings));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.flatTabControl1 = new FlatTabControl.FlatTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.projectNameTxt = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.highlightActiveTilesetGameCheckBox = new System.Windows.Forms.CheckBox();
            this.highlightActiveTilesetEditorCheckBox = new System.Windows.Forms.CheckBox();
            this.showCollisionsCheckBox = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.debugCheckBox = new System.Windows.Forms.CheckBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.aNumeric = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.bNumeric = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.gNumeric = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.rNumeric = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.colorPanel = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.showGridCheckBox = new System.Windows.Forms.CheckBox();
            this.snapToGridCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.displayLinesNumeric = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gridThicknessNumeric = new System.Windows.Forms.NumericUpDown();
            this.gridSpacingNumeric = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.screenHeightNumeric = new System.Windows.Forms.NumericUpDown();
            this.screenWidthNumeric = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.mouseVisibleCheckBox = new System.Windows.Forms.CheckBox();
            this.showConsoleCheckBox = new System.Windows.Forms.CheckBox();
            this.startFullScreenCheckBox = new System.Windows.Forms.CheckBox();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.button3 = new System.Windows.Forms.Button();
            this.flatTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.aNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rNumeric)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.displayLinesNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridThicknessNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSpacingNumeric)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.screenHeightNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.screenWidthNumeric)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(497, 389);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Apply";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(416, 389);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // flatTabControl1
            // 
            this.flatTabControl1.Controls.Add(this.tabPage1);
            this.flatTabControl1.Controls.Add(this.tabPage2);
            this.flatTabControl1.Controls.Add(this.tabPage3);
            this.flatTabControl1.Controls.Add(this.tabPage4);
            this.flatTabControl1.ImageList = this.imageList;
            this.flatTabControl1.Location = new System.Drawing.Point(5, 6);
            this.flatTabControl1.myBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(230)))), ((int)(((byte)(232)))));
            this.flatTabControl1.Name = "flatTabControl1";
            this.flatTabControl1.SelectedIndex = 0;
            this.flatTabControl1.Size = new System.Drawing.Size(571, 377);
            this.flatTabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(563, 348);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.projectNameTxt);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Location = new System.Drawing.Point(19, 18);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(522, 59);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Values";
            // 
            // projectNameTxt
            // 
            this.projectNameTxt.Location = new System.Drawing.Point(120, 23);
            this.projectNameTxt.Name = "projectNameTxt";
            this.projectNameTxt.Size = new System.Drawing.Size(370, 22);
            this.projectNameTxt.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Project Name:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.highlightActiveTilesetGameCheckBox);
            this.groupBox3.Controls.Add(this.highlightActiveTilesetEditorCheckBox);
            this.groupBox3.Controls.Add(this.showCollisionsCheckBox);
            this.groupBox3.Location = new System.Drawing.Point(19, 83);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(522, 106);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Options";
            // 
            // highlightActiveTilesetGameCheckBox
            // 
            this.highlightActiveTilesetGameCheckBox.AutoSize = true;
            this.highlightActiveTilesetGameCheckBox.Location = new System.Drawing.Point(19, 72);
            this.highlightActiveTilesetGameCheckBox.Name = "highlightActiveTilesetGameCheckBox";
            this.highlightActiveTilesetGameCheckBox.Size = new System.Drawing.Size(188, 17);
            this.highlightActiveTilesetGameCheckBox.TabIndex = 2;
            this.highlightActiveTilesetGameCheckBox.Text = "Highlight Active Tileset in Game";
            this.highlightActiveTilesetGameCheckBox.UseVisualStyleBackColor = true;
            // 
            // highlightActiveTilesetEditorCheckBox
            // 
            this.highlightActiveTilesetEditorCheckBox.AutoSize = true;
            this.highlightActiveTilesetEditorCheckBox.Location = new System.Drawing.Point(19, 49);
            this.highlightActiveTilesetEditorCheckBox.Name = "highlightActiveTilesetEditorCheckBox";
            this.highlightActiveTilesetEditorCheckBox.Size = new System.Drawing.Size(190, 17);
            this.highlightActiveTilesetEditorCheckBox.TabIndex = 1;
            this.highlightActiveTilesetEditorCheckBox.Text = "Highlight Active Tileset in Editor";
            this.highlightActiveTilesetEditorCheckBox.UseVisualStyleBackColor = true;
            // 
            // showCollisionsCheckBox
            // 
            this.showCollisionsCheckBox.AutoSize = true;
            this.showCollisionsCheckBox.Location = new System.Drawing.Point(19, 26);
            this.showCollisionsCheckBox.Name = "showCollisionsCheckBox";
            this.showCollisionsCheckBox.Size = new System.Drawing.Size(108, 17);
            this.showCollisionsCheckBox.TabIndex = 0;
            this.showCollisionsCheckBox.Text = "Show Collisions";
            this.showCollisionsCheckBox.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.White;
            this.tabPage2.Controls.Add(this.groupBox5);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(563, 348);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Build";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.debugCheckBox);
            this.groupBox5.Location = new System.Drawing.Point(21, 16);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(522, 59);
            this.groupBox5.TabIndex = 10;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Options";
            // 
            // debugCheckBox
            // 
            this.debugCheckBox.AutoSize = true;
            this.debugCheckBox.Location = new System.Drawing.Point(23, 25);
            this.debugCheckBox.Name = "debugCheckBox";
            this.debugCheckBox.Size = new System.Drawing.Size(61, 17);
            this.debugCheckBox.TabIndex = 1;
            this.debugCheckBox.Text = "Debug";
            this.debugCheckBox.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.White;
            this.tabPage3.Controls.Add(this.groupBox8);
            this.tabPage3.Controls.Add(this.groupBox2);
            this.tabPage3.Controls.Add(this.groupBox1);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(563, 348);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Grid";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.aNumeric);
            this.groupBox8.Controls.Add(this.label11);
            this.groupBox8.Controls.Add(this.bNumeric);
            this.groupBox8.Controls.Add(this.label10);
            this.groupBox8.Controls.Add(this.gNumeric);
            this.groupBox8.Controls.Add(this.label6);
            this.groupBox8.Controls.Add(this.rNumeric);
            this.groupBox8.Controls.Add(this.label5);
            this.groupBox8.Controls.Add(this.colorPanel);
            this.groupBox8.Controls.Add(this.label4);
            this.groupBox8.Location = new System.Drawing.Point(22, 228);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(522, 90);
            this.groupBox8.TabIndex = 8;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Color";
            // 
            // aNumeric
            // 
            this.aNumeric.Location = new System.Drawing.Point(214, 45);
            this.aNumeric.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.aNumeric.Name = "aNumeric";
            this.aNumeric.Size = new System.Drawing.Size(56, 22);
            this.aNumeric.TabIndex = 17;
            this.aNumeric.ValueChanged += new System.EventHandler(this.numericUpDown4_ValueChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(231, 28);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(14, 13);
            this.label11.TabIndex = 16;
            this.label11.Text = "A";
            // 
            // bNumeric
            // 
            this.bNumeric.Location = new System.Drawing.Point(151, 45);
            this.bNumeric.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.bNumeric.Name = "bNumeric";
            this.bNumeric.Size = new System.Drawing.Size(56, 22);
            this.bNumeric.TabIndex = 15;
            this.bNumeric.ValueChanged += new System.EventHandler(this.numericUpDown4_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(169, 28);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(14, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "B";
            // 
            // gNumeric
            // 
            this.gNumeric.Location = new System.Drawing.Point(88, 45);
            this.gNumeric.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.gNumeric.Name = "gNumeric";
            this.gNumeric.Size = new System.Drawing.Size(56, 22);
            this.gNumeric.TabIndex = 13;
            this.gNumeric.ValueChanged += new System.EventHandler(this.numericUpDown4_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(107, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(15, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "G";
            // 
            // rNumeric
            // 
            this.rNumeric.Location = new System.Drawing.Point(25, 45);
            this.rNumeric.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.rNumeric.Name = "rNumeric";
            this.rNumeric.Size = new System.Drawing.Size(56, 22);
            this.rNumeric.TabIndex = 11;
            this.rNumeric.ValueChanged += new System.EventHandler(this.numericUpDown4_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(43, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "R";
            // 
            // colorPanel
            // 
            this.colorPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.colorPanel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.colorPanel.Location = new System.Drawing.Point(291, 45);
            this.colorPanel.Name = "colorPanel";
            this.colorPanel.Size = new System.Drawing.Size(56, 21);
            this.colorPanel.TabIndex = 9;
            this.colorPanel.Click += new System.EventHandler(this.colorPanel_Click);
            this.colorPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.colorPanel_Paint);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(296, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Preview";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.showGridCheckBox);
            this.groupBox2.Controls.Add(this.snapToGridCheckBox);
            this.groupBox2.Location = new System.Drawing.Point(22, 15);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(522, 79);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Options";
            // 
            // showGridCheckBox
            // 
            this.showGridCheckBox.AutoSize = true;
            this.showGridCheckBox.Location = new System.Drawing.Point(19, 26);
            this.showGridCheckBox.Name = "showGridCheckBox";
            this.showGridCheckBox.Size = new System.Drawing.Size(80, 17);
            this.showGridCheckBox.TabIndex = 0;
            this.showGridCheckBox.Text = "Show Grid";
            this.showGridCheckBox.UseVisualStyleBackColor = true;
            // 
            // snapToGridCheckBox
            // 
            this.snapToGridCheckBox.AutoSize = true;
            this.snapToGridCheckBox.Location = new System.Drawing.Point(19, 51);
            this.snapToGridCheckBox.Name = "snapToGridCheckBox";
            this.snapToGridCheckBox.Size = new System.Drawing.Size(91, 17);
            this.snapToGridCheckBox.TabIndex = 1;
            this.snapToGridCheckBox.Text = "Snap to Grid";
            this.snapToGridCheckBox.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.displayLinesNumeric);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.gridThicknessNumeric);
            this.groupBox1.Controls.Add(this.gridSpacingNumeric);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(22, 100);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(522, 122);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Values";
            // 
            // displayLinesNumeric
            // 
            this.displayLinesNumeric.Location = new System.Drawing.Point(125, 88);
            this.displayLinesNumeric.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.displayLinesNumeric.Name = "displayLinesNumeric";
            this.displayLinesNumeric.Size = new System.Drawing.Size(120, 22);
            this.displayLinesNumeric.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Display Lines:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Grid Spacing:";
            // 
            // gridThicknessNumeric
            // 
            this.gridThicknessNumeric.Location = new System.Drawing.Point(125, 58);
            this.gridThicknessNumeric.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.gridThicknessNumeric.Name = "gridThicknessNumeric";
            this.gridThicknessNumeric.Size = new System.Drawing.Size(120, 22);
            this.gridThicknessNumeric.TabIndex = 5;
            // 
            // gridSpacingNumeric
            // 
            this.gridSpacingNumeric.Location = new System.Drawing.Point(125, 28);
            this.gridSpacingNumeric.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.gridSpacingNumeric.Name = "gridSpacingNumeric";
            this.gridSpacingNumeric.Size = new System.Drawing.Size(120, 22);
            this.gridSpacingNumeric.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Grid Thickness:";
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.White;
            this.tabPage4.Controls.Add(this.groupBox7);
            this.tabPage4.Controls.Add(this.groupBox6);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(563, 348);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Game";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.label8);
            this.groupBox7.Controls.Add(this.screenHeightNumeric);
            this.groupBox7.Controls.Add(this.screenWidthNumeric);
            this.groupBox7.Controls.Add(this.label9);
            this.groupBox7.Location = new System.Drawing.Point(22, 127);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(522, 98);
            this.groupBox7.TabIndex = 9;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Values";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(23, 32);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Screen Width";
            // 
            // screenHeightNumeric
            // 
            this.screenHeightNumeric.Location = new System.Drawing.Point(125, 58);
            this.screenHeightNumeric.Maximum = new decimal(new int[] {
            1536,
            0,
            0,
            0});
            this.screenHeightNumeric.Name = "screenHeightNumeric";
            this.screenHeightNumeric.Size = new System.Drawing.Size(120, 22);
            this.screenHeightNumeric.TabIndex = 5;
            // 
            // screenWidthNumeric
            // 
            this.screenWidthNumeric.Location = new System.Drawing.Point(125, 28);
            this.screenWidthNumeric.Maximum = new decimal(new int[] {
            2048,
            0,
            0,
            0});
            this.screenWidthNumeric.Name = "screenWidthNumeric";
            this.screenWidthNumeric.Size = new System.Drawing.Size(120, 22);
            this.screenWidthNumeric.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(23, 60);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Screen Height:";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.mouseVisibleCheckBox);
            this.groupBox6.Controls.Add(this.showConsoleCheckBox);
            this.groupBox6.Controls.Add(this.startFullScreenCheckBox);
            this.groupBox6.Location = new System.Drawing.Point(22, 15);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(522, 106);
            this.groupBox6.TabIndex = 8;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Options";
            // 
            // mouseVisibleCheckBox
            // 
            this.mouseVisibleCheckBox.AutoSize = true;
            this.mouseVisibleCheckBox.Location = new System.Drawing.Point(19, 76);
            this.mouseVisibleCheckBox.Name = "mouseVisibleCheckBox";
            this.mouseVisibleCheckBox.Size = new System.Drawing.Size(98, 17);
            this.mouseVisibleCheckBox.TabIndex = 2;
            this.mouseVisibleCheckBox.Text = "Mouse Visible";
            this.mouseVisibleCheckBox.UseVisualStyleBackColor = true;
            // 
            // showConsoleCheckBox
            // 
            this.showConsoleCheckBox.AutoSize = true;
            this.showConsoleCheckBox.Location = new System.Drawing.Point(19, 26);
            this.showConsoleCheckBox.Name = "showConsoleCheckBox";
            this.showConsoleCheckBox.Size = new System.Drawing.Size(100, 17);
            this.showConsoleCheckBox.TabIndex = 0;
            this.showConsoleCheckBox.Text = "Show Console";
            this.showConsoleCheckBox.UseVisualStyleBackColor = true;
            this.showConsoleCheckBox.CheckedChanged += new System.EventHandler(this.showConsoleCheckBox_CheckedChanged);
            // 
            // startFullScreenCheckBox
            // 
            this.startFullScreenCheckBox.AutoSize = true;
            this.startFullScreenCheckBox.Location = new System.Drawing.Point(19, 51);
            this.startFullScreenCheckBox.Name = "startFullScreenCheckBox";
            this.startFullScreenCheckBox.Size = new System.Drawing.Size(126, 17);
            this.startFullScreenCheckBox.TabIndex = 1;
            this.startFullScreenCheckBox.Text = "Start on Full Screen";
            this.startFullScreenCheckBox.UseVisualStyleBackColor = true;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "system.png");
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(335, 389);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "Accept";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // ProjectSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(230)))), ((int)(((byte)(232)))));
            this.ClientSize = new System.Drawing.Size(583, 417);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.flatTabControl1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProjectSettings";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Project Settings";
            this.Load += new System.EventHandler(this.ProjectSettings_Load);
            this.flatTabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.aNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rNumeric)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.displayLinesNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridThicknessNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSpacingNumeric)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.screenHeightNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.screenWidthNumeric)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private FlatTabControl.FlatTabControl flatTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.CheckBox showGridCheckBox;
        private System.Windows.Forms.CheckBox snapToGridCheckBox;
        private System.Windows.Forms.NumericUpDown gridSpacingNumeric;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown gridThicknessNumeric;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown displayLinesNumeric;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel colorPanel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox showCollisionsCheckBox;
        private System.Windows.Forms.CheckBox highlightActiveTilesetEditorCheckBox;
        private System.Windows.Forms.CheckBox highlightActiveTilesetGameCheckBox;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox projectNameTxt;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox debugCheckBox;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.CheckBox showConsoleCheckBox;
        private System.Windows.Forms.CheckBox startFullScreenCheckBox;
        private System.Windows.Forms.CheckBox mouseVisibleCheckBox;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown screenHeightNumeric;
        private System.Windows.Forms.NumericUpDown screenWidthNumeric;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.NumericUpDown bNumeric;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown gNumeric;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown rNumeric;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown aNumeric;
        private System.Windows.Forms.Label label11;
    }
}