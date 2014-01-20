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
using System.Net.Cache;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Gibbo.Editor.WPF
{
    /// <summary>
    /// Interaction logic for PicturePreview.xaml
    /// </summary>
    public partial class PicturePreview : Window
    {
        private const int maxWidth = 300;
        private const int maxHeight = 300;

        private string lastPath = string.Empty;

        public PicturePreview()
        {
            InitializeComponent();
            MouseLeave += PicturePreview_MouseLeave;
        }

        void PicturePreview_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        internal void ChangeImage(string path)
        {
                //Dispatcher.Invoke((Action)(() =>
                //{ 
                    //PreviewImage.Source = new BitmapImage(new Uri(path));
                    if (lastPath == path) return;
                    lastPath = path;

                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.UriSource = new Uri(path, UriKind.Absolute);
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.EndInit();

                    if (image.Width < maxWidth && image.Height < maxHeight)
                    {
                        PreviewImage.Width = image.Width;
                        PreviewImage.Height = image.Height;             
                    }
                    else
                    {
                        double ratioX = (double)maxWidth / (double)image.Width;
                        double ratioY = (double)maxHeight / (double)image.Height;
                        // use whichever multiplier is smaller
                        double ratio = ratioX < ratioY ? ratioX : ratioY;

                        // now we can get the new height and width            
                        int newWidth = Convert.ToInt32(image.Width * ratio);
                        int newHeight = Convert.ToInt32(image.Height * ratio);

                        PreviewImage.Width = newWidth;
                        PreviewImage.Height = newHeight;
                    }

                    PreviewImage.Source = image;//EditorUtils.ConvertBitmapToSource96DPI(image);
            //}));
          
        }
    }
}
