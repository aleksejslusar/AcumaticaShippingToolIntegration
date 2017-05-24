using System;
using SmartShipment.Adapters.Common;
using SmartShipment.Information;

namespace SmartShipment.Adapters.Control
{
    public class ShipmentAutomationComboBox : ShipmentAutomationControl
    {
        public ShipmentAutomationComboBox(IShipmentAutomationControlHelper automationControlHelper, ISmartShipmentMessagesProvider messagesProvider) : base(messagesProvider)
        {
            AutomationControlHelper = automationControlHelper;
        }

        protected override void SetControlValue()
        {
            AutomationControlHelper.Selection(this, Value);
        }

        protected override string GetcontrolValue()
        {
            return AutomationControlHelper.Selection(this);
        }
    }
}