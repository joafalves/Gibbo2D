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
