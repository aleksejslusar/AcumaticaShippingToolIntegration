using System;
using System.Collections.Generic;
using SmartShipment.Information;
using SmartShipment.Information.Exceptions;
using SmartShipment.Information.Properties;
using SmartShipment.Network.AcumaticaSoapService;
using SmartShipment.Network.Common;
using SmartShipment.Network.Export;
using SmartShipment.Network.Mapping;

namespace SmartShipment.Network
{
    public class AcumaticaNetworkProvider : IAcumaticaNetworkProvider
    {
        private readonly IBrowserHelper _browserHelper;
        private readonly IWebServiceHelper _webServiceHelper;
        private readonly ISmartShipmentMessagesProvider _messagesProvider;

        public AcumaticaNetworkProvider(IBrowserHelper browserHelper, IWebServiceHelper webServiceHelper, ISmartShipmentMessagesProvider messagesProvider)
        {
            _browserHelper = browserHelper;
            _webServiceHelper = webServiceHelper;
            _messagesProvider = messagesProvider;
        }

        public ShipmentMapper GetShipment(string shipmentNbr)
        {
            try
            {
                if (string.IsNullOrEmpty(shipmentNbr))
                {
                    shipmentNbr = _browserHelper.GetShipmentUriData().ShipmentNbr;
                }

                return _webServiceHelper.GetShipmentByShipmentId(shipmentNbr);
            }
            catch (NetworkActiveBrowserNotFoundException e)
            {
                _messagesProvider.Warn(string.Format(InformationResources.WARN_NO_WAY_TO_GET_SHIPMENT_EXTENDED, e.Message));
                throw;
            }
            catch (NetworkActiveUriNotFoundException e)
            {
                _messagesProvider.Warn(string.Format(InformationResources.WARN_NO_WAY_TO_GET_SHIPMENT_EXTENDED, e.Message));
                throw;
            }
            catch (Exception e)
            {                
                _messagesProvider.Error(new NetworkException(InformationResources.ERROR_CANNOT_CONNECT_TO_ACUMATICA));
                _messagesProvider.Log(e.Message);
                throw;
            }            
        }        
        
        public List<ShippingBox> GetShippingBoxes()
        {
            try
            {
                return _webServiceHelper.GetShippingBoxes();
            }
            catch (Exception e)
            {
                _messagesProvider.Error(e);
            }
            
            return new List<ShippingBox>();
        }

        public void UpdateShipments(List<Shipment> shipments, ISmartShipmentExportContext smartShipmentExportContext)
        {
            try
            {
                _webServiceHelper.UpdateShipments(shipments, smartShipmentExportContext);
            }
            catch (Exception e)
            {
                _messagesProvider.Error(new NetworkException(InformationResources.ERROR_SHIPMENTS_IS_NOT_UPDATED +  e.Message));
            }
        }

        public bool TestNetworkSettings()
        {
            return _webServiceHelper.TestNetworkSettings();
        }

        
    }
}
