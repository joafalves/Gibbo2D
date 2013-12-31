using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gibbo.Editor.Model;
using System.Windows;

namespace Gibbo.Editor.WPF
{
    static class LimeScriptEditor
    {
        #region fields

        private static ScriptingEditorWindow instance;

        #endregion

        #region properties

        public static ScriptingEditorWindow Instance
        {
            get {
                if (instance == null || !instance.IsVisible )
                {
                    instance = new ScriptingEditorWindow();
                    instance.Show(); 
                }

                return instance;
            }
        }

        #endregion

        #region methods

        public static void OpenScript(string path)
        {
            Instance.OpenScript(path);
        }

        #endregion
    }
}
