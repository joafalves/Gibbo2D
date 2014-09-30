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

The license applies to all versions of the software, both newer and older than the one listed, unless a newer copy 
of the license is available, in which case the most recent copy of the license supercedes all others.

*/
#endregion

using Gibbo.Editor.Model;
using Gibbo.Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
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

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
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

                // Encryption Key
                string secretKey = Encryption.GenerateKey();
                // For additional security Pin the key.
                GCHandle gch = GCHandle.Alloc(secretKey, GCHandleType.Pinned);

                List<string> extensions = new List<string>(Properties.Settings.Default.AcceptedExtensions.Split('|'));
                extensions.AddRange(Properties.Settings.Default.secundaryExtension.Split('|'));

                if (GlobalCommands.DeployProject(Gibbo.Library.SceneManager.GameProject.ProjectPath, dialog.SelectedPath, selectedOption, secretKey, extensions))
                {
                    // store secret key if deployment was successful
                    //Console.WriteLine("key::" + Convert.ToBase64String(Encoding.Default.GetBytes(secretKey)));
                    //SceneManager.SecretKey = secretKey;

                    // saves encrypted private key inside the deployed folder
                    string fullPath = System.IO.Path.Combine(dialog.SelectedPath, "Data.dat");

                    FileStream fStream = new FileStream(fullPath, FileMode.OpenOrCreate);

                    // Encrypt a copy of the data to the stream; no need for entropy, hence sending null
                    Encryption.EncryptDataToStreamWithoutEntropy(Encoding.Default.GetBytes(secretKey), DataProtectionScope.CurrentUser, fStream);

                    fStream.Close();

                    // deployed with success!
                    if (System.Windows.Forms.MessageBox.Show("Deployed successfully!\n\nOpen output directory?", "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(dialog.SelectedPath);
                    }
                }

                // Remove the Key from memory. 
                Encryption.ZeroMemory(gch.AddrOfPinnedObject(), secretKey.Length * 2);
                gch.Free();

                Gibbo.Library.SceneManager.GameProject.Debug = previousDebugMode;
                Gibbo.Library.SceneManager.GameProject.Save();
            }
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
