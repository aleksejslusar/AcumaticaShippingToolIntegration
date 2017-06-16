using System;
using System.Linq;
using Microsoft.VisualBasic.ApplicationServices;
using System.Windows.Forms;
using SmartShipment.Adapters.Common;
using SmartShipment.Information;
using SmartShipment.Information.Properties;
using SmartShipment.Settings;
using SmartShipment.UI.Common;
using SmartShipment.UI.Presenters;

namespace SmartShipment.UI.SingleInstance
{
    public interface ISingleInstanceManager
    {
        void Start(IApplicationController applicationController, SingleInstanceApplication app);
    }

    public class SingleInstanceManager : ISingleInstanceManager
    {
        private readonly ISettings _settings;
        private readonly ISmartShipmentMessagesProvider _messagesProvider;
        private static SingleInstanceApplication _app;
        private static IApplicationController _applicationController;

        public SingleInstanceManager(ISettings settings, ISmartShipmentMessagesProvider messagesProvider)
        {
            _settings = settings;
            _messagesProvider = messagesProvider;
        }

        public void Start(IApplicationController applicationController, SingleInstanceApplication app)
        {            
            _applicationController = applicationController;

            if (_app == null && _applicationController != null)
            {
                _app = app;
                _app.StartupNextInstance += _app_StartNewInstance;
                _app.Startup += _app_Startup;
                _app.Shutdown += _app_Shutdown;

                if (_settings.IsSettingsRequireSetValues())
                {
                   _messagesProvider.Info(InformationResources.MESSAGE_PARAMETERS_NEED_BE_INSTALLED);
                    InitSettingsForm();
                }

                if (_settings.IsSettingsRequireSetValues())
                {
                    _messagesProvider.Warn(InformationResources.MESSAGE_PARAMETERS_NOT_INSTALLED);
                    return;
                }
                
                InitMainForm();
                _app.Run(Environment.GetCommandLineArgs());
                
            }
        }

        private void _app_Shutdown(object sender, EventArgs e)
        {
            _messagesProvider.Log(InformationResources.INFO_APPLICATION_STOPED);
        }

        private void _app_Startup(object sender, StartupEventArgs e)
        {
            _messagesProvider.Log(InformationResources.INFO_APPLICATION_STARTED);
        }

        private void InitSettingsForm()
        {
            _applicationController.Run<SmartShipmentSettingDialogPresenter>();
        }

        private void InitMainForm()
        {
            _applicationController.Run<FloatMenuPresenter>();
        }

        private void _app_StartNewInstance(object sender, StartupNextInstanceEventArgs e)
        {
            var app = sender as SingleInstanceApplication;
            
            var forms = Application.OpenForms;
            if (app != null)
            {
                var mainForm = app.ApplicationContext.MainForm;
                if (mainForm != null && forms.Cast<Form>().Contains(mainForm))
                {
                    //Show form
                    mainForm.Activate();
                    ProcessShipmentBackground(e.CommandLine.LastOrDefault());
                }
                else
                {
                    //Start form
                    InitMainForm();
                }
            }
        }

        private void ProcessShipmentBackground(string args)
        {
            //Custom protocol parameters:
            //acumaticashippingtoolintegration:000021@UPS;

            if (string.IsNullOrEmpty(args) || !args.Contains("acumaticashippingtoolintegration")) return;

            var argsArray = args.Split(':', '@');
            var shipmentNbrParam = argsArray.Length >= 2 ? argsArray[1] : null;
            var applicationTypeParam = argsArray.Length >= 3 ? argsArray[2] : null;
            if (!string.IsNullOrEmpty(applicationTypeParam) && !string.IsNullOrEmpty(shipmentNbrParam))
            {
                ApplicationTypes applicationType;

                switch (applicationTypeParam)
                {
                    case "FED":
                        applicationType = ApplicationTypes.FedExShipmentManager;
                        break;
                    case "UPS":
                        applicationType = ApplicationTypes.UpsWorldShip;
                        break;
                    default:
                        throw new ArgumentException("Cannot locate Application type from command arguments. [ " + args + " ]");
                }
                
                var shipmentNbr = shipmentNbrParam.Trim();
                var presenter = _applicationController.GetContainer().Resolve<FloatMenuPresenter>();
                presenter.ProcessShipment(applicationType, shipmentNbr);
                return;
            }

            throw new ArgumentException("Custom protocol parameters should looks like protoName:arg1:arg2. Cannot parse parameter string. [ " + args + " ]");
        }
    }
}
