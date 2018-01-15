using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using SmartShipment.Information;
using SmartShipment.Information.Properties;
using SmartShipment.Network.AcumaticaSoapService;
using SmartShipment.Network.Common;
using SmartShipment.Network.Export;
using SmartShipment.Network.Mapping;
using SmartShipment.Settings;

namespace SmartShipment.Network
{
    public class WebServiceSoapHelper : IWebServiceHelper
    {
        private readonly ISettings _settings;
        private readonly IBrowserHelper _browserHelper;
        private readonly ISmartShipmentMessagesProvider _messagesProvider;

        public WebServiceSoapHelper(ISettings settings, IBrowserHelper browserHelper, ISmartShipmentMessagesProvider messagesProvider)
        {
            _settings = settings;
            _browserHelper = browserHelper;
            _messagesProvider = messagesProvider;
        }

        public ShipmentMapper GetShipmentByShipmentId(string shipmentId)
        {
            return GetShipment(shipmentId);
        }

        public List<ShippingBox> GetShippingBoxes()
        {
            using (var soapClient = new ShipmentSoapClient(_settings))
            {
                return soapClient.GetShipmentBoxes();
            }
        }

        public void UpdateShipments(List<Shipment> shipments, ISmartShipmentExportContext smartShipmentExportContext, ref string currentProcessedShipmentNumber)
        {            
            using (var soapClient = new ShipmentSoapClient(_settings))
            {
                foreach (var shipment in shipments.ToList())
                {
                    currentProcessedShipmentNumber = shipment.ShipmentNbr.Value;
                    var targetShipment = soapClient.GetShipmentByShipmentId(shipment.ShipmentNbr.Value);
                    smartShipmentExportContext.MapTargetToExportedShipment(targetShipment, shipment);
                    soapClient.UpdateShipment(targetShipment, _messagesProvider);
                }                
            }

            if (shipments.Any() && shipments.Last() != null)
            {
                _browserHelper.ReloadActiveBrowserPage(shipments.Last().ShipmentNbr.Value);
            }

            var shipmentsNumbers = shipments.Select(s => s.ShipmentNbr.Value);
            _messagesProvider.Log(string.Format(InformationResources.INFO_SUCCESS_UPDATE_SHIPMENT, shipments.Count, string.Join(", ", shipmentsNumbers)));
        }

        public bool TestNetworkSettings()
        {
            using (new ShipmentSoapClient(_settings)){}
            return true;
        }

        private ShipmentMapper GetShipment(string shipmentId)
        {
            Shipment shipment;
            using (var soapClient = new ShipmentSoapClient(_settings))
            {
                shipment = soapClient.GetShipmentByShipmentId(shipmentId);
            }

            return new ShipmentMapper().MapShipment(shipment);
        }

    }

    public class ShipmentSoapClient : IDisposable
    {
        private readonly DefaultSoapClient _soapClient;

        readonly BasicHttpBinding _httpBinding = new BasicHttpBinding
        {
            AllowCookies = true,
            MaxReceivedMessageSize = 6553600,
            SendTimeout = new TimeSpan(0, 0, 0, 30) //30 sec
        };

        private readonly ISettings _settings;

        public Shipment GetShipmentByShipmentId(string shipmentId)
        {
            var shipment = new Shipment
            {
                ShipmentNbr =  new StringSearch { Value = shipmentId }                
            };           

            return (Shipment)_soapClient.Get(shipment);           
        }

        public List<ShippingBox> GetShipmentBoxes()
        {
            var shippingBox = new ShippingBox();
            var shippingBoxes = _soapClient.GetList(shippingBox);
            return shippingBoxes.Cast<ShippingBox>().ToList();
        }

        public void UpdateShipment(Shipment shipment, ISmartShipmentMessagesProvider messagesProvider)
        {                        
            if (_settings.AcumaticaConfirmShipment && shipment.Packages.All(p => !string.IsNullOrEmpty(p.TrackingNumber.Value))) //User ship shipment and 
            {
                var invokeResult = _soapClient.Invoke(shipment, new ConfirmShipment());
                var processResult = _soapClient.GetProcessStatus(invokeResult);
                while (processResult.Status == ProcessStatus.InProcess)
                {
                    Thread.Sleep(1000); //pause for 1 second
                    processResult = _soapClient.GetProcessStatus(invokeResult);
                }
            }
            else if (_settings.AcumaticaConfirmShipment && shipment.Packages.All(p => string.IsNullOrEmpty(p.TrackingNumber.Value)))
            {
                messagesProvider.Warn(string.Format(InformationResources.WARN_SHIPMENT_IS_CONFIRMED_AND_CANNOT_BE_UPDATED, shipment.ShipmentNbr.Value));
            }
            else
            {
                _soapClient.Put(shipment);
            }
        }

        public ShipmentSoapClient(ISettings settings)
        {
            _settings = settings;
            var webServiceUrl = settings.AcumaticaBaseUrl + settings.AcumaticaSoapDataEndpoint;
            var remoteAddress = new EndpointAddress(webServiceUrl);
            
            //Set connection secure mode https/http
            _httpBinding.Security = remoteAddress.Uri.Scheme == Uri.UriSchemeHttps ? 
                                    new BasicHttpSecurity {Mode = BasicHttpSecurityMode.Transport} : 
                                    new BasicHttpSecurity { Mode = BasicHttpSecurityMode.None };

            _soapClient = new DefaultSoapClient(_httpBinding, remoteAddress);
            _soapClient.Login(settings.AcumaticaLogin, settings.AcumaticaPassword, settings.AcumaticaCompany, null, null);
        }

        public void Dispose()
        {
            _soapClient.Logout();            
        }

        public List<Shipment> GetShipmentsByShipmentId(List<Shipment> searchShipments)
        {
            return searchShipments.Select(s => _soapClient.Get(s)).Cast<Shipment>().ToList();
        }
    }
}