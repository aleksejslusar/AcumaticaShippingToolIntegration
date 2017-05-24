using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace SmartShipment.Network.Mapping
{
    public class ShipmentPackage : ShipmentMapperBase, IEnumerable<ShipmentValue<string>>
    {
        private ShipmentValue<string> _boxId;
        private ShipmentValue<decimal?> _codAmount;
        private ShipmentValue<bool?> _confirmed;
        private ShipmentValue<string> _customRefNbr1;
        private ShipmentValue<string> _customRefNbr2;
        private ShipmentValue<decimal?> _declaredValue;
        private ShipmentValue<string> _description;
        private ShipmentValue<string> _type;
        private ShipmentValue<string> _trackingNumber;
        private ShipmentValue<decimal?> _weight;
        private ShipmentValue<string> _uom;
        private ShipmentValue<string> _packageWeight;
        private ShipmentValue<string> _packageFormattedWeight;
        private ShipmentValue<string> _packageRowNumberOne;
        private ShipmentValue<string> _packageRowNumberTwo;

        private readonly List<ShipmentValue<string>> _packageValues;
        

        public ShipmentPackage()
        {            
            _packageValues = new List<ShipmentValue<string>> {PackageWeight, PackageFormattedWeight, PackageRowNumberOne, PackageRowNumberTwo };            
        }

        [JsonProperty(PropertyName = "BoxID")]
        public ShipmentValue<string> BoxId
        {
            get { return _boxId ?? (_boxId = new ShipmentValue<string>()); }
            set { _boxId = value; }
        }

        [JsonProperty(PropertyName = "CODAmount")]
        public ShipmentValue<decimal?> CodAmount
        {
            get { return _codAmount ?? (_codAmount = new ShipmentValue<decimal?>()); }
            set { _codAmount = value; }
        }

        [JsonProperty(PropertyName = "Confirmed")]
        public ShipmentValue<bool?> Confirmed
        {
            get { return _confirmed ?? (_confirmed = new ShipmentValue<bool?>()); }
            set { _confirmed = value; }
        }

        [JsonProperty(PropertyName = "CustomRefNbr1")]
        public ShipmentValue<string> CustomRefNbr1
        {
            get { return _customRefNbr1 ?? (_customRefNbr1 = new ShipmentValue<string>()); }
            set { _customRefNbr1 = value; }
        }

        [JsonProperty(PropertyName = "CustomRefNbr2")]
        public ShipmentValue<string> CustomRefNbr2
        {
            get { return _customRefNbr2 ?? (_customRefNbr2 = new ShipmentValue<string>()); }
            set { _customRefNbr2 = value; }
        }

        [JsonProperty(PropertyName = "DeclaredValue")]
        public ShipmentValue<decimal?> DeclaredValue
        {
            get { return _declaredValue ?? (_declaredValue = new ShipmentValue<decimal?>()); }
            set { _declaredValue = value; }
        }

        [JsonProperty(PropertyName = "Description")]
        public ShipmentValue<string> Description
        {
            get { return _description ?? (_description = new ShipmentValue<string>()); }
            set { _description = value; }
        }

        [JsonProperty(PropertyName = "Type")]
        public ShipmentValue<string> Type
        {
            get { return _type ?? (_type = new ShipmentValue<string>()); }
            set { _type = value; }
        }

        [JsonProperty(PropertyName = "TrackingNumber")]
        public ShipmentValue<string> TrackingNumber
        {
            get { return _trackingNumber ?? (_trackingNumber = new ShipmentValue<string>()); }
            set { _trackingNumber = value; }
        }

        [JsonProperty(PropertyName = "Weight")]
        public ShipmentValue<decimal?> Weight
        {
            get { return _weight ?? (_weight = new ShipmentValue<decimal?>()); }
            set { _weight = value; }
        }
        
        [JsonProperty(PropertyName = "UOM")]
        public ShipmentValue<string> Uom
        {
            get { return _uom ?? (_uom = new ShipmentValue<string>()); }
            set { _uom = value; }
        }

        [JsonIgnore]
        public ShipmentValue<string> PackageWeight
        {
            get { return _packageWeight ?? (_packageWeight = new ShipmentValue<string> {ValueMapName = nameof(PackageWeight)}); }
            set { _packageWeight  = value; }
        }

        [JsonIgnore]
        public ShipmentValue<string> PackageFormattedWeight
        {
            get { return _packageFormattedWeight ?? (_packageFormattedWeight = new ShipmentValue<string> { ValueMapName = nameof(PackageFormattedWeight) }); }
            set { _packageFormattedWeight = value; }
        }

        [JsonIgnore]
        public ShipmentValue<string> PackageRowNumberOne
        {
            get { return _packageRowNumberOne ?? (_packageRowNumberOne = new ShipmentValue<string> { ValueMapName = nameof(PackageRowNumberOne) }); }
            set { _packageRowNumberOne = value; }
        }

        [JsonIgnore]
        public ShipmentValue<string> PackageRowNumberTwo
        {
            get { return _packageRowNumberTwo ?? (_packageRowNumberTwo = new ShipmentValue<string> { ValueMapName = nameof(PackageRowNumberTwo) }); }
            set { _packageRowNumberTwo = value; }
        }

        public IEnumerator<ShipmentValue<string>> GetEnumerator()
        {
            return _packageValues.TakeWhile(control => control != null).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
