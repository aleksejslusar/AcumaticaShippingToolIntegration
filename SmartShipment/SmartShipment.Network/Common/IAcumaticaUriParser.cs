namespace SmartShipment.Network.Common
{
    public interface IAcumaticaUriParser
    {
        ParsedShipmentData GetShipmentId(string uri);
    }
}
