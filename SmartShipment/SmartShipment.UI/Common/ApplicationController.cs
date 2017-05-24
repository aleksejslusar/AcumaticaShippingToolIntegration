using System;
using SmartShipment.Adapters.Common;
using SmartShipment.Information;
using SmartShipment.Settings;
using SmartShipment.UI.FileWatcher.Common;
using SmartShipment.UI.SingleInstance;
namespace SmartShipment.UI.Common
{
    public class ApplicationController : IApplicationController
    {
        private readonly IContainer _container;

        public ApplicationController(IContainer container)
        {
            _container = container;
            _container.RegisterInstance<IApplicationController>(this);
        }

        public IApplicationController RegisterService<TService>()
        {
            _container.Register<TService>();
            return this;
        }    

        public IApplicationController RegisterView<TView, TImplementation>()
            where TImplementation : class, TView 
            where TView : IView
        {
            _container.Register<TView, TImplementation>();
            return this;
        }

        public IApplicationController RegisterInstance<TInstance>(TInstance instance)
        {
            _container.RegisterInstance(instance);
            return this;
        }

        public IApplicationController RegisterService<TModel, TImplementation>()
            where TImplementation : class, TModel
        {
            _container.Register<TModel, TImplementation>();
            return this;
        }

        public IApplicationController RegisterServicePerContainer<TModel, TImplementation>() where TImplementation : class, TModel
        {
            _container.RegisterPerContainer<TModel, TImplementation>();
            return this;
        }

        public IApplicationController RegisterService<TService, TImplementation>(string serviceName) where TImplementation : class, TService
        {
            _container.Register<TService, TImplementation>(serviceName);
            return this;
        }

        public void Run<TPresenter>() where TPresenter : class, IPresenter
        {
            if (!_container.IsRegistered<TPresenter>())
                _container.Register<TPresenter>();
            
            var presenter = _container.Resolve<TPresenter>();
            presenter.Run();
        }

        public void Run<TPresenter, TArgumnent>(TArgumnent argumnent) where TPresenter : class, IPresenter<TArgumnent>
        {
            if (!_container.IsRegistered<TPresenter>())
                _container.Register<TPresenter>();

            var presenter = _container.Resolve<TPresenter>();
            presenter.Run(argumnent);
        }

        public void StartApplicaton<TSingleInstanceManager>() where TSingleInstanceManager : ISingleInstanceManager
        {
            if (!_container.IsRegistered<TSingleInstanceManager>())
            {
                _container.Register<TSingleInstanceManager>();
            }

            var singleApplicationManager = _container.Resolve<ISingleInstanceManager>();
            var application = _container.Resolve<SingleInstanceApplication>();
            var fileWatcher = _container.Resolve<ISmartShipmentFilesMonitor>();
            fileWatcher.Start();
            singleApplicationManager.Start(this, application);
        }

        public void StartShipmentApplication<TAdapter, THelper>(string serviceName, string shipmentNbr = null) where  TAdapter : IShipmentApplicationAdapter
                                                                                    where  THelper: IShipmentApplicationHelper
        {
            var applicationHelper = _container.Resolve<THelper>(serviceName);
            var service = _container.Resolve<TAdapter>();
            if (service != null && applicationHelper != null)
            {               
                service.RunShipmentApplication(applicationHelper, shipmentNbr);                
            }
            else
            {
                throw new Exception("Adapter of type " + typeof(TAdapter) + "with name " + serviceName + "is not implemented.");
            }
        }
        
        public ISettings ApplicatoinSettings => _container.Resolve<ISettings>();

        public ISmartShipmentMessagesProvider ApplicationMessagesProvider => _container.Resolve<ISmartShipmentMessagesProvider>();

        public IContainer GetContainer()
        {
            return _container;
        }
    }
}