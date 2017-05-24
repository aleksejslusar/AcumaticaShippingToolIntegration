using System;
using System.Linq.Expressions;
using LightInject;
using SmartShipment.UI.Common;

namespace SmartShipment.UI.Container
{
    public class LightInjectAdapder : IContainer
    {
        private readonly ServiceContainer _container = new ServiceContainer();

        public void Register<TService, TImplementation>() where TImplementation : TService
        {
            _container.Register<TService, TImplementation>();
        }

        public void RegisterPerContainer<TService, TImplementation>() where TImplementation : TService
        {
            _container.Register<TService, TImplementation>(new PerContainerLifetime());
        }

        public void Register<TService, TImplementation>(string name) where TImplementation : TService
        {
            _container.Register<TService, TImplementation>(name);
        }       

        public void Register<TService>()
        {
            _container.Register<TService>();
        }        

        public void RegisterInstance<T>(T instance)
        {
            _container.RegisterInstance(instance);
        }

        public void Register<TService, TArgument>(Expression<Func<TArgument, TService>> factory)
        {
            _container.Register(serviceFactory => factory);
        }

        public TService Resolve<TService>()
        {
            return _container.GetInstance<TService>();
        }

        public TService Resolve<TService>(string serviceName)
        {
            return _container.GetInstance<TService>(serviceName);
        }

        public bool IsRegistered<TService>()
        {
            return _container.CanGetInstance(typeof (TService), string.Empty);
        }
    }
}