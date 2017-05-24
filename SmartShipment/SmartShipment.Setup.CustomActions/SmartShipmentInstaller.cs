using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using SmartShipment.Setup.CustomActions.SetupHelpers;

namespace SmartShipment.Setup.CustomActions
{
    [RunInstaller(true)]
    public partial class SmartShipmentInstaller : Installer
    {
        private readonly SetupCustomActions _customActions;
        private readonly ISetupLogger _logger;

        public SmartShipmentInstaller()
        {
            InitializeComponent();
            _logger = new SetupLogger();
            _customActions = new SetupCustomActions(_logger);
        }

        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);
            _logger.Info("Begin installation");
            if (!_customActions.InstallSettings())
            {
                throw new InstallException("Error on install application settngs");
            }

            if (!_customActions.InstallUpsSettings())
            {
                throw new InstallException("Error on install UPS WorldShip integration.");
            }

            if (!_customActions.InstallFedexSettings())
            {
                throw new InstallException("Error on install FedEx Ship manager integration.");
            }

            _logger.Info("Complete installation");

        }

        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Commit(IDictionary savedState)
        {
            base.Commit(savedState);            
        }

        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Rollback(IDictionary savedState)
        {
            base.Rollback(savedState);
        }

        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Uninstall(IDictionary savedState)
        {
            base.Uninstall(savedState);
            _customActions.UninstallSettings();
            _customActions.UninstallFedexSettings();
            _customActions.UninstallUpsSettings();
            _customActions.UninstallApplicationData();
        }
    }
}
