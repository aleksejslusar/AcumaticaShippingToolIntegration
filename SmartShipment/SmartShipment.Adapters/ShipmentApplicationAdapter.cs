using System;
using System.Linq;
using SmartShipment.Adapters.Common;
using SmartShipment.Information;
using SmartShipment.Information.Exceptions;
using SmartShipment.Information.Properties;
using SmartShipment.Network.Common;
using SmartShipment.Network.Mapping;
using SmartShipment.Network.Validation;

namespace SmartShipment.Adapters
{
    public class ShipmentApplicationAdapter : IShipmentApplicationAdapter
    {
        private readonly IAcumaticaNetworkProvider _acumaticaNetworkProvider;
        private readonly ISmartShipmentMessagesProvider _messagesProvider;


        public ShipmentApplicationAdapter(IAcumaticaNetworkProvider acumaticaNetworkProvider, ISmartShipmentMessagesProvider messagesProvider)
        {
            _acumaticaNetworkProvider = acumaticaNetworkProvider;
            _messagesProvider = messagesProvider;  
        }

           

        public void RunShipmentApplication(IShipmentApplicationHelper applicationHelper, string shipmentNbr = null)
        {
            try
            {

                var shipment = GetShipment(shipmentNbr);
                
                //Validate shipment                    
                var shipmentValidationResult = new ShipmentValidationContext(shipment, _acumaticaNetworkProvider).Validate();
                if (shipmentValidationResult.Any())
                {
                    _messagesProvider.Warn(shipmentValidationResult.First().Value);
                    return;
                }

                if (applicationHelper.RunShipmentApplication(shipment.ShipmentNbr.Value) && applicationHelper.PopulateApplicaitonControlMap())
                {
                    _messagesProvider.Log(string.Format(InformationResources.INFO_RECEIVED_SHIPMENT, shipment.ShipmentNbr.Value, shipment.Packages.Count));
                    applicationHelper.PopulateApplicaitonByShipmentData(shipment);
                }
                
            }                
            catch (Exception e)
            {
                if (!(e is NetworkException)) //IAcumaticaNetworkProvider handles own errors someself
                {
                    _messagesProvider.Fatal(e);
                }
            }             
        }        

        private ShipmentMapper GetShipment(string shipmentNbr)
        {
            return _acumaticaNetworkProvider.GetShipment(shipmentNbr);
        }
    }
}
