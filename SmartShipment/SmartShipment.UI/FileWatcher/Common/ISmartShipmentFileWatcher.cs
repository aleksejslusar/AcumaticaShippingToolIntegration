namespace SmartShipment.UI.FileWatcher.Common
{
    public interface ISmartShipmentFileWatcher
    {
        void InitFileWatcher(ISmartShipmentFileProvider fileProvider, string fileName);
    }
}