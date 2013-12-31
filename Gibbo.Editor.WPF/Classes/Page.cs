using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gibbo.Editor.WPF
{
    class Page
    {
        #region Properties

        public string Subtitle{ get; private set; }
        public string Description { get; private set; }
        public string PicturePath { get; private set; }

        #endregion

        #region Constructors

        public Page()
        {
            this.Subtitle = string.Empty;
            this.Description = string.Empty;
        }
        public Page(string subtitle, string description, string picturePath)
        {
            this.Subtitle = subtitle;
            this.Description = description;
            this.PicturePath = picturePath;
        }

        #endregion

        #region Methods

        #endregion
    }
}
