using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Gibbo.Editor.WPF
{
    partial class DarkThemeResourseDictionary : ResourceDictionary
    {

        //private Visibility _actionsVisible;
        //public Visibility ActionsVisible
        //{
        //    get
        //    {
        //        Window win = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
        //        if (win != null)
        //        {
        //            if (win.ResizeMode == ResizeMode.CanResize)
        //                _actionsVisible = Visibility.Visible;
        //            else
        //                _actionsVisible = Visibility.Collapsed;

        //        }
        //        return _actionsVisible;
        //    }
        //    set
        //    {
        //        _actionsVisible = value;
        //    }
        //}

        public DarkThemeResourseDictionary()
        {
            InitializeComponent();
        }

        void grid_mouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Window)
                (sender as Window).DragMove();
        }

        void titleBarMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (e.ClickCount == 2)
                {
                    //Window win = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);

                    //if (win == null || !win.IsVisible || win.ResizeMode != ResizeMode.CanResize) return;

                    //if (win.WindowState == WindowState.Maximized)
                    //{
                    //    win.WindowState = WindowState.Normal;
                    //    if (win is MainWindow)
                    //        (win as MainWindow).setFullScreenName(false);
                    //}
                    //else
                    //{
                    //    if (win is MainWindow)
                    //        (win as MainWindow).SetFullScreen(false);
                    //    else
                    //        win.WindowState = WindowState.Maximized;
                    //}
                }
            } 
            
        }

        void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Window win = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);

            if (win == null || !win.IsVisible) return;

            win.Close();
        }

        void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            Window win = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);

            if (win == null || !win.IsVisible || (win.ResizeMode != ResizeMode.CanResize && win.ResizeMode != ResizeMode.CanResizeWithGrip) ||win.WindowState == WindowState.Maximized) return;

            if (win is MainWindow) // como tem fullscreen
                (win as MainWindow).SetFullScreen(false);
            else
                win.WindowState = WindowState.Maximized;

        }

        void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            Window win = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);

            win.WindowState = WindowState.Minimized;
            
        }
    }
}
