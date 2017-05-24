using System.Collections.Generic;
using SmartShipment.Information.Properties;
using SmartShipment.Network.Mapping;

namespace SmartShipment.Network.Validation
{
    public class ShipmentTypeValidator : ShipmentValidatorBase
    {
        public override Dictionary<string, string> HandleValidation(ShipmentMapper shipment)
        {
            if (shipment.Type.Value != "Shipment")
            {
                ErrorsResult.Add("Shipment Type Validation Error", string.Format(InformationResources.WARN_SHIPMENT_VALIDATION_TYPE, shipment.ShipmentNbr.Value));
                return ErrorsResult;
            }

            return Successor != null ? Successor.HandleValidation(shipment) : ErrorsResult;
        }
    }
}