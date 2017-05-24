using SmartShipment.Adapters.Common;

namespace SmartShipment.Adapters.Control
{
    public class ShipmentAutomationButton : ShipmentAutomationBase, IShipmentAutomationButton
    {
        public ShipmentAutomationButton(IShipmentAutomationControlHelper automationControlHelper)
        {
            AutomationControlHelper = automationControlHelper;
        }

        public void Click()
        {
            AutomationControlHelper.Click(this);
        }
               
    }
}