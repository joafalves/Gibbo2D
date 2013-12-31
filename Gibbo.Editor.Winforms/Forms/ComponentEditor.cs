using System;
using System.Collections;
using System.Windows.Forms;
using Gibbo.Library;
using ComponentFactory.Krypton.Toolkit;

namespace Gibbo.Editor
{
    public partial class ComponentEditor : KryptonForm
    {
        #region internal classes

        public class NodeSorter : IComparer
        {
            public int Compare(object thisObj, object otherObj)
            {
                TreeNode thisNode = thisObj as TreeNode;
                TreeNode otherNode = otherObj as TreeNode;

                //alphabetically sorting
                return thisNode.Text.CompareTo(otherNode.Text);
            }
        }

        #endregion

        #region fields

        private GameObject gameObject;

        #endregion

        #region constructors

        public ComponentEditor(GameObject gameObject)
        {
            InitializeComponent();

            this.gameObject = gameObject;
            this.Text = gameObject.Name + " - " + this.Text;

            this.bufferedTreeView.TreeViewNodeSorter = new NodeSorter();
        }

        #endregion

        #region methods

        /// <summary>
        /// 
        /// </summary>
        private void FillTreeView()
        {
            bufferedTreeView.Nodes.Clear();

            foreach (ObjectComponent component in gameObject.GetComponents())
            {
                TreeNode node = new TreeNode();
                node.Name = GibboHelper.SplitCamelCase(component.GetType().Name);
                node.Text = node.Name;
                node.Tag = component;

                bufferedTreeView.Nodes.Add(node);
            }

            if (bufferedTreeView.Nodes.Count > 0)
                bufferedTreeView.SelectedNode = bufferedTreeView.Nodes[0];
        }

        #endregion

        #region events

        private void ComponentEditor_Load(object sender, EventArgs e)
        {
            FillTreeView();
        }

        private void bufferedTreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {

        }

        private void removeComponentBtn_Click(object sender, EventArgs e)
        {
            if (bufferedTreeView.SelectedNode == null) return;

            if (MessageBox.Show("Are you sure you want to remove this component?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
            {
                if (propertyGrid1.SelectedObject == bufferedTreeView.SelectedNode.Tag)
                    propertyGrid1.SelectedObject = null;

                gameObject.RemoveComponent((ObjectComponent)bufferedTreeView.SelectedNode.Tag);
                bufferedTreeView.SelectedNode.Remove();
            }
        }

        private void removeAllBtn_Click(object sender, EventArgs e)
        {
            if (bufferedTreeView.SelectedNode == null) return;

            if (MessageBox.Show("Are you sure you want to remove all components?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
            {
                propertyGrid1.SelectedObject = null;
                gameObject.RemoveAllComponents();
                bufferedTreeView.Nodes.Clear();
            }
        }

        private void bufferedTreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            propertyGrid1.SelectedObject = (ObjectComponent)e.Node.Tag;
        }

        #endregion
    }
}
