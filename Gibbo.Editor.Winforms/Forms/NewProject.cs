using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Gibbo.Library;
using ComponentFactory.Krypton.Toolkit;

namespace Gibbo.Editor
{
    public partial class NewProject : KryptonForm
    {
        public string ProjectPath { get; set; }

        private string defaultProjectsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Gibbo";

        /// <summary>
        /// 
        /// </summary>
        public NewProject()
        {
            InitializeComponent();
            ProjectPath = string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewProject_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(defaultProjectsPath))
                Directory.CreateDirectory(defaultProjectsPath);
            
            pathTxt.Text = defaultProjectsPath;
        }

        /// <summary>
        /// Tries to create a project with the given information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createBtn_Click(object sender, EventArgs e)
        {
            // the pre-requisites are done?
            if (nameTxt.Text.Trim() != string.Empty && pathTxt.Text != string.Empty)
            {
                nameTxt.Text = nameTxt.Text.Trim();
                string path = pathTxt.Text + "\\" + nameTxt.Text;

                // The project already exists?
                if (Directory.Exists(path))
                {
                    KryptonMessageBox.Show("There is already a project with that name, please choose another", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                GibboProject gp = new GibboProject(nameTxt.Text, pathTxt.Text);
                gp.Save();

                //SceneManager.GameProject = new GibboProject(nameTxt.Text, pathTxt.Text);
                //SceneManager.GameProject.Save();

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

                ProjectPath = gp.ProjectFilePath;
                this.DialogResult = System.Windows.Forms.DialogResult.Yes;
                this.Close();
            }
            else
            {
                KryptonMessageBox.Show("Fill all the required fields please.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Gives the user a UI wich he can select a folder for his project
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void findPathBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Select and Empty Directory";
            fbd.ShowNewFolderButton = true;

            if (pathTxt.Text == string.Empty)
                fbd.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            else
                fbd.SelectedPath = pathTxt.Text;
  
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // is an empty folder? 
                if (Directory.GetFiles(fbd.SelectedPath).Count() > 0)
                {
                    KryptonMessageBox.Show("Please select an empty directory.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                pathTxt.Text = fbd.SelectedPath;
                this.toolTip.SetToolTip(this.pathTxt, pathTxt.Text);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
