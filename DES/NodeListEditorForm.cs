using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DES
{
    public partial class NodeListEditorForm : Form
    {
        List<ServiceNode> theList;
        public NodeListEditorForm(object value)
        {
            theList = (List<ServiceNode>) value;
            InitializeComponent();

            // populate list of nodes
            foreach (ServiceNode sn in theList)
            {
                lsbNode.Items.Add(sn);
            }

            // populate combobox
            cbxNodeType.Items.Add(ServiceNodeType.SingleQueueNode);
            cbxNodeType.Items.Add(ServiceNodeType.MultipleQueueNode);
            cbxNodeType.SelectedIndex = 0;
        }

        private void lsbNode_SelectedIndexChanged(object sender, EventArgs e)
        {
            propertyGrid.SelectedObject = lsbNode.SelectedItem;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ServiceNode aNode = null;
            switch ((ServiceNodeType)cbxNodeType.SelectedItem)
            {
                case ServiceNodeType.SingleQueueNode:
                    aNode = new SingleQueueServiceNode(1);
                    break;
                case ServiceNodeType.MultipleQueueNode:
                    aNode = new MultiQueueServiceNode(2);
                    break;
            }
            lsbNode.Items.Add(aNode);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            theList.Clear();
            foreach (object obj in lsbNode.Items)
            {
                theList.Add((ServiceNode)obj);
            }
        }
      
    }
}
