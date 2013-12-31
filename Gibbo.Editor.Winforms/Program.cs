using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Gibbo.Editor.Forms;

namespace Gibbo.Editor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ComponentFactory.Krypton.Toolkit.KryptonManager kmRenderSettings = new ComponentFactory.Krypton.Toolkit.KryptonManager();
            kmRenderSettings.GlobalPaletteMode = ComponentFactory.Krypton.Toolkit.PaletteModeManager.Office2010Silver;

            //Application.Run(new VisualScriptingWindow());
            Application.Run(new ProjectStartup());
            //new ProjectStartup().ShowDialog();
        }
    }
}
