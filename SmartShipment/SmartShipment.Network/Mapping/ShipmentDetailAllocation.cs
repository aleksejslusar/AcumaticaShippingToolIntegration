using System;
using Newtonsoft.Json;

namespace SmartShipment.Network.Mapping
{
    public class ShipmentDetailAllocation : ShipmentMapperBase
    {
        private ShipmentValue<string> _description;
        private ShipmentValue<string> _locationId;
        private ShipmentValue<string> _lotSerialNbr;
        private ShipmentValue<decimal?> _qty;
        private ShipmentValue<string> _uom;
        private ShipmentValue<DateTime?> _expirationDate;

        [JsonProperty(PropertyName = "Description")]
        public ShipmentValue<string> Description
        {
            get { return _description ?? (_description = new ShipmentValue<string>()); }
            set { _description = value; }
        }

        [JsonProperty(PropertyName = "LocationID")]
        public ShipmentValue<string> LocationId
        {
            get { return _locationId ?? (_locationId = new ShipmentValue<string>()); }
            set { _locationId = value; }
        }

        [JsonProperty(PropertyName = "LotSerialNbr")]
        public ShipmentValue<string> LotSerialNbr
        {
            get { return _lotSerialNbr ?? (_lotSerialNbr = new ShipmentValue<string>()); }
            set { _lotSerialNbr = value; }
        }

        [JsonProperty(PropertyName = "Qty")]
        public ShipmentValue<decimal?> Qty
        {
            get { return _qty ?? (_qty = new ShipmentValue<decimal?>()); }
            set { _qty = value; }
        }

        [JsonProperty(PropertyName = "UOM")]
        public ShipmentValue<string> Uom
        {
            get { return _uom ?? (_uom = new ShipmentValue<string>()); }
            set { _uom = value; }
        }

        [JsonProperty(PropertyName = "ExpirationDate")]
        public ShipmentValue<DateTime?> ExpirationDate
        {
            get { return _expirationDate ?? (_expirationDate = new ShipmentValue<DateTime?>()); }
            set { _expirationDate = value; }
        }
    }
}