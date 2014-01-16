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
using System.Windows;

namespace Gibbo.Editor.WPF
{
    public class ButtonVisibilityViewModel
    {
        #region Properties

        public ResizeBasedVisibility ButtonVisibility { get; set; }

        #endregion

        #region Constructor
        
        public ButtonVisibilityViewModel()
        {
            
            Window win = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            if (win != null)
            {
                ResizeBasedVisibility bar = new ResizeBasedVisibility();
                switch (win.ResizeMode)
                {
                    case ResizeMode.CanMinimize:
                        bar.MaximizeVisibility = Visibility.Hidden;
                        bar.MinimizeVisibility = Visibility.Visible;
                        break;
                    case ResizeMode.CanResize:
                    case ResizeMode.CanResizeWithGrip:
                        bar.MaximizeVisibility = Visibility.Visible;
                        bar.MaximizeVisibility = Visibility.Visible;
                        break;
                    case ResizeMode.NoResize:
                    default:
                        bar.MaximizeVisibility = Visibility.Collapsed;
                        bar.MinimizeVisibility = Visibility.Collapsed;
                        break;
                }
            }
        }

        #endregion

    }
}
