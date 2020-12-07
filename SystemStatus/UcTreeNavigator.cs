using System;
using System.Windows.Forms;

namespace MultiFilling.SystemStatus
{
    public partial class UcTreeNavigator : UserControl
    {
        public void SelectNode(string nodeKey)
        {
            var nodes = tvNavigator.Nodes.Find(nodeKey, true);
            tvNavigator.SelectedNode = nodes.Length > 0 ? nodes[0] : null;
        }

        public UcTreeNavigator()
        {
            InitializeComponent();
        }

        private void TreeNavigatorUc_Load(object sender, EventArgs e)
        {
            tvNavigator.ExpandAll();
        }

        private void tvNavigator_MouseUp(object sender, MouseEventArgs e)
        {
            var node = tvNavigator.GetNodeAt(e.Location);
            tvNavigator.SelectedNode = node;
            if (node == null) return;
            if (node.Nodes.Count == 0)
            {
                var parent = Parent;
                while (parent != null)
                {
                    if (parent is Form)
                    {
                        Data.Navigate(GetType(), new NavigateTreeArgs
                            {
                                Panel = parent, 
                                NodeName = node.Name
                            });
                        break;
                    }
                    parent = parent.Parent;
                }
            }
            else
            {
                if (node.IsExpanded) node.Collapse();
                else node.Expand();
            }
        }
    }
}
