/*************************************************************************************

   Extended WPF Toolkit

   Copyright (C) 2007-2013 Xceed Software Inc.

   This program is provided to you under the terms of the Microsoft Public
   License (Ms-PL) as published at http://wpftoolkit.codeplex.com/license 

   For more features, controls, and fast professional support,
   pick up the Plus Edition at http://xceed.com/wpf_toolkit

   Stay informed: follow @datagrid on Twitter or Like http://facebook.com/datagrids

  ***********************************************************************************/

using System.Windows.Controls;
using System.Windows.Media;

namespace Xceed.Wpf.Toolkit.PropertyGrid.Editors
{
  public class TextBoxEditor : TypeEditor<WatermarkTextBox>
  {
    protected override void SetControlProperties()
    {
        //Editor.Style = PropertyGridUtilities.NoBorderControlStyle;
    }

    protected override void SetValueDependencyProperty()
    {

        ValueProperty = TextBox.TextProperty;
       // Editor.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#383838")
        Editor.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
        Editor.VerticalAlignment = System.Windows.VerticalAlignment.Center;
        Editor.Height = 18;
        Editor.Background = new SolidColorBrush(Color.FromRgb(76, 76, 76));
        Editor.BorderBrush = new SolidColorBrush(Color.FromRgb(86, 86, 86));
        Editor.Foreground = new SolidColorBrush(Color.FromRgb(230, 230, 230));
       // onde se ve a estrutura do controlo?» como assim? onde tem o resto...x
    }
  }
}
