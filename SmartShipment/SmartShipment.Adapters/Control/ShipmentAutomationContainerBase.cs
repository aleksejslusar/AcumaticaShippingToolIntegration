using SmartShipment.Adapters.Common;

namespace SmartShipment.Adapters.Control
{
    public class ShipmentAutomationContainerBase : ShipmentAutomationBase
    {
        public ShipmentAutomationContainerBase(IShipmentAutomationControlHelper automationControlHelper)
        {
            AutomationControlHelper = automationControlHelper;            
        }
    }
}