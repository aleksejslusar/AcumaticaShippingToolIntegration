using SmartShipment.Network.Mapping;

namespace SmartShipment.Adapters.Common
{
    public interface IShipmentApplicationHelper
    {
        bool RunShipmentApplication(string shipmentNumber);
        void PopulateApplicaitonByShipmentData(ShipmentMapper shipment);
        bool PopulateApplicaitonControlMap();
    }
}
