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
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Gibbo.Editor.Model
{
    public static class GlobalCommands
    {
        public static void RemoveEmptyFolders(string path, SearchOption searchOption)
        {
            foreach (string dirPath in Directory.GetDirectories(path, "*", SearchOption.AllDirectories))
            {
                if (Directory.GetFiles(dirPath).Count() == 0 && Directory.GetDirectories(dirPath).Count() == 0)
                    Directory.Delete(dirPath);
            }
        }

        public static bool DeployProject(string projectPath, string destinationPath, string platform)
        {
            List<string> blockedDirs = new List<string>();
            blockedDirs.Add("bin");
            blockedDirs.Add("obj");

            List<string> blockedFileExtensions = new List<string>();
            blockedFileExtensions.Add(".cs");
            //blockedFileExtensions.Add(".gibbo");
            blockedFileExtensions.Add(".csproj");
            blockedFileExtensions.Add(".sln");

            switch (platform.ToLower())
            {
                case "windows":
                    // creates shadow directories
                    foreach (string dirPath in Directory.GetDirectories(projectPath, "*", SearchOption.AllDirectories))
                    {
                        Directory.CreateDirectory(dirPath.Replace(projectPath, destinationPath));
                    }

                    // clear blocked directories from the root folder:
                    foreach (string dirPath in Directory.GetDirectories(destinationPath, "*", SearchOption.TopDirectoryOnly))
                    {
                        if (blockedDirs.Contains(Path.GetFileName(dirPath)))
                            Directory.Delete(dirPath, true);
                    }

                    // copy all the files
                    foreach (string path in Directory.GetFiles(projectPath, "*.*", SearchOption.AllDirectories))
                    {
                        string filename = Path.GetFileName(path);
                        string ext = Path.GetExtension(path);
                        if (!blockedFileExtensions.Contains(ext) && 
                            Directory.Exists(Path.GetDirectoryName(path.Replace(projectPath, destinationPath))))
                        {
                            if (filename.ToLower().Equals("gibbo.engine.windows.exe"))
                            {
                                File.Copy(path, Path.GetDirectoryName(path.Replace(projectPath, destinationPath)) + "\\" + SceneManager.GameProject.ProjectName + ".exe", true);
                            }
                            else
                            {
                                File.Copy(path, path.Replace(projectPath, destinationPath), true);
                            }
                        }
                    }

                    RemoveEmptyFolders(destinationPath, SearchOption.AllDirectories);

                    return true;
                case "windowsstore":
                    // creates shadow directories
                    foreach (string dirPath in Directory.GetDirectories(projectPath, "*", SearchOption.AllDirectories))
                        Directory.CreateDirectory(destinationPath.Replace(projectPath, destinationPath));

                    //Copy all the files
                    foreach (string path in Directory.GetFiles(projectPath, "*.*", SearchOption.AllDirectories))
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
                        else
                        {
                            File.Copy(path, path.Replace(projectPath, destinationPath), true);
                        }
                    }

                    RemoveEmptyFolders(destinationPath, SearchOption.AllDirectories);

                    return true;
            }

            return false;
        }
    }
}
