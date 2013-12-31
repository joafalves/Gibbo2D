using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;

using System.Runtime.Serialization;
using System.Diagnostics;

namespace Gibbo.Library
{
    /// <summary>
    /// The default project structure
    /// </summary>
#if WINDOWS
    [Serializable]
#endif
    [DataContract]
    public class GibboProject
    {
        #region fields
        [DataMember]
        private String projectName;
        [DataMember]
        private String projectPath;
        [DataMember]
        private String projectFilePath;
        [DataMember]
        private String sceneStartPath;
        [DataMember]
        private GibboProjectSettings projectSettings = new GibboProjectSettings();
        [DataMember]
        private GibboProjectEditorSettings editorSettings = new GibboProjectEditorSettings();
        [DataMember]
        private bool debug = true;
        [DataMember]
        private Settings settings = new Settings();

        //#if WINDOWS
        //        [NonSerialized]
        //#endif
        //        private VisualScriptManager visualScriptManager = new VisualScriptManager();

        #endregion

        #region properties

        /// <summary>
        /// 
        /// </summary>
//#if WINDOWS
//        [Browsable(false)]
//#endif
//        public VisualScriptManager VisualScriptManager
//        {
//            get { return visualScriptManager; }
//        }

        /// <summary>
        /// Build Mode
        /// </summary>
#if WINDOWS
        [Category("Project Properties")]
        [DisplayName("Debug"), Description("Determine if the application should compiled and run in debug mode.")]
#endif
        public bool Debug
        {
            get { return debug; }
            set { debug = value; }
        }

        /// <summary>
        /// The initial scene path
        /// </summary>
#if WINDOWS        
        [Category("Project Properties"), Browsable(false)]
#endif
        public String SceneStartPath
        {
            get { return sceneStartPath; }
            set { sceneStartPath = value; }
        }

        /// <summary>
        /// The name of the project
        /// </summary>
#if WINDOWS
        [Category("Project Properties")]
        [DisplayName("Project Name"), Description("The name of the project")]
#endif
        public String ProjectName
        {
            get { return projectName; }
            set { projectName = value; }
        }

        /// <summary>
        /// The project path.
        /// This variable is updated everytime the editor/engine loads the project.
        /// </summary>
#if WINDOWS
        [Browsable(false)]
#endif
        public String ProjectPath
        {
            get { return projectPath; }
            set { projectPath = value; }
        }

        /// <summary>
        /// The project file path.
        /// This variable is updated everytime the editor /engine loads the project.
        /// </summary>
#if WINDOWS
        [Browsable(false)]
#endif
        public String ProjectFilePath
        {
            get { return projectFilePath; }
            set { projectFilePath = value; }
        }

        /// <summary>
        /// The projects settings.
        /// </summary>
#if WINDOWS
        [Category("Project Properties")]
        [DisplayName("Project Settings"), Description("The project settings")]
#endif
        public GibboProjectSettings ProjectSettings
        {
            get { return projectSettings; }
            set { projectSettings = value; }
        }

        /// <summary>
        /// The editor settings
        /// </summary>
#if WINDOWS
        [Category("Project Properties")]
        [DisplayName("Editor Settings"), Description("The editor settings")]
#endif
        public GibboProjectEditorSettings EditorSettings
        {
            get { return editorSettings; }
            set { editorSettings = value; }
        }

        /// <summary>
        /// Project settings
        /// Reloaded every time the project is loaded.
        /// To reload manually, call ReloadSettings()
        /// </summary>
#if WINDOWS
        [Category("Project Properties")]
        [DisplayName("Settings"), Description("The executable settings")]
#endif
        public Settings Settings
        {
            get { return settings; }
        }

        #endregion

        #region constructors


        /// <summary>
        /// The default constructor
        /// </summary>
        /// <param name="projectName">The name of the project</param>
        /// <param name="projectPath">The path of the project</param>
        public GibboProject(String projectName, String projectPath)
        {
            this.projectName = projectName;
            this.projectPath = projectPath + "\\" + projectName;
            this.projectFilePath = this.projectPath + "\\" + projectName + ".gibbo";
#if WINDOWS
            this.CreateProjectDirectory();
#endif

            this.Save();
        }

        #endregion

        #region methods

        /// <summary>
        /// Loads a project and returns it
        /// </summary>
        /// <param name="filepath">The source filepath</param>
        /// <returns>A deserialized gibbo project</returns>
        public static GibboProject Load(string filepath)
        {
#if WINDOWS
            GibboProject project = (GibboProject)GibboHelper.DeserializeObject(filepath);
#elif WINRT
            string npath = filepath.Replace(Windows.ApplicationModel.Package.Current.InstalledLocation.Path + "\\", string.Empty);
            GibboProject project = (GibboProject)GibboHelper.DeserializeObject(typeof(GibboProject), npath);
#endif    
            // Update the project path:
            project.ProjectPath = Path.GetDirectoryName(filepath);
            project.ProjectFilePath = filepath;

            // Load data files
            LoadData(ref project);

            // Create a virtual settings file:
            project.settings.ReloadPath(project.ProjectPath);

            return project;
        }

        private static void LoadData(ref GibboProject project)
        {
            string dataPath = project.projectPath + "\\Data";

            //#if WINDOWS
            //            if (!Directory.Exists(dataPath))
            //            {
            //                Directory.CreateDirectory(dataPath);

            //                VisualScriptManager vsManager = new VisualScriptManager();
            //                project.visualScriptManager = vsManager;
            //                project.SaveVisualScripts();
            //            }

            //            // Load Visual Scripts
            //            project.visualScriptManager = VisualScriptManager.Load(dataPath + "\\VisualScripts.data");
            //#endif
        }

        /// <summary>
        /// Saves the project visual scripts
        /// </summary>
        public void SaveVisualScripts()
        {
            string dataPath = this.projectPath + "\\Data";
            //this.visualScriptManager.Save(dataPath + "\\VisualScripts.data");
        }

        /// <summary>
        /// Saves the project at the current project path location
        /// </summary>
        public void Save()
        {
            this.Settings.SaveToFile();

            GibboHelper.SerializeObject(this.projectFilePath, this);
        }

        /// <summary>
        /// Reloads the settings.ini configurations to memory
        /// </summary>
        public void ReloadSettings()
        {
            if (this.settings != null)
                settings.ReloadSettings();
        }

#if WINDOWS
        private void CreateProjectDirectory()
        {
            Directory.CreateDirectory(this.projectPath);

            //TODO: space to create other directories
            Directory.CreateDirectory(this.projectPath + "//Libs");
            Directory.CreateDirectory(this.projectPath + "//Scenes");
            Directory.CreateDirectory(this.projectPath + "//Content");
        }
#endif

        #endregion
    }
}
