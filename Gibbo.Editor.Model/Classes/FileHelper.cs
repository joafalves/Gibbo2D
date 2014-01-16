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
