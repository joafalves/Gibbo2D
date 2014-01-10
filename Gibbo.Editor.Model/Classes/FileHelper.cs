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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Gibbo.Editor
{
    public enum FileTemplate { None, Component };

    public static class FileHelper
    {
        /// <summary>
        /// Checks if the input filename is available, if it isn't, returns the next available name
        /// Structure : [filename]{number}.[extension]
        /// </summary>
        /// <param name="filename"></param>
        /// <returns>An available filename [filename]{number}.[extension]</returns>
        public static string ResolveFilename(string filename)
        {
            // The file does not exist?
            if (!File.Exists(filename)) return filename;

            string extension = Path.GetExtension(filename);
            string nameNoExtension = Path.GetFileNameWithoutExtension(filename);
            string _filename = Path.GetDirectoryName(filename) + "\\" + nameNoExtension;

            int cnt = 1;
            while (File.Exists(_filename + cnt + extension)) cnt++;
            _filename += cnt + extension;

            return _filename;
        }

        /// <summary>
        /// Creates a file with the given input
        /// </summary>
        /// <param name="path"></param>
        /// <returns>The path of the created file</returns>
        public static string CreateFile(string path, FileTemplate template)
        {
            // Resolve the selected filename
            path = ResolveFilename(path);

            // Create a file or append
            using (System.IO.FileStream fs = new System.IO.FileStream(path, FileMode.Append))
            {
              
            }

            switch (template)
            {
                case FileTemplate.Component:

                    //File.WriteAllText(path, Properties.Settings.Default.NewComponentTemplate);
                    break;

            }

            return path;
        }
    }
}
