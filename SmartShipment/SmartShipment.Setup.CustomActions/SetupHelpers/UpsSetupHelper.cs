using System;
using System.IO;
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
        private readonly string _upsShipmentAppConfigDirectoryLocation;
        private readonly string _upsShipmentImpExpFilePath;

        public UpsSetupHelper(SmartShipmentsSettingsHelper settingsProviderHelper, ISetupLogger logger)
        {            
            _settingsProviderHelper = settingsProviderHelper;
            _logger = logger;

            _upsShipmentAppConfigDirectoryLocation = GetUpsRegistryApplicationConfigDirectoryLocation();
            _upsShipmentImpExpFilePath = Path.Combine(GetUpsRegistryDataDirectoryLocation(), UPS_BASE_DATA_PATH, "UPSOUT.dat");
        }
        
        public bool Install()
        {            
            _logger.Info("UPS Woprldship: setup environment started");
            try
            {                
                _logger.Info("UPS Woprldship: starting copy export-import data file.");
                CopyMapFile();

                _logger.Info("UPS Woprldship: starting install automap settings.");
                InstallAutoMapsSettings(_upsShipmentImpExpFilePath);
                
                _logger.Info("UPS Woprldship: starting install user menu settings.");
                InstallShipUserSettings("UPSOUT");
                
                _logger.Info("UPS Woprldship: setup environment completed.");
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
            if (File.Exists(_upsShipmentImpExpFilePath))
            {
                File.Delete(_upsShipmentImpExpFilePath);
            }

            _logger.Info("UPS Woprldship: end uninstall");
        }

        private void CopyMapFile()
        {
            var source = Path.Combine(_settingsProviderHelper.ApplicationBasePath, "Devices\\UPS", "UPSOUT.dat");            

            if (File.Exists(source))
            {
                File.Copy(source, _upsShipmentImpExpFilePath, true);
                _logger.Info("UPS Worldship: copy files from: "+ source + " to: " + _upsShipmentImpExpFilePath);
            }
            else
            {
                _logger.Error("UPS Worldship: copy files error: file not found " + source);
                throw new Exception();
            }
        }

        private void InstallAutoMapsSettings(string mapFileName)
        {
            var mapIniPath = Path.Combine(_upsShipmentAppConfigDirectoryLocation, "wstdAutoExportMaps.ini");

            if (File.Exists(mapIniPath))
            {
                if (string.IsNullOrWhiteSpace(mapFileName))
                {
                    _logger.Info("UPS Worldship: uninstall active auto map");
                }
                else
                {
                    _logger.Info("UPS Worldship: install auto map: " + mapFileName);
                }
               
                var iniParser = new FileIniDataParser();
                var iniData = iniParser.ReadFile(mapIniPath);
                iniData["AutoExportShipments"]["ExportMapName"] = mapFileName;                
                iniData["AutoExportFreightShipments"]["ExportMapName"] = mapFileName;                
                iniParser.WriteFile(mapIniPath, iniData);
            }
            else
            {
                _logger.Error("UPS Worldship: install auto maps: error: file not found " + mapIniPath);
                throw new Exception();
            }
        }

        private  void InstallShipUserSettings(string mapFileName)
        {
            var menuIniPath = Path.Combine(_upsShipmentAppConfigDirectoryLocation, "wstdShipuser.ini");

            if (File.Exists(menuIniPath))
            {
                if (string.IsNullOrWhiteSpace(mapFileName))
                {
                    _logger.Info("UPS Worldship: uninstall active auto map from user menu");
                }
                else
                {
                    _logger.Info("UPS Worldship: install auto maps to user menu: " + mapFileName);      
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
                if (!string.IsNullOrEmpty(mapFileName))
                {
                    var delimiter = !string.IsNullOrEmpty(autoExportData) ? "," : "";
                    if (!autoExportData.Contains(mapFileName))
                    {
                        iniData["UPS OnLine Connect"]["AutoExportRecent"] = mapFileName + delimiter + autoExportData;
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
                if (!string.IsNullOrEmpty(mapFileName))
                {
                    var delimiter = !string.IsNullOrEmpty(freightAutoExportData) ? "," : "";
                    if (!freightAutoExportData.Contains(mapFileName))
                    {
                        iniData["UPS OnLine Connect"]["FreightAutoExportRecent"] = mapFileName + delimiter + freightAutoExportData;
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

        private string GetUpsRegistryDataDirectoryLocation()
        {
            string directoryofDataLocation = null;
            try
            {
                using (var key = Registry.LocalMachine.OpenSubKey("Software\\Wow6432Node\\UPS\\Installation"))
                {                    
                    var networkShareDir = key?.GetValue("NetworkShareDir") as string;                    
                    var localDataDir = key?.GetValue("DataDirectory") as string;                    
                    directoryofDataLocation = !string.IsNullOrEmpty(networkShareDir) ? networkShareDir : localDataDir;                    
                }
            }
            catch (Exception ex)  
            {
                _logger.Info("UPS Woprldship: can't get UPS Data Directory Location from Windows registry. " + ex);
            }

            return Path.Combine(directoryofDataLocation);
        }

        private string GetUpsRegistryApplicationConfigDirectoryLocation()
        {
            string directoryLocation = null;
            try
            {
                using (var key = Registry.LocalMachine.OpenSubKey("Software\\Wow6432Node\\UPS\\Installation"))
                {                                        
                    directoryLocation = key?.GetValue("DataDirectory") as string;                    
                }
            }
            catch (Exception ex)  
            {
                _logger.Info("UPS Woprldship: can't get UPS Application Configuration Directory from Windows registry. " + ex);
            }

            return Path.Combine(directoryLocation);
        }

    }
}