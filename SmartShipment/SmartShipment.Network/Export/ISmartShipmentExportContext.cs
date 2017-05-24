using System.Collections.Generic;
using SmartShipment.Network.AcumaticaSoapService;

namespace SmartShipment.Network.Export
{
    public interface ISmartShipmentExportContext
    {
        SmartShipmentExportContext ExportData(IEnumerable<ShipmentFileExportRow> shipmentFileExportRows);
        SmartShipmentExportContext UpdateAcumatica();
        void MapTargetToExportedShipment(Shipment targetShipment, Shipment sourceShipment);
    }
}