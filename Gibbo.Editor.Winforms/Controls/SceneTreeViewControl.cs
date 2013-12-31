using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Gibbo.Library;
using System.Diagnostics;
using System.Reflection;
using ComponentFactory.Krypton.Toolkit;

namespace Gibbo.Editor
{
    public partial class SceneTreeViewControl : UserControl
    {
        #region internal classes

        public class NodeSorter : IComparer
        {
            public int Compare(object thisObj, object otherObj)
            {
                TreeNode thisNode = thisObj as TreeNode;
                TreeNode otherNode = otherObj as TreeNode;

                if (thisNode.Tag is Layer)
                    return 0;

                //alphabetically sorting
                return thisNode.Text.CompareTo(otherNode.Text);
            }
        }

        #endregion

        #region fields



        #endregion

        #region properties

        /// <summary>
        /// The current selected tree node.
        /// </summary>
        public TreeNode SelectedNode { get; set; }

        #endregion

        #region constructors

        /// <summary>
        /// 
        /// </summary>
        public SceneTreeViewControl()
        {
            InitializeComponent();

            // (No sorting for now)
            //treeView.TreeViewNodeSorter = new NodeSorter();
            treeView.OnDragDropSuccess += treeView_OnDragDropSuccess;
        }

        #endregion

        #region methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="tag"></param>
        /// <param name="imageIndex"></param>
        private TreeNode AddNode(TreeNode parent, object tag, int imageIndex)
        {
            TreeNode node = new TreeNode(tag.ToString());
            node.Tag = tag;
            node.ImageIndex = imageIndex;
            node.SelectedImageIndex = imageIndex;
            node.ContextMenuStrip = objectContextMenuStrip;

            parent.Nodes.Insert(0, node);

            if (!parent.IsExpanded)
                parent.Expand();

            return node;
        }

        /// <summary>
        /// 
        /// </summary>
        public void CreateView()
        {
            treeView.Nodes.Clear();

            if (SceneManager.ActiveScene == null)
            {
                TreeNode noScene = new TreeNode("No scene in memory");
                treeView.Nodes.Add(noScene);
                return;
            }

            TreeNode rootnode = new TreeNode("Scene Hierarchy");
            treeView.Nodes.Add(rootnode);
            rootnode.ContextMenuStrip = rootContextMenuStrip;

            // Set all layers
            foreach (Layer layer in SceneManager.ActiveScene.Layers)
            {
                //Console.WriteLine(layer.Name);
                string name = layer.Name == null ? "Layer" : layer.Name;
                layer.Name = name;

                TreeNode node = AddNode(rootnode, layer, 1);
                node.ContextMenuStrip = LayerContextMenuStrip;

                // Set all this layer game objects
                foreach (GameObject gameObject in layer.GameObjects)
                {
                    name = gameObject.Name == null ? "Game Object" : gameObject.Name;
                    gameObject.Name = name;


                    TreeNode _node = AddNode(node, gameObject, GameObjectImageIndex(gameObject));
                    _node.ContextMenuStrip = objectContextMenuStrip;

                    FillGameObjects(gameObject, _node);    
                }
            }

            treeView.Nodes[0].Expand();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_gameObject"></param>
        /// <param name="node"></param>
        private void FillGameObjects(GameObject _gameObject, TreeNode node)
        {
            foreach (GameObject gameObject in _gameObject.Children)
            {
                string name = gameObject.Name == null ? "Game Object" : gameObject.Name;
                gameObject.Name = name;

                TreeNode _node = AddNode(node, gameObject, GameObjectImageIndex(gameObject));
                _node.ContextMenuStrip = objectContextMenuStrip;

                FillGameObjects(gameObject, _node);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        private int GameObjectImageIndex(GameObject gameObject)
        {
            if (gameObject is Sprite)
                return 2;
            else if (gameObject is AnimatedSprite)
                return 4;
            else if (gameObject is AudioObject)
                return 5;
            else if (gameObject is Tileset)
                return 6;
            else if (gameObject is BMFont)
                return 7;
           
            // Empty game object
            return 3;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        private void HandleOpenFile(TreeNode node)
        {
            //EditorHandler.ChangeSelectedObject(node.Tag);
            //EditorHandler.ChangeSelectedObject(node.Tag);
            if (node.Tag is GameObject)
            {
                EditorHandler.SelectedGameObjects = new List<GameObject>();
                EditorHandler.SelectedGameObjects.Add((GameObject)node.Tag);
                EditorHandler.ChangeSelectedObjects();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddGameObject(GameObject gameObject, string type)
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
                } 
            }
            else
            {
                gameObject.Initialize();
            }

            if (SelectedNode.Tag is Layer)
            {
                Layer layer = (Layer)SelectedNode.Tag;
                layer.GameObjects.Add(gameObject);
            }
            else if (SelectedNode.Tag is GameObject)
            {
                GameObject _gameObject = (GameObject)SelectedNode.Tag;
                _gameObject.Children.Add(gameObject);
            }

            if (gameObject != null)
            {
                TreeNode node = AddNode(SelectedNode, gameObject, GameObjectImageIndex(gameObject));
                node.ContextMenuStrip = objectContextMenuStrip;

                EditorHandler.SelectedGameObjects = new List<GameObject>();
                EditorHandler.SelectedGameObjects.Add(gameObject);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddLayer()
        {
            Layer layer = new Layer();
            layer.Name = "Layer";

            SceneManager.ActiveScene.Layers.Add(layer);

            TreeNode node = AddNode(SelectedNode, layer, 1);
            node.ContextMenuStrip = LayerContextMenuStrip;
        }

        private void AddFromState()
        {
            OpenFileDialog ofd = new OpenFileDialog();
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
                    KryptonMessageBox.Show(ex.ToString());
                }
            }
        }

        #endregion

        #region events

        void treeView_OnDragDropSuccess(TreeNode source, TreeNode target, CancelEventArgs e)
        {
            // don't allow moving tilesets, only allow game objects
            if (!(source.Tag is GameObject))
            {
                e.Cancel = true;
                return;
            }

            GameObject _source = source.Tag as GameObject;

            if (target.Tag is Layer || target.Tag is GameObject)
            {
                // parent is a layer?
                if (_source.Transform.Parent == null)
                {
                    Layer parent = source.Parent.Tag as Layer;
                    parent.GameObjects.Remove(_source);
                }
                else
                {
                    GameObject parent = source.Parent.Tag as GameObject;
                    parent.Children.Remove(_source);
                }

                if (target.Tag is Layer)
                {
                    (target.Tag as Layer).GameObjects.Insert((target.Tag as Layer).GameObjects.Count - 1, _source);
                }
                else
                {
                    (target.Tag as GameObject).Children.Insert((target.Tag as GameObject).Children.Count - 1, _source);
                }
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void treeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Parent == null) return;

            HandleOpenFile(e.Node);

            if(e.Node.Tag is GameObject)
            {
                ComponentEditor ce = new ComponentEditor((GameObject)e.Node.Tag);
                ce.Show();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (SceneManager.ActiveScene != null)
            {

            }
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SceneManager.ActiveScene != null)
            {
                CreateView();
            }
        }

        private void emptyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddGameObject(null, "empty");
        }

        private void spriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddGameObject(null, "sprite");
        }

        private void audioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddGameObject(null, "audio");
        }

        private void audioObjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddGameObject(null, "audio");
        }

        private void tilesetToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddGameObject(null, "tileset");
        }

        private void bMFontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddGameObject(null, "bmfont");
        }


        private void bMFontToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            AddGameObject(null, "bmfont");
        }

        private void tilesetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddGameObject(null, "tileset");
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            SelectedNode = e.Node;
        }

        private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            treeView.SelectedNode = e.Node;
            SelectedNode = e.Node;

            if (e.Node.Tag is GameObject)
            {
                HandleOpenFile(e.Node);

                // TODO : multiple selection
                EditorHandler.SelectedGameObjects = new List<GameObject>();
                EditorHandler.SelectedGameObjects.Add((GameObject)e.Node.Tag);
            }
        }

        private void addLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddLayer();
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedNode.BeginEdit();
        }

        private void renameToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SelectedNode.BeginEdit();
        }

        private void treeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label == null || e.Label == string.Empty)
            {
                e.CancelEdit = true;
                return;
            }

            if (e.Node.Tag is Layer)
            {
                (e.Node.Tag as Layer).Name = e.Label;
            }
            else if (e.Node.Tag is GameObject)
            {
                (e.Node.Tag as GameObject).Name = e.Label;
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this scene?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Layer layer = (Layer)SelectedNode.Tag;
                SelectedNode.Remove();
                SceneManager.ActiveScene.Layers.Remove(layer);
            }
        }

        private void removeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this game object?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                GameObject gameObject = (GameObject)SelectedNode.Tag;
                TreeNode parentNode = SelectedNode.Parent;
                SelectedNode.Remove();

                if (parentNode.Tag is Layer) (parentNode.Tag as Layer).GameObjects.Remove(gameObject);
                else if (parentNode.Tag is GameObject) (parentNode.Tag as GameObject).Children.Remove(gameObject);

                EditorHandler.SelectedGameObjects = new List<GameObject>();
                EditorHandler.ChangeSelectedObjects();
            }
        }

        private void emptyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddGameObject(null, "empty");
        }

        private void spriteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddGameObject(null, "sprite");
        }

        private void animatedSpriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddGameObject(null, "animatedsprite");
        }

        private void animatedSpriteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddGameObject(null, "animatedsprite");
        }

        private void propertiesStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorHandler.ChangeSelectedObject(SelectedNode.Tag);
        }

        private void moveDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Layer selectedLayer = (Layer)SelectedNode.Tag;
            int selectedLayerIndex = SceneManager.ActiveScene.Layers.IndexOf(selectedLayer);

            // The layer is already on bottom?
            if (selectedLayerIndex == 0)
                return;

            // Shift layers
            Layer bottomLayer = SceneManager.ActiveScene.Layers.ElementAt(selectedLayerIndex - 1);
            SceneManager.ActiveScene.Layers.Remove(bottomLayer);
            SceneManager.ActiveScene.Layers.Insert(selectedLayerIndex, bottomLayer);

            // Shift layers in the tree view
            TreeNode bottomLayerNode = SelectedNode.Parent.Nodes[SelectedNode.Index + 1];
            SelectedNode.Parent.Nodes.Remove(bottomLayerNode);
            SelectedNode.Parent.Nodes.Insert(SelectedNode.Index, bottomLayerNode);
        }

        private void moveUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Layer selectedLayer = (Layer)SelectedNode.Tag;
            int selectedLayerIndex = SceneManager.ActiveScene.Layers.IndexOf(selectedLayer);

            // The layer is already on top?
            if (selectedLayerIndex == SceneManager.ActiveScene.Layers.Count-1)
                return;

            // Shift layers
            Layer topLayer = SceneManager.ActiveScene.Layers.ElementAt(selectedLayerIndex + 1);
            SceneManager.ActiveScene.Layers.Remove(topLayer);
            SceneManager.ActiveScene.Layers.Insert(selectedLayerIndex, topLayer);

            // Shift layers in the tree view
            TreeNode topLayerNode = SelectedNode.Parent.Nodes[SelectedNode.Index -1];
            SelectedNode.Parent.Nodes.Remove(topLayerNode);
            SelectedNode.Parent.Nodes.Insert(SelectedNode.Index+1, topLayerNode);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GameObject gameObject = (GameObject)SelectedNode.Tag;
            TreeNode parentNode = SelectedNode.Parent;

            if (parentNode.Tag is Layer)
            {
                int index = (parentNode.Tag as Layer).GameObjects.FindIndex(o => o.GetHashCode() == gameObject.GetHashCode());
                
                // already on top?
                if (index == (parentNode.Tag as Layer).GameObjects.Count - 1)
                    return;

                Layer parent = (Layer)parentNode.Tag;

                // Shift objects 
                GameObject topObject = parent.GameObjects[index + 1];
                parent.GameObjects.Remove(topObject);
                parent.GameObjects.Insert(index, topObject);
            }
            else if (parentNode.Tag is GameObject)
            {
                int index = (parentNode.Tag as GameObject).Children.FindIndex(o => o.GetHashCode() == gameObject.GetHashCode());

                // already on top?
                if (index == (parentNode.Tag as GameObject).Children.Count - 1)
                    return;

                GameObject parent = (GameObject)parentNode.Tag;

                // Shift objects 
                GameObject topObject = parent.Children[index + 1];
                parent.Children.Remove(topObject);
                parent.Children.Insert(index, topObject);
            }

            // Shift in treeview
            TreeNode topNode = SelectedNode.Parent.Nodes[SelectedNode.Index - 1];
            SelectedNode.Parent.Nodes.Remove(topNode);
            SelectedNode.Parent.Nodes.Insert(SelectedNode.Index + 1, topNode);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            GameObject gameObject = (GameObject)SelectedNode.Tag;
            TreeNode parentNode = SelectedNode.Parent;

            if (parentNode.Tag is Layer)
            {
                int index = (parentNode.Tag as Layer).GameObjects.FindIndex(o => o.GetHashCode() == gameObject.GetHashCode());

                // already on bottom?
                if (index == 0)
                    return;

                Layer parent = (Layer)parentNode.Tag;

                // Shift objects 
                GameObject topObject = parent.GameObjects[index - 1];
                parent.GameObjects.Remove(topObject);
                parent.GameObjects.Insert(index, topObject);
            }
            else if (parentNode.Tag is GameObject)
            {
                int index = (parentNode.Tag as GameObject).Children.FindIndex(o => o.GetHashCode() == gameObject.GetHashCode());

                // already on bottom?
                if (index == 0)
                    return;

                GameObject parent = (GameObject)parentNode.Tag;

                // Shift objects 
                GameObject topObject = parent.Children[index - 1];
                parent.Children.Remove(topObject);
                parent.Children.Insert(index, topObject);
            }

            // Shift in treeview
            TreeNode topNode = SelectedNode.Parent.Nodes[SelectedNode.Index + 1];
            SelectedNode.Parent.Nodes.Remove(topNode);
            SelectedNode.Parent.Nodes.Insert(SelectedNode.Index, topNode);
        }

        private void objectContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            addComponentToolStripMenuItem.DropDownItems.Clear();

            if (SceneManager.ScriptsAssembly == null)
            {
                addComponentToolStripMenuItem.Visible = false;
                return;
            }
            else
            {     
                addComponentToolStripMenuItem.Visible = true;

                ObjectComponent dummy = new ObjectComponent();
                foreach (Type type in SceneManager.ScriptsAssembly.GetTypes())
                {
                    if (type.IsSubclassOf(dummy.GetType()))
                    {
                        string fullname = type.FullName;

                        ToolStripMenuItem lastItem = addComponentToolStripMenuItem;

                        if (fullname != type.Name && fullname.Contains('.'))
                        {
                            string[] splitted = fullname.Split('.');
                            int scount = splitted.Count() - 1;
                            
                            for (int i = 0; i < scount; i++)
                            {
                                ToolStripMenuItem item = null;
                                string camelCaseFix = GibboHelper.SplitCamelCase(splitted[i]);

                                foreach(ToolStripMenuItem _item in lastItem.DropDownItems)
                                {
                                    if (_item.Text.Equals(camelCaseFix))
                                    {
                                        item = _item;
                                        break;
                                    }
                                }

                                if(item == null)
                                    item = new ToolStripMenuItem(camelCaseFix);

                                lastItem.DropDownItems.Insert(0, item);
                                lastItem = item;
                            }
                        }

                        ToolStripMenuItem newItem = new ToolStripMenuItem(GibboHelper.SplitCamelCase(type.Name));
                        newItem.Tag = type;
                        newItem.Image = Properties.Resources.component_item;
                        newItem.Click += new EventHandler(component_Click);

                        lastItem.DropDownItems.Add(newItem);
                    }
                }
            }
        }

        private void component_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem _sender = (ToolStripMenuItem)sender;

            ObjectComponent component = (ObjectComponent)Activator.CreateInstance((Type)_sender.Tag, new object[] { /* params here */ });
            GameObject gameObject = (GameObject)SelectedNode.Tag;
            gameObject.AddComponent(component);
        }

        private void componentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ComponentEditor ce = new ComponentEditor((GameObject)SelectedNode.Tag);
            ce.Show();
        }

        private void savePrefabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = SceneManager.GameProject.ProjectPath;

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                (treeView.SelectedNode.Tag as GameObject).Save(fbd.SelectedPath + "//" + (treeView.SelectedNode.Tag as GameObject).Name + ".state");
            }
        }

        private void loadStateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open State";
            ofd.Filter = @"(*.state)|*.state";

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    GameObject gameObject = (GameObject)SelectedNode.Tag;
                    TreeNode parentNode = SelectedNode.Parent;
                    
                    GameObject loadedState = (GameObject)GibboHelper.DeserializeObject(ofd.FileName);
                    
                    loadedState.Name = gameObject.Name;
                    loadedState.Transform = (Transform)gameObject.Transform.Clone();
                    loadedState.Initialize();

                    //EditorHandler.SelectedGameObject = loadedState;

                    SelectedNode.Tag = loadedState;
                    SelectedNode.Text = loadedState.Name;

                    if (parentNode.Tag is Layer)
                    {
                        int index = (parentNode.Tag as Layer).GameObjects.FindIndex(o => o.GetHashCode() == gameObject.GetHashCode());
                        (parentNode.Tag as Layer).GameObjects.Remove(gameObject);
                        (parentNode.Tag as Layer).GameObjects.Insert(index, loadedState);
                    }
                    else if (parentNode.Tag is GameObject)
                    {
                        int index = (parentNode.Tag as GameObject).Children.FindIndex(o => o.GetHashCode() == gameObject.GetHashCode());
                        (parentNode.Tag as GameObject).Children.Remove(gameObject);
                        (parentNode.Tag as GameObject).Children.Insert(index, loadedState);
                    }
                }
                catch (Exception ex)
                {
                    KryptonMessageBox.Show(ex.ToString());
                }
            }
        }

        private void addFromStateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddFromState();
        }

        private void addFromStateToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddFromState();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetData("GameObject", (SelectedNode.Tag as GameObject).Clone());
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameObject gameObject = (GameObject)Clipboard.GetData("GameObject");

            if (gameObject != null)
            {
                AddGameObject(gameObject, string.Empty);
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            GameObject gameObject = (GameObject)Clipboard.GetData("GameObject");

            if (gameObject != null)
            {
                AddGameObject(gameObject, string.Empty);
            }
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetData("GameObject", (SelectedNode.Tag as GameObject).Clone());

            GameObject gameObject = (GameObject)SelectedNode.Tag;
            TreeNode parentNode = SelectedNode.Parent;
            SelectedNode.Remove();

            if (parentNode.Tag is Layer) (parentNode.Tag as Layer).GameObjects.Remove(gameObject);
            else if (parentNode.Tag is GameObject) (parentNode.Tag as GameObject).Children.Remove(gameObject);
        }

        #endregion
    }
}
