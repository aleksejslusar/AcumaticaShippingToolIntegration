using System.Collections.Generic;
using SmartShipment.Network.Mapping;

namespace SmartShipment.Network.Validation
{
    public abstract class ShipmentValidatorBase
    {
        protected ShipmentValidatorBase Successor { get; private set; }
        protected Dictionary<string, string> ErrorsResult { get; set; }

        protected ShipmentValidatorBase()
        {
            ErrorsResult = new Dictionary<string, string>();
        }

        public abstract Dictionary<string, string> HandleValidation(ShipmentMapper shipment);

        /// <summary>
        /// Set next validation
        /// </summary>
        /// <param name="successor"></param>
        public void SetSuccessor(ShipmentValidatorBase successor)
        {
            Successor = successor;
        }
    }
}