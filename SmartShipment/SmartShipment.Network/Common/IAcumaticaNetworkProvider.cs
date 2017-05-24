using System.Collections.Generic;
using SmartShipment.Network.AcumaticaSoapService;
using SmartShipment.Network.Export;
using SmartShipment.Network.Mapping;

namespace SmartShipment.Network.Common
{
    public interface IAcumaticaNetworkProvider
    {       
        List<ShippingBox> GetShippingBoxes();     
        void UpdateShipments(List<Shipment> shipments, ISmartShipmentExportContext smartShipmentExportContext);
        bool TestNetworkSettings();
        ShipmentMapper GetShipment(string shipmentNbr);
    }
}
