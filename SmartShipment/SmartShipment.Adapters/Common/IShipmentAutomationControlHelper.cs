using System.Windows.Automation;
using SmartShipment.Adapters.Control;

namespace SmartShipment.Adapters.Common
{
    public interface IShipmentAutomationControlHelper
    {
        //Edit, Pane
        void Text(IShipmentAutomationControl control, string text);
        string Text(IShipmentAutomationControl control);
        //Combobox
        void Selection(IShipmentAutomationControl control, string text);
        string Selection(IShipmentAutomationControl control);
        //Checkbox
        void Checked(IShipmentAutomationControl control, bool value);
        bool Checked(IShipmentAutomationControl control);
        //Button
        void Click(IShipmentAutomationButton control);
        void CloseModalWindowsWait(AutomationElement mainWindow);
        bool IsMainWindowReadyForUserInteraction(AutomationElement mainWindow);
    }
}