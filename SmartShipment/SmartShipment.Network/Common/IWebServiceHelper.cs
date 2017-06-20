using System.Collections.Generic;
using SmartShipment.Network.AcumaticaSoapService;
using SmartShipment.Network.Export;
using SmartShipment.Network.Mapping;

namespace SmartShipment.Network.Common
{
    public interface IWebServiceHelper
    {      
        ShipmentMapper GetShipmentByShipmentId(string shipmentId);
        List<ShippingBox> GetShippingBoxes(); 
        void UpdateShipments(List<Shipment> shipments, ISmartShipmentExportContext smartShipmentExportContext, ref string currentProcessedShipmentNumber);
        bool TestNetworkSettings();        
    }
}
