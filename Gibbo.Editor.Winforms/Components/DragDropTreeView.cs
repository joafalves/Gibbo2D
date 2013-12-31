using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Gibbo.Editor
{
    class DragDropTreeView : BufferedTreeView
    {
        public delegate void DragNotificationHandler(TreeNode source, TreeNode target, CancelEventArgs e);
        public event DragNotificationHandler OnDragDropSuccess;

        public DragDropTreeView()
        {
            AllowDrop = true;
        }

        protected override void OnDragDrop(DragEventArgs e)
        {
            base.OnDragDrop(e);

            // Retrieve the client coordinates of the drop location.
            Point targetPoint = PointToClient(new Point(e.X, e.Y));

            // Retrieve the node at the drop location.
            TreeNode targetNode = GetNodeAt(targetPoint);

            // Retrieve the node that was dragged.
            TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));

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
                    if (e.Effect == DragDropEffects.Move)
                    {
                        draggedNode.Remove();
                        targetNode.Nodes.Add(draggedNode);
                    }

                    // If it is a copy operation, clone the dragged node  
                    // and add it to the node at the drop location. 
                    else if (e.Effect == DragDropEffects.Copy)
                    {
                        targetNode.Nodes.Add((TreeNode)draggedNode.Clone());
                    }

                    // Expand the node at the location  
                    // to show the dropped node.
                    targetNode.Expand();
                }
            }
        }

        // Determine whether one node is a parent  
        // or ancestor of a second node. 
        private bool ContainsNode(TreeNode node1, TreeNode node2)
        {
            // Check the parent node of the second node. 
            if (node2.Parent == null) return false;
            if (node2.Parent.Equals(node1)) return true;

            // If the parent node is not null or equal to the first node,  
            // call the ContainsNode method recursively using the parent of  
            // the second node. 
            return ContainsNode(node1, node2.Parent);
        }

        protected override void OnDragOver(DragEventArgs e)
        {
            base.OnDragOver(e);

            // Retrieve the client coordinates of the mouse position.
            Point targetPoint = PointToClient(new Point(e.X, e.Y));

            // Select the node at the mouse position.
            SelectedNode = GetNodeAt(targetPoint);
        }

        protected override void OnDragEnter(DragEventArgs e)
        {
            base.OnDragEnter(e);

            e.Effect = e.AllowedEffect;
        }

        protected override void OnItemDrag(ItemDragEventArgs e)
        {
            base.OnItemDrag(e);

            // Move the dragged node when the left mouse button is used. 
            if (e.Button == MouseButtons.Left)
            {
                DoDragDrop(e.Item, DragDropEffects.Move);
            }

            // Copy the dragged node when the right mouse button is used. 
            else if (e.Button == MouseButtons.Right)
            {
                DoDragDrop(e.Item, DragDropEffects.Copy);
            }
        }
    }
}
