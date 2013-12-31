using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gibbo.Library
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class VisualScriptManager
    {
        #region fields

        private int lastSafeID = 0;
        private Dictionary<int, VisualScript> visualScripts = new Dictionary<int, VisualScript>();

        #endregion

        #region properties

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<int, VisualScript> VisualScripts
        {
            get { return visualScripts; }
            set { visualScripts = value; }
        }

        #endregion

        #region constructors

        /// <summary>
        /// 
        /// </summary>
        public VisualScriptManager()
        {

        }

        #endregion

        #region methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="visualScript"></param>
        public void AddVisualScript(VisualScript visualScript)
        {
            while (visualScripts.ContainsKey(lastSafeID))
                lastSafeID++;

            this.visualScripts.Add(lastSafeID, visualScript);

            SceneManager.GameProject.SaveVisualScripts();
        }

        internal static VisualScriptManager Load(string filepath)
        {
            VisualScriptManager manager = (VisualScriptManager)GibboHelper.DeserializeObject(filepath);
            return manager;
        }

        internal void Save(string filePath)
        {
            GibboHelper.SerializeObject(filePath, this);
        }

        #endregion
    }
}
