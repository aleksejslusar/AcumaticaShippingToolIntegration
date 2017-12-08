using System;
using System.IO;
using IniParser;
using SmartShipment.Settings.SettingsHelper;

namespace SmartShipment.Setup.CustomActions.SetupHelpers
{
    public class UpsSetupHelper
    {
        private readonly SmartShipmentsSettingsHelper _settingsProviderHelper;
        private readonly ISetupLogger _logger;
        private const string UPS_BASE_DATA_PATH = "UPS\\WSTD\\ImpExp\\Shipment";
        private readonly string _upsShipmentMapPath;

        public UpsSetupHelper(SmartShipmentsSettingsHelper settingsProviderHelper, ISetupLogger logger)
        {
            _settingsProviderHelper = settingsProviderHelper;
            _logger = logger;
            _upsShipmentMapPath = Path.Combine(_settingsProviderHelper.ProgramDataPath, UPS_BASE_DATA_PATH, "UPSOUT.dat");
        }

        public bool Install()
        {
            _logger.Info("UPS Woprldship: setup environment started");
            try
            {                
                CopyMapFile();
                
                InstallAutoMapsSettings(_upsShipmentMapPath);
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
            if (File.Exists(_upsShipmentMapPath))
            {
                File.Delete(_upsShipmentMapPath);
            }

            _logger.Info("UPS Woprldship: end uninstall");
        }

        private void CopyMapFile()
        {
            var source = Path.Combine(_settingsProviderHelper.ApplicationBasePath, "Devices\\UPS", "UPSOUT.dat");            

            if (File.Exists(source))
            {
                File.Copy(source, _upsShipmentMapPath, true);
                _logger.Info("UPS Worldship: copy files from: "+ source + " to: " + _upsShipmentMapPath);
            }
            else
            {
                _logger.Error("UPS Worldship: copy files error: file not found " + source);
                throw new Exception();
            }
        }

        private void InstallAutoMapsSettings(string mapName)
        {
            var mapIniPath = Path.Combine(_settingsProviderHelper.ProgramDataPath, "UPS\\WSTD\\wstdAutoExportMaps.ini");

            if (File.Exists(mapIniPath))
            {
                if (string.IsNullOrWhiteSpace(mapName))
                {
                    _logger.Info("UPS Worldship: uninstall active auto map");
                }
                else
                {
                    _logger.Info("UPS Worldship: install auto map: " + mapName);
                }
               
                var iniParser = new FileIniDataParser();
                var iniData = iniParser.ReadFile(mapIniPath);
                iniData["AutoExportShipments"]["ExportMapName"] = mapName;                
                iniData["AutoExportFreightShipments"]["ExportMapName"] = mapName;                
                iniParser.WriteFile(mapIniPath, iniData);
            }
            else
            {
                _logger.Error("UPS Worldship: install auto maps: error: file not found " + mapIniPath);
                throw new Exception();
            }
        }

        private  void InstallShupUserSettings(string mapName)
        {
            var menuIniPath = Path.Combine(_settingsProviderHelper.ProgramDataPath, "UPS\\WSTD\\wstdShipuser.ini");

            if (File.Exists(menuIniPath))
            {
                if (string.IsNullOrWhiteSpace(mapName))
                {
                    _logger.Info("UPS Worldship: uninstall active auto map from user menu");
                }
                else
                {
                    _logger.Info("UPS Worldship: install auto maps to user menu: " + mapName);      
                }
                var iniParser = new FileIniDataParser();
                var iniData = iniParser.ReadFile(menuIniPath);

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

                iniParser.WriteFile(menuIniPath, iniData);
            }
            else
            {
                _logger.Info("UPS Worldship: install auto maps to user menu: error: file not found " + menuIniPath);
                throw new Exception();
            }
        }
    }
}