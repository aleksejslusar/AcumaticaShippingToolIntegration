using System.Windows.Automation;
using SmartShipment.Adapters.Common;
using SmartShipment.Adapters.Control;
using SmartShipment.Information;
using SmartShipment.Network.Mapping;

namespace SmartShipment.Adapters.Map
{
    public class FedExShipAlertTabMap : ShipmentAutomationMapBase
    {        
        public ShipmentAutomationControl Email;

        public FedExShipAlertTabMap(IShipmentAutomationControlHelper automationControlHelper, AutomationElement rootAutomationElement, ISmartShipmentMessagesProvider messagesProvider) : base(automationControlHelper, rootAutomationElement, messagesProvider)
        {
     
        }

        protected override void InitAutomationControls()
        {
            Email = new ShipmentAutomationPane(AutomationControlHelper, MessagesProvider)
            {
                Name = "Email",                
                DescendentIdPath = new []{ "shipAlert", "flowLayoutPanelShipAlert","panelShipAlertTop","edtRecipientEmail" },
                DataFieldName = nameof(ShipmentContact.Email),
                ShipmentDataType = ShipmentDataType.Shipment,
                ValidateFunc = s => s == Email.Value,
                Order = 9
            };

            ControlsList.Add(Email);
        }
    }
}