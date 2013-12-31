using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Gibbo.Editor.WPF
{
    class DragDropTreeViewItem : TreeViewItem
    {
        internal bool CanDrag = true;
        internal string TagText = string.Empty;
       
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            HitTestResult result = VisualTreeHelper.HitTest(this, e.GetPosition(this));

            // Move the dragged node when the left mouse button is used. EditorUtils.GetParent(result.VisualHit, 2) == 
            if (e.LeftButton == MouseButtonState.Pressed && CanDrag && (result.VisualHit as UIElement) != null && (result.VisualHit as UIElement).IsDescendantOf(this))
            {
                DragDrop.DoDragDrop(this, this, DragDropEffects.Move);
            }

            DragDropTreeViewItem targetNode = GetNearestContainer(e.Source as UIElement);
            if (targetNode == null)
                DragDropHelper.RemoveInsertionAdorner();
            //Console.WriteLine(this.PointFromScreen(Mouse.GetPosition(targetNode)));
        }

        protected override void OnDragOver(DragEventArgs e)
        {
            base.OnDragEnter(e);

            HitTestResult result = VisualTreeHelper.HitTest(this, e.GetPosition(this));

            if ((result.VisualHit as UIElement).IsDescendantOf(this))
            {
                UIElement element = (result.VisualHit as UIElement);
                DragDropTreeViewItem targetNode = GetNearestContainer(e.Source as UIElement);
                if (e.GetPosition(targetNode).Y < targetNode.ActualHeight * 0.2f)
                {
                    // meter em cima
                    //Console.WriteLine("cima");
                    DragDropHelper.insertionPlace = DragDropHelper.InsertionPlace.Top;
                    DragDropHelper.CreateInsertionAdorner(targetNode, true);
                }
                else if (e.GetPosition(targetNode).Y > targetNode.ActualHeight * 0.8f)
                {
                    //Console.WriteLine("baixo");
                    DragDropHelper.insertionPlace = DragDropHelper.InsertionPlace.Bottom;
                    DragDropHelper.CreateInsertionAdorner(targetNode, false);
                }
                else
                {
                    //Console.WriteLine("centro");
                    DragDropHelper.RemoveInsertionAdorner();
                    DragDropHelper.insertionPlace = DragDropHelper.InsertionPlace.Center;
                    var converter = new System.Windows.Media.BrushConverter();
                    Background = (Brush)converter.ConvertFromString("#555");
                }
            }
        }

        protected override void OnDragEnter(DragEventArgs e)
        {
            base.OnDragEnter(e);

            //HitTestResult result = VisualTreeHelper.HitTest(this, e.GetPosition(this));

            //if ((result.VisualHit as UIElement).IsDescendantOf(this))
            //{
            //    UIElement element = (result.VisualHit as UIElement);
            //    DragDropTreeViewItem targetNode = GetNearestContainer(e.Source as UIElement);
            //    if (e.GetPosition(targetNode).Y < targetNode.ActualHeight * 0.2f)
            //    {
            //        // meter em cima
            //        //Console.WriteLine("cima");
            //        DragDropHelper.insertionPlace = DragDropHelper.InsertionPlace.Top;
            //        DragDropHelper.CreateInsertionAdorner(targetNode, true);
            //    }
            //    else if (e.GetPosition(targetNode).Y > targetNode.ActualHeight * 0.8f)
            //    {
            //        //Console.WriteLine("baixo");
            //        DragDropHelper.insertionPlace = DragDropHelper.InsertionPlace.Bottom;
            //        DragDropHelper.CreateInsertionAdorner(targetNode, false);
            //    }
            //    else
            //    {
            //        //Console.WriteLine("centro");
            //        DragDropHelper.insertionPlace = DragDropHelper.InsertionPlace.Center;
            //        var converter = new System.Windows.Media.BrushConverter();
            //        Background = (Brush)converter.ConvertFromString("#555");
            //    }
            //}

            //DragDropTreeViewItem targetNode = GetNearestContainer(e.Source as UIElement);
            //Console.WriteLine("op2: " + e.GetPosition(targetNode));
            //Mouse.GetPosition(this); ve se da algo de jeito deve dar, le a info eu sei fui eu que fiz -mas. a-cho qeu nao dava vou ver
            //Console.WriteLine(this.PointToScreen(Mouse.GetPosition(targetNode)));
            //Console.WriteLine("op: " + Mouse.GetPosition(targetNode));
          
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            Background = Brushes.Transparent;
            DragDropHelper.RemoveInsertionAdorner();
        }

        protected override void OnDragLeave(DragEventArgs e)
        {
            base.OnDragLeave(e);
            Background = Brushes.Transparent;
            DragDropHelper.RemoveInsertionAdorner();
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
