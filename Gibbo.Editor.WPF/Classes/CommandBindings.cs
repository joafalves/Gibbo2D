using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Gibbo.Editor.WPF
{
    static class CommandBindings
    {
        public static readonly RoutedUICommand FullDebug = new RoutedUICommand("Full Debug", "FullDebug", typeof(MainWindow));
        public static readonly RoutedUICommand ToCollisionBlock = new RoutedUICommand("To Collision Block", "ToCollisionBlock", typeof(MainWindow));
    }
}
