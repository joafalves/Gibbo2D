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
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Gibbo.Editor.WPF
{
    partial class DarkThemeResourceDictionary : ResourceDictionary
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

        public DarkThemeResourceDictionary()
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

        void LayoutTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Text = Properties.Settings.Default.Layout;
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

            if (win == null || !win.IsVisible || (win.ResizeMode != ResizeMode.CanResize && win.ResizeMode != ResizeMode.CanResizeWithGrip) || win.WindowState == WindowState.Maximized) return;

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

        void Layouts_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if ((sender as ComboBox).SelectedItem == null) return;

            string name = ((sender as ComboBox).SelectedItem as TextBlock).Text;
            LayoutHelper.LoadLayout(name);

            DependencyObject parent = EditorUtils.GetParent(sender as ComboBox, 3);
            (parent as TextBox).Text = name;
        }

        void LayoutTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string layoutName = (sender as TextBox).Text.Trim();
                if (layoutName.Equals(string.Empty)) return;

                if (LayoutHelper.RenameLayout(layoutName))
                {
                    (sender as TextBox).Text = "";
                    EditorUtils.SelectAnotherElement<TextBox>(sender as DependencyObject);
                }
                //if (LayoutHelper.LoadLayout(layoutName))
                //{
                //    (sender as TextBox).Text = "";
                //    EditorUtils.SelectAnotherElement<TextBox>(sender as DependencyObject);
                //}
                //else
                //{
                //    if (LayoutHelper.CreateNewLayout(layoutName))
                //    {
                //        (sender as TextBox).Text = "";
                //        EditorUtils.SelectAnotherElement<TextBox>(sender as DependencyObject);
                //    }
                //}
            }
        }

        void LayoutsPreviewMouseDown(object sender, MouseEventArgs e)
        {
            (sender as ComboBox).Items.Clear();

            foreach (var layout in LayoutHelper.GetLayouts())
            {
                (sender as ComboBox).Items.Add(new TextBlock()
                {
                    Margin = new Thickness(4, 0, 0, 0),
                    Text = layout
                });
            }
        }

        void removeLayoutMouseDown(object sender, RoutedEventArgs e)
        {
            DependencyObject parent = EditorUtils.GetParent(sender as TextBlock, 3);

            string layoutName = (parent as TextBox).Text.Trim();

            if (layoutName.Equals(string.Empty)) return;

            if (LayoutHelper.RemoveLayout(layoutName))
            {
                (parent as TextBox).Text = "";
                EditorUtils.SelectAnotherElement<TextBox>(parent);
            }
        }

        void addLayoutMouseDown(object sender, RoutedEventArgs e)
        {
            DependencyObject parent = EditorUtils.GetParent(sender as TextBlock, 3);

            string layoutName = (parent as TextBox).Text.Trim();

            if (layoutName.Equals(string.Empty)) return;

            if (LayoutHelper.CreateNewLayout(layoutName))
            {
                (parent as TextBox).Text = "";
                EditorUtils.SelectAnotherElement<TextBox>(parent);
            }
        }

        void TagPreviewMouseDown(object sender, MouseEventArgs e)
        {
            (sender as ComboBox).Items.Clear();

            foreach (var item in Gibbo.Library.SceneManager.ActiveScene.CommonTags)
            {
                (sender as ComboBox).Items.Add(new TextBlock()
                {
                    Margin = new Thickness(4, 0, 0, 0),
                    Text = item
                });
            }
        }

        void TagItemChanged(object sender, RoutedEventArgs e)
        {
            if ((sender as ComboBox).SelectedItem == null) return;

            DependencyObject parent = EditorUtils.GetParent(sender as ComboBox, 3);
            (parent as TextBox).Text = ((sender as ComboBox).SelectedItem as TextBlock).Text;

            EditorUtils.SelectAnotherElement<TextBox>(parent);
        }


        // TODO: add global extension file import. Any property starting with Path_

        void TexturePathMouseDown(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
            ofd.Title = "Set BMFont Texture Path";
            ofd.Filter = "PNG|*.png";
            this.ProcessDialog(sender, e, ofd, "Fonts\\");
        }

        void FntPathMouseDown(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
            ofd.Title = "Set BMFont File Path";
            ofd.Filter = "FNT|*.fnt";
            this.ProcessDialog(sender, e, ofd, "Fonts\\");
        }

        void AudioPathMouseDown(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
            ofd.Title = "Set Audio File Path";
            ofd.Filter = "All Supported Audio Types|*.mp3;*.wav|WAV|*.wav|MP3|*.mp3";
            this.ProcessDialog(sender, e, ofd, "Audio\\");
        }

        void PathMouseDown(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
            ofd.Title = "Set Image Path";
            ofd.Filter = "All Supported Graphics Types|*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff" + "|BMP|*.bmp|GIF|*.gif|JPG|*.jpg;*.jpeg|PNG|*.png|TIFF|*.tif;*.tiff";
            this.ProcessDialog(sender, e, ofd, "Images\\");
        }

        void ProcessDialog(object sender, RoutedEventArgs e, System.Windows.Forms.OpenFileDialog ofd, string specificFolder = "")
        {
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //(EditorUtils.FindVisualChildren<ScrollViewer>(parent).ElementAt(0) as ScrollViewer).Visibility = Visibility.Collapsed;
                DependencyObject parent = EditorUtils.GetParent(sender as TextBlock, 3);

                string destFolder = (Gibbo.Library.SceneManager.GameProject.ProjectPath + "\\Content\\" + specificFolder).Trim();
                string filename = System.IO.Path.GetFileName(ofd.FileName);

                bool fileOnDirectory = ofd.FileName.StartsWith(Gibbo.Library.SceneManager.GameProject.ProjectPath);

                if (!System.IO.Directory.Exists(destFolder) && !fileOnDirectory)
                    System.IO.Directory.CreateDirectory(destFolder);

                if (!System.IO.File.Exists(destFolder + filename) || fileOnDirectory)
                    this.SetNewPath(ofd.FileName, destFolder, specificFolder, filename, parent);
                else
                {
                    MessageBoxResult overwriteResult = MessageBox.Show("A file with the name " + filename + " already exists. Would you like to overwrite it?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
                    if (overwriteResult == MessageBoxResult.Yes)
                        this.SetNewPath(ofd.FileName, destFolder, specificFolder, filename, parent, true);
                }
            }
        }

        void SetNewPath(string srcPath, string destFolder, string specificFolder, string filename, DependencyObject parentDO, bool overwrite = false)
        {
            try
            {
                bool fileOnDirectory = srcPath.StartsWith(Gibbo.Library.SceneManager.GameProject.ProjectPath);

                if (!fileOnDirectory)
                    System.IO.File.Copy(srcPath, destFolder + filename, overwrite);

                string relativePath = (@"\Content\" + specificFolder + filename).Trim();
                if (fileOnDirectory)
                    relativePath = srcPath.Replace(Gibbo.Library.SceneManager.GameProject.ProjectPath, string.Empty);

                if (relativePath.StartsWith("\\"))
                    relativePath = relativePath.Substring(1, relativePath.Length - 1);

                (parentDO as TextBox).Text = relativePath;

                EditorUtils.SelectAnotherElement<TextBox>(parentDO);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
