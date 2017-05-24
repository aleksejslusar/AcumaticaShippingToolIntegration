using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace SmartShipment.Network.Mapping
{
    public class ShipmentMapper : ShipmentMapperBase, IEnumerable<IShipmentValue<string>>
    {
        private ShipmentContact _contact;
        private List<ShipmentDetail> _details;
        private List<ShipmentPackage> _packages;
        private ShipmentValue<string> _customerId;
        private ShipmentValue<string> _toWarehouseId;
        private ShipmentValue<DateTime?> _shipmentDate;
        private ShipmentValue<string> _shipmentNbr;
        private ShipmentValue<string> _warehouseId;
        private ShipmentValue<string> _status;
        private ShipmentValue<string> _shipVia;
        private ShipmentValue<bool?> _hold;
        private ShipmentValue<string> _operation;
        private ShipmentValue<string> _type;
        private ShipmentValue<string> _workgroup;
        private ShipmentValue<decimal?> _shippedQuantity;
        private ShipmentValue<bool?> _residentialDelivery;
        private ShipmentValue<bool?> _saturdayDelivery;
        private ShipmentValue<bool?> _useCustomersAccount;
        private ShipmentValue<bool?> _overrideAddress;
        private ShipmentValue<bool?> _overrideContact;

        private readonly List<IShipmentValue<string>>  _shipmentValues;

        public ShipmentMapper()
        {
            _shipmentValues = new List<IShipmentValue<string>>
            {
                CustomerId,
                ShipmentNbr,
                Contact.DisplayName,
                Contact.Position,
                Contact.Phone1,
                Contact.Email,
                Contact.Address.AddressLine1,
                Contact.Address.AddressLine2,
                Contact.Address.Country,
                Contact.Address.CountryName,
                Contact.Address.PostalCode,               
                Contact.Address.City,
                Contact.Address.State
            };

        }

        #region Properties
        [JsonIgnore]
        public ParsedShipmentData ParsedShipmentData { get; set; }

        [JsonProperty(PropertyName = "Contact")]
        public ShipmentContact Contact
        {
            get { return _contact ?? (_contact = new ShipmentContact()); }
            set { _contact = value; }
        }

        [JsonProperty(PropertyName = "Details")]
        public List<ShipmentDetail> Details
        {
            get { return _details ?? (_details = new List<ShipmentDetail>()); }
            set { _details = value; }
        }

        [JsonProperty(PropertyName = "Packages")]
        public List<ShipmentPackage> Packages
        {
            get { return _packages ?? (_packages = new List<ShipmentPackage>()); }
            set { _packages = value; }
        }

        [JsonProperty(PropertyName = "CustomerID")]
        public ShipmentValue<string> CustomerId
        {
            get { return _customerId ?? (_customerId = new ShipmentValue<string>() {ValueMapName = nameof(CustomerId)});}
            set { _customerId = value; }
        }

        [JsonProperty(PropertyName = "ToWarehouseID")]
        public ShipmentValue<string> ToWarehouseId
        {
            get { return _toWarehouseId ?? (_toWarehouseId = new ShipmentValue<string>()); }
            set { _toWarehouseId = value; }
        }

        [JsonProperty(PropertyName = "ShipmentDate")]
        public ShipmentValue<DateTime?> ShipmentDate
        {
            get { return _shipmentDate ?? (_shipmentDate = new ShipmentValue<DateTime?>()); }
            set { _shipmentDate = value; }
        }

        [JsonProperty(PropertyName = "ShipmentNbr")]
        public ShipmentValue<string> ShipmentNbr
        {
            get { return _shipmentNbr ?? (_shipmentNbr = new ShipmentValue<string>() {ValueMapName = nameof(ShipmentNbr)}); }
            set { _shipmentNbr = value; }
        }

        [JsonProperty(PropertyName = "WarehouseID")]
        public ShipmentValue<string> WarehouseId
        {
            get { return _warehouseId ?? (_warehouseId = new ShipmentValue<string>()); }
            set { _warehouseId = value; }
        }

        [JsonProperty(PropertyName = "Status")]
        public ShipmentValue<string> Status
        {
            get { return _status ?? (_status = new ShipmentValue<string>()); }
            set { _status = value; }
        }

        [JsonProperty(PropertyName = "ShipVia")]
        public ShipmentValue<string> ShipVia
        {
            get { return _shipVia ?? (_shipVia = new ShipmentValue<string>()); }
            set { _shipVia = value; }
        }

        [JsonProperty(PropertyName = "Hold")]
        public ShipmentValue<bool?> Hold
        {
            get { return _hold ?? (_hold = new ShipmentValue<bool?>()); }
            set { _hold = value; }
        }

        [JsonProperty(PropertyName = "Operation")]
        public ShipmentValue<string> Operation
        {
            get { return _operation ?? (_operation = new ShipmentValue<string>()); }
            set { _operation = value; }
        }

        [JsonProperty(PropertyName = "Type")]
        public ShipmentValue<string> Type
        {
            get { return _type ?? (_type = new ShipmentValue<string>()); }
            set { _type = value; }
        }

        [JsonProperty(PropertyName = "Workgroup")]
        public ShipmentValue<string> Workgroup
        {
            get { return _workgroup ?? (_workgroup = new ShipmentValue<string>()); }
            set { _workgroup = value; }
        }

        [JsonProperty(PropertyName = "ShippedQuantity")]
        public ShipmentValue<decimal?> ShippedQuantity
        {
            get { return _shippedQuantity ?? (_shippedQuantity = new ShipmentValue<decimal?>()); }
            set { _shippedQuantity = value; }
        }

        [JsonProperty(PropertyName = "ResidentialDelivery")]
        public ShipmentValue<bool?> ResidentialDelivery
        {
            get { return _residentialDelivery ?? (_residentialDelivery = new ShipmentValue<bool?>()); }
            set { _residentialDelivery = value; }
        }

        [JsonProperty(PropertyName = "SaturdayDelivery")]
        public ShipmentValue<bool?> SaturdayDelivery
        {
            get { return _saturdayDelivery ?? (_saturdayDelivery = new ShipmentValue<bool?>()); }
            set { _saturdayDelivery = value; }
        }

        [JsonProperty(PropertyName = "UseCustomersAccount")]
        public ShipmentValue<bool?> UseCustomersAccount
        {
            get { return _useCustomersAccount ?? (_useCustomersAccount = new ShipmentValue<bool?>()); }
            set { _useCustomersAccount = value; }
        }

        [JsonProperty(PropertyName = "OverrideAddress")]
        public ShipmentValue<bool?> OverrideAddress
        {
            get { return _overrideAddress ?? (_overrideAddress = new ShipmentValue<bool?>()); }
            set { _overrideAddress = value; }
        }

        [JsonProperty(PropertyName = "OverrideContact")]
        public ShipmentValue<bool?> OverrideContact
        {
            get { return _overrideContact ?? (_overrideContact = new ShipmentValue<bool?>()); }
            set { _overrideContact = value; }
        }

        #endregion

        public IEnumerator<IShipmentValue<string>> GetEnumerator()
        {
            return _shipmentValues.TakeWhile(control => control != null).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
              
    }
}
