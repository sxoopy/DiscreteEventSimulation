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
    public partial class ItineraryItemEditorForm : Form
    {
        public static DESmodel TheModel;
        List<ItineraryItem> theItemList;

        public ItineraryItemEditorForm(object value)
        {
            theItemList = (List<ItineraryItem>)value;

            InitializeComponent();

            // populate exisiting items
            foreach (ItineraryItem ii in theItemList)
            {
                lsbItem.Items.Add(ii);
            }

            // prepare list of nodes
            if (TheModel != null)
            {
                foreach (ServiceNode sn in TheModel.Nodes)
                {
                    cbxNodes.Items.Add(sn);
                }
            }

            // prepare list of RVG types
            cbxRVGTypes.Items.Add("None");
            cbxRVGTypes.Items.Add("Exponential");
            cbxRVGTypes.Items.Add("Uniform");
            cbxRVGTypes.Items.Add("Normal");
            cbxRVGTypes.SelectedIndex = 0;
        }

        private void lsbItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsbItem.SelectedIndex >= 0)
            {
                btnRemove.Enabled = true;
                ppgItem.SelectedObject = lsbItem.SelectedItem; ;
            }
            else
            {
                btnRemove.Enabled = false;
                ppgItem.SelectedObject = null;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ItineraryItem item = new ItineraryItem((ServiceNode)cbxNodes.SelectedItem);
            switch (cbxRVGTypes.SelectedIndex)
            {
                case 0: // None
                    break;
                case 1: // Exp
                    item.ServiceTimeGenerator = new ExponentialRVG(1.0);
                    break;
                case 2: // Uni
                    item.ServiceTimeGenerator = new UniformRVG(1.0, 3.0);
                    break;
                case 3: // Normal
                    // mean = 2.0, std = 0.5
                    // lb = 0.01
                    break;
            }
            lsbItem.Items.Add(item);
        }

        private void cbxNodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnAdd.Enabled = cbxNodes.SelectedIndex < 0 ? false : true;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            lsbItem.Items.Remove(lsbItem.SelectedItem);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            theItemList.Clear();
            foreach (object o in lsbItem.Items)
            {
                theItemList.Add((ItineraryItem)o);
            }
        }
    }
}
