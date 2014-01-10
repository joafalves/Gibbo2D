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
using System.ComponentModel;
using System;

using System.Runtime.Serialization;

namespace Gibbo.Library
{
    /// <summary>
    /// Settings class.
    /// </summary>
#if WINDOWS
    [Serializable, TypeConverter(typeof(ExpandableObjectConverter))]
#endif
    [DataContract]
    public class Settings
    {
        #region fields

#if WINDOWS
        [NonSerialized]
#endif
        private string rootPath;

#if WINDOWS
        [NonSerialized]
#endif
        private IniFile settingsFile;

        #endregion

        #region properties

        /// <summary>
        /// The screen width
        /// </summary>
#if WINDOWS
        [DisplayName("Screen Width"), Description("The screen width")]
#endif
        public int ScreenWidth { get; set; }

        /// <summary>
        /// The screen height
        /// </summary>
#if WINDOWS
        [DisplayName("Screen Height"), Description("The screen height")]
#endif
        public int ScreenHeight { get; set; }

        #endregion

        #region constructors


        #endregion

        #region methods

        internal void ReloadPath(string rootPath)
        {
            this.rootPath = rootPath;
            settingsFile = new IniFile(rootPath + "\\settings.ini");

            this.ReloadSettings();
        }

        internal void ReloadSettings()
        {
            if (settingsFile != null)
            {
                ScreenWidth = Convert.ToInt32(settingsFile.IniReadValue("Window", "Width").Trim());
                ScreenHeight = Convert.ToInt32(settingsFile.IniReadValue("Window", "Height").Trim());
            }
        }

        internal void SaveToFile()
        {
            if (settingsFile != null)
            {
                settingsFile.IniWriteValue("Window", "Width", " " + ScreenWidth.ToString());
                settingsFile.IniWriteValue("Window", "Height", " " + ScreenHeight.ToString());
            }
        }

        /// <summary>
        /// To String
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Empty;
        }

        #endregion
    }
}
