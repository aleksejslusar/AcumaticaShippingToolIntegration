using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SmartShipment.Network.Mapping
{
    public class ShipmentDetail : ShipmentMapperBase
    {
        private List<ShipmentDetailAllocation> _allocations;
        private ShipmentValue<string> _inventoryId;
        private ShipmentValue<int?> _lineNbr;
        private ShipmentValue<string> _locationId;
        private ShipmentValue<decimal?> _openQty;
        private ShipmentValue<int?> _orderLineNbr;
        private ShipmentValue<string> _orderNbr;
        private ShipmentValue<decimal?> _orderedQty;
        private ShipmentValue<string> _reasonCode;
        private ShipmentValue<decimal?> _shippedQty;
        private ShipmentValue<string> _warehouseId;
        private ShipmentValue<string> _description;
        private ShipmentValue<string> _uom;
        private ShipmentValue<string> _orderType;
        private ShipmentValue<DateTime?> _expirationDate;
        private ShipmentValue<bool?> _freeItem;
        private ShipmentValue<string> _lotSerialNbr;

        [JsonProperty(PropertyName = "Allocations")]
        public List<ShipmentDetailAllocation> Allocations
        {
            get { return _allocations ?? (_allocations = new List<ShipmentDetailAllocation>()); }
            set { _allocations = value; }
        }

        [JsonProperty(PropertyName = "InventoryID")]
        public ShipmentValue<string> InventoryId
        {
            get { return _inventoryId ?? (_inventoryId = new ShipmentValue<string>()); }
            set { _inventoryId = value; }
        }

        [JsonProperty(PropertyName = "LineNbr")]
        public ShipmentValue<int?> LineNbr
        {
            get { return _lineNbr ?? (_lineNbr = new ShipmentValue<int?>()); }
            set { _lineNbr = value; }
        }

        [JsonProperty(PropertyName = "LocationID")]
        public ShipmentValue<string> LocationId
        {
            get { return _locationId ?? (_locationId = new ShipmentValue<string>()); }
            set { _locationId = value; }
        }

        [JsonProperty(PropertyName = "OpenQty")]
        public ShipmentValue<decimal?> OpenQty
        {
            get { return _openQty ?? (_openQty = new ShipmentValue<decimal?>()); }
            set { _openQty = value; }
        }

        [JsonProperty(PropertyName = "OrderLineNbr")]
        public ShipmentValue<int?> OrderLineNbr
        {
            get { return _orderLineNbr ?? (_orderLineNbr = new ShipmentValue<int?>()); }
            set { _orderLineNbr = value; }
        }

        [JsonProperty(PropertyName = "OrderNbr")]
        public ShipmentValue<string> OrderNbr
        {
            get { return _orderNbr ?? (_orderNbr = new ShipmentValue<string>()); }
            set { _orderNbr = value; }
        }

        [JsonProperty(PropertyName = "OrderedQty")]
        public ShipmentValue<decimal?> OrderedQty
        {
            get { return _orderedQty ?? (_orderedQty = new ShipmentValue<decimal?>()); }
            set { _orderedQty = value; }
        }

        [JsonProperty(PropertyName = "ReasonCode")]
        public ShipmentValue<string> ReasonCode
        {
            get { return _reasonCode ?? (_reasonCode = new ShipmentValue<string>()); }
            set { _reasonCode = value; }
        }

        [JsonProperty(PropertyName = "ShippedQty")]
        public ShipmentValue<decimal?> ShippedQty
        {
            get { return _shippedQty ?? (_shippedQty = new ShipmentValue<decimal?>()); }
            set { _shippedQty = value; }
        }

        [JsonProperty(PropertyName = "WarehouseID")]
        public ShipmentValue<string> WarehouseId
        {
            get { return _warehouseId ?? (_warehouseId = new ShipmentValue<string>()); }
            set { _warehouseId = value; }
        }

        [JsonProperty(PropertyName = "Description")]
        public ShipmentValue<string> Description
        {
            get { return _description ?? (_description = new ShipmentValue<string>()); }
            set { _description = value; }
        }

        [JsonProperty(PropertyName = "UOM")]
        public ShipmentValue<string> Uom
        {
            get { return _uom ?? (_uom = new ShipmentValue<string>()); }
            set { _uom = value; }
        }

        [JsonProperty(PropertyName = "OrderType")]
        public ShipmentValue<string> OrderType
        {
            get { return _orderType ?? (_orderType = new ShipmentValue<string>()); }
            set { _orderType = value; }
        }

        [JsonProperty(PropertyName = "ExpirationDate")]
        public ShipmentValue<DateTime?> ExpirationDate
        {
            get { return _expirationDate ?? (_expirationDate = new ShipmentValue<DateTime?>()); }
            set { _expirationDate = value; }
        }

        [JsonProperty(PropertyName = "FreeItem")]
        public ShipmentValue<bool?> FreeItem
        {
            get { return _freeItem ?? (_freeItem = new ShipmentValue<bool?>()); }
            set { _freeItem = value; }
        }

        [JsonProperty(PropertyName = "LotSerialNbr")]
        public ShipmentValue<string> LotSerialNbr
        {
            get { return _lotSerialNbr ?? (_lotSerialNbr = new ShipmentValue<string>()); }
            set { _lotSerialNbr = value; }
        }
    }
}
