using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Gibbo.Editor.WPF.Controls
{
    //public class GlobalFilePathEditor : Xceed.Wpf.Toolkit.PropertyGrid.Editors.ITypeEditor
    //{
    //    public FrameworkElement ResolveEditor(Xceed.Wpf.Toolkit.PropertyGrid.PropertyItem propertyItem)
    //    {
    //        TextBox textBox = new TextBox();
    //        textBox.Background = new SolidColorBrush(Colors.Red);

    //        //create the binding from the bound property item to the editor
    //        var _binding = new Binding("Value"); //bind to the Value property of the PropertyItem
    //        _binding.Source = propertyItem;
    //        _binding.ValidatesOnExceptions = true;
    //        _binding.ValidatesOnDataErrors = true;
    //        _binding.Mode = propertyItem.IsReadOnly ? BindingMode.OneWay : BindingMode.TwoWay;
    //        BindingOperations.SetBinding(textBox, TextBox.TextProperty, _binding);
    //        return textBox;
    //    }
    //}
}
