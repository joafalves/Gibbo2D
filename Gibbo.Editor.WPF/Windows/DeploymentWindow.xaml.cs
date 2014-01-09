using Gibbo.Editor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Gibbo.Editor.WPF
{
    /// <summary>
    /// Interaction logic for DeploymentWindow.xaml
    /// </summary>
    public partial class DeploymentWindow : Window
    {
        string selectedOption = "Windows";

        public DeploymentWindow()
        {
            InitializeComponent();
        }

        private void DeploymentBtn_Checked_1(object sender, RoutedEventArgs e)
        {
            foreach (RoundedButtonToggle item in EditorUtils.FindVisualChildren<RoundedButtonToggle>(ContainersDockPanel))
            {
                if (item != (sender as RoundedButtonToggle) && item.IsChecked == true)
                    item.IsChecked = false;
            }

            selectedOption = (sender as RoundedButtonToggle).Tag as string;
        }

        private void DeployBtn_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog()
            {
                ShowNewFolderButton = true,
                Description = "Select a destination folder"
            };

            if (Properties.Settings.Default.LastDeploymentFolder != string.Empty)
                dialog.SelectedPath = Properties.Settings.Default.LastDeploymentFolder;

            dialog.ShowDialog();

            // path is fine?
            if (dialog.SelectedPath == Gibbo.Library.SceneManager.GameProject.ProjectPath)
            {
                System.Windows.Forms.MessageBox.Show("You cannot select the folder of your project.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Properties.Settings.Default.LastDeploymentFolder = dialog.SelectedPath;
            Properties.Settings.Default.Save();

            bool previousDebugMode = Gibbo.Library.SceneManager.GameProject.Debug;
            Gibbo.Library.SceneManager.GameProject.Debug = false;
            Gibbo.Library.SceneManager.GameProject.Save();

            if (GlobalCommands.DeployProject(Gibbo.Library.SceneManager.GameProject.ProjectPath, dialog.SelectedPath, selectedOption))
            {
                // deployed with success!
                if (System.Windows.Forms.MessageBox.Show("Deployed successfully!\n\nOpen output directory?", "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(dialog.SelectedPath);
                }
            }

            Gibbo.Library.SceneManager.GameProject.Debug = previousDebugMode;
            Gibbo.Library.SceneManager.GameProject.Save();
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
