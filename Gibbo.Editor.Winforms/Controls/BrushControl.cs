using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;
using Gibbo.Library;

namespace Gibbo.Editor
{
    public partial class BrushControl : UserControl
    {
        #region fields

        private string imagePath = string.Empty;
        private Image activeImage;
        private Point selectionStartPoint;
        private Rectangle selectionRectangle;
        private bool selectionStarted;
        private int brushSizeX;
        private int brushSizeY;

        #endregion

        #region properties

        public int BrushSizeX
        {
            get { return brushSizeX; }
            set { brushSizeX = value; }
        }

        public int BrushSizeY
        {
            get { return brushSizeY; }
            set { brushSizeY = value; }
        }

        public Rectangle SelectionRectangle
        {
            get { return selectionRectangle; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Microsoft.Xna.Framework.Rectangle CurrentSelectionXNA
        {
            get
            {
                return new Microsoft.Xna.Framework.Rectangle()
                {
                    X = SelectionRectangle.X,
                    Y = SelectionRectangle.Y,
                    Width = SelectionRectangle.Width,
                    Height = SelectionRectangle.Height
                };
            }
        }

        #endregion

        #region constructors

        public BrushControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region methods

        /// <summary>
        /// 
        /// </summary>
        private void Initialize()
        {
            activeImage = new Bitmap(1, 1);
            brushSizeX = 32;
            brushSizeY = 32;
        }

        /// <summary>
        /// 
        /// </summary>
        private void DrawSelection()
        {
            if (imageHolder.Image == null) return;

            if (brushSizeX == 0 || brushSizeY == 0) return;

            Graphics gr = Graphics.FromImage(imageHolder.Image);

            gr.Clear(Color.Transparent);

            Color myColor = new Color();
            myColor = Color.FromArgb(100, 57, 87, 180);
            SolidBrush myBrush = new SolidBrush(myColor);

            Pen pen = new Pen(Color.White, 3);
            pen.Alignment = PenAlignment.Inset;

            Color myColor2 = Color.FromArgb(125, 0, 0, 0);
            Pen pen2 = new Pen(myColor2, 1);

            gr.DrawRectangle(Pens.Black, selectionRectangle);
            gr.DrawRectangle(pen, new Rectangle(selectionRectangle.X + 1, selectionRectangle.Y + 1, selectionRectangle.Width - 1, selectionRectangle.Height - 1));
            gr.DrawRectangle(Pens.Black, new Rectangle(selectionRectangle.X + 4, selectionRectangle.Y + 4, selectionRectangle.Width - 8, selectionRectangle.Height - 8));

            int cx = imageHolder.Width / brushSizeX;
            int cy = imageHolder.Height / brushSizeY;

            // draw horizontal lines
            for (int i = 0; i <= cx; i++)
            {
                gr.DrawLine(pen2, i * brushSizeX, 0, i * brushSizeX, imageHolder.Height);
            }

            // draw vertical lines
            for (int i = 0; i <= cy; i++)
            {
                gr.DrawLine(pen2, 0, i * brushSizeY, imageHolder.Width, i * brushSizeY);
            }
          
            imageHolder.Refresh();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void UpdateSelection(MouseEventArgs e)
        {
            if (imageHolder.Image == null) return;

            if (e.X > this.Width + this.HorizontalScroll.Value)
            {
                if (this.HorizontalScroll.Value + brushSizeX <= this.HorizontalScroll.Maximum)
                {
                    this.HorizontalScroll.Value += brushSizeX;
                }
                else
                {
                    this.HorizontalScroll.Value = this.HorizontalScroll.Maximum;
                }
            }
            else if (e.X < this.HorizontalScroll.Value)
            {
                if (this.HorizontalScroll.Value - brushSizeX >= 0)
                {
                    this.HorizontalScroll.Value -= brushSizeX;
                }
                else
                {
                    this.HorizontalScroll.Value = 0;
                }
            }

            if (e.Y > this.Height + this.VerticalScroll.Value)
            {
                if (this.VerticalScroll.Value + brushSizeY <= this.VerticalScroll.Maximum)
                {
                    this.VerticalScroll.Value += brushSizeY;
                }
                else
                {
                    this.VerticalScroll.Value = this.VerticalScroll.Maximum;
                }
            }
            else if (e.Y < this.VerticalScroll.Value)
            {
                if (this.VerticalScroll.Value - brushSizeY >= 0)
                {
                    this.VerticalScroll.Value -= brushSizeY;
                }
                else
                {
                    this.VerticalScroll.Value = 0;
                }
            }


            if (this.imageHolder.BackgroundImage != null && e.X < this.imageHolder.BackgroundImage.Width - 1 
                && e.Y < this.imageHolder.BackgroundImage.Height - 1 && e.X > 0 && e.Y > 0)
            {
                int x, y, width, height;

                int ex = e.X / brushSizeX;
                int ey = e.Y / brushSizeY;

                x = selectionStartPoint.X;
                y = selectionStartPoint.Y;

                width = 1;
                height = 1;

                if (ex > selectionStartPoint.X) width = ex - selectionStartPoint.X + 1;
                else if (ex < selectionStartPoint.X)
                {
                    x = ex;
                    width = (selectionStartPoint.X - x) + 1;
                }
                if (ey > selectionStartPoint.Y) height = ey - selectionStartPoint.Y + 1;
                else if (ey < selectionStartPoint.Y)
                {
                    y = ey;
                    height = (selectionStartPoint.Y - y) + 1;

                }

                selectionRectangle = new Rectangle(x * brushSizeX, y * brushSizeY, width * brushSizeX, height * brushSizeY);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path">The path to the image</param>
        public void ChangeImageSource(string path)
        {
            this.imagePath = path;

            if (!File.Exists(SceneManager.GameProject.ProjectPath + "\\" + path))
            {
                activeImage = new Bitmap(1, 1);
                this.imageHolder.BackgroundImage = null;
                return;
            }

            this.activeImage = Image.FromFile(SceneManager.GameProject.ProjectPath + "\\" + path);

            this.imageHolder.BackgroundImage = activeImage;
            this.imageHolder.BackgroundImageLayout = ImageLayout.None;
            this.imageHolder.Image = new Bitmap(activeImage.Width +1, activeImage.Height +1);

            DrawSelection();

            this.Refresh();
        }

        /// <summary>
        /// Changes the brush size of the brushControl
        /// </summary>
        /// <param name="brushSizeX">Width</param>
        /// <param name="brushSizeY">Height</param>
        public void ChangeSelectionSize(int brushSizeX, int brushSizeY)
        {
            BrushSizeX = brushSizeX;
            BrushSizeY = brushSizeY;
        }

        #endregion

        #region events

        private void imageHolder_MouseDown(object sender, MouseEventArgs e)
        {
            if (brushSizeX == 0 || brushSizeY == 0) return;

            if (!this.selectionStarted)
            {
                selectionStartPoint.X = e.X / brushSizeX;
                selectionStartPoint.Y = e.Y / brushSizeY;
                this.selectionStarted = true;

                UpdateSelection(e);
                DrawSelection();
            }    
        }

        private void imageHolder_MouseUp(object sender, MouseEventArgs e)
        {
            selectionStarted = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (EditorHandler.SelectedGameObjects != null &&
                EditorHandler.SelectedGameObjects.Count > 0 && EditorHandler.SelectedGameObjects[0] is Tileset)
            {
                BrushSizeX = (EditorHandler.SelectedGameObjects[0] as Tileset).TileWidth;
                BrushSizeY = (EditorHandler.SelectedGameObjects[0] as Tileset).TileHeight;

                if ((EditorHandler.SelectedGameObjects[0] as Tileset).ImagePath != imagePath)
                {
                    ChangeImageSource((EditorHandler.SelectedGameObjects[0] as Tileset).ImagePath);
                }
            }
        }

        private void BrushControl_Scroll(object sender, ScrollEventArgs e)
        {
            DrawSelection();
        }


        private void imageHolder_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.selectionStarted)
            {
                UpdateSelection(e);
                DrawSelection();
            }
        }

        #endregion    


      
    }
}
