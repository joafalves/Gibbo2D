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
