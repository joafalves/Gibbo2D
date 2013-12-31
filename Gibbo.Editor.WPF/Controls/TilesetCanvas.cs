using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace Gibbo.Editor.WPF
{
    class TilesetCanvas : Canvas
    {
        private int brushSizeX = 32;    
        private int brushSizeY = 32;
        private Rect selection = Rect.Empty;

        private Pen penGrid;
        private Pen penWhite;
        private Pen penBlack;

        #region properties

        public int BrushSizeX
        {
            get { return brushSizeX; }
            set { brushSizeX = value; InvalidateVisual(); }
        }

        public int BrushSizeY
        {
            get { return brushSizeY; }
            set { brushSizeY = value; InvalidateVisual(); }
        }

        public Rect Selection
        {
            get { return selection; }
            set { selection = value; InvalidateVisual(); }
        }

        #endregion

        public TilesetCanvas()
        {
            penGrid = new Pen(Brushes.Black, 1);
            penWhite = new Pen(Brushes.White, 2);
            penBlack = new Pen(Brushes.Black, 4);
        }

        protected override void OnRender(System.Windows.Media.DrawingContext dc)
        {
            base.OnRender(dc);

            try
            {
                int cx = (int)Width / brushSizeX;
                int cy = (int)Height / brushSizeY;

                // draw vertical lines:
                for (int i = 0; i <= cx; i++)
                {
                    dc.DrawLine(penGrid, new Point(i * brushSizeX, 0), new Point(i * brushSizeX, Height));
                }

                // draw horizontal lines:
                for (int i = 0; i <= cy; i++)
                {
                    dc.DrawLine(penGrid, new Point(0, i * brushSizeY), new Point(Width, i * brushSizeY));
                }

                // draw selection:
                if (selection.Width > 0 && selection.Height > 0)
                {
                    dc.DrawRectangle(Brushes.Transparent, penBlack, selection);
                    dc.DrawRectangle(Brushes.Transparent, penWhite, selection);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
