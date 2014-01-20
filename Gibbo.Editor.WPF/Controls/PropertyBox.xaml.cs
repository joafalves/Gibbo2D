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

The license applies to all versions of the software, both newer and older than the one listed, unless a newer copy 
of the license is available, in which case the most recent copy of the license supercedes all others.

*/
#endregion

using Gibbo.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Gibbo.Editor.WPF
{
    /// <summary>
    /// Interaction logic for PropertyBox.xaml
    /// </summary>
    public partial class PropertyBox : UserControl
    {
        object selected;

        public object SelectedObject
        {
            get { return PropertyGrid.SelectedObject; }
            set
            {
                Dispatcher.Invoke((Action)(() =>
                {
                    selected = value;
                    PropertyGrid.SelectedObject = value;
                    Title.Content = GibboHelper.SplitCamelCase(value.ToString());

                    if (value is ObjectComponent)
                    {
                        SettingsBtn.Visibility = System.Windows.Visibility.Visible;
                    }
                    else
                    {
                        SettingsBtn.Visibility = System.Windows.Visibility.Hidden;
                    }
                }));
            }
        }

        //public object[] SelectedObjects
        //{
        //    get { return PropertyGrid.SelectedObjects; }
        //    set
        //    {
        //        PropertyGrid.SelectedObjects = value;
        //        Title.Content = "Multiple Objects";
        //    }
        //}

        public PropertyBox()
        {
            InitializeComponent();
            //PropertyGrid.SelectedObjectChanged += PropertyGrid_SelectedObjectChanged;
            //PropertyGrid.SelectedPropertyItemChanged += PropertyGrid_SelectedPropertyItemChanged;
        }

        void PropertyGrid_SelectedPropertyItemChanged(object sender, RoutedPropertyChangedEventArgs<Xceed.Wpf.Toolkit.PropertyGrid.PropertyItemBase> e)
        {

        }

        void PropertyGrid_SelectedObjectChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

        }

        private void VisibilityHandlerBtn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ToggleExpand();
        }

        public void ToggleExpand()
        {
            //var disp = this.Dispatcher;
            //disp.BeginInvoke(DispatcherPriority.Background, (Action)(() =>
            //{

                bool expanded = false;
                if (PropertyGridContainer.Visibility == System.Windows.Visibility.Collapsed)
                {
                    PropertyGridContainer.Visibility = System.Windows.Visibility.Visible;
                    VisibilityHandlerBtn.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("Gibbo.Content/_arrow_down.png");
                    expanded = true;
                }
                else if (PropertyGridContainer.Visibility == System.Windows.Visibility.Visible)
                {
                    PropertyGridContainer.Visibility = System.Windows.Visibility.Collapsed;
                    VisibilityHandlerBtn.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("Gibbo.Content/_arrow_right.png");
                }

                if (SelectedObject is ObjectComponent)
                    (SelectedObject as ObjectComponent).EditorExpanded = expanded;
            //}));
        }

        private void PropertyGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            Dispatcher.Invoke((Action)(() =>
            {
                PropertyGrid.Update();
            }));
        }

        private void SettingsBtn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this component?", "Warning!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                ObjectComponent oc = this.PropertyGrid.SelectedObject as ObjectComponent;
                oc.Transform.GameObject.RemoveComponent(oc);

                this.Visibility = System.Windows.Visibility.Collapsed;

                EditorCommands.CheckPropertyGridConsistency();
            }
        }
    }
}
