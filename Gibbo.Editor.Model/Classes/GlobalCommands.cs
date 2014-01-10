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
