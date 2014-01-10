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
using System.Windows;
using System.Windows.Controls;

namespace Gibbo.Editor.WPF
{
    /// <summary>
    /// Interaction logic for AddNewItemWindow.xaml
    /// </summary>
    public partial class AddNewItemWindow : Window
    {
        UIElement target = null;
        GameObject targetObject = null;

        public AddNewItemWindow(UIElement target)
        {
            InitializeComponent();

            itemListBox.SelectedIndex = 1;

            this.target = target;
            if (target != null && (target as DragDropTreeViewItem).Tag is GameObject)
                targetObject = (target as DragDropTreeViewItem).Tag as GameObject;

            this.KeyUp += AddNewItemWindow_KeyUp;
        }

        void AddNewItemWindow_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                TryAdd();
            } 
        }

        private void UpdateText()
        {
            switch (itemListBox.SelectedIndex)
            {
                case 0:
                    itemInfoTextBlock.Text = string.Format("Empty: \n\nThis object has no particular properties. It's mostly used as a container.");
                    break;
                case 1:
                    itemInfoTextBlock.Text = string.Format("Sprite: \n\nWith this game object, you can load and draw images on the scene.");
                    break;
                case 2:
                    itemInfoTextBlock.Text = string.Format("Animated Sprite: \n\nWith this game object, you can load and draw images on the scene using animation properties.");
                    break;
                case 3:
                    itemInfoTextBlock.Text = string.Format("Tileset: \n\nWith a tileset object you can draw your map very easily using a tileset image.");
                    break;
                case 4:
                    itemInfoTextBlock.Text = string.Format("Audio: \n\nWith audio objects you can reproduce sound in your game.");
                    break;
                case 5:
                    itemInfoTextBlock.Text = string.Format("Bitmap Font: \n\nUsing a bitmap font, you can use bitmaps to draw text in your game.");
                    break;
                case 6:
                    itemInfoTextBlock.Text = string.Format("Particle Emiter: \n\nWith a particle emitter you can create great visuals effects for your game by using particles.");
                    break;
            }
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void itemListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateText();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            TryAdd();
        }

        private void nameTextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            
        }

        private void TryAdd()
        {
            try
            {
                string name = (nameTextBox.Text.Trim().Equals(string.Empty) ? "New Game Object" : nameTextBox.Text);

                GameObject obj = new GameObject();
                switch (itemListBox.SelectedIndex)
                {
                    case 1:
                        obj = new Sprite();
                        break;
                    case 2:
                        obj = new AnimatedSprite();
                        break;
                    case 3:
                        obj = new Tileset();
                        break;
                    case 4:
                        obj = new AudioObject();
                        break;
                    case 5:
                        obj = new BMFont();
                        break;
                    case 6:
                        obj = new ParticleEmitter();
                        break;
                }

                obj.Name = name;
                DragDropTreeViewItem insertedItem;
                if (addToSelectedCheckBox.IsChecked == true)
                {
                    if (targetObject == null)
                        SceneManager.ActiveScene.GameObjects.Add(obj);
                    else
                        targetObject.Children.Add(obj);

                    insertedItem = EditorHandler.SceneTreeView.AddNode(target as DragDropTreeViewItem, obj, EditorHandler.SceneTreeView.GameObjectImageSource(obj));
                }
                else
                {
                    SceneManager.ActiveScene.GameObjects.Add(obj);
                    insertedItem = EditorHandler.SceneTreeView.AddNode(null, obj, EditorHandler.SceneTreeView.GameObjectImageSource(obj));

                }

                EditorHandler.SelectedGameObjects.Clear();
                EditorHandler.SelectedGameObjects.Add(obj);
                EditorHandler.ChangeSelectedObjects();

                this.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
