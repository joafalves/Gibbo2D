namespace Gibbo.Editor
{
    partial class BrushControl
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
            this.imageHolder = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.imageHolder)).BeginInit();
            this.SuspendLayout();
            // 
            // imageHolder
            // 
            this.imageHolder.Location = new System.Drawing.Point(0, 0);
            this.imageHolder.Name = "imageHolder";
            this.imageHolder.Size = new System.Drawing.Size(154, 217);
            this.imageHolder.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imageHolder.TabIndex = 1;
            this.imageHolder.TabStop = false;
            this.imageHolder.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imageHolder_MouseDown);
            this.imageHolder.MouseMove += new System.Windows.Forms.MouseEventHandler(this.imageHolder_MouseMove);
            this.imageHolder.MouseUp += new System.Windows.Forms.MouseEventHandler(this.imageHolder_MouseUp);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // BrushControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(230)))), ((int)(((byte)(232)))));
            this.Controls.Add(this.imageHolder);
            this.Name = "BrushControl";
            this.Size = new System.Drawing.Size(241, 307);
            this.Scroll += new System.Windows.Forms.ScrollEventHandler(this.BrushControl_Scroll);
            ((System.ComponentModel.ISupportInitialize)(this.imageHolder)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.PictureBox imageHolder;
        private System.Windows.Forms.Timer timer1;
    }
}
