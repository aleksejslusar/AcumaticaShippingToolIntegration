using System.Collections.Generic;
using SmartShipment.Network.Common;
using SmartShipment.Network.Mapping;

namespace SmartShipment.Network.Validation
{
    public class ShipmentValidationContext
    {
        private readonly ShipmentMapper _shipment;
        private readonly ShipmentOpenValidator _shipmentOpenValidator;

        public ShipmentValidationContext(ShipmentMapper shipment, IAcumaticaNetworkProvider networkProvider)
        {
            _shipment = shipment;
            _shipmentOpenValidator = new ShipmentOpenValidator();
            var shipmentTypeValidator = new ShipmentTypeValidator();
            var shipmentBoxValidator = new ShipmentBoxValidator(networkProvider);

            _shipmentOpenValidator.SetSuccessor(shipmentTypeValidator);
            shipmentTypeValidator.SetSuccessor(shipmentBoxValidator);
        }

        public Dictionary<string, string> Validate()
        {
           return _shipmentOpenValidator.HandleValidation(_shipment);
        } 
    }
}