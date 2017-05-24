using System;
using System.Collections.Generic;
using SmartShipment.Network.Common;

namespace SmartShipment.Network.Export
{
    public class ShipmentFileExportRow
    {       
        public ShipmentFileExportRow(IReadOnlyList<string> rawShipmentData, ISmartShipmentExportParametersParser exportParametersParser)
        {
            ShipmentNbr             = exportParametersParser.Trim(rawShipmentData[0]);
            RowNumber               = exportParametersParser.ParseRowNumber(rawShipmentData[1]);
            ShipmentTrackingNumber  = exportParametersParser.Trim(rawShipmentData[2]);
            PackageTrackingNumber   = exportParametersParser.Trim(rawShipmentData[3]);
            FreightCost             = exportParametersParser.ParseDecimal((rawShipmentData[4]));
            ShipmentDate            = exportParametersParser.ParseShipmentDateTime(rawShipmentData[5]);
            PackageWeight           = exportParametersParser.ParseDecimal(rawShipmentData[6]);
            VoidIndicator           = exportParametersParser.ParseVoidIndicator(rawShipmentData[7]);
        }



        /* Export file header
         * ------------------------ 
         * ShipmentNbr,
         * RowNumber,
         * ShipmentTrackingNumber,
         * PackageTrackingNumber,
         * FreightCost,
         * ShipmentDate,
         * PackageWeight,
         * VoidIndicator
         */

        public string ShipmentNbr { get; set; }
        public long RowNumber { get; set; }
        public string ShipmentTrackingNumber { get; set; }
        public string PackageTrackingNumber { get; set; }
        public decimal FreightCost { get; set; }
        public DateTime ShipmentDate { get; set; }
        public decimal PackageWeight { get; set; }
        public bool VoidIndicator { get; set; }        
    }
}
