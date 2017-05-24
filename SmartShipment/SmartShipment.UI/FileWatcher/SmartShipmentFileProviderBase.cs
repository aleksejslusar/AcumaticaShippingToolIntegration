using System.Collections.Generic;
using System.IO;
using System.Threading;
using SmartShipment.Information;
using SmartShipment.Information.Properties;
using SmartShipment.Network.Common;
using SmartShipment.Network.Export;
using File = System.IO.File;

namespace SmartShipment.UI.FileWatcher
{
    public abstract class SmartShipmentFileProviderBase
    {
        protected ISmartShipmentExportParametersParser ExportParameterParser;
        protected IAcumaticaNetworkProvider NetworkProvider;
        private readonly ISmartShipmentMessagesProvider _messagesProvider;
        private readonly ISmartShipmentExportContext _exportContext;

        protected SmartShipmentFileProviderBase(ISmartShipmentMessagesProvider messagesProvider, ISmartShipmentExportContext exportContext)
        {
            _messagesProvider = messagesProvider;
            _exportContext = exportContext;
        }

        protected abstract IEnumerable<string> ParseExportedData(IReadOnlyCollection<string> exportFileData);
        protected abstract void ClearFile(string fileName, IReadOnlyCollection<string> exportFileData);

        public void ReadFile(string fileName)
        {
            string[] exportFileData;

            //TODO: Need to refactoring later 
            //Pause before start reading
            Thread.Sleep(1000);

            while (true)
            {
                try
                {
                    exportFileData = File.ReadAllLines(fileName);
                    break;
                }
                catch (IOException)
                {
                    Thread.Sleep(300);              
                }
            }                

            ProcessDataFile(fileName, exportFileData);
        }

        protected void ProcessDataFile(string fileName, IReadOnlyCollection<string> exportFileData)
        {
            var rowCount = exportFileData.Count;
            var exportedData = ParseExportedData(exportFileData);
            try
            {
                var shipmentFileExportRows = exportedData.ToShipmentExportedRows(ExportParameterParser);
                _exportContext.ExportData(shipmentFileExportRows).UpdateAcumatica();
                _messagesProvider.Log(string.Format(InformationResources.INFO_EXPORT_FILE_PROCESSED, fileName, rowCount));
            }
            finally
            {
                ClearFile(fileName, exportFileData);
            }  
        } 
    }
}