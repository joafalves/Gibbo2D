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
using Gibbo.Editor.Model;
using Gibbo.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for CompilerForm.xaml
    /// </summary>
    public partial class CompilerWindow : Window
    {
        public bool Result { get; set; }

        public CompilerWindow()
        {
            InitializeComponent();
            ErrorDataGrid.Visibility = Visibility.Collapsed;
            
            ErrorDataGrid.MouseDoubleClick += ErrorDataGrid_MouseDoubleClick;
        }

        void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                
                DataGridRow row = sender as DataGridRow;
                if (row != null)
                { 
                    ErrorLog item = row.Item as ErrorLog;
                    LimeScriptEditor.Instance.OpenScriptAndSeek(SceneManager.GameProject.ProjectPath + @"\" + item.FileName, item.LineNumber, item.ColumnNumber);
                    this.Close();
                }
            }
        }

        void ErrorDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //if (sender != null)
            //{
            //    DataGrid grid = sender as DataGrid;
            //    if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
            //    {
            //        //DataGridRow dgr = grid.ItemContainerGenerator.ContainerFromItem(grid.SelectedItem) as DataGridRow;
            //        ErrorLog item = grid.SelectedItem as ErrorLog;

            //        LimeScriptEditor.Instance.OpenScriptAndSeek(SceneManager.GameProject.ProjectPath + @"\" + item.FileName, item.LineNumber, item.ColumnNumber);
            //    }
            //}
        }

        public void SafeClose()
        {
            // Make sure we're running on the UI thread
            //if (this.InvokeRequired)
            //{
            //    BeginInvoke(new Action(SafeClose));
            //    return;
            //}

            if (this.Dispatcher.CheckAccess())
            {
                this.Dispatcher.BeginInvoke(new Action(SafeClose));
                return;
            }

            //// Close the form now that we're running on the UI thread
            //this.DialogResult = System.Windows.Forms.DialogResult.Yes;
            DialogResult = true;
            Close();

        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            if (ScriptsBuilder.ReloadScripts())
            {
                // Sucess:
                this.progresslbl.Content = "Loading...";

                System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
                timer.Interval = 5;
                timer.Tick += timer_Tick;
                timer.Enabled = true;

                Result = true;
            }
            else
            {
                progressBar.Value = 80;
                this.progresslbl.Content = "Failure!";
                Result = false;
                EditorCommands.ShowOutputMessage("Error while compiling the scripts");

                ErrorDataGrid.Visibility = Visibility.Visible;
                ErrorDataGrid.ItemsSource = ScriptsBuilder.Logger.Errors;
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            progressBar.Value += GibboHelper.RandomNumber(4, 5) ;

            if (progressBar.Value >= 100)
            {
                this.progresslbl.Content = "Success!";
                (sender as System.Windows.Forms.Timer).Enabled = false;
                this.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.progresslbl.Content = "Gathering data and setting parameters to compile...";
        }
    }


}
