using System.Windows.Automation;
using SmartShipment.Adapters.Common;
using SmartShipment.Adapters.Control;
using SmartShipment.Information;
using SmartShipment.Network.Mapping;

namespace SmartShipment.Adapters.Map
{
    public class FedExMultiPieceShipmentMap : ShipmentAutomationMapBase
    {
        public ShipmentAutomationControl PrintLabelsAfterCompleteShipment;
        private ShipmentAutomationControl _packageWeight;
        private ShipmentAutomationControl _packageDeclaredValue;
        public ShipmentAutomationButton PackageAddButton;
        public ShipmentAutomationButton PackageSaveAndCloseButton;

        public FedExMultiPieceShipmentMap(IShipmentAutomationControlHelper automationControlHelper, AutomationElement rootAutomationElement, ISmartShipmentMessagesProvider messagesProvider) : base(automationControlHelper, rootAutomationElement, messagesProvider)
        {
                       
        }

        protected override void InitAutomationControls()
        {
            PrintLabelsAfterCompleteShipment = new ShipmentAutomationCheckBox(AutomationControlHelper, MessagesProvider)
            {
                Name = "Print labels after I have completed this shipment",
                AutomaitonId = "chkPrintLabelsAfter",               
                ShipmentDataType = ShipmentDataType.Package,
                ValidateFunc = s => s == PrintLabelsAfterCompleteShipment.Value,
                Order = 0
            };

            _packageWeight = new ShipmentAutomationPane(AutomationControlHelper, MessagesProvider)
            {
                Name = "Package Weight",
                AutomaitonId = "edtWeight",
                DataFieldName = nameof(ShipmentPackage.PackageWeight),
                ShipmentDataType = ShipmentDataType.Package,
                ValidateFunc = s => s == _packageWeight.Value,
                Order = 0
            };

            _packageDeclaredValue = new ShipmentAutomationPane(AutomationControlHelper, MessagesProvider)
            {
                Name = "Declared value",
                AutomaitonId = "edtValue",
                DataFieldName = nameof(ShipmentPackage.DeclaredValue),
                ShipmentDataType = ShipmentDataType.Package,
                ValidateFunc = s => s == _packageDeclaredValue.Value,
                Order = 0
            };

            PackageAddButton = new ShipmentAutomationButton(AutomationControlHelper)
            {
                Name = "Add",
                AutomaitonId = "btnAddOrShipPackage",               
            };

            PackageSaveAndCloseButton = new ShipmentAutomationButton(AutomationControlHelper)
            {
                Name = "Save & Exit",
                AutomaitonId = "btnSaveAndExit"
            };

            ControlsList.Add(PrintLabelsAfterCompleteShipment);
            ControlsList.Add(_packageWeight);
            ControlsList.Add(_packageDeclaredValue);
            ControlsList.Add(PackageAddButton);
            ControlsList.Add(PackageSaveAndCloseButton);
        }

        
    }
}