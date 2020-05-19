using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace DES
{
    public class RandomRVGEditor : UITypeEditor
    {
        static ComboBox cbxRVGType;

        /// <summary>
        /// Constructor of RandomRVGEditor
        /// </summary>
        static RandomRVGEditor()
        {
            cbxRVGType = new ComboBox();
            cbxRVGType.Items.Add(ContinousRVGType.Exp);
            cbxRVGType.Items.Add(ContinousRVGType.Uni);
            cbxRVGType.SelectedIndex = 0;
        }
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService service;
            service = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            service.DropDownControl(cbxRVGType);

            switch ((ContinousRVGType)cbxRVGType.SelectedItem)
            {
                case ContinousRVGType.Exp:
                    if (!(value is ExponentialRVG))
                        value = new ExponentialRVG(0.8);
                    break;
                case ContinousRVGType.Uni:
                    if (!(value is UniformRVG))
                        value = new UniformRVG(0, 5);
                    break;
            }
            //RandomRVGEditorForm dlg = new RandomRVGEditorForm(value);
            //dlg.ShowDialog();
            return value;
        }
    }
}
