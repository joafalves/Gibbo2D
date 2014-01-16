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
