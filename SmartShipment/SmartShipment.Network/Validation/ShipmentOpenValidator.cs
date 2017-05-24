using System.Collections.Generic;
using SmartShipment.Information.Properties;
using SmartShipment.Network.Mapping;

namespace SmartShipment.Network.Validation
{
    public class ShipmentOpenValidator : ShipmentValidatorBase
    {
        public override Dictionary<string, string> HandleValidation(ShipmentMapper shipment)
        {
            if (shipment.Status.Value != "Open" || shipment.Hold.Value == true)
            {
                ErrorsResult.Add("Shipment Status Validation error", string.Format(InformationResources.WARN_SHIPMENT_VALIDATION_OPEN, shipment.ShipmentNbr.Value));
                return ErrorsResult;
            }

            return Successor != null ? Successor.HandleValidation(shipment) : ErrorsResult;
        }
    }
}