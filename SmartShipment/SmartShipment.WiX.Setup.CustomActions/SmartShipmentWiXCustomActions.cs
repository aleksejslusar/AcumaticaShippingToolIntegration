using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Deployment.WindowsInstaller;
using SmartShipment.Setup.CustomActions.SetupHelpers;

namespace SmartShipment.WiX.Setup.CustomActions
{
    public class SmartShipmentWiXCustomActions
    {
        private static readonly SetupCustomActions _customActionsHelper;
        private static readonly ISetupLogger _logger;
        private const string LOG_PREFIX = "[SmartShipmentWiXCustomActions] Execute action: ";
        private const string LOG_POSTFIX = "ERROR: ";

        static SmartShipmentWiXCustomActions()
        {
            //If debug needed- uncomment bellow line
            //System.Diagnostics.Debugger.Launch();

            _logger = new SetupLogger();
            _customActionsHelper = new SetupCustomActions(_logger);
        }

        [CustomAction]  
        public static ActionResult SetupBaseSettings(Session session)
        {
            _logger.Info(LOG_PREFIX + "SetupBaseSettings");
            return _customActionsHelper.InstallSettings() ? ActionResult.Success : ActionResult.Failure;
        }

        [CustomAction]
        public static ActionResult RemoveBaseSettings(Session session)
        {
            _logger.Info(LOG_PREFIX + "RemoveBaseSettings");
            try
            {
                _customActionsHelper.UninstallSettings();
                _customActionsHelper.UninstallFedexSettings();
                _customActionsHelper.UninstallUpsSettings();
                _customActionsHelper.UninstallApplicationData();
            }
            catch (Exception e)
            {
                _logger.Info(LOG_PREFIX + "RemoveBaseSettings" + LOG_POSTFIX + e);                
            }

            return ActionResult.Success;
        }

        [CustomAction]
        public static ActionResult SetupUpsSettings(Session session)
        {
            _logger.Info(LOG_PREFIX + "SetupUpsSettings");
            return _customActionsHelper.InstallUpsSettings() ? ActionResult.Success : ActionResult.Failure;
        }

        [CustomAction]
        public static ActionResult RemoveUpsSettings(Session session)
        {
            _logger.Info(LOG_PREFIX + "RemoveUpsSettings");
            try
            {
                _customActionsHelper.UninstallUpsSettings();                
            }
            catch (Exception e)
            {
                _logger.Info(LOG_PREFIX + "RemoveUpsSettings" + LOG_POSTFIX + e);
            }

            return ActionResult.Success;
        }

        [CustomAction]
        public static ActionResult SetupFedExSettings(Session session)
        {
            _logger.Info(LOG_PREFIX + "SetupFedExSettings");
            return _customActionsHelper.InstallFedexSettings() ? ActionResult.Success : ActionResult.Failure;
        }

        [CustomAction]
        public static ActionResult RemoveFedExSettings(Session session)
        {
            _logger.Info(LOG_PREFIX + "RemoveFedExSettings");
            try
            {
                _customActionsHelper.UninstallFedexSettings();
            }
            catch (Exception e)
            {
                _logger.Info(LOG_PREFIX + "RemoveFedExSettings" + LOG_POSTFIX + e);
            }
            return ActionResult.Success;
        }
    }
}
