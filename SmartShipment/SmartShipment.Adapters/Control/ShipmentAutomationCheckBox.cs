using SmartShipment.Adapters.Common;
using SmartShipment.Information;

namespace SmartShipment.Adapters.Control
{
    public class ShipmentAutomationCheckBox : ShipmentAutomationControl
    {
        public ShipmentAutomationCheckBox(IShipmentAutomationControlHelper automationControlHelper, ISmartShipmentMessagesProvider messagesProvider) : base(messagesProvider)
        {
            AutomationControlHelper = automationControlHelper;
        }

        protected override void SetControlValue()
        {
            bool value;
            bool.TryParse(Value.ToLower(), out value);
            AutomationControlHelper.Checked(this, value);
        }

        protected override string GetcontrolValue()
        {
            return AutomationControlHelper.Checked(this).ToString().ToLower();
        }
    }
}