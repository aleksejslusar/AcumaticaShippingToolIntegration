using System;
using Newtonsoft.Json;

namespace SmartShipment.Network.Mapping
{
    public class ShipmentMapperBase
    {        

        [JsonProperty(PropertyName = "id")]
        public Guid? Id { get; set; }

        [JsonProperty(PropertyName = "rowNumber")]
        public long? RowNumber { get; set; }

        [JsonProperty(PropertyName = "note")]
        public string Note { get; set; }
    }
}