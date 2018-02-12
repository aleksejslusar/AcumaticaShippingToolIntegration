using System.IO;
using SmartShipment.Information.Logger;
using SmartShipment.UI.FileWatcher.Common;

namespace SmartShipment.UI.FileWatcher
{
    public class SmartShipmentFileWatcher : ISmartShipmentFileWatcher
    {
        private readonly ILogger _logger;
        private FileSystemWatcher _watcher;
        private ISmartShipmentFileProvider _smartShipmentFileProvider;

        public SmartShipmentFileWatcher(ILogger logger)
        {
            _logger = logger;
        }

        public void InitFileWatcher(ISmartShipmentFileProvider fileProvider, string fileName)
        {          
            _smartShipmentFileProvider = fileProvider;
            if (string.IsNullOrEmpty(fileName))
            {
                return;
            }

            if (!File.Exists(fileName))
            {
                _logger.Warn($"File Monitoring: File {fileName} does not exists. Watcher for this file was stopped.");
                return;
            }

            _watcher = new FileSystemWatcher {Filter = fileName.Substring(fileName.LastIndexOf('\\') + 1)};
            _watcher.Path = fileName.Substring(0, fileName.Length - _watcher.Filter.Length);

            _watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            _watcher.IncludeSubdirectories = false;
            
            _watcher.Changed += OnChanged;           
            _watcher.Created += OnChanged;
            _watcher.Deleted += OnChanged;
            _watcher.EnableRaisingEvents = true;

            _logger.Warn($"File Monitoring: Watcher for {fileName} was started.");
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            try
            {
                _watcher.EnableRaisingEvents = false;
                _smartShipmentFileProvider.ReadFile(e.FullPath);
            }
            finally
            {
                _watcher.EnableRaisingEvents = true;
            }            
        }
    }
}