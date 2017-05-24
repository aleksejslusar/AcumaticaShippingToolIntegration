using SmartShipment.Adapters.Common;
using SmartShipment.Information;

namespace SmartShipment.Adapters.Control
{
    public class ShipmentAutomationPane : ShipmentAutomationControl
    {
        public ShipmentAutomationPane(IShipmentAutomationControlHelper automationControlHelper, ISmartShipmentMessagesProvider messagesProvider) : base(messagesProvider)
        {
            AutomationControlHelper = automationControlHelper;
        }

        protected override void SetControlValue()
        {
            if (MaxLength > 0 && Value.Length > MaxLength)
            {
                Value = Value.Substring(0, MaxLength);
            }
            AutomationControlHelper.Text(this, Value);
        }

        protected override string GetcontrolValue()
        {
            return AutomationControlHelper.Text(this);
        }
    }
}