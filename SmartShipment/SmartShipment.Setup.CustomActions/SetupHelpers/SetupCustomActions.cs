using SmartShipment.Settings.SettingsHelper;

namespace SmartShipment.Setup.CustomActions.SetupHelpers
{
    public class SetupCustomActions
    {
        private readonly ISetupLogger _logger;
        private readonly SettingsSetupHelper _settingsHelper;
        private readonly UpsSetupHelper _upsHelper;
        private readonly FedexSetupHelper _fedexHelper;

        public SetupCustomActions(ISetupLogger logger)
        {
            _logger = logger;
            var settingsProviderHelper = new SmartShipmentsSettingsHelper();
            _settingsHelper = new SettingsSetupHelper(settingsProviderHelper, _logger);
            _upsHelper = new UpsSetupHelper(settingsProviderHelper, _logger);
            _fedexHelper = new FedexSetupHelper(settingsProviderHelper, _logger);
        }

        public bool InstallSettings()
        {           
           return _settingsHelper.Install();
        }

        public void UninstallSettings()
        {
            _settingsHelper.Uninstall();
        }

        public bool InstallUpsSettings()
        {
            return _upsHelper.Install();
        }

        public void UninstallUpsSettings()
        {
            _upsHelper.Uninstall();
        }

        public bool InstallFedexSettings()
        {
            return _fedexHelper.Install();
        }

        public void UninstallFedexSettings()
        {
            _fedexHelper.Uninstall();
        }

        public void UninstallApplicationData()
        {
            _settingsHelper.UninstallAll();
        }
    }
}