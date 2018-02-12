using System.Threading.Tasks;
using SmartShipment.Adapters.Common;
using SmartShipment.Information;
using SmartShipment.Information.Properties;
using SmartShipment.Settings;
using SmartShipment.UI.Common;
using SmartShipment.UI.FileWatcher.Common;

namespace SmartShipment.UI.FileWatcher
{
    public class SmartShipmentFilesMonitor : ISmartShipmentFilesMonitor
    {
        private readonly IApplicationController _applicationController;
        private readonly ISettings _settings;
        private readonly ISmartShipmentMessagesProvider _messagesProvider;

        public SmartShipmentFilesMonitor(IApplicationController applicationController, ISettings settings, ISmartShipmentMessagesProvider messagesProvider)
        {
            _applicationController = applicationController;
            _settings = settings;
            _messagesProvider = messagesProvider;
        }

        public void Start()
        {

            Task.Factory.StartNew(() =>
            {
                var upsExportWatcher = _applicationController.GetContainer().Resolve<ISmartShipmentFileWatcher>();
                var upsFileProvider = _applicationController.GetContainer().Resolve<ISmartShipmentFileProvider>(ApplicationTypes.UpsWorldShip.ToString());                
                _messagesProvider.Log(string.Format(InformationResources.INFO_START_EXPORTFILE_MONITORING, _settings.UpsExportFilePath));
                upsExportWatcher.InitFileWatcher(upsFileProvider, _settings.UpsExportFilePath);
            });
            
            Task.Factory.StartNew(() =>
            {
                var fedexExportWatcher = _applicationController.GetContainer().Resolve<ISmartShipmentFileWatcher>();
                var fedexFileProvider = _applicationController.GetContainer().Resolve<ISmartShipmentFileProvider>(ApplicationTypes.FedExShipmentManager.ToString());
                _messagesProvider.Log(string.Format(InformationResources.INFO_START_EXPORTFILE_MONITORING, _settings.FedexExportFilePath));
                fedexExportWatcher.InitFileWatcher(fedexFileProvider, _settings.FedexExportFilePath);
            });
        }        
    }
}
