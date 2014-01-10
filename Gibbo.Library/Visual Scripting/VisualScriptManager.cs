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
