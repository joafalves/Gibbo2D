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
