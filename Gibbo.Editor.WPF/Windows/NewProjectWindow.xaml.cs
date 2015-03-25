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

using Gibbo.Library;
using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class NewProjectWindow : Window
    {
        public string ProjectPath { get; set; }

        private string defaultProjectsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Gibbo";


        /// <summary>
        /// New Project Window's Default Constructor
        /// </summary>
        public NewProjectWindow()
        {

            InitializeComponent();
            ProjectPath = string.Empty;
           
        }

        /// <summary>
        /// Sets path to the default one
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!Directory.Exists(defaultProjectsPath))
                Directory.CreateDirectory(defaultProjectsPath);

            this.pathTxt.Text = defaultProjectsPath;

            ToolTip tooltip = new ToolTip { Content = pathTxt.Text };
            this.pathBtn.ToolTip = tooltip;
        }

        /// <summary>
        /// Tries to create a project with the given information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createBtn_Click(object sender, RoutedEventArgs e)
        {
            // are the pre-requirements done?
            if (nameTxt.Text.Trim() != string.Empty && pathTxt.Text != string.Empty)
            {
                nameTxt.Text = nameTxt.Text.Trim();
                string path = pathTxt.Text + "\\" + nameTxt.Text;

                // does the project already exist?
                if (Directory.Exists(path))
                {
                    MessageBox.Show("There is already a project with that name, please choose another", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                GibboProject gp = new GibboProject(nameTxt.Text, pathTxt.Text);
                gp.Save();

                File.Copy("Project Templates\\Gibbo.Engine.Windows.exe", path + "\\Gibbo.Engine.Windows.exe", true);
                File.Copy("Project Templates\\GameProject.csproj", path + "\\Scripts.csproj", true);
                File.Copy("Project Templates\\settings.ini", path + "\\settings.ini", true);


                // Solution Preparations
                string slnFile = File.ReadAllText("Project Templates\\GameProject.sln");
                slnFile = slnFile.Replace("{%P_NAME%}", nameTxt.Text);

                // Solution Save
                File.WriteAllText(path + "\\Scripts.sln", slnFile);

                File.Copy("Gibbo.Library.dll", path + "\\Gibbo.Library.dll", true);               

                GibboHelper.CopyDirectory("Project Templates\\libs", path + "", true);
                GibboHelper.CopyDirectory("Project Templates\\samples", path + "\\samples", true);

                File.Copy("MonoGame.Framework.dll", path + "\\MonoGame.Framework.dll", true);
                File.Copy("OpenTK.dll", path + "\\OpenTK.dll", true);

                ProjectPath = gp.ProjectFilePath;
                DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Fill all the required fields please.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Closes the dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Opens a browser dialog, so the user can choose his project's folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pathBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            fbd.Description = "Select and Empty Directory";
            fbd.ShowNewFolderButton = true;

            if (pathTxt.Text == string.Empty)
                fbd.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            else
                fbd.SelectedPath = pathTxt.Text;

            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // is it an empty folder? 
                if (Directory.GetFiles(fbd.SelectedPath).Count() > 0)
                {
                    MessageBox.Show("Please select an empty directory.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                pathTxt.Text = fbd.SelectedPath;
            }
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void samplesBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Feature yet to implement.");
        }
    }
}
