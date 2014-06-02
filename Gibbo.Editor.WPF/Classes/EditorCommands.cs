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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gibbo.Library;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Xml;
using Gibbo.Editor.Model;
using System.Windows.Threading;

namespace Gibbo.Editor.WPF
{
    class EditorCommands
    {
        internal static void DeleteDirectoryRecursively(string path)
        {
            try
            {
                Directory.Delete(path, true);
            }
            catch (Exception ex)
            {
                Thread.Sleep(1);
                DeleteDirectoryRecursively(path);
            }
        }

        internal static void UpdatePropertyGrid()
        {
            if (EditorHandler.SelectedObjectPG != null)
            {
                EditorHandler.SelectedObjectPG.Dispatcher.Invoke((Action)(() =>
                {
                    EditorHandler.SelectedObjectPG.Update();
                }));
            }
        }

        static object lockPaste = new object();
        internal static void CopySelectedObjects()
        {
            lock (lockPaste)
            {
                Clipboard.Clear();

                foreach (var obj in EditorHandler.SelectedGameObjects)
                    obj.SaveComponentValues();

                if (EditorHandler.SelectedGameObjects != null && EditorHandler.SelectedGameObjects.Count > 0)
                    Clipboard.SetData("GameObjects", new List<GameObject>(EditorHandler.SelectedGameObjects));
            }
        }


        internal static void PasteSelectedObjects()
        {
            lock (lockPaste)
            {
                List<GameObject> list = (List<GameObject>)Clipboard.GetData("GameObjects");
                Clipboard.Clear();

                if (list == null || list.Count == 0) return;

                EditorHandler.SelectedGameObjects.Clear();
                // verificar se tem um pai
                foreach (var obj in list)
                {
                    var parent = obj.Transform.Parent;
                    while (parent != null && !list.Contains(parent.GameObject))
                        parent = parent.Parent;

                    if (parent != null && list.Contains(parent.GameObject))
                    {
                        parent.GameObject.Children.Add(obj);
                    }
                    else
                    {
                        var selected = EditorHandler.SceneTreeView.SelectedItem as DragDropTreeViewItem;
                        EditorHandler.SceneTreeView.AddGameObject(obj, string.Empty, false);
                    }

                    EditorHandler.SelectedGameObjects.Add(obj);
                }

                EditorHandler.ChangeSelectedObjects();
            }
        }

        internal static void CreatePropertyGridView()
        {
            EditorHandler.PropertyGridContainer.Children.Clear();

            if (EditorHandler.SelectedGameObjects.Count > 0)
            {
                PropertyBox properties;

                properties = new PropertyBox();

                properties.SelectedObject = EditorHandler.SelectedGameObjects[0] as GameObject;
                EditorHandler.SelectedObjectPG = properties.PropertyGrid;
                properties.ToggleExpand();

                EditorHandler.PropertyGridContainer.Children.Add(properties);

                for (int i = 0; i < EditorHandler.SelectedGameObjects.Count; i++)
                {
                    if (EditorHandler.SelectedGameObjects[i] is GameObject)
                    {
                        foreach (ObjectComponent component in EditorHandler.SelectedGameObjects[i].GetComponents())
                        {
                            properties = new PropertyBox();
                            properties.SelectedObject = component;
                            properties.Title.Content += " (Component)";

                            if (component.EditorExpanded)
                                properties.ToggleExpand();

                            EditorHandler.PropertyGridContainer.Children.Add(properties);
                        }
                    }
                }
            }
        }

        internal static void CheckPropertyGridConsistency()
        {
            for (int i = EditorHandler.PropertyGridContainer.Children.Count - 1; i >= 0; i--)
            {
                if ((EditorHandler.PropertyGridContainer.Children[i] as PropertyBox).SelectedObject is ObjectComponent)
                {
                    ObjectComponent component = (EditorHandler.PropertyGridContainer.Children[i] as PropertyBox).SelectedObject as ObjectComponent;

                    if (component.Transform.GameObject.GetComponents().Find(o => o == component) == null)
                        EditorHandler.PropertyGridContainer.Children.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Loads a saved project
        /// </summary>
        internal static bool LoadProject()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open project";
            ofd.Filter = @"(*.gibbo)|*.gibbo";

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                LoadProject(ofd.FileName);
                EditorCommands.AddToProjectHistory(ofd.FileName);
            }

            EditorCommands.ShowOutputMessage("Project loaded with success");

            return false;
        }

        internal static bool LoadProject(string filename)
        {
            try
            {
                if (File.Exists(filename))
                {
                    SceneManager.GameProject = GibboProject.Load(filename);

                    //File.Copy("Farseer Engine MonoGame OpenGL.dll", SceneManager.GameProject.ProjectPath + "\\Farseer Engine MonoGame OpenGL.dll", true);
                    File.Copy("Gibbo.Library.dll", SceneManager.GameProject.ProjectPath + "\\Gibbo.Library.dll", true);
                    File.Copy("Project Templates\\Gibbo.Library.xml", SceneManager.GameProject.ProjectPath + "\\Gibbo.Library.xml", true);
                    File.Copy("Project Templates\\Gibbo.Engine.Windows.exe", SceneManager.GameProject.ProjectPath + "\\Gibbo.Engine.Windows.exe", true);
                    GibboHelper.CopyDirectory("Project Templates\\libs", SceneManager.GameProject.ProjectPath + "", true);

                    // load user settings: 
                    if (!File.Exists(SceneManager.GameProject.ProjectPath + "\\_userPrefs.pgb"))
                    {
                        UserPreferences.Instance = new UserPreferences();
                        GibboHelper.SerializeObject(SceneManager.GameProject.ProjectPath + "\\_userPrefs.pgb", UserPreferences.Instance);
                    }
                    else
                    {
                        UserPreferences.Instance = GibboHelper.DeserializeObject(SceneManager.GameProject.ProjectPath + "\\_userPrefs.pgb") as Model.UserPreferences;
                    }

                    SceneManager.ActiveScene = null;
                    EditorHandler.SelectedGameObjects.Clear();
                    EditorHandler.ChangeSelectedObjects();
                    EditorHandler.SceneTreeView.CreateView();
                    EditorHandler.ProjectTreeView.CreateView();

                    CompilerWindow cf = new CompilerWindow();
                    cf.ShowDialog();
                    bool success = cf.Result;

                    Reload();

                    if (success)
                    {
                        LoadLastScene();

                        EditorCommands.ShowOutputMessage("Project loaded with success");

                        //kryptonNavigator1.SelectedIndex = 1;
                    }
                    else
                    {
                        EditorCommands.ShowOutputMessage("Project loaded with script errors");
                    }

                    EditorHandler.Settings = new IniFile(SceneManager.GameProject.ProjectPath + "\\settings.ini");

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid File\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        internal static void Reload()
        {
            SceneManager.Content = new Microsoft.Xna.Framework.Content.ContentManager(EditorHandler.SceneViewControl.Services, "Content"); //SceneManager.GameProject.ProjectPath);
            SceneManager.ActiveScene = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal static bool LoadLastScene()
        {
            if (SceneManager.GameProject == null) return false;

            string path = SceneManager.GameProject.ProjectPath + "\\" + SceneManager.GameProject.EditorSettings.LastOpenScenePath;

            if (!path.Trim().Equals(string.Empty) && File.Exists(path))
            {
                SceneManager.LoadScene(path, true);
                EditorHandler.SceneTreeView.CreateView();

                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        internal static void ShowOutputMessage(string message)
        {
            EditorHandler.OutputMessages.Add(new OutputMessage() { Time = DateTime.Now.ToString("HH:mm:ss").ToString(), Message = message.ToString() });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        internal static void AddToProjectHistory(string path)
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
        internal static void SaveScene(bool saveAs)
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
        internal static void SaveProject()
        {
            if (SceneManager.GameProject != null)
            {
                // Save project in a different thread
                Thread saveThread = new Thread(() =>
                {
                    SceneManager.GameProject.Save();
                    if (UserPreferences.Instance != null)
                        GibboHelper.SerializeObject(SceneManager.GameProject.ProjectPath + "\\_userPrefs.pgb", UserPreferences.Instance);
                });

                EditorCommands.ShowOutputMessage("Project Saved");
                saveThread.Start();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        internal static void DebugGame()
        {
            if (SceneManager.ActiveScene == null)
            {
                MessageBox.Show("Ups!\n\nThere is no scene loaded", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SceneManager.GameProject.SceneStartPath = GibboHelper.MakeExclusiveRelativePath(SceneManager.GameProject.ProjectPath, SceneManager.ActiveScenePath);

            SaveProject();
            SaveScene(false);

            if (File.Exists(SceneManager.GameProject.ProjectPath + "\\Gibbo.Engine.Windows.exe"))
            {
                if (File.Exists(SceneManager.GameProject.ProjectPath + "\\libs\\Scripts.dll"))
                    File.Delete(SceneManager.GameProject.ProjectPath + "\\libs\\Scripts.dll");

                if (!Directory.Exists(SceneManager.GameProject.ProjectPath + "\\libs"))
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
                    CompilerWindow cf = new CompilerWindow();
                    cf.ShowDialog();

                    if (cf.DialogResult.Value) return;

                    // Update path
                    dllpath = SceneManager.GameProject.ProjectPath + "\\bin\\" + (SceneManager.GameProject.Debug ? "Debug" : "Release") + "\\" + SceneManager.ScriptsAssembly.GetName().Name + ".dll";
                }

                File.Copy(dllpath, SceneManager.GameProject.ProjectPath + "\\libs\\Scripts.dll", true);

                try
                {
                    Process debug = new Process();
                    debug.StartInfo.WorkingDirectory = SceneManager.GameProject.ProjectPath;
                    debug.StartInfo.FileName = SceneManager.GameProject.ProjectPath + "\\Gibbo.Engine.Windows.exe";
                    debug.StartInfo.Arguments = "";
                    debug.StartInfo.CreateNoWindow = true;
                    debug.Start();
                    debug.WaitForExit();
                    debug.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Ups!\n\nIt seems there is no engine set up for your game!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Apply Blur Effect on the window
        /// </summary>
        /// <param name=”win”></param>
        internal static void ApplyBlurEffect(System.Windows.Window win)
        {
            System.Windows.Media.Effects.BlurEffect objBlur = new System.Windows.Media.Effects.BlurEffect();
            objBlur.Radius = 4;
            win.Effect = objBlur;
        }

        /// <summary>
        /// Remove Blur Effects
        /// </summary>
        /// <param name=”win”></param>
        internal static void ClearEffect(System.Windows.Window win)
        {
            win.Effect = null;
        }
    }
}
