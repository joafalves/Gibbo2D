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

The license applies to all versions of the software, both newer and older than the one listed, unless a newer copy 
of the license is available, in which case the most recent copy of the license supercedes all others.

*/
#endregion

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
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;                     
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.ExceptionObject.ToString());
        }

        void App_Startup(object sender, StartupEventArgs e)
        {
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

            //base.OnStartup(e);
        }
    }
}
