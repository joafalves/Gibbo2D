#region Copyrights
/*
Gibbo2D License - Version 1.0
Copyright (c) 2013 - Gibbo2D Team
Founders Joao Alves <joao.cpp.sca@gmail.com> & Luis Fernandes <luisapidcloud@hotmail.com>

Permission is granted to use this software and associated documentation files (the "Software") free of charge, 
to any person or company. The code can be used, modified and merged without restrictions, but you cannot sell 
the software itself and parts where this license applies. Still, permission is granted for anyone to sell 
applications made using this software (for example, a game). This software cannot be claimed as your own, 
except for copyright holders. This license notes should also be available on any of the changed or added files.

The software is provided "as is", without warranty of any kind, express or implied, including but not limited to 
the warranties of merchantability, fitness for a particular purpose and non-infrigement. In no event shall the 
authors or copyright holders be liable for any claim, damages or other liability.

The license applies to all versions of the software, both newer and older than the one listed, unless a newer copy 
of the license is available, in which case the most recent copy of the license supercedes all others.

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
