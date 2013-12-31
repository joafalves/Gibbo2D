using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gibbo.Editor.Model
{
    [Serializable]
    public class UserPreferences
    {
        private static UserPreferences instance;

        public static UserPreferences Instance
        {
            get { return instance; }
            set { instance = value; }
        }

        private String lastProjectLoaded;
        private String projectSlnFilePath;
        private String projectCsProjFilePath;
        private bool highlightActiveTileset;
        private bool loadLastProjectAutomatically = true;

        /// <summary>
        /// 
        /// </summary>
        public bool HighlightActiveTileset
        {
            get { return highlightActiveTileset; }
            set { highlightActiveTileset = value; }
        }

        /// <summary>
        /// The project sln file path.
        /// This variable is updated everytime the editor compiles the project
        /// </summary>
        public String ProjectSlnFilePath
        {
            get { return projectSlnFilePath; }
            set { projectSlnFilePath = value; }
        }

        /// <summary>
        /// The project csproj file path.
        /// This variable is updated everytime the editor compiles the project
        /// </summary>
        public String ProjectCsProjFilePath
        {
            get { return projectCsProjFilePath; }
            set { projectCsProjFilePath = value; }
        }
    }
}
