namespace SmartShipment.Adapters.Common
{
    public interface IShipmentApplicationAdapter
    {
        void RunShipmentApplication(IShipmentApplicationHelper applicationHelper, string shipmentNbr = null);        
    }
}
