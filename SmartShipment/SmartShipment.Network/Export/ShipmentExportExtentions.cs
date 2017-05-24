using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using SmartShipment.Network.AcumaticaSoapService;
using SmartShipment.Network.Common;

namespace SmartShipment.Network.Export
{
    public static class ShipmentExportExtentions
    {
        private const char SPLIT_SEPARATOR = ',';        
        private const int COL_LENGTH = 8;

        public static string[] StringToRawShipmentRow(this string line)
        {
            return line.Split(SPLIT_SEPARATOR);
        }

        public static string RawShipmentRowToString(this string[] rawShipmentRow)
        {
            return string.Join(",", rawShipmentRow);
        }
        
        public static IEnumerable<ShipmentFileExportRow> ToShipmentExportedRows(this IEnumerable<string> rawDataShipment, ISmartShipmentExportParametersParser exportParametersParser)
        {
            var shipmentFileExportRows =  rawDataShipment.Select(rawShipment => rawShipment.StringToRawShipmentRow())
                                                        .Where(rawShipment => rawShipment.Length == COL_LENGTH)
                                                        .Select(rawShipment => new ShipmentFileExportRow(rawShipment, exportParametersParser))
                                                        .ToList();
            //Try to set shipmentNbr to all row
            foreach (var shipmentTrackingGroup in shipmentFileExportRows.GroupBy(s => s.ShipmentTrackingNumber))
            {
                var shipmentFileExportRow = shipmentTrackingGroup.FirstOrDefault(s => !string.IsNullOrEmpty(s.ShipmentNbr));
                if (shipmentFileExportRow != null)
                {
                    var shipmentNbr = shipmentFileExportRow.ShipmentNbr;
                    foreach (var row in shipmentTrackingGroup.Where(row => string.IsNullOrEmpty(row.ShipmentNbr)))
                    {
                        row.ShipmentNbr = shipmentNbr;
                    }
                }
            }
            
            return shipmentFileExportRows;
        }

      
    }
}