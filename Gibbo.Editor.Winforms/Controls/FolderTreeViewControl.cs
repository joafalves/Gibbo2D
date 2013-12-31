#region usings

using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Gibbo.Library;
using System.Collections.Generic;
using ComponentFactory.Krypton.Toolkit;
using System.ComponentModel;

#endregion

namespace Gibbo.Editor
{
    public partial class FolderTreeViewControl : UserControl
    {
        #region internal classes

        public class NodeSorter : IComparer
        {
            public int Compare(object thisObj, object otherObj)
            {
                TreeNode thisNode = thisObj as TreeNode;
                TreeNode otherNode = otherObj as TreeNode;

                //if (thisNode.ImageIndex != otherNode.ImageIndex)
                //    return 1;

                int nx = thisNode.ImageIndex.CompareTo(otherNode.ImageIndex);
                if (nx != 0) return nx;

                //alphabetically sorting
                return thisNode.Text.CompareTo(otherNode.Text);
            }
        }

        #endregion

        #region fields

        private string beforeEditingPath;
        private string rootPath;

        List<string> AcceptedExtensions = new List<string>();

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
        public FolderTreeViewControl()
        {
            InitializeComponent();
            treeView.TreeViewNodeSorter = new NodeSorter();

            AcceptedExtensions.Add(".png");
            AcceptedExtensions.Add(".jpg");
            AcceptedExtensions.Add(".jpeg");
            AcceptedExtensions.Add(".gif");
            AcceptedExtensions.Add(".bmp");
            AcceptedExtensions.Add(".txt");
            //AcceptedExtensions.Add(".gibbo");
            AcceptedExtensions.Add(".scene");
            AcceptedExtensions.Add(".sln");
            AcceptedExtensions.Add(".ini");

            treeView.OnDragDropSuccess += treeView_OnDragDropSuccess;
        }

        #endregion

        #region methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rootPath"></param>
        public void CreateView(string rootPath)
        {
            if (rootPath != string.Empty)
            {
                this.rootPath = rootPath;
                treeView.Nodes.Clear();

                TreeNode rootnode = new TreeNode(rootPath);
                rootnode.Tag = "directory";
                treeView.Nodes.Add(rootnode);
                rootnode.ContextMenuStrip = directoryContextMenu;
                FillChildNodes(rootnode);
                treeView.Nodes[0].Expand();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        private void FillChildNodes(TreeNode node)
        {
            try
            {
                DirectoryInfo[] dirs = new DirectoryInfo(node.FullPath).GetDirectories();

                // Add current directory Files:
                string[] files = Directory.GetFiles(node.FullPath);
                foreach (string file in files)
                {
                    if (AcceptedExtensions.Contains(Path.GetExtension(file.ToLower())))
                    {
                        TreeNode newnode = new TreeNode(Path.GetFileName(file));
                        HandleNewNode(newnode);
                        node.Nodes.Add(newnode);
                        newnode.ContextMenuStrip = fileContextMenu;
                        newnode.Tag = "file";
                    }
                }

                // Add Sub Directories:
                foreach (DirectoryInfo dir in dirs)
                {
                    TreeNode newnode = new TreeNode(dir.Name);
                    node.Nodes.Add(newnode);

                    // Does the cur directory contain any files or sub directories?
                    if (Directory.GetFiles(dir.FullName).Count() > 0 ||
                        Directory.GetDirectories(dir.FullName).Count() > 0)
                    {
                        newnode.Nodes.Add("*");
                    }

                    newnode.ContextMenuStrip = directoryContextMenu;
                    newnode.Tag = "directory";
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(ex.Message.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        private void UpdateNode(TreeNode node)
        {
            string extension = Path.GetExtension(node.Text).ToLower();
            int index = 1;
            switch (extension)
            {
                case ".png":
                case ".jpeg":
                case ".jpg":
                case ".bmp":
                case ".gif":
                    index = 2;
                    break;
                case ".txt":
                case ".ini":
                    index = 3;
                    break;
                case ".gibbo":
                    index = 4;
                    break;
                case ".cs":
                    index = 5;
                    break;
                case ".scene":
                    index = 6;
                    break;
                case ".sln":
                    index = 7;
                    break;
            }

            node.ImageIndex = index;
            node.SelectedImageIndex = index;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        private void HandleNewNode(TreeNode node)
        {
            UpdateNode(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        private void CreateDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                int c = 1;
                while (Directory.Exists(path + c)) c++;
                path += c;
            }

            Directory.CreateDirectory(path);
            TreeNode newnode = new TreeNode(Path.GetFileName(path));
            newnode.ContextMenuStrip = directoryContextMenu;
            newnode.Tag = "directory";
            SelectedNode.Nodes.Add(newnode);
            SelectedNode.Expand();

            //treeView.Sort();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns>The path of the created file</returns>
        private string CreateFile(string path, FileTemplate template)
        {
            if (SelectedNode == null) return string.Empty;

            path = FileHelper.CreateFile(path, template);

            // Add the new tree node to the TreeView
            TreeNode newnode = new TreeNode(Path.GetFileName(path));
            newnode.ContextMenuStrip = fileContextMenu;
            HandleNewNode(newnode);
            newnode.Tag = "file";

            if ((SelectedNode.Nodes.Count > 0 && !SelectedNode.Nodes[0].Text.Equals("*")) || SelectedNode.Nodes.Count == 0)
            {
                SelectedNode.Nodes.Add(newnode);
            }

            SelectedNode.Expand();

            //treeView.Sort();

            return path;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        private void HandleOpenFile(TreeNode node)
        {
            // Optional: implement open file behaviour
            string extension = Path.GetExtension(node.Text).ToLower();
            switch (extension)
            {
                case ".gibbo":
                    //EditorHandler.ChangeSelectedObject(SceneManager.GameProject);
                    break;
                case ".scene":
                    //System.Threading.ThreadPool.QueueUserWorkItem(o => Gibbo.Library.SceneManager.LoadScene(node.FullPath));
                    Gibbo.Library.SceneManager.LoadScene(node.FullPath);
                    EditorHandler.ChangeSelectedObject(Gibbo.Library.SceneManager.ActiveScene);
                    EditorHandler.SelectedGameObjects = new List<GameObject>();
                    EditorHandler.SceneTreeView.CreateView();
                    EditorHandler.EditorSplitterContainer.Panel2Collapsed = true;
                    EditorHandler.ChangeSelectedObjects();
                    break;
                case ".cs":
                    // TODO: add behaviour for opening .cs files

                    break;
                default:
                    // Default behaviour, tries to use the default user opening for this type of file:
                    try
                    {
                        Process.Start(node.FullPath);
                    }
                    catch (Exception ex)
                    {
                        KryptonMessageBox.Show(ex.Message);
                    }
                    break;
            }
        }

        #endregion

        #region events

        void treeView_OnDragDropSuccess(TreeNode source, TreeNode target, CancelEventArgs e)
        {
            string sourceType = (string)source.Tag;
            string targetType = (string)target.Tag;

            // the target node is not a directory?
            if (!targetType.Equals("directory"))
            {
                e.Cancel = true;
                return;
            }

            string targetPath = target.FullPath;
            if (sourceType.Equals("directory"))
            {
                targetPath += "\\" + source.Text;

                if (!Directory.Exists(targetPath))
                    Directory.CreateDirectory(targetPath);
            }

            if (sourceType.Equals("directory"))
            {
                try
                {
                    GibboHelper.CopyDirectory(source.FullPath, targetPath, true);
                    Directory.Delete(source.FullPath, true);
                }
                catch (Exception ex)
                {
                    e.Cancel = true;
                    KryptonMessageBox.Show("Error: " + ex.Message, "Error!");
                }
            }
            else
            {
                try
                {
                    File.Move(source.FullPath, targetPath + "\\" + source.Text);
                }
                catch (Exception ex)
                {
                    e.Cancel = true;
                    KryptonMessageBox.Show("Error: " + ex.Message, "Error!");                
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            string type = (String)e.Node.Tag;
            if (e.Node.Parent == null || type == "directory") return;

            HandleOpenFile(e.Node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            // Has the directory been open already?
            if (e.Node.Nodes[0].Text.Equals("*"))
            {
                e.Node.Nodes.Clear();
                FillChildNodes(e.Node);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedNode == null) return;

            string path = SelectedNode.FullPath + "\\New Folder";
            CreateDirectory(path);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = SelectedNode.FullPath;
            if (SelectedNode == null || SelectedNode == this.treeView.Nodes[0]) return;

            if (MessageBox.Show("Are you sure you want to delete this folder?\n" + path, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    Directory.Delete(path, true);
                    treeView.Nodes.Remove(SelectedNode);
                }
                catch (Exception ex)
                {
                    KryptonMessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedNode.BeginEdit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            // Is the user trying to rename the root node?
            if (SelectedNode == treeView.Nodes[0] || SelectedNode == null || Path.GetExtension(e.Node.FullPath) == ".gibbo")
                e.CancelEdit = true;

            beforeEditingPath = SelectedNode.FullPath;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Node.Parent == null) return;

            string destination = e.Node.Parent.FullPath + "\\" + e.Label;

            // The path was changed?
            // The destination path already exists?
            if (!beforeEditingPath.Equals(destination) &&
                !Directory.Exists(destination) &&
                !File.Exists(destination))
            {
                Directory.Move(beforeEditingPath, destination);
            }
            else
            {
                e.CancelEdit = true;
            }

            this.BeginInvoke(new Action(() => AfterAfterEdit(e.Node)));
        }


        /// <summary>
        /// This event is trigged after the label of a tree node has truly changed
        /// </summary>
        /// <param name="node"></param>
        private void AfterAfterEdit(TreeNode node)
        {
            treeView.Sort();

            // The node represents a file?
            if ((node.Tag as String).Equals("file"))
                UpdateNode(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            SelectedNode = e.Node;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_NodeMouseClick_1(object sender, TreeNodeMouseClickEventArgs e)
        {
            treeView.SelectedNode = e.Node;
            SelectedNode = e.Node;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateView(this.rootPath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateFile(SelectedNode.FullPath + "\\ScriptFile.cs", FileTemplate.Component);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addGameSceneMenuItem_Click(object sender, EventArgs e)
        {
            string fn = CreateFile(SelectedNode.FullPath + "\\GameScene.scene", FileTemplate.None);
            Gibbo.Library.SceneManager.CreateScene(fn);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HandleOpenFile(SelectedNode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void renameToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SelectedNode.BeginEdit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = SelectedNode.FullPath;
            if (SelectedNode == null) return;

            if (MessageBox.Show("Are you sure you want to delete this file?\n" + path, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                File.Delete(path);
                treeView.Nodes.Remove(SelectedNode);
            }
        }

        #endregion
    }
}
