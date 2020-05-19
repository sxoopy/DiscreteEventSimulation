using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace DES
{
    public class TimedQueueEditor : UITypeEditor
    {
        static ComboBox cbxOptions = new ComboBox();
        static TimedQueueEditor()
        {
            cbxOptions.Items.Add("Timed Queue");
            cbxOptions.Items.Add("Priority Queue");
        }
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService editorService;
            editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            TimedQueue theQueue = (TimedQueue)value;
            if (theQueue.CapacityLimitEnabled)
                cbxOptions.SelectedIndex = 1;
            else if (value is TimedQueue)
                cbxOptions.SelectedIndex = 0;

            editorService.DropDownControl(cbxOptions);

            TimedQueue newQueue;
            switch (cbxOptions.SelectedIndex)
            {
                case 0: // General Queue
                    if (!(value is TimedQueue))
                    {
                        newQueue = new TimedQueue();
                        newQueue.CapacityLimitEnabled = false;
                        return newQueue;
                    }
                    break;
                case 1: // Capacitied Queue
                    if (!(value is TimedQueue))
                    {
                        newQueue = new TimedQueue();
                        newQueue.CapacityLimitEnabled = true;
                        return newQueue;
                    }
                    break;
                case 2: // Priority Queue
                    break;
            }
            return base.EditValue(context, provider, value);
        }
    } 
}