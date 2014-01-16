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
