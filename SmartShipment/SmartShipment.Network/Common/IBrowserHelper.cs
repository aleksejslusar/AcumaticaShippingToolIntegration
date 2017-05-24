namespace SmartShipment.Network.Common
{
    public interface IBrowserHelper
    {
        ParsedShipmentData GetShipmentUriData();
        void ReloadActiveBrowserPage(string shipmentId);
    }
}
