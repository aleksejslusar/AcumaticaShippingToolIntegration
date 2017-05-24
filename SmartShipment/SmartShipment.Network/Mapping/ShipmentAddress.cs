using Newtonsoft.Json;

namespace SmartShipment.Network.Mapping
{
    public class ShipmentAddress : ShipmentMapperBase
    {
        private ShipmentValue<string> _addressLine1;
        private ShipmentValue<string> _addressLine2;
        private ShipmentValue<string> _city;
        private ShipmentValue<string> _country;
        private ShipmentValue<string> _postalCode;
        private ShipmentValue<string> _state ;
        private ShipmentValue<string> _countryName;

        [JsonProperty(PropertyName = "AddressLine1")]
        public ShipmentValue<string> AddressLine1
        {
            get { return _addressLine1 ?? (_addressLine1 = new ShipmentValue<string> {ValueMapName = nameof(AddressLine1)}); }
            set { _addressLine1 = value; }
        }

        [JsonProperty(PropertyName = "AddressLine2")]
        public ShipmentValue<string> AddressLine2
        {
            get { return _addressLine2 ?? (_addressLine2 = new ShipmentValue<string> {ValueMapName = nameof(AddressLine2)}); }
            set { _addressLine2 = value; }
        }

        [JsonProperty(PropertyName = "City")]
        public ShipmentValue<string> City
        {
            get { return _city ?? (_city = new ShipmentValue<string> {ValueMapName = nameof(City)}); }
            set { _city = value; }
        }

        [JsonProperty(PropertyName = "Country")]
        public ShipmentValue<string> Country
        {
            get { return _country ?? (_country = new ShipmentValue<string> {ValueMapName = nameof(Country)}); }
            set { _country = value; }
        }

        [JsonIgnore]
        public ShipmentValue<string> CountryName
        {
            get { return _countryName ?? (_countryName = new ShipmentValue<string> { ValueMapName = nameof(CountryName) }); }
            set { _countryName = value; }
        }

        [JsonProperty(PropertyName = "PostalCode")]
        public ShipmentValue<string> PostalCode
        {
            get { return _postalCode ?? (_postalCode = new ShipmentValue<string> {ValueMapName = nameof(PostalCode)}); }
            set { _postalCode = value; }
        }

        [JsonProperty(PropertyName = "State")]
        public ShipmentValue<string> State
        {
            get { return _state ?? (_state = new ShipmentValue<string> { ValueMapName = nameof(State) }); }
            set { _state  = value; }
        }
    }
}