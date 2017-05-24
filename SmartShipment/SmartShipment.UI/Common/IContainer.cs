using System;
using System.Linq.Expressions;

namespace SmartShipment.UI.Common
{
    public interface IContainer
    {
        void Register<TService, TImplementation>() where TImplementation : TService;
        void RegisterPerContainer<TService, TImplementation>() where TImplementation : TService;
        void Register<TService, TImplementation>(string name) where TImplementation : TService;        
        void Register<TService>();       
        void RegisterInstance<T>(T instance);
        TService Resolve<TService>();
        TService Resolve<TService>(string serviceName);
        bool IsRegistered<TService>();
        void Register<TService, TArgument>(Expression<Func<TArgument, TService>> factory);
    }
}