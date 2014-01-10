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
using System.Diagnostics;
using System.Linq;
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
    /// Interaction logic for TestsWindow.xaml
    /// </summary>
    public partial class TestsWindow : Window
    {
        public TestsWindow()
        {
            InitializeComponent();

           

            //for (int i = 0; i < 10; i++)
            //{
            //    StackPanel sp = new StackPanel();
            //    sp.Orientation = Orientation.Horizontal;
            //    sp.Children.Add(new Image()
            //    {
            //        Source = (ImageSource)FindResource("GameObjectIcon")
            //    });
            //    sp.Children.Add(new TextBlock()
            //    {
            //        Text = "I'm a banana " + i,
            //        Margin = new Thickness(4, 0, 0, 0)
            //    });

            //    TreeViewItem item = new TreeViewItem();
            //    item.Header = sp;

            //    item.Items.Add(new TreeViewItem()
            //    {
            //        Header = "Child"
            //    });

            //    treeView.Items.Add(item);
            //}
            //k1 = Audio.LoadSoundToBuffer(@"C:\Users\João\Dropbox\Gibbo Projects\SpaceRidge Glast\Content\Audio\Explosion.mp3", 1);
            //k2 = Audio.LoadSoundToBuffer(@"C:\Users\João\Dropbox\Gibbo Projects\SpaceRidge Glast\Content\Audio\laser.wav", 1);
        }

        int k1, k2;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new StartupForm().ShowDialog();

            //Audio.PlayFromBuffer(k1);
            //Audio.PlayFromBuffer(k2);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            Stopwatch stopwatch = new Stopwatch();

            // Begin timing
            stopwatch.Start();

            PropertyGrid.SelectedObject = new Gibbo.Library.AnimatedSprite();

            stopwatch.Stop();

            Console.WriteLine(stopwatch.Elapsed);
        }
    }
}
