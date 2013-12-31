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
