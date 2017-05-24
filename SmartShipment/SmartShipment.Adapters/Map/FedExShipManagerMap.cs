using System;
using System.Text.RegularExpressions;
using System.Windows.Automation;
using SmartShipment.Adapters.Common;
using SmartShipment.Adapters.Control;
using SmartShipment.Information;
using SmartShipment.Network.Mapping;

namespace SmartShipment.Adapters.Map
{
    public class FedexShipManagerMap : ShipmentAutomationMapBase
    {        
        private ShipmentAutomationControl _recipientId;
        private ShipmentAutomationControl _country;
        private ShipmentAutomationControl _contactName;
        private ShipmentAutomationControl _companyName;
        private ShipmentAutomationControl _addressOne;
        private ShipmentAutomationControl _addressTwo;
        public ShipmentAutomationControl PostalCode;
        private ShipmentAutomationControl _state;
        private ShipmentAutomationControl _city;
        public ShipmentAutomationControl Telephone;       
        private ShipmentAutomationControl _shipmentId;

       

        //Package
        public ShipmentAutomationControl PackageNumbers;
        public ShipmentAutomationControl PackageWeight;
        public ShipmentAutomationControl PackageServiceType;
        public ShipmentAutomationControl PackageType;
        public ShipmentAutomationControl PackageBillTransportationTo;

        private ShipmentAutomationControl _packageContentsOne;
        private ShipmentAutomationControl _packageContentsTwo;
        public ShipmentAutomationCheckBox SaveUpdateAddressBook;

        public FedexShipManagerMap(IShipmentAutomationControlHelper automationControlHelper, AutomationElement rootAutomationElement, ISmartShipmentMessagesProvider messagesProvider) : base(automationControlHelper, rootAutomationElement, messagesProvider)
        {
                        
        }

        protected override void InitAutomationControls()
        {           
            _recipientId = new ShipmentAutomationComboBox(AutomationControlHelper, MessagesProvider)
            {
                Name = "Recipient ID",
                AutomaitonId = "cboRecipientId",                
                DataFieldName = nameof(ShipmentMapper.CustomerId),
                ShipmentDataType = ShipmentDataType.Shipment,
                IsValueRequired = true,
                ValidateFunc = s => s == _recipientId.Value,
                Order = 0
            };

            _country = new ShipmentAutomationListBox(AutomationControlHelper, MessagesProvider)
            {
                Name = "Country",
                AutomaitonId = "cboCountry",               
                DataFieldName = nameof(ShipmentAddress.Country),
                ShipmentDataType = ShipmentDataType.Shipment,
                IsValueRequired = true,
                ValidateFunc = s => s.StartsWith(_country.Value),
                Order = 1
            };

            _contactName = new ShipmentAutomationPane(AutomationControlHelper, MessagesProvider)
            {
                Name = "Contact name",
                AutomaitonId = "edtContact",                
                DataFieldName = nameof(ShipmentContact.Position),
                ShipmentDataType = ShipmentDataType.Shipment,
                ValidateFunc = s => s == _contactName.Value,
                IsValueRequired = true,
                Order = 2
            };

            _companyName = new ShipmentAutomationPane(AutomationControlHelper, MessagesProvider)
            {
                Name = "Company name",
                AutomaitonId = "edtCompany",                
                DataFieldName = nameof(ShipmentContact.DisplayName),
                ShipmentDataType = ShipmentDataType.Shipment,
                ValidateFunc = s => s == _companyName.Value,
                Order = 3
            };

            _addressOne = new ShipmentAutomationPane(AutomationControlHelper, MessagesProvider)
            {
                Name = "Address 1",
                AutomaitonId = "edtAddress1",
                DataFieldName = nameof(ShipmentAddress.AddressLine1),
                ShipmentDataType = ShipmentDataType.Shipment,
                ValidateFunc = s => s == _addressOne.Value,
                IsValueRequired = true,
                MaxLength = 35,
                Order = 4
            };

            _addressTwo = new ShipmentAutomationPane(AutomationControlHelper, MessagesProvider)
            {
                Name = "Address 2",
                AutomaitonId = "edtAddress2",
                DataFieldName = nameof(ShipmentAddress.AddressLine2),
                ShipmentDataType = ShipmentDataType.Shipment,
                ValidateFunc = s => s == _addressTwo.Value,
                MaxLength = 35,
                Order = 5
            };

            PostalCode = new ShipmentAutomationPane(AutomationControlHelper, MessagesProvider)
            {
                Name = "Postal code",
                AutomaitonId = "edtZipPostal",
                DataFieldName = nameof(ShipmentAddress.PostalCode),
                ShipmentDataType = ShipmentDataType.Shipment,
                ValidateFunc = s => Regex.Replace(s, @"\W+", "") ==  PostalCode.Value,
                IsValueRequired = true,
                IsTypedInputRequired = true,
                Order = 6
            };

            _state = new ShipmentAutomationListBox(AutomationControlHelper, MessagesProvider)
            {
                Name = "State/Province",
                AutomaitonId = "cboStateProvince",
                DataFieldName = nameof(ShipmentAddress.State),
                ShipmentDataType = ShipmentDataType.Shipment,
                ValidateFunc = s => s.StartsWith(_state.Value),
                IsValueRequired = true,
                IsTypedInputRequired = true,
                Order = 7
            };

            _city = new ShipmentAutomationComboBox(AutomationControlHelper, MessagesProvider)
            {
                Name = "City",
                AutomaitonId = "cboCity",
                DataFieldName = nameof(ShipmentAddress.City),
                ShipmentDataType = ShipmentDataType.Shipment,
                ValidateFunc = s => s == _city.Value,
                IsValueRequired = true,
                Order = 8
            };

            Telephone = new ShipmentAutomationPane(AutomationControlHelper, MessagesProvider)
            {
                Name = "Telephone",
                AutomaitonId = "edtPhone",
                DataFieldName = nameof(ShipmentContact.Phone1),
                ShipmentDataType = ShipmentDataType.Shipment,
                ValidateFunc = s => Regex.Replace(s, @"\W+", "") == Telephone.Value,
                IsTypedInputRequired = true,
                Order = 9
            };            

            _shipmentId = new ShipmentAutomationPane(AutomationControlHelper, MessagesProvider)
            {
                Name = "Shipment ID",
                AutomaitonId = "cboAdditionalRef3",
                DataFieldName = nameof(ShipmentMapper.ShipmentNbr),
                ShipmentDataType = ShipmentDataType.Shipment,
                ValidateFunc = s => s == _shipmentId.Value,
                IsValueRequired = true,                
                Order = 10
            };

            SaveUpdateAddressBook = new ShipmentAutomationCheckBox(AutomationControlHelper, MessagesProvider)
            {
                Name = "Save in/update my address book",
                AutomaitonId = "chkSaveUpdateMyAddressBook",
                ShipmentDataType = ShipmentDataType.Shipment,
                ValidateFunc = s => true,
                Order = 19
            };


            //Package
            PackageNumbers = new ShipmentAutomationPane(AutomationControlHelper, MessagesProvider)
            {
                Name = "Number of packages",
                AutomaitonId = "edtNumberOfPackages",
                ShipmentDataType = ShipmentDataType.Package,
                IsValueRequired = true,
                IsTypedInputRequired = true,
                ValidateFunc = s => s == PackageNumbers.Value,
                Order = 11
            };

            PackageWeight = new ShipmentAutomationPane(AutomationControlHelper, MessagesProvider)
            {
                Name = "Weight",
                AutomaitonId = "edtWeight",
                DataFieldName = nameof(ShipmentPackage.PackageWeight),
                ShipmentDataType = ShipmentDataType.Package,
                IsTypedInputRequired = true,
                IsValueRequired = true,
                ValidateFunc = s =>
                {
                    decimal ds, dt;
                    decimal.TryParse(s, out ds);
                    decimal.TryParse(PackageWeight.Value, out dt);
                    return ds == dt;
                },

                Order = 12                
            };

            PackageServiceType = new ShipmentAutomationComboBox(AutomationControlHelper, MessagesProvider)
            {
                Name = "Service type",
                AutomaitonId = "cboServiceType",                
                ShipmentDataType = ShipmentDataType.Package,
                IsValueRequired = true,
                IsTypedInputRequired = true,
                ValidateFunc = s => s.StartsWith(PackageServiceType.Value),
                Order = 13                
            };

            PackageType = new ShipmentAutomationComboBox(AutomationControlHelper, MessagesProvider)
            {
                Name = "Package type",
                AutomaitonId = "cboPackageType",
                ShipmentDataType = ShipmentDataType.Package,
                ValidateFunc = s => s.StartsWith(PackageType.Value),
                IsValueRequired = true,
                Order = 14,
                IsFocusedInputRequired = true

            };

            PackageBillTransportationTo = new ShipmentAutomationComboBox(AutomationControlHelper, MessagesProvider)
            {
                Name = "Tracking #",
                AutomaitonId = "cboBillShipmentTo",
                ShipmentDataType = ShipmentDataType.Package,
                ValidateFunc = s => s.StartsWith(PackageBillTransportationTo.Value),
                IsValueRequired = true,
                IsTypedInputRequired = true,
                Order = 15
            };

            _packageContentsOne = new ShipmentAutomationPane(AutomationControlHelper, MessagesProvider)
            {
                Name = "Package contents 1",
                AutomaitonId = "edtPackageContents1",
                DataFieldName = nameof(ShipmentPackage.PackageRowNumberTwo),
                ShipmentDataType = ShipmentDataType.Package,
                ValidateFunc = s => s == _packageContentsOne.Value,
                Order = 16
            };

            _packageContentsTwo = new ShipmentAutomationPane(AutomationControlHelper, MessagesProvider)
            {
                Name = "Package contents 2",
                AutomaitonId = "edtPackageContents2",
                ShipmentDataType = ShipmentDataType.Package,
                ValidateFunc = s => s == _packageContentsTwo.Value,
                Order = 17
            };

           

            //Shipment                                                               
            ControlsList.Add(_recipientId);
            ControlsList.Add(_country);
            ControlsList.Add(_contactName);
            ControlsList.Add(_companyName);
            ControlsList.Add(_addressOne);
            ControlsList.Add(_addressTwo);
            ControlsList.Add(PostalCode);
            ControlsList.Add(_state);
            ControlsList.Add(_city);
            ControlsList.Add(Telephone);            
            ControlsList.Add(_shipmentId);
            ControlsList.Add(SaveUpdateAddressBook);

            //Package
            ControlsList.Add(PackageNumbers);
            ControlsList.Add(PackageWeight);
            ControlsList.Add(PackageServiceType);
            ControlsList.Add(PackageType);
            ControlsList.Add(PackageBillTransportationTo);                    
        }
    }
}
