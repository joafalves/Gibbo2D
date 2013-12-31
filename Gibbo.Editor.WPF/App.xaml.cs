using Gibbo.Library;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Threading.Tasks;
using System.Net;

namespace Gibbo.Editor.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Startup += App_Startup;
        }

        void App_Startup(object sender, StartupEventArgs e)
        {
            //new ScriptingEditorWindow().ShowDialog();

 
            // remove temp folder
            try
            {
                if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\tmp\"))
                {
                    Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\tmp\", true);
                }

                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\tmp\");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
