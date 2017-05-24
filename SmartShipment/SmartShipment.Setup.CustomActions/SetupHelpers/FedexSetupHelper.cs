using System;
using System.IO;
using System.Xml;
using SmartShipment.Settings.SettingsHelper;

namespace SmartShipment.Setup.CustomActions.SetupHelpers
{
    public class FedexSetupHelper
    {
        private readonly ISmartShipmentsSettingsHelper _settingsProviderHelper;
        private readonly ISetupLogger _logger;

        public FedexSetupHelper(ISmartShipmentsSettingsHelper settingsProviderHelper, ISetupLogger logger)
        {
            _settingsProviderHelper = settingsProviderHelper;
            _logger = logger;
        }

        private const string FEDEX_CONFIG_FILE = "C:\\ProgramData\\FedEx\\Integration\\Configurations\\FXIAnetConfig.xml";
        private const string FEDEX_PROFILE_FILE_NAME = "FEDEXOUT.xml";
        private const string FEDEX_EXPORT_FILE_NAME = "FEDEXOUT.txt";
        private const string FEDEX_APPLICATION_PROFILE_PATH = "Devices\\FEDEX";
        private const string FEDEX_PROFILES_DIRECTORY = "C:\\ProgramData\\FedEx\\Integration\\Profiles";
        private const string FEDEX_ACTIVE_PROFILE_NAME = "FEDEXOUT";

        public bool Install()
        {
            _logger.Info("FedEx Ship Manager: setup environment started");
            try
            {
                CopyFedexProfileFile();
                PatchProfileFile();
                SetFedexActiveProfile(FEDEX_ACTIVE_PROFILE_NAME);
                _logger.Info("FedEx Ship Manager: setup environment completed");
                return true;
            }
            catch (Exception e)
            {
                _logger.Error("FedEx Ship Manager: setup environment error " + e);
                return false;
            }
        }

        private void CopyFedexProfileFile()
        {
            var source = Path.Combine(_settingsProviderHelper.ApplicationBasePath, FEDEX_APPLICATION_PROFILE_PATH, FEDEX_PROFILE_FILE_NAME);
            var target = Path.Combine(FEDEX_PROFILES_DIRECTORY, FEDEX_PROFILE_FILE_NAME);

            if (File.Exists(source))
            {
                File.Copy(source, target, true);
                _logger.Info("FedEx Ship Manager: copy files from: " + source + " to: " + target);
            }
            else
            {
                _logger.Error("UPS Worldship: copy files error: file not found " + source);
                throw new Exception();
            }
        }

        private void PatchProfileFile()
        {
            var target = Path.Combine(FEDEX_PROFILES_DIRECTORY, FEDEX_PROFILE_FILE_NAME);
            var valueToPatch = Path.Combine(_settingsProviderHelper.ApplicationBasePath, FEDEX_APPLICATION_PROFILE_PATH, FEDEX_EXPORT_FILE_NAME);
            var profile = new XmlDocument();
            profile.Load(target);
            var datasourceName = profile.SelectSingleNode("profile/datasource/name");
            var datasourceInformatoin = profile.SelectSingleNode("profile/datasource/datasourceinformation");

            if (datasourceName != null)
            {
                datasourceName.InnerText = valueToPatch;
            }

            if (datasourceInformatoin != null)
            {
                datasourceInformatoin.InnerText = valueToPatch;
            }

            profile.Save(target);
            _logger.Info("FedEx Ship Manager: patch profile file, value " + valueToPatch);
        }

        private void DeleteFedexProfileFile()
        {            
            var target = Path.Combine(FEDEX_PROFILES_DIRECTORY, FEDEX_PROFILE_FILE_NAME);

            if (File.Exists(target))
            {
                File.Delete(target);
                _logger.Info("FedEx Ship Manager: delete files from: " + target);
            }
            else
            {
                _logger.Error("FedEx Ship Manager: delete files error. File not found " + target);
                throw new Exception();
            }
        }

        private void SetFedexActiveProfile(string activeProfile)
        {
            var settings = new XmlDocument();
            settings.Load(FEDEX_CONFIG_FILE);
            var appSettingsNode = settings.SelectSingleNode("configuration/appSettings");

            if (appSettingsNode != null)
            {
                foreach (XmlElement appSettingNode in appSettingsNode.ChildNodes)
                {
                    var key = appSettingNode.Attributes["key"].Value;
                    if (key == "ActiveProfile")
                    {
                        appSettingNode.Attributes["value"].Value = activeProfile;
                    }
                }
            }

            settings.Save(FEDEX_CONFIG_FILE);
            _logger.Info("FedEx Ship Manager: set active profile " + activeProfile);
        }



        public void Uninstall()
        {
            _logger.Info("FedEx Ship Manager: Uninstall");        
            DeleteFedexProfileFile();
            SetFedexActiveProfile("");
        }
    }
}