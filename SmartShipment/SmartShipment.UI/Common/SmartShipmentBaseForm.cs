using System;
using System.Windows.Forms;

namespace SmartShipment.UI.Common
{
    public class SmartShipmentBaseForm : Form
    {
        public readonly ApplicationContext ApplicationContext;

        public SmartShipmentBaseForm() { }

        public SmartShipmentBaseForm(ApplicationContext applicationContext)
        {
            ApplicationContext = applicationContext;
        }

        internal void Invoke(Action action)
        {
            action?.Invoke();
        }

        internal void Invoke(Action<object, EventArgs> action, object sender, EventArgs eventArgs)
        {            
            action?.Invoke(sender, eventArgs);
        }

        internal void Invoke(Action<object, MouseEventArgs> action, object sender, MouseEventArgs eventArgs)
        {
            action?.Invoke(sender, eventArgs);
        }
    }
}
