namespace SmartShipment.Network.Export
{
    public abstract class SmartShipmentExportParametersParserBase
    {
        public bool ParseVoidIndicator(string stringValue)
        {
            return Trim(stringValue).Equals("Y");
        }

        public string Trim(string stringValue)
        {
            return stringValue.Trim('"');
        }

        public decimal ParseDecimal(string stringValue)
        {
            decimal decimalValue;
            decimal.TryParse(Trim(stringValue), out decimalValue);
            return decimalValue;
        }

        public long ParseRowNumber(string stringValue)
        {
            long longValue;
            long.TryParse(Trim(stringValue), out longValue);
            return longValue;
        }
    }
}