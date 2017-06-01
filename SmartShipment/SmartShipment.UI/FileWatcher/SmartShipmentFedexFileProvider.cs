using System.Collections.Generic;
using System.IO;
using System.Linq;
using SmartShipment.Adapters.Common;
using SmartShipment.Information;
using SmartShipment.Network.Common;
using SmartShipment.Network.Export;
using SmartShipment.Settings;
using SmartShipment.UI.Common;
using SmartShipment.UI.FileWatcher.Common;

namespace SmartShipment.UI.FileWatcher
{
    public class SmartShipmentFedexFileProvider : SmartShipmentFileProviderBase, ISmartShipmentFileProvider
    {
        private const string PROCESSED_ROWS_MARKER = "\"*\"";
        private const string NOT_PROCESSED_ROWS_MARKER = "\"\"";
        private const string DELETED_ROWS_MARKER = "\"Y\"";
        private const int RAW_SHIPMENT_ROW_LENGTH = 8;

        public SmartShipmentFedexFileProvider(IApplicationController applicationController, ISmartShipmentMessagesProvider messagesProvider, ISettings settings, ISmartShipmentExportContext exportContext): base(messagesProvider, exportContext)
        {
            ExportParameterParser = applicationController.GetContainer().Resolve<ISmartShipmentExportParametersParser>(ApplicationTypes.FedExShipmentManager.ToString());
        }

        protected override IEnumerable<string> ParseExportedData(IReadOnlyCollection<string> exportFileData)
        {
            var rawShipmentData = new List<string>();
            if (!exportFileData.Any() || exportFileData.Count <= 1) return rawShipmentData;

            foreach (var stringArray in exportFileData.Skip(1).Select(r => r.StringToRawShipmentRow()).Where(r => r.Last() != PROCESSED_ROWS_MARKER))
            {
                rawShipmentData.Add(stringArray.RawShipmentRowToString());
            }

            return rawShipmentData;
        }

        protected override void ClearFile(string fileName, IReadOnlyCollection<string> exportFileData)
        {
            var consistentFileData = new List<string>();
            var rawShipmentData = new List<string>();

            if (!exportFileData.Any() || exportFileData.Count <= 1) return;
           
            //Add header
            consistentFileData.AddRange(exportFileData.Take(1).ToArray());
            
            //Add shipment data
            foreach (var stringArray in exportFileData.Select(r => r.StringToRawShipmentRow()).Where(r => r.Length == RAW_SHIPMENT_ROW_LENGTH 
                                                                                                       && r.Last() == NOT_PROCESSED_ROWS_MARKER || r.Last() == PROCESSED_ROWS_MARKER))
            {
                if (stringArray[RAW_SHIPMENT_ROW_LENGTH - 1] == NOT_PROCESSED_ROWS_MARKER)
                {
                    stringArray[RAW_SHIPMENT_ROW_LENGTH - 1] = PROCESSED_ROWS_MARKER;
                }

                rawShipmentData.Add(stringArray.RawShipmentRowToString());
            }

            // Fedex Ship Manager create rows in export file for each ship operation.
            // When user make ship, but not delete this one, and then shipped again - as result in export file stay rows which will never be processed.
            // Here we clear file from duplicated shipment rows and use only last export result.
            var clearedRawData = rawShipmentData.Select(r => r.StringToRawShipmentRow())
                                                .GroupBy(r => r[0])
                                                .ToDictionary(s => s.Key, s => s.GroupBy(e => e[2]).LastOrDefault())
                                                .ToDictionary(s => s.Key, s => s.Value)
                                                .SelectMany(c => c.Value)
                                                .Select(c => c.RawShipmentRowToString())
                                                .ToList();


            if (clearedRawData.Any())
            {
                consistentFileData.AddRange(clearedRawData);
            }

            File.WriteAllLines(fileName, consistentFileData);
        }
    }
}