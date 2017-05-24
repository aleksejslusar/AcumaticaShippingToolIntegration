using System;
using System.Globalization;
using SmartShipment.Network.Common;

namespace SmartShipment.Network.Export
{
    public class SmartShipmentUpsExportParametersParser : SmartShipmentExportParametersParserBase, ISmartShipmentExportParametersParser
    {
        public DateTime ParseShipmentDateTime(string stringValue)
        {
            DateTime shipmentDate;
            var dataProvider = CultureInfo.CurrentCulture;
            var dateFormat = "yyyyMMddHHmmss";
            DateTime.TryParseExact(Trim(stringValue), dateFormat, dataProvider, DateTimeStyles.None, out shipmentDate);
            return shipmentDate;
        }
    }
}