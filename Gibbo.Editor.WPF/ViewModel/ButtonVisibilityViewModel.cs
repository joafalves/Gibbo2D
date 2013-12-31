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
