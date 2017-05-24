using SmartShipment.Adapters.Common;
using SmartShipment.AutomationUI.UIAutomation;

namespace SmartShipment.Adapters.Control
{
    public class ShipmentAutomationTabItem : ShipmentAutomationBase
    {


        public ShipmentAutomationTabItem(IShipmentAutomationControlHelper automationControlHelper)
        {
            AutomationControlHelper = automationControlHelper;                      
        }

        public void Select()
        {
            var selectionPattern = AutomationElement.GetSelectionItemPattern();
            selectionPattern.Select();            
        }               
    }
}