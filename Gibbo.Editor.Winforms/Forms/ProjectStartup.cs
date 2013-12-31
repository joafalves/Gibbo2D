using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using System.IO;

namespace Gibbo.Editor.Forms
{
    public partial class ProjectStartup : KryptonForm
    {
        #region fields

        #endregion

        #region constructors

        public ProjectStartup()
        {
            InitializeComponent();

            if (AppDomain.CurrentDomain.SetupInformation.ActivationArguments != null)
            {
                string[] activationData = AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationData;
                if (activationData != null && activationData.Length > 0)
                {
                    Uri uri = new Uri(activationData[0]);
                    string path = uri.LocalPath.ToString();

                    if (File.Exists(path))
                        StartEditor(path);
                }
            }
        }

        #endregion

        #region methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectPath"></param>
        private void StartEditor(string projectPath)
        {
            this.Hide();
            new Editor(projectPath).ShowDialog();
            this.Close();
        }

        #endregion

        #region events

        private void ProjectStartup_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.LastLoadedProjects.Trim() != string.Empty)
            {
                string[] splitted = Properties.Settings.Default.LastLoadedProjects.Split('|');
                foreach (string split in splitted)
                {
                    if (split.Trim() != string.Empty && File.Exists(split))
                    {
                        KryptonListItem item = new KryptonListItem(Path.GetDirectoryName(split));
                        item.Tag = split;

                        listBox.Items.Add(item);
                    }
                }
            }
        }

        private void kryptonButton4_Click(object sender, EventArgs e)
        {
            if (listBox.SelectedItem != null)
            {
                string path = (string)((KryptonListItem)listBox.SelectedItem).Tag;

                EditorCommands.AddToProjectHistory(path);
                StartEditor(path);
            }
            else
            {
                KryptonMessageBox.Show("No project selected!\nPlease select from the list, browser or create a new project..", "Ups", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open project";
            ofd.Filter = @"(*.gibbo)|*.gibbo";

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                EditorCommands.AddToProjectHistory(ofd.FileName);
                StartEditor(ofd.FileName);
            }
        }

        private void listBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBox.SelectedItem != null)
            {
                string path = (string)((KryptonListItem)listBox.SelectedItem).Tag;

                EditorCommands.AddToProjectHistory(path);
                StartEditor(path);
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            NewProject np = new NewProject();

            // A project was created?
            if (np.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
            {
                EditorCommands.AddToProjectHistory(np.ProjectPath);
                StartEditor(np.ProjectPath);
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.LastLoadedProjects = string.Empty;
            Properties.Settings.Default.Save();

            listBox.Items.Clear();
        }

        #endregion+
    }
}
