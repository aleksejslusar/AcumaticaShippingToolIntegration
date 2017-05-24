using System;
using Microsoft.Test.Input;
using SmartShipment.Adapters.Common;
using SmartShipment.Information;

namespace SmartShipment.Adapters.Control
{
    public class ShipmentAutomationListBox : ShipmentAutomationControl
    {
        public ShipmentAutomationListBox(IShipmentAutomationControlHelper automationControlHelper, ISmartShipmentMessagesProvider messagesProvider) : base(messagesProvider)
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