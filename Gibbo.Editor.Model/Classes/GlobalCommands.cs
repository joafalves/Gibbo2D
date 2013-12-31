using Gibbo.Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Gibbo.Editor.Model
{
    public static class GlobalCommands
    {
        public static bool DeployProject(string projectPath, string destinationPath, string platform)
        {
            switch (platform.ToLower())
            {
                case "windows":

                    break;
                case "windowsstore":
                    // creates shadow directories
                    foreach (string dirPath in Directory.GetDirectories(projectPath, "*",
                        SearchOption.AllDirectories))
                        Directory.CreateDirectory(destinationPath.Replace(projectPath, destinationPath));

                    //Copy all the files
                    foreach (string path in Directory.GetFiles(projectPath, "*.*",
                        SearchOption.AllDirectories))
                    {
                        if (!Directory.Exists(Path.GetDirectoryName(path.Replace(projectPath, destinationPath))))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(path.Replace(projectPath, destinationPath)));
                        }

                        if (path.ToLower().EndsWith(".scene"))
                        {
                            GameScene scene = (GameScene)GibboHelper.DeserializeObject(path);
                            GibboHelper.SerializeObjectXML(path.Replace(projectPath, destinationPath), scene);
                        }
                        else if (path.ToLower().EndsWith(".gibbo"))
                        {
                            GibboProject gp = (GibboProject)GibboHelper.DeserializeObject(path);
                            GibboHelper.SerializeObjectXML(path.Replace(projectPath, destinationPath), gp);
                        }
                        else if (path.ToLower().EndsWith(".cs"))
                        {
                            // do nothing
                        }
                        else
                        {
                            File.Copy(path, path.Replace(projectPath, destinationPath), true);
                        }
                    }


                    break;
            }
    
            return true;
        }
    }
}
