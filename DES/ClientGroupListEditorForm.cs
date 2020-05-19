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
    public partial class ClientGroupListEditorForm : Form
    {
        public static DESmodel TheModel;
        List<ClientGroup> theList;

        public ClientGroupListEditorForm(object value)
        {
            theList = (List<ClientGroup>)value;

            InitializeComponent();

            if (TheModel != null)
                cbxItinerary.Items.AddRange(TheModel.Itineraries.ToArray());
            if (cbxItinerary.Items.Count > 0)
                cbxItinerary.SelectedIndex = 0;
            foreach (ClientGroup group in theList)
            {
                lsbClientGroups.Items.Add(group);
            }
        }

        private void lsbClientGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRemove.Enabled = lsbClientGroups.SelectedIndex >= 0 ? true : false;
            ppgClientGroup.SelectedObject = lsbClientGroups.SelectedItem;
        }

        private void cbxItinerary_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnAdd.Enabled = cbxItinerary.SelectedIndex >= 0 ? true : false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ClientGroup aGroup;
            if (cbxItinerary.SelectedItem == null)
                return;
            aGroup = new ClientGroup((Itinerary)cbxItinerary.SelectedItem);
            lsbClientGroups.Items.Add(aGroup);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            theList.Clear();
            foreach (object o in lsbClientGroups.Items)
            {
                theList.Add((ClientGroup)o);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {

        }

        private void ppgClientGroup_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            lsbClientGroups.Items[lsbClientGroups.SelectedIndex] = ppgClientGroup.SelectedObject;
        }
    }
}
