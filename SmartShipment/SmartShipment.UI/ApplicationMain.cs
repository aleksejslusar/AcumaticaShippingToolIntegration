using System;
using System.Windows.Forms;
using SmartShipment.Adapters;
using SmartShipment.Adapters.Cache;
using SmartShipment.Adapters.Common;
using SmartShipment.Adapters.Helpers;
using SmartShipment.AutomationUI.Browser;
using SmartShipment.Information;
using SmartShipment.Information.Logger;
using SmartShipment.Information.Properties;
using SmartShipment.Network;
using SmartShipment.Network.Common;
using SmartShipment.UI.Common;
using SmartShipment.UI.Container;
using SmartShipment.UI.FileWatcher;
using SmartShipment.UI.FileWatcher.Common;
using SmartShipment.UI.Forms;
using SmartShipment.UI.SingleInstance;
using SmartShipment.UI.Views;
using SmartShipment.Network.Export;
using SmartShipment.Settings;
using SmartShipment.Settings.SettingsHelper;

namespace SmartShipment.UI
{
    static class ApplicationMain
    {
        private static IApplicationController _controller;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            InitContainer();            
            try
            {
                _controller.ApplicationMessagesProvider.Log(InformationResources.INFO_APPLICATION_STARTING);
                _controller.StartApplicaton<ISingleInstanceManager>();
                
            }
            catch (Exception e)
            {
                _controller.ApplicationMessagesProvider.Fatal(e);
                _controller.ApplicationMessagesProvider.Log(InformationResources.ERROR_APPLICATION_STOPPED_BY_ERROR);
            }
                     
        }

        private static void InitContainer()
        {
            var application = new SingleInstanceApplication();
            _controller = new ApplicationController(new LightInjectAdapder())
                .RegisterInstance(application)
                .RegisterInstance(application.ApplicationContext)
                .RegisterService<ISmartShipmentsSettingsHelper, SmartShipmentsSettingsHelper>()
                .RegisterService<ISmartShipmentMessagesProvider, SmartShipmentMessagesProvider>()
                .RegisterService<ISingleInstanceManager, SingleInstanceManager>()
                .RegisterServicePerContainer<ISettings, SmartShipmentSettings>()
                .RegisterView<IFloatMenuView, FloatMenuWindow>()
                .RegisterView<ISmartSettingsDialogView, SmartShipmentSettingsDialog>();

            //Logger
            _controller.RegisterServicePerContainer<ILogger, SmartShipmentLogger>();

            //UI Automation
            _controller.RegisterService<IUIAutomationBrowserHelper, UIAutomationBrowserHelper>();

            //Network adapter
            _controller.RegisterService<IWebServiceHelper, WebServiceSoapHelper>();

            _controller.RegisterService<IAcumaticaUriParser, AcumaticaUriParser>()
                       .RegisterService<IBrowserHelper, BrowserHelper>()
                       .RegisterService<IAcumaticaNetworkProvider, AcumaticaNetworkProvider>();

            //Cache
            _controller.RegisterServicePerContainer<IShipmentAutomationCache, ShipmentAutomationCache>();

            //Export & Import contexts
            _controller.RegisterService<ISmartShipmentExportContext, SmartShipmentExportContext>();

            //File Watcher
            _controller.RegisterService<ISmartShipmentFilesMonitor, SmartShipmentFilesMonitor>();
            _controller.RegisterService<ISmartShipmentFileProvider, SmartShipmentFileProvider>(ApplicationTypes.UpsWorldShip.ToString());
            _controller.RegisterService<ISmartShipmentFileProvider, SmartShipmentFedexFileProvider>(ApplicationTypes.FedExShipmentManager.ToString());
            _controller.RegisterService<ISmartShipmentExportParametersParser, SmartShipmentUpsExportParametersParser>(ApplicationTypes.UpsWorldShip.ToString());
            _controller.RegisterService<ISmartShipmentExportParametersParser, SmartShipmentFedexExportParametersParser>(ApplicationTypes.FedExShipmentManager.ToString());
            _controller.RegisterService<ISmartShipmentFileWatcher, SmartShipmentFileWatcher>();

            //Applications adapters
            _controller.RegisterService<IShipmentApplicationHelper, UpsShipmentApplicationHelper>(ApplicationTypes.UpsWorldShip.ToString())
                       .RegisterService<IShipmentApplicationHelper, FedexShipmentApplicationHelper>(ApplicationTypes.FedExShipmentManager.ToString())
                       .RegisterService<IShipmentApplicationAdapter, ShipmentApplicationAdapter>();
        }
    }
}
