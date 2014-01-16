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

*/
#endregion

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
