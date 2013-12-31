/*************************************************************************************

   Extended WPF Toolkit

   Copyright (C) 2007-2013 Xceed Software Inc.

   This program is provided to you under the terms of the Microsoft Public
   License (Ms-PL) as published at http://wpftoolkit.codeplex.com/license 

   For more features, controls, and fast professional support,
   pick up the Plus Edition at http://xceed.com/wpf_toolkit

   Stay informed: follow @datagrid on Twitter or Like http://facebook.com/datagrids

  ***********************************************************************************/

using System.Collections.Generic;
using System.Collections;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Xceed.Wpf.Toolkit.PropertyGrid.Editors
{
    public abstract class ComboBoxEditor : TypeEditor<System.Windows.Controls.ComboBox>
    {
        protected override void SetValueDependencyProperty()
        {
            ValueProperty = System.Windows.Controls.ComboBox.SelectedItemProperty;
            Editor.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            Editor.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            Editor.Height = 18;
            Editor.Loaded += combobox_Loaded;
            Editor.BorderBrush = new SolidColorBrush(Color.FromRgb(86, 86, 86));
            Editor.Foreground = new SolidColorBrush(Color.FromRgb(230, 230, 230));
        }

        private void combobox_Loaded(object sender, RoutedEventArgs e)
        {
            Popup popup = FindVisualChildByName<Popup>((sender as DependencyObject), "PART_Popup");
            
            //Grid grid = FindVisualChildByName<Grid>(popup.Child, "DropDown");
            //grid.Background = Brushes.Red;
            //Border border = FindVisualChildByName<Border>(popup.Child, "DropDownBorder");
            //border.Background = Brushes.Red;
        }

        private T FindVisualChildByName<T>(DependencyObject parent, string name) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                string controlName = child.GetValue(Control.NameProperty) as string;
                if (controlName == name)
                {
                    return child as T;
                }
                else
                {
                    T result = FindVisualChildByName<T>(child, name);
                    if (result != null)
                        return result;
                }
            }
            return null;
        }

        protected override void SetControlProperties()
        {
            //Editor.Style = PropertyGridUtilities.ComboBoxStyle;
            //Editor.ItemContainerStyle.Setters.Add(new System.Windows.Setter(Control.BackgroundProperty, Colors.Red));
        }

        protected override void ResolveValueBinding(PropertyItem propertyItem)
        {
            SetItemsSource(propertyItem);
            base.ResolveValueBinding(propertyItem);
        }

        protected abstract IEnumerable CreateItemsSource(PropertyItem propertyItem);

        private void SetItemsSource(PropertyItem propertyItem)
        {
            Editor.ItemsSource = CreateItemsSource(propertyItem);
        }
    }
}
