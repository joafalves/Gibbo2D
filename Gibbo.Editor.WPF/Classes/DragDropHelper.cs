using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;

namespace Gibbo.Editor.WPF
{
    internal static class DragDropHelper
    {
        internal enum InsertionPlace { Top, Center, Bottom };

        internal static InsertionPlace insertionPlace;
        internal static InsertionAdorner insertionAdorner;

        public static void CreateInsertionAdorner(DragDropTreeViewItem target, bool firstHalf)
        {
            if (target != null && insertionAdorner == null && !(target is ExplorerTreeViewItem))
            {
                // Here, I need to get adorner layer from targetItemContainer and not targetItemsControl. 
                // This way I get the AdornerLayer within ScrollContentPresenter, and not the one under AdornerDecorator (Snoop is awesome).
                // If I used targetItemsControl, the adorner would hang out of ItemsControl when there's a horizontal scroll bar.
                var adornerLayer = AdornerLayer.GetAdornerLayer(target);
                insertionAdorner = new InsertionAdorner(true, firstHalf, target, adornerLayer);
                
            }
        }

        public static void RemoveInsertionAdorner()
        {
            if (insertionAdorner != null)
            {
                insertionAdorner.Detach();
                insertionAdorner = null;
            }
        }
    }
}
