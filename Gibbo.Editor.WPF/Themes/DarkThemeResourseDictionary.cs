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

        void PathMouseDown(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
            ofd.Title = "Set Image Path";
            ofd.Filter = "All Graphics Types|*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff" + "|BMP|*.bmp|GIF|*.gif|JPG|*.jpg;*.jpeg|PNG|*.png|TIFF|*.tif;*.tiff";

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //(EditorUtils.FindVisualChildren<ScrollViewer>(parent).ElementAt(0) as ScrollViewer).Visibility = Visibility.Collapsed;
                DependencyObject parent = EditorUtils.GetParent(sender as TextBlock, 3);
                string destFolder = Gibbo.Library.SceneManager.GameProject.ProjectPath + "\\Content\\";
                string filename = System.IO.Path.GetFileName(ofd.FileName);

                if (!System.IO.File.Exists(destFolder + filename))
                    this.setNewImagePath(ofd.FileName, destFolder, filename, parent);
                else
                {
                    MessageBoxResult overwriteResult = MessageBox.Show("A file with the name " + filename + " already exists. Would you like to overwrite it?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
                    if (overwriteResult == MessageBoxResult.Yes )
                        this.setNewImagePath(ofd.FileName, destFolder, filename, parent, true);
                }
            }
        }

        void setNewImagePath(string srcPath, string destFolder, string filename, DependencyObject parentDO, bool overwrite = false)
        {
            System.IO.File.Copy(srcPath, destFolder + filename, overwrite);
            string relativePath = @"Content\" + filename;
            (parentDO as TextBox).Text = relativePath;

            FrameworkElement parent = (FrameworkElement)(parentDO as TextBox).Parent;
            while (parent != null && parent is IInputElement && !((IInputElement)parent).Focusable)
            {
                parent = (FrameworkElement)parent.Parent;
            }

            DependencyObject scope = FocusManager.GetFocusScope((parentDO as TextBox));
            FocusManager.SetFocusedElement(scope, parent as IInputElement);
        }
    }
}
