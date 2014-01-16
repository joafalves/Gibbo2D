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

using Gibbo.Library;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Gibbo.Editor.WPF
{
    public class TreeViewExtension : DependencyObject
    {
        public static bool GetEnableMultiSelect(DependencyObject obj)
        {
            return (bool)obj.GetValue(EnableMultiSelectProperty);
        }

        public static void SetEnableMultiSelect(DependencyObject obj, bool value)
        {
            obj.SetValue(EnableMultiSelectProperty, value);
        }

        // Using a DependencyProperty as the backing store for EnableMultiSelect.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EnableMultiSelectProperty =
            DependencyProperty.RegisterAttached("EnableMultiSelect", typeof(bool), typeof(TreeViewExtension), new FrameworkPropertyMetadata(false)
            {
                PropertyChangedCallback = EnableMultiSelectChanged,
                BindsTwoWayByDefault = true
            });

        public static IList GetSelectedItems(DependencyObject obj)
        {
            return (IList)obj.GetValue(SelectedItemsProperty);
        }

        public static void SetSelectedItems(DependencyObject obj, IList value)
        {
            obj.SetValue(SelectedItemsProperty, value);
        }

        // Using a DependencyProperty as the backing store for SelectedItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.RegisterAttached("SelectedItems", typeof(IList), typeof(TreeViewExtension), new PropertyMetadata(null));

        static TreeViewItem GetAnchorItem(DependencyObject obj)
        {
            return (TreeViewItem)obj.GetValue(AnchorItemProperty);
        }

        static void SetAnchorItem(DependencyObject obj, TreeViewItem value)
        {
            obj.SetValue(AnchorItemProperty, value);
        }

        // Using a DependencyProperty as the backing store for AnchorItem.  This enables animation, styling, binding, etc...
        static readonly DependencyProperty AnchorItemProperty =
            DependencyProperty.RegisterAttached("AnchorItem", typeof(TreeViewItem), typeof(TreeViewExtension), new PropertyMetadata(null));

        static void EnableMultiSelectChanged(DependencyObject s, DependencyPropertyChangedEventArgs args)
        {
            TreeView tree = (TreeView)s;
            var wasEnable = (bool)args.OldValue;
            var isEnabled = (bool)args.NewValue;
            if (wasEnable)
            {
                tree.RemoveHandler(TreeViewItem.PreviewMouseUpEvent, new MouseButtonEventHandler(ItemClicked));
                tree.RemoveHandler(TreeViewItem.PreviewMouseDownEvent, new MouseButtonEventHandler(ItemDown));
                tree.RemoveHandler(TreeView.KeyDownEvent, new KeyEventHandler(KeyDown));
            }
            if (isEnabled)
            {
                tree.AddHandler(TreeViewItem.PreviewMouseUpEvent, new MouseButtonEventHandler(ItemClicked));
                tree.AddHandler(TreeViewItem.PreviewMouseDownEvent, new MouseButtonEventHandler(ItemDown), true);
                tree.AddHandler(TreeView.KeyDownEvent, new KeyEventHandler(KeyDown));
            }
        }

        static void ItemClicked(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem _item = FindTreeViewItem(e.OriginalSource);
            if (_item == null)
                return;

            var mouseButton = e.ChangedButton;
            if ((mouseButton == MouseButton.Left) && ((Keyboard.Modifiers & (ModifierKeys.Shift | ModifierKeys.Control)) == ModifierKeys.None))
            {
                TreeViewItem item = FindTreeViewItem(e.OriginalSource);
                MakeSingleSelection(GetTree(item), item);

                return;
            }
        }

        static void ItemDown(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem item = FindTreeViewItem(e.OriginalSource);
            if (item == null)
                return;

            TreeView tree = (TreeView)sender;

            var mouseButton = e.ChangedButton;
            if (mouseButton != MouseButton.Left)
            {
                if ((mouseButton == MouseButton.Right) && ((Keyboard.Modifiers & (ModifierKeys.Shift | ModifierKeys.Control)) == ModifierKeys.None))
                {
                    if (GetIsSelected(item))
                    {
                        UpdateAnchorAndActionItem(tree, item);
                        return;
                    }

                    if (!GetIsSelected(item)) // item is already selected?
                        MakeSingleSelection(tree, item);
                }

                return;
            }
            if ((Keyboard.Modifiers & (ModifierKeys.Shift | ModifierKeys.Control)) != (ModifierKeys.Shift | ModifierKeys.Control))
            {
                if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
                {
                    MakeToggleSelection(tree, item);
                    return;
                }
                if ((Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
                {
                    MakeAnchorSelection(tree, item, true);
                    return;
                }

                if (!GetIsSelected(item)) // item is already selected?
                    MakeSingleSelection(tree, item);

                return;
            }
        }

        public static TreeView GetTree(TreeViewItem item)
        {
            Func<DependencyObject, DependencyObject> getParent = (o) => VisualTreeHelper.GetParent(o);
            FrameworkElement currentItem = item;
            while (!(getParent(currentItem) is TreeView))
                currentItem = (FrameworkElement)getParent(currentItem);
            return (TreeView)getParent(currentItem);
        }

        static void RealSelectedChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            TreeViewItem item = (TreeViewItem)sender;

            var selectedItems = GetSelectedItems(GetTree(item));
            if (selectedItems != null)
            {
                var isSelected = GetIsSelected(item);
                if (isSelected)
                    try
                    {
                        selectedItems.Add(item.Header);
                    }
                    catch (ArgumentException)
                    {

                    }
                else
                {
                    selectedItems.Remove(item.Header);
                }
            }
        }

        static void KeyDown(object sender, KeyEventArgs e)
        {
            TreeView tree = (TreeView)sender;
            if (e.Key == Key.A && e.KeyboardDevice.Modifiers == ModifierKeys.Control)
            {
                foreach (var item in GetExpandedTreeViewItems(tree))
                {
                    SetIsSelected(item, true);
                }
                e.Handled = true;
            }
        }

        private static TreeViewItem FindTreeViewItem(object obj)
        {
            DependencyObject dpObj = obj as DependencyObject;
            if (dpObj == null)
                return null;
            if (dpObj is TreeViewItem)
                return (TreeViewItem)dpObj;
            return FindTreeViewItem(VisualTreeHelper.GetParent(dpObj));
        }
        
        public static IEnumerable<TreeViewItem> GetExpandedTreeViewItems(ItemsControl tree)
        {
            for (int i = 0; i < tree.Items.Count; i++)
            {
                var item = (TreeViewItem)tree.ItemContainerGenerator.ContainerFromIndex(i);
                if (item == null)
                    continue;
                yield return item;
                if (item.IsExpanded)
                    foreach (var subItem in GetExpandedTreeViewItems(item))
                        yield return subItem;
            }
        }

        private static void MakeAnchorSelection(TreeView tree, TreeViewItem actionItem, bool clearCurrent)
        {
            if (GetAnchorItem(tree) == null)
            {
                var selectedItems = GetSelectedTreeViewItems(tree);
                if (selectedItems.Count > 0)
                {
                    SetAnchorItem(tree, selectedItems[selectedItems.Count - 1]);
                }
                else
                {
                    SetAnchorItem(tree, GetExpandedTreeViewItems(tree).Skip(3).FirstOrDefault());
                }
                if (GetAnchorItem(tree) == null)
                {
                    return;
                }
            }

            var anchor = GetAnchorItem(tree);

            var items = GetExpandedTreeViewItems(tree);
            bool betweenBoundary = false;
            bool end = false;
            foreach (var item in items)
            {
                bool isBoundary = item == anchor || item == actionItem;
                if (isBoundary)
                {
                    betweenBoundary = !betweenBoundary;
                }
                if (betweenBoundary || isBoundary)
                    SetIsSelected(item, true);
                else
                    if (clearCurrent)
                        SetIsSelected(item, false);
                    else
                        break;

            }
        }

        public static List<TreeViewItem> GetSelectedTreeViewItems(TreeView tree)
        {
            return GetExpandedTreeViewItems(tree).Where(i => GetIsSelected(i)).ToList();
        }

        private static void MakeSingleSelection(TreeView tree, TreeViewItem item)
        {
            foreach (TreeViewItem selectedItem in GetExpandedTreeViewItems(tree))
            {
                if (selectedItem == null)
                    continue;
                if (selectedItem != item)
                    SetIsSelected(selectedItem, false);
                else
                {
                    SetIsSelected(selectedItem, true);
                }
            }
            UpdateAnchorAndActionItem(tree, item);
        }

        public static void UnselectAll(TreeView tree)
        {
            foreach (TreeViewItem selectedItem in GetExpandedTreeViewItems(tree))
            {
                SetIsSelected(selectedItem, false);
            }
        }

        private static void MakeToggleSelection(TreeView tree, TreeViewItem item)
        {
            SetIsSelected(item, !GetIsSelected(item));
            UpdateAnchorAndActionItem(tree, item);
        }

        private static void UpdateAnchorAndActionItem(TreeView tree, TreeViewItem item)
        {
            SetAnchorItem(tree, item);
        }

        public static bool GetIsSelected(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsSelectedProperty);
        }

        public static void SetIsSelected(DependencyObject obj, bool value)
        {
            obj.SetValue(IsSelectedProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsSelected.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.RegisterAttached("IsSelected", typeof(bool), typeof(TreeViewExtension), new PropertyMetadata(false)
            {
                PropertyChangedCallback = RealSelectedChanged
            });
    }
}
