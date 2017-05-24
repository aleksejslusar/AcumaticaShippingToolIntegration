using System.Collections.Generic;
using System.Linq;
using SmartShipment.Adapters.Common;
using SmartShipment.Information;
using SmartShipment.Network.Common;
using SmartShipment.Network.Export;
using SmartShipment.Settings;
using SmartShipment.UI.Common;
using SmartShipment.UI.FileWatcher.Common;
using File = System.IO.File;

namespace SmartShipment.UI.FileWatcher
{
    public class SmartShipmentFileProvider : SmartShipmentFileProviderBase, ISmartShipmentFileProvider
    {
        public SmartShipmentFileProvider(IApplicationController applicationController, ISmartShipmentMessagesProvider messagesProvider, ISettings settings, ISmartShipmentExportContext exportContext) : base(messagesProvider, exportContext)
        {
            ExportParameterParser = applicationController.GetContainer().Resolve<ISmartShipmentExportParametersParser>(ApplicationTypes.UpsWorldShip.ToString());
        }

        protected override IEnumerable<string> ParseExportedData(IReadOnlyCollection<string> exportFileData)
        {
            var rawShipmentData = new List<string>();

            if (exportFileData.Any() && exportFileData.Count > 1)
            {
                rawShipmentData = exportFileData.Skip(1).ToList();
            }

            return rawShipmentData;
        }

        protected override void ClearFile(string fileName, IReadOnlyCollection<string> exportFileData)
        {
            if (!exportFileData.Any() || exportFileData.Count <= 1) return;
            var dataHeader = exportFileData.Take(1).ToArray();
            File.WriteAllLines(fileName, dataHeader);
        }
    }
}