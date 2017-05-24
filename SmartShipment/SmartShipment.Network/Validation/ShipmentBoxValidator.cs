using System.Collections.Generic;
using System.Linq;
using SmartShipment.Information.Properties;
using SmartShipment.Network.Common;
using SmartShipment.Network.Mapping;

namespace SmartShipment.Network.Validation
{
    public class ShipmentBoxValidator : ShipmentValidatorBase
    {
        private readonly IAcumaticaNetworkProvider _networkProvider;

        public ShipmentBoxValidator(IAcumaticaNetworkProvider networkProvider)
        {
            _networkProvider = networkProvider;
        }

        public override Dictionary<string, string> HandleValidation(ShipmentMapper shipment)
        {
            if (!shipment.Packages.Any() && !_networkProvider.GetShippingBoxes().Any())
            {
                ErrorsResult.Add("Shipment Box Validation error", string.Format(InformationResources.WARN_SHIPMENT_VALIDATION_BOX, shipment.ShipmentNbr.Value));
                return ErrorsResult;
            }

            return Successor != null ? Successor.HandleValidation(shipment) : ErrorsResult;
        }
    }
}