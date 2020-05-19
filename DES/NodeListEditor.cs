﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DES
{
    public class NodeListEditor : UITypeEditor
    {

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            // pop up a dialog
            NodeListEditorForm dlg = new NodeListEditorForm(value);
            dlg.ShowDialog();
            return base.EditValue(context, provider, value);
        }
    }
}
