using System;
using System.IO;
using System.Windows.Forms;
using IniParser;
using Microsoft.Win32;
using SmartShipment.Settings.SettingsHelper;

namespace SmartShipment.Setup.CustomActions.SetupHelpers
{
    public class UpsSetupHelper
    {
        private readonly SmartShipmentsSettingsHelper _settingsProviderHelper;
        private readonly ISetupLogger _logger;
        private const string UPS_BASE_DATA_PATH = "ImpExp\\Shipment";
        private const string UPS_DATA_DIRECTORY_LOCATION = "UPS\\WSTD\\";
        private readonly string _upsShipmentDataDirectoryLocation;
        private readonly string _upsShipmentMapPath;

        public UpsSetupHelper(SmartShipmentsSettingsHelper settingsProviderHelper, ISetupLogger logger)
        {           
            _settingsProviderHelper = settingsProviderHelper;
            _logger = logger;
                        
            _upsShipmentDataDirectoryLocation = GetUpsDataDirectoryLocation();
            _upsShipmentMapPath = Path.Combine(_upsShipmentDataDirectoryLocation, UPS_BASE_DATA_PATH, "UPSOUT.dat");
        }



        public bool Install()
        {
            _logger.Info("UPS Woprldship: setup environment started");
            try
            {                
                CopyMapFile();
                
                InstallAutoMapsSettings(_upsShipmentMapPath);
                InstallShipUserSettings("UPSOUT");
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
            InstallShipUserSettings("");

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

            var directoryPath = Path.Combine(_upsShipmentDataDirectoryLocation, UPS_BASE_DATA_PATH);
            
            if (!Directory.Exists(directoryPath))
            {
                _logger.Error($"UPS Worldship: install auto maps: directory {directoryPath} does not exist");
                throw new Exception();
            }

            var mapIniPath = Path.Combine(_upsShipmentDataDirectoryLocation, "wstdAutoExportMaps.ini");

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

        private  void InstallShipUserSettings(string mapName)
        {
            var menuIniPath = Path.Combine(_upsShipmentDataDirectoryLocation, "wstdShipuser.ini");

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
                if (!iniData.Sections.ContainsSection("UPS OnLine Connect") || !iniData["UPS OnLine Connect"].ContainsKey("AutoExportRecent"))
                {
                    _logger.Info("WARNING:UPS Worldship: install auto maps to user menu: section UPS OnLine Connect or key AutoExportRecent does not found in file " + menuIniPath);
                    if (!iniData.Sections.ContainsSection("UPS OnLine Connect"))
                    {
                        iniData.Sections.AddSection("UPS OnLine Connect");
                    }

                    if (!iniData["UPS OnLine Connect"].ContainsKey("AutoExportRecent"))
                    {
                        iniData["UPS OnLine Connect"].AddKey("AutoExportRecent");
                    }
                }
                
                //AutoExportRecent: Set section value
                var autoExportData = iniData["UPS OnLine Connect"]["AutoExportRecent"];
                if (!string.IsNullOrEmpty(mapName))
                {
                    var delimiter = !string.IsNullOrEmpty(autoExportData) ? "," : "";
                    if (!autoExportData.Contains(mapName))
                    {
                        iniData["UPS OnLine Connect"]["AutoExportRecent"] = mapName + delimiter + autoExportData;
                    }
                }
                else
                {
                    iniData["UPS OnLine Connect"]["AutoExportRecent"] = "{ Multiple Maps }";
                }
                
                

                //FreightAutoExportRecent
                if (!iniData.Sections.ContainsSection("UPS OnLine Connect") || !iniData["UPS OnLine Connect"].ContainsKey("FreightAutoExportRecent"))
                {
                    _logger.Info("WARNING:UPS Worldship: install auto maps to user menu: section UPS OnLine Connect or key FreightAutoExportRecent does not found in file " + menuIniPath);
                    if (!iniData.Sections.ContainsSection("UPS OnLine Connect"))
                    {
                        iniData.Sections.AddSection("UPS OnLine Connect");
                    }

                    if (!iniData["UPS OnLine Connect"].ContainsKey("FreightAutoExportRecent"))
                    {
                        iniData["UPS OnLine Connect"].AddKey("FreightAutoExportRecent");
                    }


                }
                
                var freightAutoExportData = iniData["UPS OnLine Connect"]["FreightAutoExportRecent"];
                if (!string.IsNullOrEmpty(mapName))
                {
                    var delimiter = !string.IsNullOrEmpty(freightAutoExportData) ? "," : "";
                    if (!freightAutoExportData.Contains(mapName))
                    {
                        iniData["UPS OnLine Connect"]["FreightAutoExportRecent"] = mapName + delimiter + freightAutoExportData;
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

        private string GetUpsDataDirectoryLocation()
        {
            var registryUpsDataLocation = GetUpsDirectoryofDataLocation();
            if (registryUpsDataLocation != null)
            {
                registryUpsDataLocation = Path.Combine(registryUpsDataLocation, " ").TrimEnd(); //Just add end directory separator
            }


            var calculatedUpsDataLocation = Path.Combine(_settingsProviderHelper.ProgramDataPath, UPS_DATA_DIRECTORY_LOCATION);
            if (string.IsNullOrEmpty(registryUpsDataLocation))
            {
                return calculatedUpsDataLocation;
            }

            return registryUpsDataLocation.Equals(calculatedUpsDataLocation, StringComparison.CurrentCultureIgnoreCase)
                ? calculatedUpsDataLocation
                : registryUpsDataLocation;
        }

        private string GetUpsDirectoryofDataLocation()
        {
            string directoryofDataLocation = null;
            try
            {
                using (var key = Registry.LocalMachine.OpenSubKey("Software\\Wow6432Node\\UPS\\Installation"))
                {
                    directoryofDataLocation = key?.GetValue("DataDirectory") as string;                    
                }
            }
            catch (Exception ex)  
            {
                _logger.Info("UPS Woprldship: can't get DirectoryofDataLocation from Windows registry. " + ex);
            }

            return directoryofDataLocation;
        }
    }
}