using System;
using SmartShipment.Network.Common;

namespace SmartShipment.Network.Export
{
    public class SmartShipmentFedexExportParametersParser : SmartShipmentExportParametersParserBase, ISmartShipmentExportParametersParser
    {               
        public DateTime ParseShipmentDateTime(string stringValue)
        {
            DateTime shipmentDate;
            DateTime.TryParse(Trim(stringValue), out shipmentDate);
            return shipmentDate;
        }
    }
}
