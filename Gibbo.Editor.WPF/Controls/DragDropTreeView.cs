using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Gibbo.Editor.WPF
{
    class DragDropTreeView : TreeView
    {
        public delegate void DragNotificationHandler(DragDropTreeViewItem source, DragDropTreeViewItem target, CancelEventArgs e);
        public event DragNotificationHandler OnDragDropSuccess;

        public DragDropTreeView()
        {
            this.AllowDrop = true;
        }

        protected override void OnDragOver(DragEventArgs e)
        {
   
        }

        protected override void OnDrop(DragEventArgs e)
        {
            base.OnDrop(e);
            
            // Retrieve the client coordinates of the drop location.
            Point targetPoint = e.GetPosition(this);

            try
            {
                // Retrieve the node at the drop location.
                DragDropTreeViewItem targetNode = GetNearestContainer(e.Source as UIElement);

                DragDropHelper.RemoveInsertionAdorner();
                
                // Retrieve the node that was dragged.
                DragDropTreeViewItem draggedNode = (DragDropTreeViewItem)e.Data.GetData(typeof(DragDropTreeViewItem));
                
                if(draggedNode == null)
                    draggedNode = (DragDropTreeViewItem)e.Data.GetData(typeof(ExplorerTreeViewItem));

                if (targetNode == null || draggedNode == null) return;

                // Confirm that the node at the drop location is not  
                // the dragged node or a descendant of the dragged node. 
                if (!draggedNode.Equals(targetNode) && !ContainsNode(draggedNode, targetNode))
                {
                    CancelEventArgs evt = new CancelEventArgs();
                    OnDragDropSuccess(draggedNode, targetNode, evt);

                    if (!evt.Cancel)
                    {
                        // If it is a move operation, remove the node from its current  
                        // location and add it to the node at the drop location. 
                        if (e.Effects == DragDropEffects.Move)
                        {
                            List<TreeViewItem> items = TreeViewExtension.GetSelectedTreeViewItems(TreeViewExtension.GetTree(targetNode));

                            if (items != null && items.Count > 0)
                            {
                                // multi selection drag:
                                foreach (var ti in items)
                                {
                                    (ti.Parent as ItemsControl).Items.Remove(ti);

                                    if (DragDropHelper.insertionPlace == DragDropHelper.InsertionPlace.Center)
                                    {
                                        targetNode.Items.Add(ti);
                                    }
                                    else
                                    {
                                        int index = (targetNode.Parent as ItemsControl).ItemContainerGenerator.IndexFromContainer(targetNode);
                                        if (index < 0) index = 0;

                                        if (DragDropHelper.insertionPlace == DragDropHelper.InsertionPlace.Bottom)
                                            index++;

                                        (targetNode.Parent as ItemsControl).Items.Insert(index, ti);
                                    }

                                    ti.IsSelected = true;

                                    ReApplyStyle(ti as DragDropTreeViewItem, "IgniteMultiTreeViewItem");
                                }
                            }
                            else
                            {
                                (draggedNode.Parent as ItemsControl).Items.Remove(draggedNode);

                                if (DragDropHelper.insertionPlace == DragDropHelper.InsertionPlace.Center)
                                {
                                    targetNode.Items.Add(draggedNode);
                                }
                                else
                                {
                                    int index = (targetNode.Parent as ItemsControl).ItemContainerGenerator.IndexFromContainer(targetNode);
                                    if (index < 0) index = 0;

                                    if (DragDropHelper.insertionPlace == DragDropHelper.InsertionPlace.Bottom)
                                        index++;

                                    (targetNode.Parent as ItemsControl).Items.Insert(index, draggedNode);
                                }

                                draggedNode.IsSelected = true;

                                ReApplyStyle(draggedNode, "IgniteTreeViewItem");
                            }
                        }
                        // OPTIONAL:
                        // If it is a copy operation, clone the dragged node  
                        // and add it to the node at the drop location. 
                        //else if (e.Effects == DragDropEffects.Copy)
                        //{
                        //    targetNode.Items.Add((DragDropTreeViewItem)draggedNode);
                        //}

                        // Expand the node at the location  
                        // to show the dropped node.
                        targetNode.IsExpanded = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void ReApplyStyle(DragDropTreeViewItem ti, string styleName)
        {
            ti.Style = null;
            ti.Style = (Style)FindResource(styleName);

            foreach (var t in ti.Items)
            {
                ReApplyStyle(t as DragDropTreeViewItem, styleName);
            }
        }

        // Determine whether one node is a parent  
        // or ancestor of a second node. 
        private bool ContainsNode(DragDropTreeViewItem node1, DragDropTreeViewItem node2)
        {
            return DragDropTreeView.TreeContainsNode(this, node1, node2);
        }

        public static bool TreeContainsNode(TreeView tv, DragDropTreeViewItem node1, DragDropTreeViewItem node2)
        {
            // Check the parent node of the second node. 
            if (node2.Parent == null || node2.Parent == tv) return false;
            if (node2.Parent == node1) return true;

            // If the parent node is not null or equal to the first node,  
            // call the ContainsNode method recursively using the parent of  
            // the second node. 
            return TreeContainsNode(tv, node1, node2.Parent as DragDropTreeViewItem);
        }

        protected override void OnDragEnter(DragEventArgs e)
        {
            base.OnDragEnter(e);

            //e.Effects = DragDropEffects.Link;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            
            // Move the dragged node when the left mouse button is used. 
            //if (e.LeftButton == MouseButtonState.Pressed && this.SelectedItem != null)
            //{
            //    DragDrop.DoDragDrop(this, this.SelectedItem, DragDropEffects.Move);
            //}

            //// Copy the dragged node when the right mouse button is used. 
            //else if (e.Button == MouseButtons.Right)
            //{
            //    DragDrop.DoDragDrop(e.Item, DragDropEffects.Copy);
            //}
        }

        private DragDropTreeViewItem GetNearestContainer(UIElement element)
        {
            // Walk up the element tree to the nearest tree view item.
            DragDropTreeViewItem container = element as DragDropTreeViewItem;
            while ((container == null) && (element != null))
            {
                element = VisualTreeHelper.GetParent(element) as UIElement;
                container = element as DragDropTreeViewItem;
            }
            return container;
        }
    }
}
