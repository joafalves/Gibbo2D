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
    public class ResizeBasedVisibility : System.ComponentModel.INotifyPropertyChanged
    {
        private Visibility maximizeVisibility;
        public Visibility MaximizeVisibility
        {
            get
            {
                return maximizeVisibility;
            }
            set
            {
                maximizeVisibility = value;
                OnPropertyChanged("MaximizeVisibility");
            }
        }

        private Visibility minimizeVisibility;
        public Visibility MinimizeVisibility
        {
            get
            {
                return minimizeVisibility;
            }
            set
            {
                minimizeVisibility = value;
                OnPropertyChanged("MinimizeVisibility");
            }
        }

        //private Visibility maximizeVisibility;
        //public Visibility MaximizeVisibility
        //{
        //    get
        //    {
        //        return maximizeVisibility;
        //    }
        //    set
        //    {
        //        maximizeVisibility = value;
        //        OnPropertyChanged("MaximizeVisibility");
        //    }
        //}

        #region INotifyPropertyChanged Members

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
