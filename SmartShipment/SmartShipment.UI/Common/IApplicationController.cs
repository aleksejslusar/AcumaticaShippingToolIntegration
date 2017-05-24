using SmartShipment.Adapters.Common;
using SmartShipment.Information;
using SmartShipment.Settings;
using SmartShipment.UI.SingleInstance;

namespace SmartShipment.UI.Common
{
    public interface IApplicationController
    {
        IApplicationController RegisterService<TService>();
        
        IApplicationController RegisterView<TView, TImplementation>() where TImplementation : class, TView 
                                                                      where TView : IView;
        IApplicationController RegisterInstance<TArgument>(TArgument instance);

        IApplicationController RegisterService<TService, TImplementation>() where TImplementation : class, TService;        

        IApplicationController RegisterService<TService, TImplementation>(string serviceName) where TImplementation : class, TService;

        void Run<TPresenter>() where TPresenter : class, IPresenter;

        void Run<TPresenter, TArgumnent>(TArgumnent argumnent) where TPresenter : class, IPresenter<TArgumnent>;
        
        void StartApplicaton<TApplicatonManager>() where TApplicatonManager : ISingleInstanceManager;

        void StartShipmentApplication<TAdapter, THelper>(string serviceName, string shipmentNbr = null) where TAdapter : IShipmentApplicationAdapter
                                                                             where THelper : IShipmentApplicationHelper;

        ISettings ApplicatoinSettings { get; }

        ISmartShipmentMessagesProvider ApplicationMessagesProvider { get; }
        IApplicationController RegisterServicePerContainer<TModel, TImplementation>() where TImplementation : class, TModel;
        IContainer GetContainer();
    }
}