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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using Microsoft.Win32;
using System.Windows.Input;

namespace Gibbo.Editor.WPF
{
    internal static class EditorUtils
    {
        public static BitmapSource ConvertBitmapToSource96DPI(BitmapImage bitmapImage)
        {
            double dpi = 96;
            int width = bitmapImage.PixelWidth;
            int height = bitmapImage.PixelHeight;

            int stride = width * 4; // 4 bytes per pixel
            byte[] pixelData = new byte[stride * height];
            bitmapImage.CopyPixels(pixelData, stride, 0);

            return BitmapSource.Create(width, height, dpi, dpi, PixelFormats.Bgra32, null, pixelData, stride);
        }

        public static BitmapImage ConvertBitmapToImage96DPI(BitmapImage bitmapImage)
        {
            double dpi = 96;
            int width = bitmapImage.PixelWidth;
            int height = bitmapImage.PixelHeight;

            int stride = width * 4; // 4 bytes per pixel
            byte[] pixelData = new byte[stride * height];
            bitmapImage.CopyPixels(pixelData, stride, 0);

            BitmapSource bs = BitmapSource.Create(width, height, dpi, dpi, PixelFormats.Bgra32, null, pixelData, stride);

            PngBitmapEncoder encoder = new PngBitmapEncoder();
            MemoryStream memoryStream = new MemoryStream();
            BitmapImage bImg = new BitmapImage();

            encoder.Frames.Add(BitmapFrame.Create(bs));
            encoder.Save(memoryStream);

            bImg.BeginInit();
            bImg.StreamSource = new MemoryStream(memoryStream.ToArray());
            bImg.EndInit();

            memoryStream.Close();

            return bImg;
        }

        internal static GibboMenuItem CreateMenuItem(string text, ImageSource imageSource = null)
        {
            GibboMenuItem menuItem = new GibboMenuItem();

            menuItem.Header = text;

            if (imageSource != null)
                menuItem.Icon = new Image() { Source = imageSource, HorizontalAlignment = HorizontalAlignment.Center };

            return menuItem;
        }

        internal static StackPanel CreateHeader(string text, ImageSource imageSource)
        {
            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;
            if (imageSource != null)
                stackPanel.Children.Add(new Image() { Source = imageSource, Margin = new Thickness(0, 0, 4, 0) });
            stackPanel.Children.Add(new TextBlock() { Text = text });

            return stackPanel;
        }

        internal static DependencyObject GetParent(DependencyObject obj, int levels = 1)
        {
            DependencyObject result = obj, t;

            for (int i = 0; i < levels; i++)
            {
                if ((t = VisualTreeHelper.GetParent(result)) == null)
                    break;

                result = t;
            }

            return result;
        }

        internal static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }


        private static Dictionary<string, string> installedApps = new Dictionary<string, string>();
        internal static Dictionary<string, string> InstalledApps
        {
            get { return installedApps; }

            private set { installedApps = value; }
        }

        internal static void StoreInstalledApplications()
        {
            string keyName;

            // store: CurrentUser
            keyName = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            StoreSubKey(Registry.CurrentUser, keyName);

            // store: LocalMachine_32
            keyName = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            StoreSubKey(Registry.LocalMachine, keyName);

            // store: LocalMachine_64
            keyName = @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall";
            StoreSubKey(Registry.LocalMachine, keyName);
        }

        private static void StoreSubKey(RegistryKey root, string subKeyName)
        {
            RegistryKey subkey;
            string displayName;
            string pathName;

            using (RegistryKey key = root.OpenSubKey(subKeyName))
            {
                if (key != null)
                {
                    foreach (string kn in key.GetSubKeyNames())
                    {
                        using (subkey = key.OpenSubKey(kn))
                        {
                            displayName = (subkey.GetValue("DisplayName") as string);
                            pathName = (subkey.GetValue("InstallLocation") as string);

                            if (displayName != null && displayName != string.Empty && !installedApps.ContainsKey(displayName))
                                installedApps.Add(displayName, pathName);

                            //if (displayName != null && displayName.Replace(" ", "").ToLower().Contains(p_name.ToLower()))
                                //return true;
                        }
                    }
                }
            }
        }
        
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="version">VisualStudio2012, VisualStudio2010, CSExpress2010</param>
        /// <returns></returns>
        internal static bool CheckVisualStudioExistance(string version)
        {
            string src = string.Empty;
            switch (version)
            {
                case "VisualStudio2013":
                    src = @"VisualStudio\12.0";
                    break;
                case "VisualStudio2012":
                    src = @"VisualStudio\11.0";
                    break;
                case "VisualStudio2010":
                    src = @"VisualStudio\10.0";
                    break;
                //case "CSExpress2010":
                //    src = @"VCSExpress\10.0";
                //    break;
            }

            using (Microsoft.Win32.RegistryKey Key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\" + src))
                if (Key != null)
                {
                    return true;
                }

            return false;
        }

        internal static void RenderPicture(ref Image picture, string path, int maxWidth, int maxHeight)
        {
            if (!File.Exists(path))
            {
                EditorCommands.ShowOutputMessage("Error loading Images");
                return;
            }

            BitmapImage image = new BitmapImage();
            image.BeginInit(); // AppDomain.CurrentDomain.BaseDirectory + @"\Tutorials\FirstSteps\Pictures\"
            image.UriSource = new Uri(path, UriKind.Absolute);
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.EndInit();

            if (image.Width < maxWidth && image.Height < maxHeight)
            {
                picture.Width = image.Width;
                picture.Height = image.Height;
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

                picture.Width = newWidth;
                picture.Height = newHeight;
            }

            picture.Source = image;

        }


        internal static void SelectAnotherElement<T>(DependencyObject obj) where T : FrameworkElement
        {
            FrameworkElement parent = (FrameworkElement)(obj as T).Parent;
            while (parent != null && parent is IInputElement && !((IInputElement)parent).Focusable)
            {
                parent = (FrameworkElement)parent.Parent;
            }

            DependencyObject scope = FocusManager.GetFocusScope((obj as T));
            FocusManager.SetFocusedElement(scope, parent as IInputElement);
        }

        internal static bool isDirectory(string fullPath)
        {
            //FileAttributes attr = File.GetAttributes(fullPath);

            ////detect whether its a directory or file
            //if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            //    return true;

            //return false;

            return false;
        }
    }
}
