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
    public partial class ServerListEditorForm : Form
    {
        EventHookList<Server> theList;
        Server[] originalServers;
        public ServerListEditorForm(object value)
        {
            InitializeComponent();
            theList = (EventHookList<Server>)value;
            originalServers = new Server[theList.Count];
            for (int i = 0; i < theList.Count; i++)
            {
                originalServers[i] = theList[i];
                lsbServer.Items.Add(theList[i]);
            }           
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

        }

        private void lsbServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsbServer.SelectedIndex >= 0)
            {
                btnRemove.Enabled = true;               
            }
            else
            {
                btnRemove.Enabled = false;
            }
            ppgServer.SelectedObject = lsbServer.SelectedItem;
        }

        private void ppgServer_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            lsbServer.Items[lsbServer.SelectedIndex] = ppgServer.SelectedObject;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Server s = new Server();
            theList.Add(s); // key point to let the newly added server generate target queue change event
            lsbServer.Items.Add(s);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            object o = lsbServer.SelectedItem;
            lsbServer.Items.Remove(o);
            theList.Remove((Server)o);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (this.DialogResult == DialogResult.Cancel)
            {
                theList.Clear();
                for (int i = 0; i < originalServers.Length; i++)
                {
                    theList.Add(originalServers[i]);
                }
            }
        }
    }
}
