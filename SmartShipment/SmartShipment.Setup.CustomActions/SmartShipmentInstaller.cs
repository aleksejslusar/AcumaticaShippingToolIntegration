using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.Windows.Forms;
using SmartShipment.Setup.CustomActions.SetupHelpers;

namespace SmartShipment.Setup.CustomActions
{
    [RunInstaller(true)]
    public partial class SmartShipmentInstaller : Installer
    {
        private readonly SetupCustomActions _customActions;
        private readonly ISetupLogger _logger;

        private bool _upsInstallChecked;
        private bool _fedexInstallChecked;
        private const string KEY_UPS = "keyups";
        private const string KEY_FEDEX = "keyfedex";


        public SmartShipmentInstaller()
        {
            #if DEBUG
            //System.Diagnostics.Debugger.Launch();
            #endif

            InitializeComponent();
            _logger = new SetupLogger();
            _customActions = new SetupCustomActions(_logger);
            
        }   

        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Install(IDictionary stateSaver)
        {
            _upsInstallChecked = Context.Parameters.ContainsKey(KEY_UPS) && Context.Parameters[KEY_UPS] == "1";
            _fedexInstallChecked = Context.Parameters.ContainsKey(KEY_FEDEX) && Context.Parameters[KEY_FEDEX] == "1";

            if (!_upsInstallChecked && !_fedexInstallChecked)
            {
                throw new InstallException("No features selected for installation");
            }

            base.Install(stateSaver);
            _logger.Info("Begin installation");

            if (!_customActions.InstallSettings())
            {
                throw new InstallException("Error on install application settngs");
            }
            if (_upsInstallChecked)
            {
                if (!_customActions.InstallUpsSettings())
                {
                    throw new InstallException("Error on install UPS WorldShip integration.");
                }
            }

            if (_fedexInstallChecked)
            {
                if (!_customActions.InstallFedexSettings())
                {
                    throw new InstallException("Error on install FedEx Ship manager integration.");
                }
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
