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
