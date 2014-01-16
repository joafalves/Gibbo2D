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

*/
#endregion

using Gibbo.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Gibbo.Editor.WPF
{
    class ExplorerTreeViewItem : DragDropTreeViewItem
    {
        #region Properties

        private Point lastpos = new Point(0, 0);

        public string Text { get; set; }
        public string FullPath
        {
            get
            {
                if (!(Parent is ExplorerTreeViewItem))
                {
                    return Text;
                }
                else
                {
                    return (Parent as ExplorerTreeViewItem).FullPath + "\\" + Text;
                }
            }
        }
        public int PriorityIndex { get; set; }

        #endregion

        #region Constructors

        public ExplorerTreeViewItem()
        {
            Text = string.Empty;
            PriorityIndex = -1;

            //(Header as StackPanel).MouseEnter += ExplorerTreeViewItem_MouseEnter;
            //(Header as StackPanel).MouseLeave += ExplorerTreeViewItem_MouseLeave;
        }


        #endregion

        #region events

        protected override void OnMouseEnter(System.Windows.Input.MouseEventArgs e)
        {
            Point p = PointToScreen(e.GetPosition(this));
            lastpos = p;

            if (Text.ToLower().EndsWith(".png") || Text.ToLower().EndsWith(".jpg") ||
                Text.ToLower().EndsWith(".jpeg") || Text.ToLower().EndsWith(".gif") ||
                Text.ToLower().EndsWith(".bmp"))
            {
                EditorHandler.PicturePreview.ChangeImage(FullPath);
                EditorHandler.PicturePreview.Visibility = System.Windows.Visibility.Visible;


                p.Y = Microsoft.Xna.Framework.MathHelper.Clamp(
                    (float)p.Y,
                    0,
                    (float)System.Windows.SystemParameters.WorkArea.Height - (float)EditorHandler.PicturePreview.PreviewImage.Height);

                EditorHandler.PicturePreview.Top = p.Y - 20;
                EditorHandler.PicturePreview.Left = p.X + 30;
            }
        }

        protected override void OnMouseLeave(System.Windows.Input.MouseEventArgs e)
        {
            try
            {
                base.OnMouseLeave(e);
                Point p = PointToScreen(e.GetPosition(this));

                if (p != lastpos)
                    EditorHandler.PicturePreview.Visibility = System.Windows.Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion
    }
}
