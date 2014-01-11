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
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;

namespace Gibbo.Editor.WPF
{
    /// <summary>
    /// Interaction logic for SceneHierarchyTreeView.xaml
    /// </summary>
    public partial class SceneHierarchyTreeView : UserControl
    {
        #region fields

        DragDropTreeViewItem selectedForEditing;
        ContextMenu contextMenu, panelContextMenu;
        GibboMenuItem addComponentItem;

        const int MARGIN = 4;

        DragDropTreeViewItem lastSelectedItem;
        ImageSource visibleItemIcon;
        ImageSource hiddenItemIcon;

        internal bool canCopyPaste;

        MenuItem renameItem;
        MenuItem moveUpItem;
        MenuItem moveDownItem;
        MenuItem saveStateItem;

        #endregion

        #region properties

        internal DragDropTreeViewItem SelectedItem
        {
            get
            {
                return treeView.SelectedItem as DragDropTreeViewItem;
            }
        }

        #endregion

        #region constructors

        public SceneHierarchyTreeView()
        {
            InitializeComponent();
            Initialize();
            visibleItemIcon = (ImageSource)FindResource("VisibleItemIcon");
            hiddenItemIcon = (ImageSource)FindResource("HiddenItemIcon");

            treeView.OnDragDropSuccess += treeView_OnDragDropSuccess;
            treeView.MouseRightButtonDown += treeView_MouseRightButtonDown;
            treeView.MouseLeftButtonDown += treeView_MouseLeftButtonDown;
        }

        #endregion

        #region methods

        void Initialize()
        {
            contextMenu = new ContextMenu();
            panelContextMenu = new ContextMenu();
            contextMenu.Opened += contextMenu_Opened;
            // Context menu:
            addComponentItem = EditorUtils.CreateMenuItem("Add Component", (ImageSource)FindResource("ComponentIcon"));
            MenuItem createObjectItem = EditorUtils.CreateMenuItem("Create New Object...", (ImageSource)FindResource("GameObjectIcon_Sprite"));
            MenuItem panelCreateObjectItem = EditorUtils.CreateMenuItem("Create New Object...", (ImageSource)FindResource("GameObjectIcon_Sprite"));
            MenuItem addFromStateItem = EditorUtils.CreateMenuItem("Add From State...", null);
            MenuItem panelAddFromStateItem = EditorUtils.CreateMenuItem("Add From State...", null);
            saveStateItem = EditorUtils.CreateMenuItem("Save State...", (ImageSource)FindResource("SaveIcon"));
            MenuItem cutItem = EditorUtils.CreateMenuItem("Cut", (ImageSource)FindResource("CutIcon"));
            MenuItem copyItem = EditorUtils.CreateMenuItem("Copy", (ImageSource)FindResource("CopyIcon"));
            MenuItem pasteItem = EditorUtils.CreateMenuItem("Paste", (ImageSource)FindResource("PasteIcon"));
            MenuItem panelPasteItem = EditorUtils.CreateMenuItem("Paste", (ImageSource)FindResource("PasteIcon"));
            MenuItem deleteItem = EditorUtils.CreateMenuItem("Delete", null);
            renameItem = EditorUtils.CreateMenuItem("Rename", (ImageSource)FindResource("RenameIcon"));
            moveUpItem = EditorUtils.CreateMenuItem("Move Up", (ImageSource)FindResource("MoveUpIcon"));
            moveDownItem = EditorUtils.CreateMenuItem("Move Down", (ImageSource)FindResource("MoveDownIcon"));

            contextMenu.Items.Add(addComponentItem);
            contextMenu.Items.Add(createObjectItem);
            contextMenu.Items.Add(new Separator());
            contextMenu.Items.Add(addFromStateItem);
            contextMenu.Items.Add(saveStateItem);
            contextMenu.Items.Add(new Separator());
            contextMenu.Items.Add(cutItem);
            contextMenu.Items.Add(copyItem);
            contextMenu.Items.Add(pasteItem);
            contextMenu.Items.Add(deleteItem);
            contextMenu.Items.Add(new Separator());
            contextMenu.Items.Add(renameItem);
            contextMenu.Items.Add(new Separator());
            contextMenu.Items.Add(moveUpItem);
            contextMenu.Items.Add(moveDownItem);

            panelContextMenu.Items.Add(panelCreateObjectItem);
            panelContextMenu.Items.Add(new Separator());
            panelContextMenu.Items.Add(panelAddFromStateItem);
            panelContextMenu.Items.Add(new Separator());
            panelContextMenu.Items.Add(panelPasteItem);

            createObjectItem.Click += createObjectItem_Click;
            addFromStateItem.Click += addFromStateItem_Click;
            renameItem.Click += renameItem_Click;
            saveStateItem.Click += saveStateItem_Click;
            cutItem.Click += cutItem_Click;
            copyItem.Click += copyItem_Click;
            pasteItem.Click += pasteItem_Click;
            deleteItem.Click += deleteItem_Click;
            moveUpItem.Click += moveUpItem_Click;
            moveDownItem.Click += moveDownItem_Click;

            panelCreateObjectItem.Click += createObjectItem_Click;
            panelAddFromStateItem.Click += addFromStateItem_Click;
            panelPasteItem.Click += panelPasteItem_Click;

        }

        void panelPasteItem_Click(object sender, RoutedEventArgs e)
        {
            Paste();
        }

        internal void Paste()
        {
            List<GameObject> gameObjects = (List<GameObject>)Clipboard.GetData("GameObject");

            if (gameObjects != null)
            {
                foreach (GameObject obj in gameObjects)
                {
                    AddGameObject(obj, string.Empty);
                }
            }
        }

        private void SelectOnClick(MouseButtonEventArgs e)
        {
            DragDropTreeViewItem ClickedTreeViewItem = new DragDropTreeViewItem();

            //find the original object that raised the event
            UIElement ClickedItem = VisualTreeHelper.GetParent(e.OriginalSource as UIElement) as UIElement;

            //find the clicked TreeViewItem
            while ((ClickedItem != null) && !(ClickedItem is DragDropTreeViewItem))
            {
                ClickedItem = VisualTreeHelper.GetParent(ClickedItem) as UIElement;
            }

            ClickedTreeViewItem = ClickedItem as DragDropTreeViewItem;
            if (ClickedTreeViewItem != null)
            {
                ClickedTreeViewItem.IsSelected = true;
                ClickedTreeViewItem.Focus();
            }
        }

        internal void AddGameObject(GameObject gameObject, string type, bool autoselect = true)
        {
            if (gameObject == null)
            {
                switch (type.ToLower())
                {
                    case "empty":
                        gameObject = new GameObject();
                        gameObject.Name = "Empty Game Object";

                        break;

                    case "sprite":
                        gameObject = new Sprite();
                        gameObject.Name = "Sprite Game Object";

                        break;

                    case "animatedsprite":
                        gameObject = new AnimatedSprite();
                        gameObject.Name = "Animated Sprite Game Object";

                        break;

                    case "audio":
                        gameObject = new AudioObject();
                        gameObject.Name = "Audio Game Object";

                        break;

                    case "tileset":
                        gameObject = new Tileset();
                        gameObject.Name = "Tileset Game Object";

                        break;

                    case "bmfont":
                        gameObject = new BMFont();
                        gameObject.Name = "Bitmap Font Object";

                        break;

                    case "particleemitter":
                        gameObject = new ParticleEmitter();
                        gameObject.Name = "Particle Emitter Object";

                        break;
                }
            }
            else
            {
                gameObject.Initialize();
            }

            if (SelectedItem == null)
            {
                SceneManager.ActiveScene.GameObjects.Add(gameObject);
            }
            else
            {
                GameObject _gameObject = (GameObject)(SelectedItem as DragDropTreeViewItem).Tag;
                _gameObject.Children.Add(gameObject);
            }

            if (gameObject != null)
            {
                DragDropTreeViewItem node = AddNode(SelectedItem as DragDropTreeViewItem, gameObject, GameObjectImageSource(gameObject));
                node.ContextMenu = contextMenu;
                // AddNodes(gameObject);
                //node.IsSelected = true;

                AttachChildren(node);
                //foreach (GameObject _obj in gameObject.Children)
                //    AddGameObject(_obj, string.Empty);

                if (autoselect)
                {
                    EditorHandler.SelectedGameObjects = new List<GameObject>();
                    EditorHandler.SelectedGameObjects.Add(gameObject);
                }
            }
        }

        private void AttachChildren(DragDropTreeViewItem node)
        {
            foreach (GameObject _obj in (node.Tag as GameObject).Children)
            {
                DragDropTreeViewItem _node = AddNode(node, _obj, GameObjectImageSource(_obj));
                AttachChildren(_node);
            }
        }

        //private void AddNodes(GameObject obj)
        //{
        //    foreach (GameObject _obj in obj.Children)
        //    {
        //        AddGameObject(_obj.Transform.GameObject, null);
        //        //if (_obj.Children.Count > 0)
        //        //    AddNodes(_obj);
        //    }
        //}

        internal void AddFromState()
        {
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
            ofd.Title = "Open State";
            ofd.Filter = @"(*.state)|*.state";

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    GameObject gameObject = (GameObject)GibboHelper.DeserializeObject(ofd.FileName);
                    gameObject.Initialize();

                    AddGameObject(gameObject, string.Empty);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        internal void BeginEditTextOnSelected()
        {
            ItemLostFocus();

            selectedForEditing = (SelectedItem as DragDropTreeViewItem);

            if (selectedForEditing == null) return;

            TextBox tb = new TextBox();
            tb.LostFocus += tb_LostFocus;
            tb.KeyDown += tb_KeyDown;
            tb.Text = lastSelectedItem.Tag.ToString();
            tb.Focusable = true;

            (selectedForEditing.Header as StackPanel).Children.RemoveAt(2);
            (selectedForEditing.Header as StackPanel).Children.Add(tb);
            selectedForEditing.CanDrag = false;

            tb.Select(0, tb.Text.Length);
            tb.Focus();
        }

        internal DragDropTreeViewItem AddNode(DragDropTreeViewItem parent, object tag, ImageSource imageSource)
        {
            DragDropTreeViewItem node = new DragDropTreeViewItem();
            node.Style = (Style)FindResource("IgniteMultiTreeViewItem");

            Image visibilityToggleImage = new Image();
            visibilityToggleImage.Cursor = Cursors.Hand;
            visibilityToggleImage.Tag = tag;
            visibilityToggleImage.Width = 8;
            visibilityToggleImage.Height = 8;
            visibilityToggleImage.Margin = new Thickness(0, 0, MARGIN, 0);
            visibilityToggleImage.MouseUp += visibilityToggleImage_MouseUp;
            if (tag is GameObject)
                visibilityToggleImage.Source = ((tag as GameObject).Visible ? visibleItemIcon : hiddenItemIcon);

            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Horizontal;
            sp.Children.Add(visibilityToggleImage);
            sp.Children.Add(new Image() { Source = imageSource });
            sp.Children.Add(new TextBlock() { Text = tag.ToString(), Margin = new Thickness(MARGIN, 0, 0, 0) });

            node.Header = sp;
            node.MouseUp += node_MouseUp;
            node.Tag = tag;
            node.MouseDoubleClick += node_MouseDoubleClick;
            node.ContextMenu = contextMenu;

            //node.ContextMenu = objectContextMenuStrip;

            if (parent == null)
            {
                treeView.Items.Insert(0, node);
            }
            else
            {
                parent.Items.Insert(0, node);
            }

            return node;
        }

        public void SetSelectedItem(ref TreeView control, object item)
        {
            try
            {
                DependencyObject dObject = control
                    .ItemContainerGenerator
                    .ContainerFromItem(item);

                //uncomment the following line if UI updates are unnecessary
                //((TreeViewItem)dObject).IsSelected = true;               

                MethodInfo selectMethod =
                   typeof(TreeViewItem).GetMethod("Select",
                   BindingFlags.NonPublic | BindingFlags.Instance);

                selectMethod.Invoke(dObject, new object[] { true });
            }
            catch { }
        }

        void node_MouseUp(object sender, MouseButtonEventArgs e)
        {
            canCopyPaste = true;
        }

        /// <summary>
        /// 
        /// </summary>
        public void CreateView()
        {
            treeView.Items.Clear();

            if (SceneManager.ActiveScene == null)
            {
                DragDropTreeViewItem noScene = new DragDropTreeViewItem();

                noScene.Header = "No Scene in memory.";
                noScene.CanDrag = false;
                noScene.IsEnabled = false;
                noScene.Style = (Style)FindResource("IgniteMultiTreeViewItem");
                // noScene.can

                treeView.Items.Add(noScene);
                return;
            }

            this.ContextMenu = panelContextMenu;

            // Set all this layer game objects
            foreach (GameObject gameObject in SceneManager.ActiveScene.GameObjects)
            {
                string name = gameObject.Name == null ? "Game Object" : gameObject.Name;
                gameObject.Name = name;

                DragDropTreeViewItem _node = AddNode(null, gameObject, GameObjectImageSource(gameObject));
                //_node.ContextMenuStrip = objectContextMenuStrip;

                FillGameObjects(gameObject, _node);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_gameObject"></param>
        /// <param name="node"></param>
        private void FillGameObjects(GameObject _gameObject, DragDropTreeViewItem node)
        {
            foreach (GameObject gameObject in _gameObject.Children)
            {
                string name = gameObject.Name == null ? "Game Object" : gameObject.Name;
                gameObject.Name = name;

                DragDropTreeViewItem _node = AddNode(node, gameObject, GameObjectImageSource(gameObject));
                //_node.ContextMenuStrip = objectContextMenuStrip;

                FillGameObjects(gameObject, _node);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        internal ImageSource GameObjectImageSource(GameObject gameObject)
        {
            if ((gameObject is Sprite) || (gameObject is AnimatedSprite))
                return (ImageSource)FindResource("GameObjectIcon_Sprite");
            else if (gameObject is BMFont)
                return (ImageSource)FindResource("GameObjectIcon_Text");
            else if (gameObject is AudioObject)
                return (ImageSource)FindResource("GameObjectIcon_Audio");
            else if (gameObject is Tileset)
                return (ImageSource)FindResource("GameObjectIcon_Tileset");
            else if (gameObject is ParticleEmitter)
                return (ImageSource)FindResource("GameObjectIcon_Particle");

            // Empty game object
            return (ImageSource)FindResource("GameObjectIcon_Empty");
        }

        private void ItemLostFocus()
        {
            if (lastSelectedItem != null && lastSelectedItem.Header != null && (lastSelectedItem.Header as StackPanel).Children.Count >= 2
                && (lastSelectedItem.Header as StackPanel).Children[2] is TextBox)
            {
                // update the current name:
                if (lastSelectedItem.Tag is GameObject)
                    (lastSelectedItem.Tag as GameObject).Name = ((lastSelectedItem.Header as StackPanel).Children[2] as TextBox).Text;

                // remove the text box
                (lastSelectedItem.Header as StackPanel).Children.RemoveAt(2);
                (lastSelectedItem.Header as StackPanel).Children.Add(new TextBlock() { Text = lastSelectedItem.Tag.ToString(), Margin = new Thickness(MARGIN, 0, 0, 0) });
            }
        }

        #endregion

        #region events

        void renameItem_Click(object sender, RoutedEventArgs e)
        {
            BeginEditTextOnSelected();
        }

        void tb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ItemLostFocus();
                selectedForEditing.CanDrag = true;
            }
        }

        void addFromStateItem_Click(object sender, RoutedEventArgs e)
        {
            AddFromState();
        }

        void contextMenu_Opened(object sender, RoutedEventArgs e)
        {
            // check disabled :
            if (TreeViewExtension.GetSelectedTreeViewItems(treeView).Count > 1)
            {
                // more than one selected:
                renameItem.IsEnabled = false;
                moveUpItem.IsEnabled = false;
                moveDownItem.IsEnabled = false;
                saveStateItem.IsEnabled = false;
            }
            else
            {
                renameItem.IsEnabled = true;
                moveUpItem.IsEnabled = true;
                moveDownItem.IsEnabled = true;
                saveStateItem.IsEnabled = true;
            }

            addComponentItem.Items.Clear();
            GibboMenuItem item = null;

            if (SceneManager.ScriptsAssembly == null)
            {
                addComponentItem.Visibility = System.Windows.Visibility.Collapsed;
                return;
            }
            else
            {
                addComponentItem.Visibility = System.Windows.Visibility.Visible;

                //ObjectComponent dummy = new ObjectComponent();
                foreach (Type type in SceneManager.ScriptsAssembly.GetTypes())
                {
                    // is a game object component?
                    if (type.IsSubclassOf(typeof(ObjectComponent)))
                    {
                        string fullname = type.FullName;
                        GibboMenuItem lastItem = addComponentItem;

                        if (fullname != type.Name && fullname.Contains('.'))
                        {
                            string[] splitted = fullname.Split('.');
                            int scount = splitted.Count() - 1;

                            for (int i = 0; i < scount; i++)
                            {
                                item = null;
                                string camelCaseFix = GibboHelper.SplitCamelCase(splitted[i]);

                                for (int j = 0; j < lastItem.Items.Count; j++)
                                {
                                    if ((lastItem.Items[j] as GibboMenuItem).TagText.Equals(camelCaseFix))
                                    {
                                        item = lastItem.Items[j] as GibboMenuItem;
                                        break;
                                    }
                                }

                                if (item == null)
                                {
                                    item = EditorUtils.CreateMenuItem(camelCaseFix, null);
                                    item.TagText = camelCaseFix;
                                    lastItem.Items.Insert(0, item);
                                }

                                lastItem = item;
                            }
                        }

                        string camelCase = GibboHelper.SplitCamelCase(type.Name);
                        GibboMenuItem newItem = EditorUtils.CreateMenuItem(camelCase, (ImageSource)FindResource("ComponentItemIcon"));
                        newItem.Tag = type;
                        newItem.TagText = camelCase;
                        newItem.Click += newItem_Click;

                        SearchAndAttachInfo(newItem, type);

                        lastItem.Items.Add(newItem);
                    }
                }
            }

            // System Components :
            GibboMenuItem subItem;

            addComponentItem.Items.Add(new Separator());
            item = EditorUtils.CreateMenuItem("Physics");

            subItem = EditorUtils.CreateMenuItem("Physical Body", (ImageSource)FindResource("ComponentItemIcon"));
            subItem.Tag = typeof(PhysicalBody);
            subItem.TagText = typeof(PhysicalBody).Name;
            subItem.Click += newItem_Click;

            SearchAndAttachInfo(subItem, typeof(PhysicalBody));

            item.Items.Add(subItem);

            subItem = EditorUtils.CreateMenuItem("Rectangle Body", (ImageSource)FindResource("ComponentItemIcon"));
            subItem.Tag = typeof(RectangleBody);
            subItem.TagText = typeof(RectangleBody).Name;
            subItem.Click += newItem_Click;

            SearchAndAttachInfo(subItem, typeof(RectangleBody));

            item.Items.Add(subItem);

            subItem = EditorUtils.CreateMenuItem("Circle Body", (ImageSource)FindResource("ComponentItemIcon"));
            subItem.Tag = typeof(CircleBody);
            subItem.TagText = typeof(CircleBody).Name;
            subItem.Click += newItem_Click;

            SearchAndAttachInfo(subItem, typeof(CircleBody));

            item.Items.Add(subItem);

            subItem = EditorUtils.CreateMenuItem("Texture Body", (ImageSource)FindResource("ComponentItemIcon"));
            subItem.Tag = typeof(TextureBody);
            subItem.TagText = typeof(TextureBody).Name;
            subItem.Click += newItem_Click;

            SearchAndAttachInfo(subItem, typeof(TextureBody));

            item.Items.Add(subItem);

            addComponentItem.Items.Add(item);
        }

        private void SearchAndAttachInfo(GibboMenuItem item, Type type)
        {
            System.Reflection.MemberInfo info;
            object[] attributes;

            info = type;
            attributes = info.GetCustomAttributes(typeof(Info), true);

            if (attributes.Count() > 0)
                item.ToolTip = (attributes[0] as Info).Value.Replace("\\n", Environment.NewLine);
        }

        void newItem_Click(object sender, RoutedEventArgs e)
        {
            GibboMenuItem _sender = (GibboMenuItem)sender;

            foreach (var ti in TreeViewExtension.GetSelectedTreeViewItems(treeView))
            {
                ObjectComponent component = (ObjectComponent)Activator.CreateInstance((Type)_sender.Tag, new object[] { /* params here */ });
                GameObject gameObject = (GameObject)ti.Tag;
                if (!gameObject.AddComponent(component))
                {
                    MessageBox.Show("The component was not added to " + gameObject.Name + ", the requirements are not met", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    EditorCommands.CreatePropertyGridView();
                }
            }
        }

        void createObjectItem_Click(object sender, RoutedEventArgs e)
        {
            new AddNewItemWindow(SelectedItem as UIElement).ShowDialog();
        }

        void treeView_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SelectOnClick(e);
        }

        void treeView_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            SelectOnClick(e);
        }

        void treeView_OnDragDropSuccess(DragDropTreeViewItem source, DragDropTreeViewItem target, System.ComponentModel.CancelEventArgs e)
        {
            if (!(source.Tag is GameObject))
            {
                e.Cancel = true;
                return;
            }

            List<TreeViewItem> movedItems = TreeViewExtension.GetSelectedTreeViewItems(treeView);
            // verify if the moved items will not be placed on any of their children or in themselfs
            foreach (var ti in movedItems)
            {
                var tx = (DragDropTreeViewItem)ti;
                if (ti == target)
                {
                    // trying to drop on one of the moved items
                    e.Cancel = true;
                    return;
                }

                if (DragDropTreeView.TreeContainsNode(treeView, tx, target))
                {
                    e.Cancel = true;
                    return;
                }
            }

            if (target.Tag is GameObject)
            {
                foreach (var ti in movedItems)
                {
                    GameObject _source = ti.Tag as GameObject;

                    // no parent?
                    if (_source.Transform.Parent == null)
                    {
                        SceneManager.ActiveScene.GameObjects.Remove(_source, false);
                    }
                    else
                    {
                        GameObject parent = (ti.Parent as DragDropTreeViewItem).Tag as GameObject;
                        parent.Children.Remove(_source, false);
                    }

                    if (DragDropHelper.insertionPlace == DragDropHelper.InsertionPlace.Center)
                    {
                        (target.Tag as GameObject).Children.Insert(0, _source);
                    }
                    else
                    {
                        int index = 0;

                        // no parent?
                        if ((target.Tag as GameObject).Transform.Parent == null)
                        {
                            index = SceneManager.ActiveScene.GameObjects.IndexOf(target.Tag as GameObject);
                        }
                        else
                        {
                            GameObject parent = (target.Parent as DragDropTreeViewItem).Tag as GameObject;
                            index = parent.Children.IndexOf(target.Tag as GameObject);
                        }

                        if (DragDropHelper.insertionPlace == DragDropHelper.InsertionPlace.Top)
                            index++;

                        if ((target.Tag as GameObject).Transform.Parent == null)
                        {
                            SceneManager.ActiveScene.GameObjects.Insert(index, _source);
                        }
                        else
                        {
                            GameObject parent = (target.Parent as DragDropTreeViewItem).Tag as GameObject;
                            parent.Children.Insert(index, _source);
                        }
                    }

                }

            }
            else
            {
                e.Cancel = true;
            }
        }

        void visibilityToggleImage_MouseUp(object sender, MouseButtonEventArgs e)
        {
            object tag = (sender as Image).Tag;

            if (tag is GameObject)
            {
                (tag as GameObject).Visible = !(tag as GameObject).Visible;
            }

            if (!(tag as GameObject).Visible)
            {
                (sender as Image).Source = hiddenItemIcon;
            }
            else
            {
                (sender as Image).Source = visibleItemIcon;
            }
        }

        void node_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //if (lastSelectedItem != null && lastSelectedItem.Tag != null)
            //{
            //    TextBox tb = new TextBox();
            //    tb.Text = lastSelectedItem.Tag.ToString();
            //    tb.LostFocus += tb_LostFocus;

            //    (lastSelectedItem.Header as StackPanel).Children.RemoveAt(2);
            //    (lastSelectedItem.Header as StackPanel).Children.Add(tb);
            //    lastSelectedItem.CanDrag = false;
            //}
        }

        void tb_LostFocus(object sender, RoutedEventArgs e)
        {
            ItemLostFocus();
            lastSelectedItem.CanDrag = true;
        }

        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            ItemLostFocus();


        }


        void moveDownItem_Click(object sender, RoutedEventArgs e)
        {
            GameObject gameObject = (GameObject)(SelectedItem as DragDropTreeViewItem).Tag;

            // no parent?
            if (!((SelectedItem as DragDropTreeViewItem).Parent is DragDropTreeView))
            {
                DragDropTreeViewItem parentNode = (SelectedItem as DragDropTreeViewItem).Parent as DragDropTreeViewItem;

                int index = (parentNode.Tag as GameObject).Children.FindIndex(o => o.GetHashCode() == gameObject.GetHashCode());

                // already on bottom?
                if (index == 0)
                    return;

                GameObject parent = (GameObject)parentNode.Tag;

                // Shift objects 
                GameObject topObject = parent.Children[index - 1];
                parent.Children.Remove(topObject);
                parent.Children.Insert(index, topObject);

                //// Shift in treeview
                DragDropTreeViewItem topNode = (DragDropTreeViewItem)parentNode.Items[parentNode.Items.IndexOf(SelectedItem) + 1];
                parentNode.Items.Remove(topNode);
                parentNode.Items.Insert(parentNode.Items.IndexOf(SelectedItem), topNode);
            }
            else
            {
                int index = SceneManager.ActiveScene.GameObjects.FindIndex(o => o.GetHashCode() == gameObject.GetHashCode());

                // already on bottom?
                if (index == 0)
                    return;

                // Shift objects 
                GameObject topObject = SceneManager.ActiveScene.GameObjects[index - 1];
                SceneManager.ActiveScene.GameObjects.Remove(topObject);
                SceneManager.ActiveScene.GameObjects.Insert(index, topObject);

                //// Shift in treeview
                DragDropTreeViewItem topNode = (DragDropTreeViewItem)treeView.Items[treeView.Items.IndexOf(SelectedItem) + 1];
                treeView.Items.Remove(topNode);
                treeView.Items.Insert(treeView.Items.IndexOf(SelectedItem), topNode);
            }
        }

        void moveUpItem_Click(object sender, RoutedEventArgs e)
        {
            GameObject gameObject = (GameObject)(SelectedItem as DragDropTreeViewItem).Tag;

            // no parent?
            if (!((SelectedItem as DragDropTreeViewItem).Parent is DragDropTreeView))
            {
                DragDropTreeViewItem parentNode = (SelectedItem as DragDropTreeViewItem).Parent as DragDropTreeViewItem;

                int index = (parentNode.Tag as GameObject).Children.FindIndex(o => o.GetHashCode() == gameObject.GetHashCode());

                // already on bottom?
                if (index == (parentNode.Tag as GameObject).Children.Count - 1)
                    return;

                GameObject parent = (GameObject)parentNode.Tag;

                // Shift objects 
                GameObject topObject = parent.Children[index + 1];
                parent.Children.Remove(topObject);
                parent.Children.Insert(index, topObject);

                //// Shift in treeview
                DragDropTreeViewItem topNode = (DragDropTreeViewItem)parentNode.Items[parentNode.Items.IndexOf(SelectedItem) - 1];
                parentNode.Items.Remove(topNode);
                parentNode.Items.Insert(parentNode.Items.IndexOf(SelectedItem) + 1, topNode);
            }
            else
            {
                int index = SceneManager.ActiveScene.GameObjects.FindIndex(o => o.GetHashCode() == gameObject.GetHashCode());

                // already on bottom?
                if (index == SceneManager.ActiveScene.GameObjects.Count - 1)
                    return;

                // Shift objects 
                GameObject topObject = SceneManager.ActiveScene.GameObjects[index + 1];
                SceneManager.ActiveScene.GameObjects.Remove(topObject);
                SceneManager.ActiveScene.GameObjects.Insert(index, topObject);

                //// Shift in treeview
                DragDropTreeViewItem topNode = (DragDropTreeViewItem)treeView.Items[treeView.Items.IndexOf(SelectedItem) - 1];
                treeView.Items.Remove(topNode);
                treeView.Items.Insert(treeView.Items.IndexOf(SelectedItem) + 1, topNode);
            }
        }

        void deleteItem_Click(object sender, RoutedEventArgs e)
        {
            List<TreeViewItem> selected = TreeViewExtension.GetSelectedTreeViewItems(treeView);
            string message = "Are you sure you want to delete the selected game object?";
            if (selected.Count > 1)
            {
                message = "Are you sure you want to delete the selected game objects?";
            }

            if (System.Windows.Forms.MessageBox.Show(message, "Warning", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
            {
                foreach (var t in selected)
                {
                    GameObject gameObject = (GameObject)(t as DragDropTreeViewItem).Tag; //(GameObject)(SelectedItem as DragDropTreeViewItem).Tag;
                    DragDropTreeViewItem parentNode = (SelectedItem as DragDropTreeViewItem).Parent as DragDropTreeViewItem;
                    if (parentNode == null)
                    {
                        SceneManager.ActiveScene.GameObjects.Remove((SelectedItem as DragDropTreeViewItem).Tag as GameObject);
                        treeView.Items.Remove(SelectedItem);
                    }
                    else
                    {
                        GameObject objParent = (GameObject)parentNode.Tag;
                        objParent.Children.Remove((SelectedItem as DragDropTreeViewItem).Tag as GameObject);
                        parentNode.Items.Remove(SelectedItem);
                    }
                }
            }
        }

        void pasteItem_Click(object sender, RoutedEventArgs e)
        {
            Paste();
        }

        void copyItem_Click(object sender, RoutedEventArgs e)
        {
            List<GameObject> toCopy = new List<GameObject>();
            foreach (TreeViewItem ti in TreeViewExtension.GetSelectedTreeViewItems(treeView))
            {
                var gameObject = (ti.Tag as GameObject);
                gameObject.SaveComponentValues();
                toCopy.Add(gameObject);

            }

            Clipboard.SetData("GameObject", toCopy);
        }

        void cutItem_Click(object sender, RoutedEventArgs e)
        {
            List<GameObject> toCopy = new List<GameObject>();
            foreach (TreeViewItem ti in TreeViewExtension.GetSelectedTreeViewItems(treeView))
            {
                var gameObject = (ti.Tag as GameObject);
                gameObject.SaveComponentValues();
                toCopy.Add(gameObject);

                DragDropTreeViewItem parentNode = ti.Parent as DragDropTreeViewItem;
                if (parentNode == null)
                {
                    SceneManager.ActiveScene.GameObjects.Remove(ti.Tag as GameObject);
                    treeView.Items.Remove(ti);
                }
                else
                {
                    GameObject objParent = (GameObject)parentNode.Tag;
                    objParent.Children.Remove(ti.Tag as GameObject);
                    parentNode.Items.Remove(ti);
                }
            }
            Clipboard.SetData("GameObject", toCopy);
            TreeViewExtension.UnselectAll(treeView);
            EditorHandler.SelectedGameObjects.Clear();
            EditorHandler.ChangeSelectedObjects();
        }

        void saveStateItem_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            fbd.SelectedPath = SceneManager.GameProject.ProjectPath;

            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ((SelectedItem as DragDropTreeViewItem).Tag as GameObject).Save(fbd.SelectedPath + "//" + ((SelectedItem as DragDropTreeViewItem).Tag as GameObject).Name + ".state");
            }
        }

        private void treeView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            canCopyPaste = true;

            if (treeView.SelectedItem != null && e.LeftButton == MouseButtonState.Pressed)
            {
                (treeView.SelectedItem as TreeViewItem).IsSelected = false;
                TreeViewExtension.UnselectAll(treeView);
            }
        }

        private void treeView_MouseLeave(object sender, MouseEventArgs e)
        {
            canCopyPaste = false;
        }

        private void treeView_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // NEW WAY TO DETECT SELECTIONS:
            if (sender != null && (sender as TreeView).SelectedItem != null)
            {
                List<TreeViewItem> it = TreeViewExtension.GetSelectedTreeViewItems(treeView);
                EditorHandler.SelectedGameObjects = new List<GameObject>();
                foreach (var i in it)
                {
                    //Console.WriteLine(i.Header);
                    //object tag = ((sender as TreeView).SelectedItem as DragDropTreeViewItem).Tag; // old
                    object tag = (i as DragDropTreeViewItem).Tag;

                    if (tag is GameObject)
                    {
                        // TODO : multiple selection                      
                        EditorHandler.SelectedGameObjects.Add((GameObject)tag);
                    }
                }
                EditorHandler.ChangeSelectedObjects();

                lastSelectedItem = (sender as TreeView).SelectedItem as DragDropTreeViewItem;
            }
            else
            {
                EditorHandler.SelectedGameObjects.Clear();
                EditorHandler.ChangeSelectedObjects();
            }
        }

        public void SelectionUpdate()
        {
            foreach (TreeViewItem item in TreeViewExtension.GetExpandedTreeViewItems(treeView))
            {
                object tag = (item as DragDropTreeViewItem).Tag;
                if (tag is GameObject)
                {
                    if (!EditorHandler.SelectedGameObjects.Contains((tag as GameObject)))
                    {
                        TreeViewExtension.SetIsSelected(item, false);
                    }
                    else
                    {
                        TreeViewExtension.SetIsSelected(item, true);
                    }
                }
            }
        }

        #endregion
    }
}
