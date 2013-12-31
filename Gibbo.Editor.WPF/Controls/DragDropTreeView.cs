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
                            //if (draggedNode.Parent == null)
                            //    this.Items.Remove(draggedNode);
                            //else
                            //    if (draggedNode.Parent == this)
                            //        this.Items.Remove(draggedNode);
                            //    else
                            //        (draggedNode.Parent as DragDropTreeViewItem).Items.Remove(draggedNode);
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
                            draggedNode.Style = null;
                            draggedNode.Style = (Style)FindResource("IgniteTreeViewItem");                          
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

        // Determine whether one node is a parent  
        // or ancestor of a second node. 
        private bool ContainsNode(DragDropTreeViewItem node1, DragDropTreeViewItem node2)
        {
            // Check the parent node of the second node. 
            if (node2.Parent == null || node2.Parent == this) return false;
            if (node2.Parent == node1) return true;

            // If the parent node is not null or equal to the first node,  
            // call the ContainsNode method recursively using the parent of  
            // the second node. 
            return ContainsNode(node1, node2.Parent as DragDropTreeViewItem);
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
