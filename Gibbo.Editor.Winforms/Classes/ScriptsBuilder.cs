using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Build.Logging;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Execution;
using System.Reflection;
using Gibbo.Library;
using System.IO;
using System.Xml.Linq;
using System.Xml;

namespace Gibbo.Editor
{
    static class ScriptsBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        public static bool ReloadScripts()
        {
            if (SceneManager.GameProject == null) return false;

            string projectFileName = SceneManager.GameProject.ProjectPath + @"\Scripts.csproj";
            string tmpProjectFileName = SceneManager.GameProject.ProjectPath + @"\_Scripts.csproj";
            string hash = string.Empty;

            hash = GibboHelper.EncryptMD5(DateTime.Now.ToString());

            try
            {
                /* Change the assembly Name */
                XmlDocument doc = new XmlDocument();
                doc.Load(projectFileName);
                doc.GetElementsByTagName("Project").Item(0).ChildNodes[0]["AssemblyName"].InnerText = hash;
                doc.Save(tmpProjectFileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            /* Compile project */
            ProjectCollection projectCollection = new ProjectCollection();
            Dictionary<string, string> GlobalProperty = new Dictionary<string, string>();
            GlobalProperty.Add("Configuration", SceneManager.GameProject.Debug ? "Debug" : "Release");
            GlobalProperty.Add("Platform", "x86");

            BuildRequestData buildRequest = new BuildRequestData(tmpProjectFileName, GlobalProperty, null, new string[] { "Build" }, null);
            BuildResult buildResult = BuildManager.DefaultBuildManager.Build(new BuildParameters(projectCollection), buildRequest);

            Console.WriteLine(buildResult.OverallResult);

            string cPath = SceneManager.GameProject.ProjectPath + @"\bin\" + (SceneManager.GameProject.Debug ? "Debug" : "Release") + "\\" + hash + ".dll";

            if (File.Exists(cPath))
            {
                /* read assembly to memory without locking the file */
                byte[] readAllBytes = File.ReadAllBytes(cPath);
                SceneManager.ScriptsAssembly = Assembly.Load(readAllBytes);

                if (SceneManager.ActiveScene != null)
                {
                    SceneManager.ActiveScene.SaveComponentValues();
                    SceneManager.ActiveScene.Initialize();
                }

                Console.WriteLine("Path: " + SceneManager.ScriptsAssembly.GetName().Name);
            }
            else
            {
                File.Delete(tmpProjectFileName);
                return false;
            }

            File.Delete(tmpProjectFileName);

            return true;
        }
    }
}
