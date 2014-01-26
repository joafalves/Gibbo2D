using Gibbo.Library;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for ManageTagsWindow.xaml
    /// </summary>
    public partial class ManageTagsWindow : Window
    {
        public ManageTagsWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.refreshList();
        }

        private void addBtn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string text = this.addBtn.Text.Trim();
                if (text.Equals(string.Empty)) return;

                if (!SceneManager.ActiveScene.CommonTags.Contains(text))
                    SceneManager.ActiveScene.CommonTags.Add(text);

                (sender as Xceed.Wpf.Toolkit.WatermarkTextBox).Text = "";

                EditorUtils.SelectAnotherElement<Xceed.Wpf.Toolkit.WatermarkTextBox>(sender as Xceed.Wpf.Toolkit.WatermarkTextBox);

                this.refreshList();
            }
        }

        private void removeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.TagsListBox.SelectedItem == null) return;
            SceneManager.ActiveScene.CommonTags.Remove(this.TagsListBox.SelectedItem.ToString());
            this.refreshList();
        }

        private void refreshList()
        {
            this.TagsListBox.Items.Clear();

            foreach (var tag in SceneManager.ActiveScene.CommonTags)
            {
                this.TagsListBox.Items.Add(tag);
            }
        }

    }
}
