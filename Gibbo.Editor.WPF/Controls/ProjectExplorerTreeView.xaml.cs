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

*/
#endregion

using Gibbo.Editor.Model;
using Gibbo.Library;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace Gibbo.Editor.WPF
{

    public partial class ProjectExplorerTreeView : UserControl
    {
        #region Fields

        const int TIMER_DELAY = 1000;

        System.Windows.Forms.Timer timer;
        ExplorerTreeViewItem selectedForEditing;

        const int MARGIN = 4;

        string beforeEditingPath = string.Empty;

        List<string> AcceptedExtensions = new List<string>();

        ContextMenu fileContextMenu = null;
        ContextMenu directoryContextMenu = null;
        ContextMenu rootContextMenu = null;

        bool firstTick = true;

        #endregion

        #region Properties

        internal DragDropTreeViewItem SelectedItem
        {
            get
            {
                return treeView.SelectedItem as DragDropTreeViewItem;
            }
        }

        #endregion

        #region Constructors

        public ProjectExplorerTreeView()
        {
            InitializeComponent();
            Initialize();

            treeView.Items.SortDescriptions.Clear();
            treeView.Items.SortDescriptions.Add(new SortDescription("PriorityIndex", ListSortDirection.Ascending));
            treeView.Items.SortDescriptions.Add(new SortDescription("Text", ListSortDirection.Ascending));

            treeView.OnDragDropSuccess += treeView_OnDragDropSuccess;
            treeView.MouseRightButtonDown += treeView_MouseRightButtonDown;
            treeView.MouseLeftButtonDown += treeView_MouseLeftButtonDown;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            // Settings
            AcceptedExtensions = new List<string>(Properties.Settings.Default.AcceptedExtensions.Split('|'));

            // Root Context Menu
            this.rootContextMenu = new ContextMenu();

            // TODO change image sources
            MenuItem createItemRoot = EditorUtils.CreateMenuItem("Create...", null);
            MenuItem sceneItemRoot = EditorUtils.CreateMenuItem("Game Scene", (ImageSource)FindResource("SceneIcon"));
            MenuItem scriptItemRoot = EditorUtils.CreateMenuItem("C# Script", (ImageSource)FindResource("CSFileIcon"));
            MenuItem addFromFolderItemRoot = EditorUtils.CreateMenuItem("Add From Folder", null);
            MenuItem createFolderItemRoot = EditorUtils.CreateMenuItem("Create Folder", (ImageSource)FindResource("FolderAddIcon"));
            MenuItem openFolderItemRoot = EditorUtils.CreateMenuItem("Open Folder in File Explorer", null);
            MenuItem copyFullPathItemRoot = EditorUtils.CreateMenuItem("Copy Full Path", null);

            createItemRoot.Items.Add(sceneItemRoot);
            createItemRoot.Items.Add(new Separator());
            createItemRoot.Items.Add(scriptItemRoot);

            this.rootContextMenu.Items.Add(createItemRoot);
            this.rootContextMenu.Items.Add(createFolderItemRoot);
            this.rootContextMenu.Items.Add(new Separator());
            this.rootContextMenu.Items.Add(addFromFolderItemRoot);
            this.rootContextMenu.Items.Add(new Separator());
            this.rootContextMenu.Items.Add(copyFullPathItemRoot);
            this.rootContextMenu.Items.Add(openFolderItemRoot);

            sceneItemRoot.Click += createSceneItem_Click;
            createFolderItemRoot.Click += createFolderItem_Click;
            addFromFolderItemRoot.Click += addFromFolder_Click;
            openFolderItemRoot.Click += openFolderItem_Click;
            copyFullPathItemRoot.Click += copyFullPath_Click;
            scriptItemRoot.Click += scriptItem_Click;

            // Directory Context Menu
            this.directoryContextMenu = new ContextMenu();

            // TODO change image sources
            MenuItem createItem = EditorUtils.CreateMenuItem("Create...", null);
            MenuItem sceneItem = EditorUtils.CreateMenuItem("Game Scene", (ImageSource)FindResource("SceneIcon"));
            MenuItem scriptItem = EditorUtils.CreateMenuItem("C# Script", (ImageSource)FindResource("CSFileIcon"));
            MenuItem createFolderItem = EditorUtils.CreateMenuItem("Create Folder", (ImageSource)FindResource("FolderAddIcon"));
            MenuItem addFromFolderItem = EditorUtils.CreateMenuItem("Add From Folder", null);
            MenuItem renameItem = EditorUtils.CreateMenuItem("Rename", (ImageSource)FindResource("RenameIcon"));
            MenuItem removeItem = EditorUtils.CreateMenuItem("Remove", null);
            MenuItem openFolderItem = EditorUtils.CreateMenuItem("Open Folder in File Explorer", null);
            MenuItem copyFullPathItemDir = EditorUtils.CreateMenuItem("Copy Full Path", null);
            MenuItem copyRelativePathItemDir = EditorUtils.CreateMenuItem("Copy Relative Path", null);

            createItem.Items.Add(sceneItem);
            createItem.Items.Add(new Separator());
            createItem.Items.Add(scriptItem);

            // other directories

            this.directoryContextMenu.Items.Add(createItem);
            this.directoryContextMenu.Items.Add(createFolderItem);
            this.directoryContextMenu.Items.Add(new Separator());
            this.directoryContextMenu.Items.Add(addFromFolderItem);
            this.directoryContextMenu.Items.Add(new Separator());
            this.directoryContextMenu.Items.Add(copyFullPathItemDir);
            this.directoryContextMenu.Items.Add(copyRelativePathItemDir);
            this.directoryContextMenu.Items.Add(openFolderItem);
            this.directoryContextMenu.Items.Add(new Separator());
            this.directoryContextMenu.Items.Add(renameItem);
            this.directoryContextMenu.Items.Add(removeItem);

            scriptItem.Click += scriptItem_Click;
            sceneItem.Click += createSceneItem_Click;
            createFolderItem.Click += createFolderItem_Click;
            addFromFolderItem.Click += addFromFolder_Click;
            copyFullPathItemDir.Click += copyFullPath_Click;
            copyRelativePathItemDir.Click += copyRelativePathItem_Click;
            openFolderItem.Click += openFolderItem_Click;
            renameItem.Click += renameItem_Click;
            removeItem.Click += removeItem_Click;

            // File Context Menu
            this.fileContextMenu = new ContextMenu();

            // TODO  change image sources
            MenuItem openItem = EditorUtils.CreateMenuItem("Open", null);
            MenuItem openContainingFolderItem = EditorUtils.CreateMenuItem("Open Containing Folder", null);
            MenuItem renameFileItem = EditorUtils.CreateMenuItem("Rename", (ImageSource)FindResource("RenameIcon"));
            MenuItem deleteItem = EditorUtils.CreateMenuItem("Delete", null);
            MenuItem copyFullPathItem = EditorUtils.CreateMenuItem("Copy Full Path", null);
            MenuItem copyRelativePathItem = EditorUtils.CreateMenuItem("Copy Relative Path", null);

            this.fileContextMenu.Items.Add(openItem);
            this.fileContextMenu.Items.Add(new Separator());
            this.fileContextMenu.Items.Add(copyFullPathItem);
            this.fileContextMenu.Items.Add(copyRelativePathItem);
            this.fileContextMenu.Items.Add(openContainingFolderItem);
            this.fileContextMenu.Items.Add(new Separator());
            this.fileContextMenu.Items.Add(renameFileItem);
            this.fileContextMenu.Items.Add(deleteItem);

            openItem.Click += openItem_Click;
            copyFullPathItem.Click += copyFullPath_Click;
            copyRelativePathItem.Click += copyRelativePathItem_Click;
            openContainingFolderItem.Click += openFolderItem_Click;
            renameFileItem.Click += renameItem_Click;
            deleteItem.Click += deleteItem_Click;

            // timer:
            timer = new System.Windows.Forms.Timer();

            timer.Interval = TIMER_DELAY;
            timer.Tick += timer_Tick;
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

        private void RealtimeFolderUpdater(ExplorerTreeViewItem folder)
        {
            // search for deleted files & directories on explorer
            foreach (ExplorerTreeViewItem node in folder.Items)
            {
                if (node.Tag.ToString().ToLower().Equals("directory"))
                {
                    if (!Directory.Exists(node.FullPath))
                        (node.Parent as ExplorerTreeViewItem).Items.Remove(node);
                }
                else
                {
                    if (!File.Exists(node.FullPath))
                    {
                        if (!(System.IO.Path.GetExtension(node.FullPath).ToLower().Equals(".cs") && !node.CanDrag))
                            (node.Parent as ExplorerTreeViewItem).Items.Remove(node);
                    }
                }
            }

            // search for new files on explorer:
            foreach (string filename in Directory.GetFiles(folder.FullPath))
            {
                if (!this.AcceptedExtensions.Contains(System.IO.Path.GetExtension(filename))) continue;

                bool found = false;
                foreach (ExplorerTreeViewItem node in folder.Items)
                {
                    //Console.WriteLine(filename + "::::" + node.FullPath.ToLower());
                    if (node.FullPath.ToLower().Equals(filename.ToLower()) && node.Tag.ToString().Equals("file"))
                    {
                        found = true;
                        break;
                    }
                }

                // file exists but it is not on the treeview?
                if (!found)
                {
                    // add item
                    ExplorerTreeViewItem newNode = AddNode(folder, System.IO.Path.GetFileName(filename), 10, GetImageSource(System.IO.Path.GetExtension(filename)));

                    newNode.Tag = "file";
                    newNode.ContextMenu = this.fileContextMenu;
                }
            }

            // search for new directories on explorer:
            foreach (string folderPath in Directory.GetDirectories(folder.FullPath))
            {
                bool found = false;
                foreach (ExplorerTreeViewItem node in folder.Items)
                {
                    if (node.FullPath.ToLower().Equals(folderPath.ToLower()) && node.Tag.ToString().Equals("directory"))
                    {
                        found = true;
                        break;
                    }
                }

                // directory exists but it is not on the treeview?
                if (!found)
                {
                    // add item
                    ExplorerTreeViewItem _node = AddNode(folder, System.IO.Path.GetFileName(folderPath), 5, (ImageSource)FindResource("FolderIcon"));
                    _node.ContextMenu = this.directoryContextMenu;
                    _node.Tag = "directory";

                    // Does the cur directory contain any files or sub directories?
                    if (Directory.GetFiles(folderPath).Count() > 0 ||
                        Directory.GetDirectories(folderPath).Count() > 0)
                    {
                        AddNode(_node, "*", 0);
                        _node.Expanded += Node_Expanded;

                    }

                    _node.Items.SortDescriptions.Clear();
                    _node.Items.SortDescriptions.Add(new SortDescription("PriorityIndex", ListSortDirection.Ascending));
                    _node.Items.SortDescriptions.Add(new SortDescription("Text", ListSortDirection.Ascending));
                }
            }

            foreach (ExplorerTreeViewItem item in folder.Items)
            {
                if (item.PriorityIndex == 5)
                {
                    if ((item.Items.Count > 0 && (item.Items[0] as ExplorerTreeViewItem).Text != "*") || item.Items.Count == 0)
                        RealtimeFolderUpdater(item);
                }
            }
        }

        bool ItemLostFocus()
        {
            if (selectedForEditing != null && selectedForEditing.Parent != null && selectedForEditing.Header != null && (selectedForEditing.Header as StackPanel).Children.Count >= 2
                && (selectedForEditing.Header as StackPanel).Children[1] is TextBox)
            {
                // update the current name:
                string newValue = ((selectedForEditing.Header as StackPanel).Children[1] as TextBox).Text;

                // rename item accordingly 
                string destination = System.IO.Path.Combine((selectedForEditing.Parent as ExplorerTreeViewItem).FullPath, newValue);

                if (File.Exists(destination))
                {
                    return false;
                }

                // trying to change a script name?
                if (System.IO.Path.GetExtension(beforeEditingPath).ToLower().Equals(".cs"))
                {
                    if (!System.IO.Path.GetExtension(destination).ToLower().Equals(".cs"))
                    {
                        destination += ".cs";
                        newValue += ".cs";
                    }
                }

                // remove the text box
                (selectedForEditing.Header as StackPanel).Children.RemoveAt(1);
                (selectedForEditing.Header as StackPanel).Children.Add(new TextBlock() { Text = newValue, Margin = new Thickness(MARGIN, 0, 0, 0) });
                selectedForEditing.Text = newValue;

                // The path was changed?
                if (!(beforeEditingPath == destination))
                {
                    if (selectedForEditing.Tag.ToString().ToLower().Equals("directory") && Directory.Exists(beforeEditingPath) &&
                        !Directory.Exists(destination))
                    {
                        Directory.Move(beforeEditingPath, destination);

                        return true;
                    }
                    else if (!File.Exists(destination) && !selectedForEditing.Tag.ToString().ToLower().Equals("directory")) // tag = "file"
                    {
                        try
                        {
                            if (File.Exists(beforeEditingPath))
                            {
                                File.Move(beforeEditingPath, destination);

                                // if .csproj 
                                if (System.IO.Path.GetExtension(beforeEditingPath).ToLower().Equals(".csproj"))
                                    UserPreferences.Instance.ProjectCsProjFilePath = destination;

                                // if .sln 
                                if (System.IO.Path.GetExtension(beforeEditingPath).ToLower().Equals(".sln"))
                                    UserPreferences.Instance.ProjectSlnFilePath = destination;
                            }
                            else if (System.IO.Path.GetExtension(beforeEditingPath).ToLower().Equals(".cs"))
                            {
                                string script = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\Project Templates\Samples\DefaultComponent.cs");
                                script = script.Replace("NAME", destination.Remove(0, destination.LastIndexOf('\\') + 1).Replace(".cs", string.Empty));

                                File.WriteAllText(destination, script);
                            }

                            // script file? change the .csproj:
                            if (System.IO.Path.GetExtension(beforeEditingPath).ToLower().Equals(".cs"))
                            {
                                string _newValue = selectedForEditing.FullPath.Replace(SceneManager.GameProject.ProjectPath + "\\", "");
                                string _oldValue = beforeEditingPath.Replace(SceneManager.GameProject.ProjectPath + "\\", "");
                                //MessageBox.Show(_newValue + ";\n" + _oldValue);

                                AddScriptToSlnFile(_newValue, _oldValue);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error");
                        }
                    }

                    return true;
                }
            }

            return false;
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
            ExplorerTreeViewItem newnode = AddNode(null, System.IO.Path.GetFileName(path), 5, (ImageSource)FindResource("FolderIcon"));
            newnode.ContextMenu = directoryContextMenu;
            newnode.Tag = "directory";

            ExplorerTreeViewItem selected = (SelectedItem as ExplorerTreeViewItem);
            selected.Items.Add(newnode);
            selected.Items.Refresh();
            selected.IsExpanded = true;
        }

        private string CreateFile(string path, FileTemplate template, ImageSource imageSource = null)
        {
            if (this.SelectedItem == null) return string.Empty;

            path = FileHelper.CreateFile(path, template);

            // Add the new tree node to the TreeView
            TreeViewItem _node = AddNode(null, System.IO.Path.GetFileName(path), 10, imageSource);
            _node.Tag = "file";
            _node.ContextMenu = fileContextMenu;

            if (((SelectedItem as ExplorerTreeViewItem).Items.Count > 0 &&
                !((SelectedItem as ExplorerTreeViewItem).Items[0] as ExplorerTreeViewItem).Text.Equals("*")) ||
                (SelectedItem as ExplorerTreeViewItem).Items.Count == 0)
            {
                (SelectedItem as ExplorerTreeViewItem).Items.Add(_node);
            }

            (SelectedItem as ExplorerTreeViewItem).IsExpanded = true;

            return path;
        }

        public void CreateView()
        {
            treeView.Items.Clear();

            ExplorerTreeViewItem rootnode = AddNode(null, SceneManager.GameProject.ProjectPath, 0, (ImageSource)FindResource("FolderIcon"));

            rootnode.Tag = "directory";
            rootnode.PriorityIndex = 0;
            rootnode.ContextMenu = this.rootContextMenu;

            treeView.Items.Add(rootnode);

            //rootnode.Items.SortDescriptions.Clear();
            //rootnode.Items.SortDescriptions.Add(new SortDescription("PriorityIndex", ListSortDirection.Ascending));
            //rootnode.Items.SortDescriptions.Add(new SortDescription("Text", ListSortDirection.Ascending));

            FillChildNodes(rootnode);

            //rootnode.Items.Refresh();
            SortNode(rootnode);
            timer.Enabled = true;

            firstTick = true;
        }

        private void FillChildNodes(ExplorerTreeViewItem node)
        {
            try
            {
                DirectoryInfo[] dirs = new DirectoryInfo(node.FullPath).GetDirectories();

                // Add current directory Files:
                string[] files = Directory.GetFiles(node.FullPath);
                foreach (string file in files)
                {
                    if (AcceptedExtensions.Contains(System.IO.Path.GetExtension(file.ToLower())))
                    {
                        ExplorerTreeViewItem newNode = AddNode(node, System.IO.Path.GetFileName(file), 10, GetImageSource(System.IO.Path.GetExtension(file)));

                        newNode.Tag = "file";
                        newNode.ContextMenu = this.fileContextMenu;
                    }
                }

                node.Items.Refresh();

                // Add Sub Directories:
                foreach (DirectoryInfo dir in dirs)
                {
                    ExplorerTreeViewItem _node = AddNode(node, System.IO.Path.GetFileName(dir.Name), 5, (ImageSource)FindResource("FolderIcon"));
                    _node.ContextMenu = this.directoryContextMenu;
                    _node.Tag = "directory";

                    // Does the cur directory contain any files or sub directories?
                    if (Directory.GetFiles(dir.FullName).Count() > 0 ||
                        Directory.GetDirectories(dir.FullName).Count() > 0)
                    {
                        AddNode(_node, "*", 0);
                        _node.Expanded += Node_Expanded;

                    }

                    _node.Items.SortDescriptions.Clear();
                    _node.Items.SortDescriptions.Add(new SortDescription("PriorityIndex", ListSortDirection.Ascending));
                    _node.Items.SortDescriptions.Add(new SortDescription("Text", ListSortDirection.Ascending));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private ExplorerTreeViewItem AddNode(ExplorerTreeViewItem root, string text, int priorityIndex, ImageSource imageSource = null)
        {
            ExplorerTreeViewItem _node = new ExplorerTreeViewItem();
            _node.Style = (Style)FindResource("IgniteTreeViewItem");
            _node.PriorityIndex = priorityIndex;
            _node.Text = text;
            _node.MouseDoubleClick += newNode_MouseDoubleClick;



            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Horizontal;
            sp.Children.Add(new Image() { Source = imageSource });
            sp.Children.Add(new TextBlock() { Text = System.IO.Path.GetFileName(text), Margin = new Thickness(MARGIN, 0, 0, 0) });

            _node.Header = sp;

            if (root != null)
            {
                root.Items.Add(_node);
                root.Items.Refresh();
            }

            return _node;
        }


        private ImageSource GetImageSource(string extension)
        {
            switch (extension.ToLower())
            {
                case ".png":
                case ".jpeg":
                case ".jpg":
                case ".bmp":
                case ".gif":
                    return (ImageSource)FindResource("ImageIcon");
                case ".txt":
                case ".ini":
                    return (ImageSource)FindResource("TextFileIcon");
                //case ".gibbo":
                //    index = 4;
                //    break;
                case ".cs":
                    return (ImageSource)FindResource("CSFileIcon");
                case ".scene":
                    return (ImageSource)FindResource("SceneIcon");
                case ".sln":
                    return (ImageSource)FindResource("SolutionIcon");
                default:
                    return (ImageSource)FindResource("FileIcon");
            }
        }

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        EnvDTE80.DTE2 dte;
        Process sharpDevelop;
        // TODO behaviour opening .cs files and .sln
        private void HandleOpenFile(ExplorerTreeViewItem explorerTreeViewItem)
        {
            if (explorerTreeViewItem.CanDrag == false)
                return;

            // Optional: implement open file behaviour
            string extension = System.IO.Path.GetExtension(explorerTreeViewItem.Text).ToLower().Trim();

            // is directory?
            if (extension.Equals(string.Empty)) return;

            if (!AcceptedExtensions.Contains(extension)) return;

            switch (extension)
            {
                case ".gibbo":
                    //EditorHandler.ChangeSelectedObject(SceneManager.GameProject);
                    break;
                case ".scene":
                    //System.Threading.ThreadPool.QueueUserWorkItem(o => Gibbo.Library.SceneManager.LoadScene(node.FullPath));
                    if (SceneManager.ActiveScene != null && MessageBox.Show("Do you want to save the current scene?", "Warning", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        SceneManager.SaveActiveScene();
                    }

                    Gibbo.Library.SceneManager.LoadScene(explorerTreeViewItem.FullPath, true);
                    EditorHandler.ChangeSelectedObject(Gibbo.Library.SceneManager.ActiveScene);
                    EditorHandler.SelectedGameObjects = new List<GameObject>();
                    EditorHandler.SceneTreeView.CreateView();
                    //EditorHandler.EditorSplitterContainer.Panel2Collapsed = true;
                    EditorHandler.ChangeSelectedObjects();
                    break;
                case ".cs":
                    // TODO: add behaviour for opening .cs files
                    string projectPath = SceneManager.GameProject.ProjectPath;

                    // se o programa que, por defeito abre os ficheiros .cs n for o ddeexec abre com notepad ou algo do género : má solução
                    //var si = new System.Diagnostics.ProcessStartInfo { UseShellExecute = true, FileName = projectPath + @"\samples\SampleController.cs", Verb = "Open" };
                    //System.Diagnostics.Process.Start(si);

                    // funciona bastante bem da 1ª vez; pode ser complicado encontrar o caminho do startinfo.filename

                    //if (p == null || p.HasExited)
                    //    p = new Process();
                    //p.StartInfo.FileName = @"C:\Program Files (x86)\Microsoft Visual Studio 11.0\Common7\IDE\devenv.com";
                    //p.StartInfo.Arguments = projectPath + @"\Scripts.sln /command ""of " + explorerTreeViewItem.FullPath;
                    //p.Start();

                    //dte = (EnvDTE80.DTE2)System.Runtime.InteropServices.Marshal.GetActiveObject("VisualStudio.DTE.11.0");

                   // MessageBox.Show(@"C:\Program Files (x86)\SharpDevelop\4.3\bin\SharpDevelop.exe :" + SceneManager.GameProject.ProjectPath + "\\Scripts.sln" + " : " + explorerTreeViewItem.FullPath);
                    //C:\Program Files (x86)\SharpDevelop\4.3\bin\SharpDevelop.exe    
                    // C:\Program Files (x86)\Xamarin Studio\bin\XamarinStudio.exe
                   // if (sharpDevelop == null || sharpDevelop.HasExited)
                   // {
                   //     ProcessStartInfo pinfo = new ProcessStartInfo();
                   //     pinfo.FileName = @"C:\Program Files (x86)\Xamarin Studio\bin\XamarinStudio.exe";
                   //     pinfo.Arguments = "-nologo " + "Scripts.sln " + GibboHelper.MakeExclusiveRelativePath(SceneManager.GameProject.ProjectPath, explorerTreeViewItem.FullPath);
                   //     pinfo.WorkingDirectory = SceneManager.GameProject.ProjectPath;
                   //     //pinfo.RedirectStandardInput = true;
                   //     //pinfo.UseShellExecute = false;
                   //     sharpDevelop = Process.Start(pinfo);
                   // }
                   // else
                   // {
                   //     SetForegroundWindow(sharpDevelop.MainWindowHandle);
                   //     //sharpDevelop.

                   //     //sharpDevelop.StandardInput.WriteLine(GibboHelper.MakeExclusiveRelativePath(SceneManager.GameProject.ProjectPath, explorerTreeViewItem.FullPath));

                   //     //ProcessStartInfo pinfo = new ProcessStartInfo();
                   //     //pinfo.FileName = @"C:\Program Files (x86)\SharpDevelop\4.3\bin\SharpDevelop.exe";
                   //     //pinfo.Arguments = GibboHelper.MakeExclusiveRelativePath(SceneManager.GameProject.ProjectPath, explorerTreeViewItem.FullPath);
                   //     //sharpDevelop.StartInfo = pinfo;
                   //     //sharpDevelop.Start();
                   //     //using (StreamWriter sw = new StreamWriter(sharpDevelop.StandardInput))
                   //     //{
                   //     //    if (sw.BaseStream.CanWrite)
                   //     //    {
                   //     //        sw.WriteLine(GibboHelper.MakeExclusiveRelativePath(SceneManager.GameProject.ProjectPath, explorerTreeViewItem.FullPath));
                   //     //    }
                   //     //}
                   //}
                    //break;

                   // Process.Start(@"C:\Program Files (x86)\SharpDevelop\4.3\bin\SharpDevelop.exe " + SceneManager.GameProject.ProjectPath + "\\Scripts.sln");

                    if (Properties.Settings.Default.DefaultScriptEditor.ToLower().Equals("lime"))
                    {
                        LimeScriptEditor.OpenScript(explorerTreeViewItem.FullPath);
                    }
                    else if (EditorUtils.CheckVisualStudioExistance(Properties.Settings.Default.DefaultScriptEditor))
                    {
                        try
                        {
                            string rf = string.Empty;
                            string editor = Properties.Settings.Default.DefaultScriptEditor;
                            switch (editor)
                            {
                                case "VisualStudio2013":
                                    rf = "VisualStudio.DTE.12.0";
                                    break;
                                case "VisualStudio2012":
                                    rf = "VisualStudio.DTE.11.0";
                                    break;
                                case "VisualStudio2010":
                                    rf = "VisualStudio.DTE.10.0";
                                    break;
                                //case "CSExpress2010":
                                //    rf = "VCSExpress.DTE.10.0";
                                //    break;
                            }

                            if (dte == null || !dte.MainWindow.Visible)
                            {
                                Type type = Type.GetTypeFromProgID(rf);
                                dte = (EnvDTE80.DTE2)Activator.CreateInstance(type);
                                dte.MainWindow.Visible = true;
                                dte.Solution.Open(UserPreferences.Instance.ProjectSlnFilePath);
                            }

                            dte.Documents.Open(explorerTreeViewItem.FullPath);
                        }
                        catch (Exception)
                        {
                            Properties.Settings.Default.DefaultScriptEditor = "None";
                            Properties.Settings.Default.Save();

                            MessageBox.Show("You don't have selected a default scripting editor!\nEither select one on the Settings Window or open the scripts solution (.sln file) with other IDE to manage the scripts", "Error!");
                        }

                    }
                    else
                    {
                        MessageBox.Show("You don't have selected a default scripting editor.\nEither select one on the Settings Window or open the scripts solution (.sln file) with other IDE to manage the scripts", "Error!");
                    }
                    break;
                default:
                    // Default behaviour, tries to use the default user opening for this type of file:
                    try
                    {
                        Process.Start(explorerTreeViewItem.FullPath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    break;
            }
        }


        internal void BeginEditTextOnSelected()
        {
            ItemLostFocus();

            selectedForEditing = (SelectedItem as ExplorerTreeViewItem);

            if (selectedForEditing == null) return;

            TextBox tb = new TextBox();
            tb.Text = selectedForEditing.Text;
            tb.LostFocus += tb_LostFocus;
            tb.KeyDown += tb_KeyDown;
            tb.Focusable = true;

            beforeEditingPath = selectedForEditing.FullPath;
            (selectedForEditing.Header as StackPanel).Children.RemoveAt(1);
            (selectedForEditing.Header as StackPanel).Children.Add(tb);
            selectedForEditing.CanDrag = false;

            tb.Select(0, tb.Text.Length - (System.IO.Path.GetExtension(tb.Text).Length));
            tb.Focus();
        }

        private void AddScriptToSlnFile(string newValue, string oldValue)
        {
            string projectFileName = SceneManager.GameProject.ProjectPath + @"\Scripts.csproj";

            XmlDocument doc = new XmlDocument();
            doc.Load(projectFileName);

            XmlNode node = doc.CreateElement("Compile", doc.DocumentElement.NamespaceURI);
            XmlAttribute attribute = doc.CreateAttribute("Include");
            attribute.Value = newValue;
            node.Attributes.Append(attribute);

            while (doc.GetElementsByTagName("ItemGroup").Count < 2)
                doc.GetElementsByTagName("Project").Item(0).AppendChild(doc.CreateElement("ItemGroup"));

            bool replaced = false;
            if (oldValue != string.Empty)
            {
                foreach (XmlNode _node in doc.GetElementsByTagName("ItemGroup").Item(1).ChildNodes)
                {
                    if (_node.Name.ToLower().Equals("compile"))
                    {
                        //Console.WriteLine("val: " + _node.Attributes.GetNamedItem("Include").Value);
                        if (_node.Attributes.GetNamedItem("Include").Value.ToLower().Equals(oldValue.ToLower()))
                        {
                            _node.Attributes.GetNamedItem("Include").Value = newValue;
                            replaced = true;
                        }
                    }
                }
            }

            if (!replaced)
                doc.GetElementsByTagName("ItemGroup").Item(1).AppendChild(node);

            doc.Save(projectFileName);
        }

        #endregion

        #region Events

        void tb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (ItemLostFocus())
                    selectedForEditing.CanDrag = true;
            }
        }

        void Node_Expanded(object sender, RoutedEventArgs e)
        {
            ExplorerTreeViewItem item = (ExplorerTreeViewItem)sender;

            if (item.Items.Count > 0 && (item.Items[0] as ExplorerTreeViewItem).Text.Equals("*"))
            {
                item.Items.RemoveAt(0);

                FillChildNodes(item);
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (treeView.Items.Count > 0)
            {
                if (firstTick)
                {
                    (treeView.Items[0] as ExplorerTreeViewItem).IsExpanded = true;
                    firstTick = false;
                }

                try
                {
                    RealtimeFolderUpdater(treeView.Items[0] as ExplorerTreeViewItem);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("RealTime Project Sync Error: " + ex.Message);
                }
            }
        }

        private void addFromFolder_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "All Files (*.*)|*.*";
            ofd.Multiselect = true;

            var result = ofd.ShowDialog();

            if (result != true) return;

            ExplorerTreeViewItem selected = (SelectedItem as ExplorerTreeViewItem);

            foreach (string filePath in ofd.FileNames)
            {
                string target = selected.FullPath + "\\" + System.IO.Path.GetFileName(filePath);
                File.Copy(filePath, target, true);

                if (System.IO.Path.GetExtension(target).ToLower().Equals(".cs"))
                {
                    string _newValue = target.Replace(SceneManager.GameProject.ProjectPath + "\\", "");

                    AddScriptToSlnFile(_newValue, string.Empty);
                }
            }
        }

        private void copyRelativePathItem_Click(object sender, RoutedEventArgs e)
        {
            ExplorerTreeViewItem selected = (SelectedItem as ExplorerTreeViewItem);
            string relativePath = selected.FullPath.Replace(SceneManager.GameProject.ProjectPath + "\\", "");
            Clipboard.SetDataObject(relativePath);
        }

        void copyFullPath_Click(object sender, RoutedEventArgs e)
        {
            ExplorerTreeViewItem selected = (SelectedItem as ExplorerTreeViewItem);
            Clipboard.SetDataObject(selected.FullPath);
        }

        void renameItem_Click(object sender, RoutedEventArgs e)
        {
            BeginEditTextOnSelected();
        }

        void tb_LostFocus(object sender, RoutedEventArgs e)
        {
            ItemLostFocus();
            selectedForEditing.CanDrag = true;

            ExplorerTreeViewItem parent = (selectedForEditing.Parent as ExplorerTreeViewItem);
            SortNode(parent);
        }

        void SortNode(ExplorerTreeViewItem node)
        {
            node.Items.SortDescriptions.Clear();
            node.Items.SortDescriptions.Add(new SortDescription("PriorityIndex", ListSortDirection.Ascending));
            node.Items.SortDescriptions.Add(new SortDescription("Text", ListSortDirection.Ascending));
            node.Items.Refresh();
            node.IsExpanded = true;
        }


        void removeItem_Click(object sender, RoutedEventArgs e)
        {
            ExplorerTreeViewItem selected = (SelectedItem as ExplorerTreeViewItem);
            ExplorerTreeViewItem parent = (selected.Parent as ExplorerTreeViewItem);

            string path = selected.FullPath;

            if (selected == null || parent == null) return;

            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this folder?\n" + path, "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                EditorCommands.DeleteDirectoryRecursively(path);
                parent.Items.Remove(selected);
            }
        }

        void createFolderItem_Click(object sender, RoutedEventArgs e)
        {
            ExplorerTreeViewItem selected = (SelectedItem as ExplorerTreeViewItem);
            if (selected == null) return;

            string path = selected.FullPath + "\\New Folder";
            CreateDirectory(path);
        }

        void deleteItem_Click(object sender, RoutedEventArgs e)
        {
            //  Need 2 delete the file itself?
            // Need 2 refresh scene explorer?
            if (MessageBox.Show("Are you sure?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                // script file? change the .csproj:
                if (System.IO.Path.GetExtension((SelectedItem as ExplorerTreeViewItem).FullPath).ToLower().Equals(".cs"))
                {
                    string _toRemove = (SelectedItem as ExplorerTreeViewItem).FullPath.Replace(SceneManager.GameProject.ProjectPath + "\\", "");

                    string projectFileName = SceneManager.GameProject.ProjectPath + @"\Scripts.csproj";

                    XmlDocument doc = new XmlDocument();
                    doc.Load(projectFileName);

                    while (doc.GetElementsByTagName("ItemGroup").Count < 2)
                        doc.GetElementsByTagName("Project").Item(0).AppendChild(doc.CreateElement("ItemGroup"));

                    foreach (XmlNode _node in doc.GetElementsByTagName("ItemGroup").Item(1).ChildNodes)
                    {
                        if (_node.Name.ToLower().Equals("compile"))
                        {
                            if (_node.Attributes.GetNamedItem("Include").Value.ToLower().Equals(_toRemove.ToLower()))
                                doc.GetElementsByTagName("ItemGroup").Item(1).RemoveChild(_node);
                        }
                    }

                    doc.Save(projectFileName);
                }

                File.Delete((SelectedItem as ExplorerTreeViewItem).FullPath);
                ((SelectedItem as ExplorerTreeViewItem).Parent as ExplorerTreeViewItem).Items.Remove(SelectedItem);
            }
        }

        void treeView_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SelectOnClick(e);
        }

        void treeView_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            SelectOnClick(e);
        }

        void createSceneItem_Click(object sender, RoutedEventArgs e)
        {
            string fn = CreateFile((SelectedItem as ExplorerTreeViewItem).FullPath + "\\GameScene.scene", FileTemplate.None, (ImageSource)FindResource("SceneIcon"));
            Gibbo.Library.SceneManager.CreateScene(fn);
        }

        void treeView_OnDragDropSuccess(TreeViewItem source, TreeViewItem target, CancelEventArgs e)
        {
            string sourceType = (string)source.Tag;
            string targetType = (string)target.Tag;

            // the target node is not a directory?
            if (!targetType.Equals("directory"))
            {
                e.Cancel = true;
                return;
            }

            string targetPath = (target as ExplorerTreeViewItem).FullPath;
            if (sourceType.Equals("directory"))
            {
                targetPath += "\\" + (source as ExplorerTreeViewItem).Text;

                if (!Directory.Exists(targetPath))
                    Directory.CreateDirectory(targetPath);
            }

            if (sourceType.Equals("directory"))
            {
                try
                {
                    GibboHelper.CopyDirectory((source as ExplorerTreeViewItem).FullPath, targetPath, true);
                    Directory.Delete((source as ExplorerTreeViewItem).FullPath, true);
                }
                catch (Exception ex)
                {
                    e.Cancel = true;
                    MessageBox.Show("Error: " + ex.Message, "Error!");
                }
            }
            else
            {
                try
                {
                    File.Move((source as ExplorerTreeViewItem).FullPath, targetPath + "\\" + (source as ExplorerTreeViewItem).Text);
                }
                catch (Exception ex)
                {
                    e.Cancel = true;
                    MessageBox.Show("Error: " + ex.Message, "Error!");
                }
            }
        }

        void newNode_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            HandleOpenFile(sender as ExplorerTreeViewItem);
        }

        void openItem_Click(object sender, RoutedEventArgs e)
        {
            HandleOpenFile((SelectedItem as ExplorerTreeViewItem));
        }

        private void openFolderItem_Click(object sender, RoutedEventArgs e)
        {
            ExplorerTreeViewItem selected = (SelectedItem as ExplorerTreeViewItem);

            if (selected == null) return;

            ExplorerTreeViewItem parent = (selected.Parent as ExplorerTreeViewItem);
            string fullPath = string.Empty;

            if (parent == null || selected.Tag.ToString().ToLower().Trim() == "directory") // parent = root
                fullPath = selected.FullPath;
            else // selected = file
                fullPath = parent.FullPath;

            ProcessStartInfo runExplorer = new ProcessStartInfo();
            runExplorer.FileName = "explorer.exe";
            runExplorer.Arguments = fullPath;
            Process.Start(runExplorer);
        }

        void scriptItem_Click(object sender, RoutedEventArgs e)
        {
            string tname = "Script"; int c = 1;
            while (File.Exists((SelectedItem as ExplorerTreeViewItem).FullPath + "\\" + tname + ".cs"))
            {
                tname = "Script" + c;
                c++;
            }

            tname += ".cs";

            ExplorerTreeViewItem node = AddNode(SelectedItem as ExplorerTreeViewItem, tname, 10, (ImageSource)FindResource("CSFileIcon"));
            (SelectedItem as ExplorerTreeViewItem).IsExpanded = true;
            node.IsSelected = true;
            node.Tag = "file";
            node.ContextMenu = fileContextMenu;

            BeginEditTextOnSelected();
            beforeEditingPath = ".cs";
        }

        #endregion
    }
}
