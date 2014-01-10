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
using System.Windows;
using System.Windows.Forms;  

namespace Gibbo.Editor.WPF
{
    /// <summary>  
   /// Displays a WindowsFormsHost control over a given placement target element in a WPF Window.  
   /// The owner window can be transparent, but not this one, due mixing DirectX and GDI drawing.  
   /// </summary>  
    public partial class WindowsFormsHostOverlay : Window
    {

        FrameworkElement t;

        System.Windows.Forms.Timer timer;

        public WindowsFormsHostOverlay(FrameworkElement target, Control child)
        {
            InitializeComponent();

            t = target;
            wfh.Child = child;

            Owner = Window.GetWindow(t);

            Owner.LocationChanged += new EventHandler(PositionAndResize);
            t.LayoutUpdated += new EventHandler(PositionAndResize);
            PositionAndResize(null, null);
            //this.SizeToContent = System.Windows.SizeToContent.WidthAndHeight;
            if (Owner.IsVisible)
                Show();
            else
                Owner.IsVisibleChanged += delegate
                {
                    if (Owner.IsVisible)
                        Show();
                };

            this.ContentRendered += WindowsFormsHostOverlay_ContentRendered;
        }

        void WindowsFormsHostOverlay_ContentRendered(object sender, EventArgs e)
        {
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1;
            timer.Tick += timer_Tick;
            timer.Enabled = true;


        }

        void timer_Tick(object sender, EventArgs e)
        {
            wfh.Child.Invalidate();
            //System.Windows.Controls.Panel.SetZIndex(wfh as UIElement, -1);
            
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Owner.LocationChanged -= new EventHandler(PositionAndResize);
            t.LayoutUpdated -= new EventHandler(PositionAndResize);
        }

        public void PositionAndResize(object sender, EventArgs e)
        {
            if (((this.Parent as System.Windows.Controls.Grid).Parent as System.Windows.Controls.TabItem).IsSelected)
            {
                Point p = t.PointToScreen(new Point());
                Left = p.X;
                Top = p.Y;

                wfh.Width = t.ActualWidth;
                wfh.Height = t.ActualHeight;
            }
        }
    }
}
