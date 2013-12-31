using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gibbo.Library;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;
using ComponentFactory.Krypton.Toolkit;

namespace Gibbo.Editor
{
    class EditorCommands
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public static void ShowOutputMessage(string message)
        {
            EditorHandler.OutputWindow.kryptonDataGridView.Rows.Insert(0, DateTime.Now, message);
            EditorHandler.StatusLabel.Text = DateTime.Now.ToString("HH:mm:ss") + " : " + message;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public static void AddToProjectHistory(string path)
        {
            if (Properties.Settings.Default.LastLoadedProjects.Contains(path))
            {
                Properties.Settings.Default.LastLoadedProjects = Properties.Settings.Default.LastLoadedProjects.Replace(path + "|", string.Empty);
            }

            Properties.Settings.Default.LastLoadedProjects = path + "|" + Properties.Settings.Default.LastLoadedProjects;
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// 
        /// </summary>
        public static void SaveScene(bool saveAs)
        {
            if (SceneManager.ActiveScene == null) return;

            if (!File.Exists(SceneManager.ActiveScenePath))
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.InitialDirectory = SceneManager.GameProject.ProjectPath;
                sfd.Filter = "(*.scene)|*.scene";
                DialogResult dr = sfd.ShowDialog();

                if (dr == DialogResult.Yes || dr == DialogResult.OK)
                {
                    SceneManager.ActiveScenePath = sfd.FileName;
                }
                else
                {
                    return;
                }
            }

            if (saveAs)
            {
                // TODO: implement "Save As" funcionality

            }
            else
            {
                SceneManager.ActiveScene.SaveComponentValues();
                SceneManager.SaveActiveScene();
            }
        }

        /// <summary>
        /// Saves the current project using a different thread
        /// </summary>
        public static void SaveProject()
        {
            if (SceneManager.GameProject != null)
            {
                // Save project in a different thread
                Thread saveThread = new Thread(() =>
                {
                    SceneManager.GameProject.Save();
                   
                });
                EditorCommands.ShowOutputMessage("Project Saved");
                saveThread.Start();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static void DebugGame()
        {
            if (SceneManager.ActiveScene == null)
            {
                KryptonMessageBox.Show("Ups!\n\nThere is no scene loaded", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SceneManager.GameProject.SceneStartPath = GibboHelper.MakeExclusiveRelativePath(SceneManager.GameProject.ProjectPath, SceneManager.ActiveScenePath);

            SaveProject();
            SaveScene(false);

            if (File.Exists(SceneManager.GameProject.ProjectPath + "\\Gibbo.Engine.Windows.exe"))
            {
                if(File.Exists(SceneManager.GameProject.ProjectPath + "\\libs\\Scripts.dll"))
                    File.Delete(SceneManager.GameProject.ProjectPath + "\\libs\\Scripts.dll");

                if(!Directory.Exists(SceneManager.GameProject.ProjectPath + "\\libs"))
                    Directory.CreateDirectory(SceneManager.GameProject.ProjectPath + "\\libs");

                // Compile scripts:
                // old (depracated): CompileScripts(false);
                //if (SceneManager.ScriptsAssembly != null)
                //{
                    string dllpath = SceneManager.GameProject.ProjectPath + "\\bin\\" + (SceneManager.GameProject.Debug ? "Debug" : "Release") + "\\" + SceneManager.ScriptsAssembly.GetName().Name + ".dll";
                    
                    // The scripts .dll exists?
                    if (!File.Exists(dllpath))
                    {
                        // Compile scripts
                        CompilerForm cf = new CompilerForm();
                        if(cf.ShowDialog() != DialogResult.Yes) return;

                        // Update path
                        dllpath = SceneManager.GameProject.ProjectPath + "\\bin\\" + (SceneManager.GameProject.Debug ? "Debug" : "Release") + "\\" + SceneManager.ScriptsAssembly.GetName().Name + ".dll";
                    }

                    File.Copy(dllpath, SceneManager.GameProject.ProjectPath + "\\libs\\Scripts.dll", true);
                //}

                Process debug = new Process();
                debug.StartInfo.WorkingDirectory = SceneManager.GameProject.ProjectPath;
                debug.StartInfo.FileName = SceneManager.GameProject.ProjectPath + "\\Gibbo.Engine.Windows.exe";
                debug.StartInfo.CreateNoWindow = true;
                debug.Start();
                debug.WaitForExit();
                debug.Close();
            }
            else
            {
                KryptonMessageBox.Show("Ups!\n\nIt seems there is no engine set up for your game!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
