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
using System.ComponentModel;
using Gibbo.Library;
using System.Diagnostics;

namespace Gibbo.Editor.WPF
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private IniFile iniSettings = new IniFile(SceneManager.GameProject.ProjectPath + "\\settings.ini");

        public SettingsWindow()
        {
            InitializeComponent();
            propertyGrid.Tag = "gibbo_general";
            propertyGrid.SelectedObject = LoadProperties("gibbo_general");
        }

        private ISettingsChannelA LoadProperties(string _ref)
        {
            ISettingsChannelA settings = null;

            switch (_ref)
            {
                case "gibbo_general":
                    settings = new GibboGeneralSettingsDynamic();
                    (settings as GibboGeneralSettingsDynamic).AutomaticProjectLoad = Properties.Settings.Default.LoadLastProject;
                    try
                    {
                        (settings as GibboGeneralSettingsDynamic).ScriptEditors = (GibboGeneralSettingsDynamic.ScriptingEditors)Enum.Parse(typeof(GibboGeneralSettingsDynamic.ScriptingEditors), Properties.Settings.Default.DefaultScriptEditor, true);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    (settings as GibboGeneralSettingsDynamic).StartOnFullScreen = Properties.Settings.Default.StartOnFullScreen;
                    (settings as GibboGeneralSettingsDynamic).ShowDebugView = Properties.Settings.Default.ShowDebugView;
                    break;

                case "gibbo_tileset":
                    settings = new GibboTilesetSettingsDynamic();
                    (settings as GibboTilesetSettingsDynamic).HighlightActiveTileset = Properties.Settings.Default.HighlightActiveTileset;
                    break;

                case "game_general":
                    settings = new GameGeneralSettingsDynamic();
                    (settings as GameGeneralSettingsDynamic).ProjectName = SceneManager.GameProject.ProjectName;
                    break;

                case "game_grid":
                    settings = new GameGridSettingsDynamic();
                    (settings as GameGridSettingsDynamic).GridSpacing = SceneManager.GameProject.EditorSettings.GridSpacing;
                    (settings as GameGridSettingsDynamic).GridThickness = SceneManager.GameProject.EditorSettings.GridThickness;
                    (settings as GameGridSettingsDynamic).GridColor = SceneManager.GameProject.EditorSettings.GridColor;
                    (settings as GameGridSettingsDynamic).DisplayLines = SceneManager.GameProject.EditorSettings.GridNumberOfLines;
                    break;

                case "game_debug":
                    settings = new GameDebugDynamic();
                    (settings as GameDebugDynamic).ShowConsole = iniSettings.IniReadValue("Console", "Visible").ToLower().Trim().Equals("true") ? true : false;

                    try
                    {
                        (settings as GameDebugDynamic).DebugMode = (GameDebugDynamic.DebugModes)Enum.Parse(typeof(GameDebugDynamic.DebugModes), (SceneManager.GameProject.Debug ? "Debug" : "Release"), true);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    break;

                case "game_screen":
                    settings = new GameScreenDynamic();
                    (settings as GameScreenDynamic).MouseVisible = iniSettings.IniReadValue("Mouse", "Visible").ToLower().Trim().Equals("true") ? true : false;
                    (settings as GameScreenDynamic).StartOnFullScreen = iniSettings.IniReadValue("Window", "StartFullScreen").ToLower().Trim().Equals("true") ? true : false;
                    (settings as GameScreenDynamic).ScreenWidth = SceneManager.GameProject.Settings.ScreenWidth;
                    (settings as GameScreenDynamic).ScreenHeight = SceneManager.GameProject.Settings.ScreenHeight;
                    break;
            }

            return settings;
        }

        private void SaveCurrent()
        {
            if (propertyGrid.Tag == null) return;

            switch (propertyGrid.Tag.ToString())
            {
                case "gibbo_general":
                    Properties.Settings.Default.LoadLastProject = (propertyGrid.SelectedObject as GibboGeneralSettingsDynamic).AutomaticProjectLoad;
                    Properties.Settings.Default.StartOnFullScreen = (propertyGrid.SelectedObject as GibboGeneralSettingsDynamic).StartOnFullScreen;
                    Properties.Settings.Default.ShowDebugView = (propertyGrid.SelectedObject as GibboGeneralSettingsDynamic).ShowDebugView;

                    string appName = (propertyGrid.SelectedObject as GibboGeneralSettingsDynamic).ScriptEditors.ToString();
                    
                    // colocar no startup do gibbo
                    EditorUtils.StoreInstalledApplications();

                    //bool isInstalled = false;
                    //foreach (string name in EditorUtils.InstalledApps.Keys)
                    //{
                    //    if (name.Replace(" ", "").ToLower().Contains(appName.ToLower()))
                    //    {
                    //        isInstalled = true;
                    //        MessageBox.Show("Found::::" + EditorUtils.InstalledApps[name]);
                    //        //break;
                    //    }
                    //}

                    //C:\Program Files (x86)\SharpDevelop\4.3\bin\SharpDevelop.exe    
                    // C:\Program Files (x86)\Xamarin Studio\bin\XamarinStudio.exe

                    //Process ide;
                    //    ProcessStartInfo pinfo = new ProcessStartInfo();
                    //    pinfo.FileName = "SharpDevelop.exe";
                    //    pinfo.WorkingDirectory = SceneManager.GameProject.ProjectPath;
                    //    pinfo.UseShellExecute = true;
                    //    ide = Process.Start(pinfo);

                    if (appName.ToLower().Equals("lime"))
                    {
                        Properties.Settings.Default.DefaultScriptEditor = (propertyGrid.SelectedObject as GibboGeneralSettingsDynamic).ScriptEditors.ToString();
                    }

                    if ((propertyGrid.SelectedObject as GibboGeneralSettingsDynamic).ScriptEditors.ToString().ToLower() != "lime")
                        if (EditorUtils.CheckVisualStudioExistance((propertyGrid.SelectedObject as GibboGeneralSettingsDynamic).ScriptEditors.ToString()))
                            Properties.Settings.Default.DefaultScriptEditor = (propertyGrid.SelectedObject as GibboGeneralSettingsDynamic).ScriptEditors.ToString();
                        else
                            MessageBox.Show("You don't have the selected visual studio IDE");
                    else
                        Properties.Settings.Default.DefaultScriptEditor = (propertyGrid.SelectedObject as GibboGeneralSettingsDynamic).ScriptEditors.ToString();
                    
                    
                    Properties.Settings.Default.Save();
                    break;

                case "gibbo_tileset":
                    Properties.Settings.Default.HighlightActiveTileset = (propertyGrid.SelectedObject as GibboTilesetSettingsDynamic).HighlightActiveTileset;
                    break;

                case "game_general":
                    SceneManager.GameProject.ProjectName = (propertyGrid.SelectedObject as GameGeneralSettingsDynamic).ProjectName;
                    break;

                case "game_grid":
                    SceneManager.GameProject.EditorSettings.GridSpacing = (propertyGrid.SelectedObject as GameGridSettingsDynamic).GridSpacing;
                    SceneManager.GameProject.EditorSettings.GridThickness = (propertyGrid.SelectedObject as GameGridSettingsDynamic).GridThickness;
                    SceneManager.GameProject.EditorSettings.GridColor = (propertyGrid.SelectedObject as GameGridSettingsDynamic).GridColor;
                    SceneManager.GameProject.EditorSettings.GridNumberOfLines = (propertyGrid.SelectedObject as GameGridSettingsDynamic).DisplayLines;
                    break;

                case "game_debug":
                    iniSettings.IniWriteValue("Console", "Visible", (propertyGrid.SelectedObject as GameDebugDynamic).ShowConsole.ToString());
                    SceneManager.GameProject.Debug = (propertyGrid.SelectedObject as GameDebugDynamic).DebugMode == GameDebugDynamic.DebugModes.Debug ? true : false;
                    break;

                case "game_screen":
                    iniSettings.IniWriteValue("Mouse", "Visible",  (propertyGrid.SelectedObject as GameScreenDynamic).MouseVisible.ToString());
                    iniSettings.IniWriteValue("Window", "StartFullScreen",  (propertyGrid.SelectedObject as GameScreenDynamic).StartOnFullScreen.ToString());
                    SceneManager.GameProject.Settings.ScreenWidth = (propertyGrid.SelectedObject as GameScreenDynamic).ScreenWidth;
                    SceneManager.GameProject.Settings.ScreenHeight = (propertyGrid.SelectedObject as GameScreenDynamic).ScreenHeight;
                    break;
            }
        } 

        private void ProjectsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProjectsListBox.SelectedItem == null || (ProjectsListBox.SelectedItem as ListBoxItem).Tag == null) return;

            SaveCurrent();

            propertyGrid.Tag = (ProjectsListBox.SelectedItem as ListBoxItem).Tag.ToString();
            propertyGrid.SelectedObject = LoadProperties(propertyGrid.Tag.ToString());
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            SaveCurrent();
        }
    }

    interface ISettingsChannelA { }

    class GibboGeneralSettingsDynamic : ISettingsChannelA
    {
        public enum ScriptingEditors { None, Lime, VisualStudio2013, VisualStudio2012, VisualStudio2010 } //CSExpress2010

        private bool automaticProjectLoad;
        private bool startOnFullScreen;
        private bool showDebugView;
        private ScriptingEditors scriptEditors;

        [Category("General")]
        [DisplayName("Show Debug View")]
        public bool ShowDebugView
        {
            get { return showDebugView; }
            set { showDebugView = value; }
        }

        [Category("General")]
        [DisplayName("Start Gibbo in Full Screen")]
        public bool StartOnFullScreen
        {
            get { return startOnFullScreen; }
            set { startOnFullScreen = value; }
        }

        [Category("General")]
        [DisplayName("Script Editor")]
        public ScriptingEditors ScriptEditors
        {
            get { return scriptEditors; }
            set { scriptEditors = value; }
        }

        [Category("General")]
        [DisplayName("Load Last Project on Start")]
        public bool AutomaticProjectLoad
        {
            get { return automaticProjectLoad; }
            set { automaticProjectLoad = value; }
        }
    }

    class GibboTilesetSettingsDynamic : ISettingsChannelA
    {
        private bool highlightActiveTileset;

        [Category("Tileset")]
        [DisplayName("Highlight Active Tileset")]
        public bool HighlightActiveTileset
        {
            get { return highlightActiveTileset; }
            set { highlightActiveTileset = value; }
        }
    }

    class GameGeneralSettingsDynamic : ISettingsChannelA
    {
        private string projectName;

        [Category("General")]
        [DisplayName("Project Name")]
        public string ProjectName
        {
            get { return projectName; }
            set { projectName = value; }
        }
    }

    class GameGridSettingsDynamic : ISettingsChannelA
    {
        private int gridSpacing;
        private int gridThickness;
        private int displayLines;

        private Microsoft.Xna.Framework.Color gridColor;

        [Category("Grid")]
        [DisplayName("Grid Spacing")]
        public int GridSpacing
        {
            get { return gridSpacing; }
            set { gridSpacing = value; }
        }

        [Category("Grid")]
        [DisplayName("Grid Thickness")]
        public int GridThickness
        {
            get { return gridThickness; }
            set { gridThickness = value; }
        }

        [Category("Grid")]
        [DisplayName("Display Lines")]
        public int DisplayLines
        {
            get { return displayLines; }
            set { displayLines = value; }
        }

        [Category("Grid")]
        [DisplayName("Grid Color")]
        public Microsoft.Xna.Framework.Color GridColor
        {
            get { return gridColor; }
            set { gridColor = value; }
        }
    }

    class GameDebugDynamic : ISettingsChannelA
    {
        public enum DebugModes { Debug, Release }

        private DebugModes debugMode;
        private bool showConsole;

        [Category("Debug")]
        [DisplayName("Debug Mode")]
        public DebugModes DebugMode
        {
            get { return debugMode; }
            set { debugMode = value; }
        }

        [Category("Debug")]
        [DisplayName("Show Console")]
        public bool ShowConsole
        {
            get { return showConsole; }
            set { showConsole = value; }
        }
    }

    class GameScreenDynamic : ISettingsChannelA
    {
        private bool startOnFullScreen;
        private bool mouseVisible;
        private int screenWidth;
        private int screenHeight;

        [Category("Screen")]
        [DisplayName("Start on Full Screen")]
        public bool StartOnFullScreen
        {
            get { return startOnFullScreen; }
            set { startOnFullScreen = value; }
        }

        [Category("Screen")]
        [DisplayName("Mouse Visible")]
        public bool MouseVisible
        {
            get { return mouseVisible; }
            set { mouseVisible = value; }
        }

        [Category("Screen")]
        [DisplayName("Screen Width")]
        public int ScreenWidth
        {
            get { return screenWidth; }
            set { screenWidth = value; }
        }

        [Category("Screen")]
        [DisplayName("Screen Height")]
        public int ScreenHeight
        {
            get { return screenHeight; }
            set { screenHeight = value; }
        }
    }
}
