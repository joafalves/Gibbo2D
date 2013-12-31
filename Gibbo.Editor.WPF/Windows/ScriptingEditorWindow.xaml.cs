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
using ICSharpCode.AvalonEdit;
using System.IO;
using ICSharpCode.AvalonEdit.Folding;
using ICSharpCode.AvalonEdit.Highlighting;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.InteropServices;
using ICSharpCode.AvalonEdit.Document;
using Gibbo.Editor.Model;
using System.Collections.ObjectModel;
using Gibbo.Library;

namespace Gibbo.Editor.WPF
{
    /// <summary>
    /// Interaction logic for ScriptingEditorWindow.xaml
    /// </summary>
    public partial class ScriptingEditorWindow : Window
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool FlashWindowEx(ref FLASHWINFO pwfi);

        [StructLayout(LayoutKind.Sequential)]
        public struct FLASHWINFO
        {
            public UInt32 cbSize;
            public IntPtr hwnd;
            public UInt32 dwFlags;
            public UInt32 uCount;
            public UInt32 dwTimeout;
        }

        public const UInt32 FLASHW_ALL = 3;
        public const UInt32 FLASHW_TIMER = 4;
        public const UInt32 FLASHW_TRAY = 2; 

        private System.Windows.Forms.Timer timer;

        private int lineToSelect = 0, columnToSelect = 0;
        private bool nextOpenTabSelect;


        #region properties

        private TextEditor ActiveEditor
        {
            get
            {
                return (((tabControl.SelectedItem as TabItem).Content as TextEditor));
            }
        }

        #endregion

        #region constructors

        public ScriptingEditorWindow()
        {
            InitializeComponent();
            this.ContentRendered += new EventHandler(ScriptingEditorWindow_ContentRendered);
        }

        #endregion

        #region methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns>The index of the tab item that contains the document.</returns>
        private int FileIsOpen(string path)
        {
            for (int i = 0; i < tabControl.Items.Count; i++)
            {
                if ((tabControl.Items[i] as TabItem).Tag.ToString().ToLower().Equals(path.ToLower()))
                    return i;
            }

            return -1;
        }

        private void CloseCurrent()
        {
            TabItem tab = tabControl.SelectedItem as TabItem;

            if (tab.Header.ToString().EndsWith("*"))
            {
                MessageBoxResult result = MessageBox.Show("This file is not saved. Do you want to close?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
                if (result == MessageBoxResult.No)
                {
                    // Fuck this shit, I don't wan't to stay on this method anymore
                    return;
                }
            }

            tabControl.Items.Remove(tab);

            // no more files open?
            if (tabControl.Items.Count == 0) this.Close();
        }

        private void Compile()
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                if (!ScriptsBuilder.ReloadScripts())
                {
                    ErrorDataGrid.ItemsSource = ScriptsBuilder.Logger.Errors;
                    if (ErrorDataGrid.Items.Count > 0)
                        ErrorsExpander.IsExpanded = true;
                }
                else
                {
                    ErrorDataGrid.ItemsSource = null;
                    EditorCommands.CreatePropertyGridView();
                }
            }));     
        }

        private void SaveCurrent()
        {
            SaveByTabIndex(tabControl.SelectedIndex);

            Compile();
        }

        private void SaveAll()
        {
            for (int i = 0; i < tabControl.Items.Count; i++)
            {
                SaveByTabIndex(i);
            }

            Compile();
        }

        private void SaveByTabIndex(int index)
        {
            try
            {
                TabItem tab = tabControl.Items[index] as TabItem;
                string path = tab.Tag.ToString();

                File.WriteAllText(path, (tab.Content as TextEditor).Text);

                tab.Header = System.IO.Path.GetFileNameWithoutExtension(path);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void OpenScriptAndSeek(string path, int line, int column)
        {
            nextOpenTabSelect = true;
            lineToSelect = line;
            columnToSelect = column;

            if (!OpenScript(path))
            {
                SelectText();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns>true when a new tab is created</returns>
        public bool OpenScript(string path)
        {
            int srcFileOpen = FileIsOpen(path);

            if (srcFileOpen == -1) // not found
            {
                FoldingManager foldingManager;
                AbstractFoldingStrategy foldingStrategy;
                foldingStrategy = new BraceFoldingStrategy();

                TextEditor textEditor = new TextEditor();
                textEditor.TextArea.IndentationStrategy = new ICSharpCode.AvalonEdit.Indentation.CSharp.CSharpIndentationStrategy(textEditor.Options);
                textEditor.Foreground = FindResource("ForegroundGray") as Brush;
                textEditor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("C#");
                textEditor.ScrollToEnd();
                textEditor.FontFamily = new FontFamily("Consolas");
                textEditor.FontSize = 11;
                textEditor.ShowLineNumbers = true;
                textEditor.Text = File.ReadAllText(path, Encoding.UTF8);
                textEditor.Tag = path; // stores the file path in memory
                textEditor.TextChanged += textEditor_TextChanged;
                textEditor.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
                textEditor.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;

                foldingManager = FoldingManager.Install(textEditor.TextArea);
                foldingStrategy.UpdateFoldings(foldingManager, textEditor.Document);

                TabItem tabItem = new TabItem();
                tabItem.Style = FindResource("TabItemStyle") as Style;
                tabItem.Foreground = FindResource("ForegroundGray") as Brush;
                tabItem.Header = System.IO.Path.GetFileNameWithoutExtension(path);
                tabItem.Content = textEditor;
                tabItem.Tag = path; // stores the file path in memory
                tabItem.Loaded += tabItem_Loaded;

                tabControl.Items.Add(tabItem);

                tabControl.SelectedIndex = tabControl.Items.Count - 1;

                return true;
            }
            else
            {
                tabControl.SelectedIndex = srcFileOpen;
 
                this.Activate();

                FLASHWINFO pf = new FLASHWINFO();

                pf.cbSize = Convert.ToUInt32(Marshal.SizeOf(pf));
                pf.hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle; 
                pf.dwFlags = FLASHW_TIMER | FLASHW_TRAY; // (or FLASHW_ALL to flash and if it is not minimized)
                pf.uCount = 8;
                pf.dwTimeout = 75;

                FlashWindowEx(ref pf);

                return false;
            }
        }

        private void SelectText()
        {
            ActiveEditor.SelectionLength = 0;
            ActiveEditor.ScrollTo(lineToSelect, columnToSelect);
            ActiveEditor.TextArea.Caret.BringCaretToView();
            ActiveEditor.TextArea.Caret.Location = new TextLocation(lineToSelect, columnToSelect);
            ActiveEditor.SelectionLength = 1;
        }

        #endregion

        #region events

        void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGridRow row = sender as DataGridRow;
                if (row != null)
                {
                    ErrorLog item = row.Item as ErrorLog;
                    OpenScriptAndSeek(SceneManager.GameProject.ProjectPath + @"\" + item.FileName, item.LineNumber, item.ColumnNumber);
                }
            }
        }

        void tabItem_Loaded(object sender, RoutedEventArgs e)
        {
            this.Focus();
            this.Activate();

            if (nextOpenTabSelect)
            {
                nextOpenTabSelect = false;
                SelectText();
            }
        }

        void ScriptingEditorWindow_ContentRendered(object sender, EventArgs e)
        {
            this.Activate();

            Compile();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            SaveCurrent();
        }

        private void saveAllBtn_Click(object sender, RoutedEventArgs e)
        {
            SaveAll();
        }

        private void cutBtn_Click(object sender, RoutedEventArgs e)
        {
            ActiveEditor.Cut();
        }

        private void copyBtn_Click(object sender, RoutedEventArgs e)
        {
            ActiveEditor.Copy();
        }

        private void pasteBtn_Click(object sender, RoutedEventArgs e)
        {
            ActiveEditor.Paste();
        }

        private void undoBtn_Click(object sender, RoutedEventArgs e)
        {
            ActiveEditor.Undo();
        }

        private void redoBtn_Click(object sender, RoutedEventArgs e)
        {
            ActiveEditor.Redo();
        }

        void textEditor_TextChanged(object sender, EventArgs e)
        {
            TabItem selectedTab = tabControl.SelectedItem as TabItem;
            if (!selectedTab.Header.ToString().EndsWith("*"))
                selectedTab.Header += " *";
        }

        //private string LastWord()
        //{
        //    int pos = ActiveEditor.SelectionStart - 1;
        //    string word = "";

        //    if ( pos > 1 )
        //    {
				
        //        string tmp = "";
        //        char f = new char();
        //        while ( f != ' ' && f != 10 && pos > 0 )
        //        {
        //            pos--;
        //            tmp = this.ActiveEditor.Text.Substring(pos, 1);
        //            f = (char) tmp[0];
        //            word += f;	
        //        }

        //        char[] ca = word.ToCharArray();
        //        Array.Reverse( ca );
        //        word = new String( ca );
        //    }

        //    return word.Trim();
        //}

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            for (int i = 0; i < tabControl.Items.Count; i++)
            {
                if ((tabControl.Items[i] as TabItem).Header.ToString().EndsWith("*"))
                {
                    MessageBoxResult result = MessageBox.Show("You have unsaved files on the editor. Do you want to save them before leaving?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
                    if (result == MessageBoxResult.Yes)
                    {
                        SaveAll();
                    }

                    break;
                }
            }
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            CloseCurrent();
        }

        private void SaveCommand(object sender, ExecutedRoutedEventArgs e)
        {
            SaveCurrent();
        }

        private void Expander_Collapsed(object sender, RoutedEventArgs e)
        {
            LimeGrid.RowDefinitions[3].Height = new GridLength(0);
        }

        private void Expander_Expanded_1(object sender, RoutedEventArgs e)
        {
            LimeGrid.RowDefinitions[3].Height = new GridLength(100);
        }

        #endregion

        
    }
}
