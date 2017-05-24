using System;

namespace SmartShipment.Network.Common
{
    public interface ISmartShipmentExportParametersParser
    {
        DateTime ParseShipmentDateTime(string stringValue);
        long ParseRowNumber(string stringValue);
        bool ParseVoidIndicator(string stringValue);
        decimal ParseDecimal(string stringValue);
        string Trim(string stringValue);
    }
}