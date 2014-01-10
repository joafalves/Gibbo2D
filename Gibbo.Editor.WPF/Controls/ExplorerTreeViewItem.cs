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
