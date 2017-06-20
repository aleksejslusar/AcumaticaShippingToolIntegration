using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using SmartShipment.Network.AcumaticaSoapService;
using SmartShipment.Network.Common;
using SmartShipment.Network.Export;
using SmartShipment.Network.Mapping;
using SmartShipment.Settings;

namespace SmartShipment.Network
{
    public class WebServiceRestHelper : IWebServiceHelper        
    {
        public ShipmentMapper GetShipmentByShipmentId(ParsedShipmentData shipmentId)
        {
            throw new NotImplementedException();
        }

        public ShipmentMapper GetShipmentByShipmentId(string shipmentId)
        {
            throw new NotImplementedException();
        }

        public List<ShippingBox> GetShippingBoxes()
        {
            throw new NotImplementedException();
        }

        public void UpdateShipments(List<Shipment> shipments, ISmartShipmentExportContext smartShipmentExportContext, ref string currentProcessedShipmentNumber)
        {
            throw new NotImplementedException();
        }

        public bool TestNetworkSettings()
        {
            throw new NotImplementedException();
        }
       
    }

    public class ShipmentHttpClient : IDisposable
    {                
        public void Dispose()
        {

        }
    }
}