using System.Text.RegularExpressions;
using System.Windows.Automation;
using Microsoft.Test.Input;
using SmartShipment.Adapters.Common;
using SmartShipment.Adapters.Control;
using SmartShipment.Information;
using SmartShipment.Network.Mapping;

namespace SmartShipment.Adapters.Map
{
    public class UpsManagerMap : ShipmentAutomationMapBase
    {        
        public ShipmentAutomationControl UpdateAddressBookCheckBox;
        public ShipmentAutomationCheckBox SaturdayDeliveryCheckBox;
        private ShipmentAutomationControl _customerIdPane;
        private ShipmentAutomationControl _companyOrNameIdPane;
        private ShipmentAutomationControl _countryPane;
        private ShipmentAutomationControl _statePane;
        private ShipmentAutomationControl _cityPane;
        private ShipmentAutomationControl _attentionPane;
        private ShipmentAutomationControl _addressOnePane;
        private ShipmentAutomationControl _addressTwoPane;
        public ShipmentAutomationControl TelephonePane;
        private ShipmentAutomationControl _emailAddressPane;
        public ShipmentAutomationControl PostalCodePane;
        public ShipmentAutomationControl PackageWeightPane;
        public ShipmentAutomationControl ReferenceNumberOnePane;
        private ShipmentAutomationControl _referenceNumberTwoPane;
        public ShipmentAutomationButton AddPackageButton;
        public ShipmentAutomationListBox UpsServiceComboBox;
        public ShipmentAutomationPane GeneralDescOfGoodsPane;

        public UpsManagerMap(IShipmentAutomationControlHelper automationControlHelper, AutomationElement rootAutomationElement, ISmartShipmentMessagesProvider messagesProvider) : base(automationControlHelper, rootAutomationElement, messagesProvider)
        {
        }

        protected override void InitAutomationControls()
        {            
            UpdateAddressBookCheckBox = new ShipmentAutomationCheckBox(AutomationControlHelper, MessagesProvider)
            {
                Name = "Update Address Book",
                AutomaitonId = "13163",
                AutomationControlType = "ControlType.CheckBox",
                IsTypedInputRequired = true,
                ShipmentDataType = ShipmentDataType.Shipment,
                ValidateFunc = s => s == UpdateAddressBookCheckBox.Value,
                Order = 0
            };

            SaturdayDeliveryCheckBox = new ShipmentAutomationCheckBox(AutomationControlHelper, MessagesProvider)
            {
                Name = "Saturday Delivery",
                AutomaitonId = "14506",
                AutomationControlType = "ControlType.CheckBox",
                IsTypedInputRequired = true,
                ShipmentDataType = ShipmentDataType.Shipment,
                ValidateFunc = s => s == SaturdayDeliveryCheckBox.Value,
                Order = 0
            };

            
            _customerIdPane = new ShipmentAutomationPane(AutomationControlHelper, MessagesProvider)
            {
                Name = "Customer ID",
                AutomaitonId = "14045",
                AutomationControlType = "ControlType.Pane",
                DataFieldName = nameof(ShipmentMapper.CustomerId),
                ShipmentDataType = ShipmentDataType.Shipment,
                ValidateFunc = s => s == _customerIdPane.Value,
                Order = 1
            };

            _companyOrNameIdPane = new ShipmentAutomationComboBox(AutomationControlHelper, MessagesProvider)
            {
                Name = "Company or Name",
                AutomaitonId = "13102",
                AutomationControlType = "ControlType.Pane",                
                IsFocusedInputRequired = true,
                IsValueRequired = true,
                DataFieldName = nameof(ShipmentContact.DisplayName),
                ShipmentDataType = ShipmentDataType.Shipment,
                ValidateFunc = s => s == _companyOrNameIdPane.Value,
                Order = 2
            };

            _attentionPane = new ShipmentAutomationPane(AutomationControlHelper, MessagesProvider)
            {
                Name = "Attention",
                AutomaitonId = "13105",
                AutomationControlType = "ControlType.Pane",
                DataFieldName = nameof(ShipmentContact.Position),
                ShipmentDataType = ShipmentDataType.Shipment,
                ValidateFunc = s => s == _attentionPane.Value,
                Order = 3
            };

            _addressOnePane = new ShipmentAutomationPane(AutomationControlHelper, MessagesProvider)
            {
                Name = "Address 1",
                AutomaitonId = "13107",
                AutomationControlType = "ControlType.Pane",
                IsValueRequired = true,
                DataFieldName = nameof(ShipmentAddress.AddressLine1),
                ShipmentDataType = ShipmentDataType.Shipment,
                ValidateFunc = s => s == _addressOnePane.Value,
                Order = 4
            };

            _addressTwoPane = new ShipmentAutomationPane(AutomationControlHelper, MessagesProvider)
            {
                Name = "Address 2",
                AutomaitonId = "13108",
                AutomationControlType = "ControlType.Pane",
                DataFieldName = nameof(ShipmentAddress.AddressLine2),
                ShipmentDataType = ShipmentDataType.Shipment,
                ValidateFunc = s => s == _addressTwoPane.Value,
                Order = 5
            };

            _countryPane = new ShipmentAutomationListBox(AutomationControlHelper, MessagesProvider)
            {
                Name = "",
                AutomaitonId = "13113",
                AutomationControlType = "ControlType.Combobox",
                IsValueRequired = true,
                DataFieldName = nameof(ShipmentAddress.CountryName),
                ShipmentDataType = ShipmentDataType.Shipment,
                ValidateFunc = s => s == _countryPane.Value,
                IsCharInputRequired = true,                
                Order = 6
            };

            PostalCodePane = new ShipmentAutomationPane(AutomationControlHelper, MessagesProvider)
            {
                Name = "Postal Code",
                AutomaitonId = "13112",
                AutomationControlType = "ControlType.Pane",
                IsTypedInputRequired = true,
                IsValueRequired = true,
                DataFieldName = nameof(ShipmentAddress.PostalCode),
                ShipmentDataType = ShipmentDataType.Shipment,
                ValidateFunc = s => Regex.Replace(s, @"\W+", "") == PostalCodePane.Value,
                Order = 7
            };

            _statePane = new ShipmentAutomationListBox(AutomationControlHelper, MessagesProvider)
            {
                Name = "State/Province/County",
                AutomaitonId = "13111",
                AutomationControlType = "ControlType.Combobox",
                IsValueRequired = true,
                DataFieldName = nameof(ShipmentAddress.State),
                ShipmentDataType = ShipmentDataType.Shipment,
                ValidateFunc = s => s == _statePane.Value,
                Order = 8
            };

            _cityPane = new ShipmentAutomationPane(AutomationControlHelper, MessagesProvider)
            {
                Name = "City or Town",
                AutomaitonId = "13110",
                AutomationControlType = "ControlType.Pane",
                IsValueRequired = true,
                DataFieldName = nameof(ShipmentAddress.City),
                ShipmentDataType = ShipmentDataType.Shipment,
                ValidateFunc = s => s == _cityPane.Value,
                Order = 9
            };
                       
            TelephonePane = new ShipmentAutomationPane(AutomationControlHelper, MessagesProvider)
            {
                Name = "Telephone",
                AutomaitonId = "13114",
                AutomationControlType = "ControlType.Pane",
                DataFieldName = nameof(ShipmentContact.Phone1),
                ShipmentDataType = ShipmentDataType.Shipment,
                ValidateFunc = s =>
                {
                    if (TelephonePane.IsClearMask)
                    {
                        return Regex.Replace(s, @"\W+", "") == TelephonePane.Value;
                    }
                    return s == TelephonePane.Value;
                },
                IsCharInputRequired = true,    
                MaxLength = 15, 
                Order = 10
            };

            _emailAddressPane = new ShipmentAutomationPane(AutomationControlHelper, MessagesProvider)
            {
                Name = "E-mail Address",
                AutomaitonId = "16673",
                AutomationControlType = "ControlType.Pane",
                DataFieldName = nameof(ShipmentContact.Email),
                ShipmentDataType = ShipmentDataType.Shipment,
                ValidateFunc = s => s == _emailAddressPane.Value,
                Order = 11
            };
            
            PackageWeightPane = new ShipmentAutomationPane(AutomationControlHelper, MessagesProvider)
            {
                Name = "Package",
                AutomaitonId = "13079",
                AutomationControlType = "ControlType.Pane",                
                DataFieldName = nameof(ShipmentPackage.PackageFormattedWeight),
                ShipmentDataType = ShipmentDataType.Package,
                ValidateFunc = s => s == PackageWeightPane.Value,
                Order = 15
            };

            ReferenceNumberOnePane = new ShipmentAutomationPane(AutomationControlHelper, MessagesProvider)
            {
                Name = "Reference Number 1",
                AutomaitonId = "13193",
                AutomationControlType = "ControlType.Pane",
                IsTypedInputRequired = true,
                DataFieldName = nameof(ShipmentPackage.PackageRowNumberOne),
                ShipmentDataType = ShipmentDataType.Package,
                ValidateFunc = s => Regex.Replace(s, @"\W+", "") == ReferenceNumberOnePane.Value,
                Order = 14
            };

            _referenceNumberTwoPane = new ShipmentAutomationPane(AutomationControlHelper, MessagesProvider)
            {
                Name = "Reference Number 2",
                AutomaitonId = "13194",
                AutomationControlType = "ControlType.Pane",
                IsCharInputRequired = true,
                DataFieldName = nameof(ShipmentPackage.PackageRowNumberTwo),
                ShipmentDataType = ShipmentDataType.Package,
                ValidateFunc = s => Regex.Replace(s, @"\W+", "") == _referenceNumberTwoPane.Value,
                Order = 13
            };

            UpsServiceComboBox = new ShipmentAutomationListBox(AutomationControlHelper, MessagesProvider)
            {
                Name = "UPS Service",
                AutomaitonId = "13066",
                AutomationControlType = "ControlType.Combobox",                
                ShipmentDataType = ShipmentDataType.Shipment,
                ValidateFunc = s => s.StartsWith(UpsServiceComboBox.Value),
                IsCharInputRequired = true,
                Order = 14
            };


            GeneralDescOfGoodsPane = new ShipmentAutomationPane(AutomationControlHelper, MessagesProvider)
            {
                Name = "General Desc. of Goods",
                AutomaitonId = "13091",
                AutomationControlType = "ControlType.Pane",                
                ShipmentDataType = ShipmentDataType.Shipment,
                ValidateFunc = s => s == GeneralDescOfGoodsPane.Value,
                Order = 15
            };

            AddPackageButton = new ShipmentAutomationButton(AutomationControlHelper)
            {
                Name = "Add",
                AutomaitonId = "13508",
                AutomationControlType = "ControlType.Button",
            };

            ControlsList.Add(UpdateAddressBookCheckBox);
            ControlsList.Add(SaturdayDeliveryCheckBox);
            ControlsList.Add(_customerIdPane);
            ControlsList.Add(_companyOrNameIdPane);
            ControlsList.Add(_attentionPane);
            ControlsList.Add(_addressOnePane);
            ControlsList.Add(_addressTwoPane);                       
            ControlsList.Add(_countryPane);
            ControlsList.Add(PostalCodePane);            
            ControlsList.Add(_cityPane);
            ControlsList.Add(_statePane);
            ControlsList.Add(TelephonePane);
            ControlsList.Add(_emailAddressPane);
            ControlsList.Add(PackageWeightPane);
            ControlsList.Add(ReferenceNumberOnePane);
//            ControlsList.Add(_referenceNumberTwoPane);
            ControlsList.Add(UpsServiceComboBox);
            ControlsList.Add(GeneralDescOfGoodsPane);

            ControlsList.Add(AddPackageButton);

        }

       
    }
}