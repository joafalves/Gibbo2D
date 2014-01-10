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
