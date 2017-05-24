using Newtonsoft.Json;

namespace SmartShipment.Network.Mapping
{
    public interface IShipmentValue
    {        
        string ValueMapName { get; set; }
    }

    public interface IShipmentValue<T> : IShipmentValue
    {
        T Value { get; set; }        
    }

    public class ShipmentValue<T> : IShipmentValue<T>
    {
        [JsonProperty(PropertyName = "value")]
        public T Value { get; set; }

        public string ValueMapName { get; set; }       

        public void Set(IShipmentValue<T> obj)
        {
            if (obj != null)
            {
                Value = obj.Value;
            }
        }
    }
}
