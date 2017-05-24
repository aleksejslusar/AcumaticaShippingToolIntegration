using Newtonsoft.Json;

namespace SmartShipment.Network.Mapping
{
    public class ShipmentContact : ShipmentMapperBase
    {
        private ShipmentAddress _address;
        private ShipmentValue<int?> _contactId;
        private ShipmentValue<string> _displayName;
        private ShipmentValue<string> _position;
        private ShipmentValue<string> _email;
        private ShipmentValue<string> _phone1;
        private ShipmentValue<string> _fax;
        private ShipmentValue<string> _phone2;
        private ShipmentValue<string> _web;
        private ShipmentValue<string> _firstName;
        private ShipmentValue<string> _middleName;
        private ShipmentValue<string> _lastName;
        private ShipmentValue<string> _title;
        private ShipmentValue<string> _phone1Type;
        private ShipmentValue<string> _phone2Type;

        [JsonProperty(PropertyName = "Address")]
        public ShipmentAddress Address
        {
            get { return _address ?? (_address = new ShipmentAddress()); }
            set { _address = value; }
        }

        [JsonProperty(PropertyName = "ContactID")]
        public ShipmentValue<int?> ContactId
        {
            get { return _contactId ?? (_contactId = new ShipmentValue<int?>()); }
            set { _contactId = value; }
        }

        [JsonProperty(PropertyName = "DisplayName")]
        public ShipmentValue<string> DisplayName
        {
            get { return _displayName ?? (_displayName = new ShipmentValue<string>() {ValueMapName = nameof(DisplayName) }); }
            set { _displayName = value; }
        }

        [JsonProperty(PropertyName = "Position")]
        public ShipmentValue<string> Position
        {
            get { return _position ?? (_position = new ShipmentValue<string>() { ValueMapName = nameof(Position) }); }
            set { _position = value; }
        }

        [JsonProperty(PropertyName = "Email")]
        public ShipmentValue<string> Email
        {
            get { return _email ?? (_email = new ShipmentValue<string>() { ValueMapName = nameof(Email) }); }
            set { _email = value; }
        }

        [JsonProperty(PropertyName = "Phone1")]
        public ShipmentValue<string> Phone1
        {
            get { return _phone1 ?? (_phone1 = new ShipmentValue<string>() { ValueMapName = nameof(Phone1) }); }
            set { _phone1 = value; }
        }

        [JsonProperty(PropertyName = "Fax")]
        public ShipmentValue<string> Fax
        {
            get { return _fax ?? (_fax = new ShipmentValue<string>()); }
            set { _fax = value; }
        }

        [JsonProperty(PropertyName = "Phone2")]
        public ShipmentValue<string> Phone2
        {
            get { return _phone2 ?? (_phone2 = new ShipmentValue<string>()); }
            set { _phone2 = value; }
        }

        [JsonProperty(PropertyName = "Web")]
        public ShipmentValue<string> Web
        {
            get { return _web ?? (_web = new ShipmentValue<string>()); }
            set { _web = value; }
        }

        [JsonProperty(PropertyName = "FirstName")]
        public ShipmentValue<string> FirstName
        {
            get { return _firstName ?? (_firstName = new ShipmentValue<string>()); }
            set { _firstName = value; }
        }

        [JsonProperty(PropertyName = "MiddleName")]
        public ShipmentValue<string> MiddleName
        {
            get { return _middleName ?? (_middleName = new ShipmentValue<string>()); }
            set { _middleName = value; }
        }

        [JsonProperty(PropertyName = "LastName")]
        public ShipmentValue<string> LastName
        {
            get { return _lastName ?? (_lastName = new ShipmentValue<string>()); }
            set { _lastName = value; }
        }

        [JsonProperty(PropertyName = "Title")]
        public ShipmentValue<string> Title
        {
            get { return _title ?? (_title = new ShipmentValue<string>()); }
            set { _title = value; }
        }

        [JsonProperty(PropertyName = "Phone1Type")]
        public ShipmentValue<string> Phone1Type
        {
            get { return _phone1Type ?? (_phone1Type = new ShipmentValue<string>()); }
            set { _phone1Type = value; }
        }

        [JsonProperty(PropertyName = "Phone2Type")]
        public ShipmentValue<string> Phone2Type
        {
            get { return _phone2Type ?? (_phone2Type = new ShipmentValue<string>()); }
            set { _phone2Type = value; }
        }
    }
}