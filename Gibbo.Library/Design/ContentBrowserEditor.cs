#if WINDOWS 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms.Design;
using System.Drawing.Design;
using System.Windows.Forms;
using System.IO;


namespace Gibbo.Library
{
    /// <summary>
    /// 
    /// </summary>
    public class ContentBrowserEditor : UITypeEditor
    {
        private IWindowsFormsEditorService service;

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            // This tells it to show the [...] button which is clickable firing off EditValue below.
            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (provider != null)
                service = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            if (service != null)
            {
                // This is the code you want to run when the [...] is clicked and after it has been verified.
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.InitialDirectory = SceneManager.GameProject.ProjectPath;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string path = GibboHelper.MakeExclusiveRelativePath(SceneManager.GameProject.ProjectPath, ofd.FileName);

                    if (File.Exists(SceneManager.GameProject.ProjectPath + "\\" + path))
                        value = path;
                }

                // Return the newly selected color.
               // value =
            }

            return value;
        }
    }
}

#endif