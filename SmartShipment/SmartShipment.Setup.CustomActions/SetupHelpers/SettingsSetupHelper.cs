using System;
using System.IO;
using System.Linq;
using SmartShipment.Settings.SettingsHelper;

namespace SmartShipment.Setup.CustomActions.SetupHelpers
{
    public class SettingsSetupHelper
    {
        private readonly ISmartShipmentsSettingsHelper _settingsProviderHelper;
        private readonly ISetupLogger _logger;
        private const string DEFAULT_ACUMATICASOAPDATAENDPOINT = "/entity/Default/6.00.001";
        private const string DEFAULT_ACUMATICASOAPLOGINENDPOINT = "/entity/auth/login";
        private const string DEFAULT_UPSPROCESSNAME = "WorldShipTD";
        private const string DEFAULT_UPSEXPORTFILEPATH = "Devices\\UPS\\UPSOUT.txt";
        private const string DEFAULT_FEDEXPROCESSNAME = "FedEx.Gsm.Cafe.ApplicationEngine.Gui";
        private const string DEFAULT_FEDEXEXPORTFILEPATH = "Devices\\FEDEX\\FEDEXOUT.txt";

        public SettingsSetupHelper(ISmartShipmentsSettingsHelper settingsProviderHelper, ISetupLogger logger)
        {
            _settingsProviderHelper = settingsProviderHelper;
            _logger = logger;
        }

        #region Deployment custom actions

        public bool Install()
        {
            _logger.Info("SettingsSetupHelper: setup environment begin");
            try
            {
                if (!Directory.Exists(_settingsProviderHelper.ApplicationSettingsDirectoryPath))
                {
                    Directory.CreateDirectory(_settingsProviderHelper.ApplicationSettingsDirectoryPath);
                    _logger.Info("SettingsSetupHelper:  create settings file directory" );
                    if (!File.Exists(_settingsProviderHelper.ApplicationSettingsFilePath))
                    {
                        File.WriteAllLines(_settingsProviderHelper.ApplicationSettingsFilePath, new[] { string.Empty });
                        _logger.Info("SettingsSetupHelper:  create settings file");
                    }
                }

                _settingsProviderHelper.InitializeIniFile();

                InitDefaultSections();
                InitDefaultValues();

                _settingsProviderHelper.SaveIniFile();
                _logger.Info("SettingsSetupHelper: setup environment complete");
                return true;
            }
            catch(Exception e)
            {
                _logger.Error("SettingsSetupHelper: setup environment error " +  e);
                return false;
            }

            
        }

        public void Uninstall()
        {
            var directory = _settingsProviderHelper.ApplicationSettingsDirectoryPath;
            if (Directory.Exists(directory))
            {
                _logger.Info("SettingsSetupHelper:  delete settings file directory");
                Directory.Delete(directory, true);                
            }
        }

        public void UninstallAll()
        {            
            var directory = _settingsProviderHelper.ApplicationBasePath;            
            if (Directory.Exists(directory))
            {
                _logger.Info("SettingsSetupHelper:  delete all parameters directories");
                Directory.Delete(directory, true);                
            }
            
            var parentDirectory = _settingsProviderHelper.ApplicationParentPath;
            if (Directory.Exists(parentDirectory) && !Directory.EnumerateFileSystemEntries(parentDirectory).Any())
            {                               
                Directory.Delete(parentDirectory, true);
            }

        }

        private void InitDefaultSections()
        {
            _settingsProviderHelper.InitDefaultSection(SmartShipmentsSettingsHelper.SECTION_GENERAL, @"Smart Shipment settings file. Created on " + DateTime.Now);
            _settingsProviderHelper.InitDefaultSection(SmartShipmentsSettingsHelper.SECTION_UPS, @"Application parameters for integration with UPS WorldShip");
            _settingsProviderHelper.InitDefaultSection(SmartShipmentsSettingsHelper.SECTION_FEDEX, @"Application parameters for integration with FedEx Ship Manager");
            _settingsProviderHelper.InitDefaultSection(SmartShipmentsSettingsHelper.SECTION_ACUMATICA, @"Application parameters for integration with Acumatica ERP");
        }

        private void InitDefaultValues()
        {
            _settingsProviderHelper.InitDefaultValue("AcumaticaSoapDataEndPoint", DEFAULT_ACUMATICASOAPDATAENDPOINT);
            _settingsProviderHelper.InitDefaultValue("AcumaticaSoapLoginEndPoint", DEFAULT_ACUMATICASOAPLOGINENDPOINT);
            _settingsProviderHelper.InitDefaultValue("UpsProcessName", DEFAULT_UPSPROCESSNAME);
            _settingsProviderHelper.InitDefaultValue("UpsExportFilePath", Path.Combine(_settingsProviderHelper.ApplicationBasePath, DEFAULT_UPSEXPORTFILEPATH));
            _settingsProviderHelper.InitDefaultValue("FedexProcessName", DEFAULT_FEDEXPROCESSNAME);
            _settingsProviderHelper.InitDefaultValue("FedexExportFilePath", Path.Combine(_settingsProviderHelper.ApplicationBasePath, DEFAULT_FEDEXEXPORTFILEPATH));
        }

        #endregion

    }
}