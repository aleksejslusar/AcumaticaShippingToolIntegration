using System;
using System.IO;
using IniParser;
using SmartShipment.Settings.SettingsHelper;
using SmartShipment.Settings.SettingsProvider;

namespace SmartShipment.Setup.CustomActions.SetupHelpers
{
    public class UpsSetupHelper
    {
        private readonly SmartShipmentsSettingsHelper _settingsProviderHelper;
        private readonly ISetupLogger _logger;
        private const string UPS_BASE_DATA_PATH = "C:\\ProgramData\\UPS\\WSTD\\ImpExp\\Shipment";
        private static readonly string UpsShipmentMapsPath = Path.Combine(UPS_BASE_DATA_PATH, "UPSOUT.dat");

        public UpsSetupHelper(SmartShipmentsSettingsHelper settingsProviderHelper, ISetupLogger logger)
        {
            _settingsProviderHelper = settingsProviderHelper;
            _logger = logger;
        }

        public bool Install()
        {
            _logger.Info("UPS Woprldship: setup environment started");
            try
            {                
                CopyMapFile();
                
                InstallAutoMapsSettings(UpsShipmentMapsPath);
                InstallShupUserSettings("UPSOUT");
                _logger.Info("UPS Woprldship: setup environment complete");
                return true;
            }
            catch (Exception e)
            {
                _logger.Error("UPS Woprldship: setup environment error " + e);
                return false;
            }            
        }

        public void Uninstall()
        {
            _logger.Info("UPS Woprldship: begin uninstall");
            //Clear UPS ini files
            InstallAutoMapsSettings("");
            InstallShupUserSettings("");

            //Delete map
            var target = Path.Combine(UPS_BASE_DATA_PATH, "UPSOUT.dat");
            if (File.Exists(target))
            {
                File.Delete(target);
            }

            _logger.Info("UPS Woprldship: end uninstall");
        }

        private void CopyMapFile()
        {
            var source = Path.Combine(_settingsProviderHelper.ApplicationBasePath, "Devices\\UPS", "UPSOUT.dat");            

            if (File.Exists(source))
            {
                File.Copy(source, UpsShipmentMapsPath, true);
                _logger.Info("UPS Worldship: copy files from: "+ source + " to: " + UpsShipmentMapsPath);
            }
            else
            {
                _logger.Error("UPS Worldship: copy files error: file not found " + source);
                throw new Exception();
            }
        }

        private void InstallAutoMapsSettings(string mapName)
        {
            const string path = "C:\\ProgramData\\UPS\\WSTD\\wstdAutoExportMaps.ini";

            if (File.Exists(path))
            {
                _logger.Info("UPS Worldship: install auto maps:" + mapName);
                var iniParser = new FileIniDataParser();
                var iniData = iniParser.ReadFile(path);
                iniData["AutoExportShipments"]["ExportMapName"] = mapName;                
                iniData["AutoExportFreightShipments"]["ExportMapName"] = mapName;                
                iniParser.WriteFile(path, iniData);
            }
            else
            {
                _logger.Error("UPS Worldship: install auto maps: error: file not found " + path);
                throw new Exception();
            }
        }

        private  void InstallShupUserSettings(string mapName)
        {
            const string path = "C:\\ProgramData\\UPS\\WSTD\\wstdShipuser.ini";

            if (File.Exists(path))
            {
                _logger.Info("UPS Worldship: install auto maps to user menu: " + mapName);
                var iniParser = new FileIniDataParser();
                var iniData = iniParser.ReadFile(path);

                //AutoExportRecent
                if (!iniData["UPS OnLine Connect"].ContainsKey("AutoExportRecent"))
                {
                    return;
                }

                var data = iniData["UPS OnLine Connect"]["AutoExportRecent"];
                if (!string.IsNullOrEmpty(mapName))
                {
                    var delimiter = !string.IsNullOrEmpty(data) ? "," : "";
                    if (!data.Contains(mapName))
                    {
                        iniData["UPS OnLine Connect"]["AutoExportRecent"] = mapName + delimiter + data;
                    }
                }
                else
                {
                    iniData["UPS OnLine Connect"]["AutoExportRecent"] = "{ Multiple Maps }";
                }

                //FreightAutoExportRecent
                if (!iniData["UPS OnLine Connect"].ContainsKey("FreightAutoExportRecent"))
                {
                    return;
                }

                data = iniData["UPS OnLine Connect"]["FreightAutoExportRecent"];
                if (!string.IsNullOrEmpty(mapName))
                {
                    var delimiter = !string.IsNullOrEmpty(data) ? "," : "";
                    if (!data.Contains(mapName))
                    {
                        iniData["UPS OnLine Connect"]["FreightAutoExportRecent"] = mapName + delimiter + data;
                    }
                }
                else
                {
                    iniData["UPS OnLine Connect"]["FreightAutoExportRecent"] = "";
                }

                iniParser.WriteFile(path, iniData);
            }
            else
            {
                _logger.Info("UPS Worldship: install auto maps to user menu: error: file not found " + path);
                throw new Exception();
            }
        }
    }
}