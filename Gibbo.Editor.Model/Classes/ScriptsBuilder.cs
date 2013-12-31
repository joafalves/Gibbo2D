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
using Gibbo.Editor.Model;
using Microsoft.Build.Framework;

namespace Gibbo.Editor
{
    public static class ScriptsBuilder
    {
        private static object locker = new object();
        private static ErrorLogger logger;

        public static ErrorLogger Logger { get { return logger; }}

        public static bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        public static bool ReloadScripts()
        {
            if (SceneManager.GameProject == null) return false;

            lock (locker)
            {

                try
                {
                    if (File.Exists(SceneManager.GameProject.ProjectPath + @"\_Scripts.csproj"))
                        File.Delete(SceneManager.GameProject.ProjectPath + @"\_Scripts.csproj");
                    //search for .sln and .csproj files
                    foreach (string filename in Directory.GetFiles(SceneManager.GameProject.ProjectPath))
                    {
                        if (Path.GetExtension(filename).ToLower().Equals(".csproj"))
                        {
                            UserPreferences.Instance.ProjectCsProjFilePath = filename;
                        }
                        else if (Path.GetExtension(filename).ToLower().Equals(".sln"))
                        {
                            UserPreferences.Instance.ProjectSlnFilePath = filename;
                        }
                    }

                    // Check if scripts were removed:
                    bool removed = false;

                    string projectFileName = UserPreferences.Instance.ProjectCsProjFilePath;
                    XmlDocument doc = new XmlDocument();
                    doc.Load(projectFileName);

                    while (doc.GetElementsByTagName("ItemGroup").Count < 2)
                        doc.GetElementsByTagName("Project").Item(0).AppendChild(doc.CreateElement("ItemGroup"));

                    foreach (XmlNode _node in doc.GetElementsByTagName("ItemGroup").Item(1).ChildNodes)
                    {
                        if (_node.Name.ToLower().Equals("compile"))
                        {
                            if (!File.Exists(SceneManager.GameProject.ProjectPath + "\\" + _node.Attributes.GetNamedItem("Include").Value))
                            {
                                doc.GetElementsByTagName("ItemGroup").Item(1).RemoveChild(_node);
                                removed = true;
                            }
                        }
                    }

                    if (removed)
                        doc.Save(projectFileName);

                    string tmpProjectFileName = SceneManager.GameProject.ProjectPath + @"\_Scripts.csproj";
                    string hash = string.Empty;

                    hash = GibboHelper.EncryptMD5(DateTime.Now.ToString());

                    //try
                    //{
                        /* Change the assembly Name */
                        doc = new XmlDocument();
                        doc.Load(projectFileName);
                        doc.GetElementsByTagName("Project").Item(0).ChildNodes[0]["AssemblyName"].InnerText = hash;
                        doc.Save(tmpProjectFileName);
                    //}
                    //catch (Exception ex)
                    //{
                    //    Console.WriteLine(ex.Message);
                    //}

                    /* Compile project */
                    ProjectCollection projectCollection = new ProjectCollection();
                    Dictionary<string, string> GlobalProperty = new Dictionary<string, string>();
                    GlobalProperty.Add("Configuration", SceneManager.GameProject.Debug ? "Debug" : "Release");
                    GlobalProperty.Add("Platform", "x86");

                    //FileLogger logger = new FileLogger() { Parameters = @"logfile=C:\gibbo_log.txt" };
                    logger = new ErrorLogger();

                    BuildRequestData buildRequest = new BuildRequestData(tmpProjectFileName, GlobalProperty, null, new string[] { "Build" }, null);
                    BuildResult buildResult = BuildManager.DefaultBuildManager.Build(
                            new BuildParameters(projectCollection)
                            {
                                BuildThreadPriority = System.Threading.ThreadPriority.AboveNormal,
                                Loggers = new List<ILogger>() { logger }
                            },
                            buildRequest);

                    //foreach (var tr in logger.Errors)
                    //{
                    //    Console.WriteLine(tr.ToString());
                    //}

                    //Console.WriteLine(buildResult.OverallResult);

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

                        //Console.WriteLine("Path: " + SceneManager.ScriptsAssembly.GetName().Name);
                    }
                    else
                    {
                        File.Delete(tmpProjectFileName);
                        return false;
                    }

                    File.Delete(tmpProjectFileName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return true;
            }
        }
    }
}
