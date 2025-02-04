using System;
using System.Collections.Generic;
using System.Text;

namespace ConvMVVM2.Core.MVVM
{
    public abstract class AppBootstrapper
    {
        #region Private Property
        private readonly IServiceCollection container;
        private readonly ILayerManager layerManager;
        private readonly IViewModelMapper viewModelMapper;
        private List<IModule> modules;
        #endregion


        #region Constructor

        protected AppBootstrapper()
        {
            ConfigureModule();
        }
        #endregion


        #region Private Functions

        private void ConfigureModule()
        {
            modules = new List<IModule>();

        }
        #endregion

        #region Protected Functions
        protected abstract void RegisterViewModels(IViewModelMapper viewModelMapper);
        protected abstract void RegisterDependencies(IServiceCollection container);
        protected abstract void ViewMapping(IServiceCollection container, ILayerManager layerManager);
        protected abstract void OnStartUp();
        protected abstract void RegisterModules();
        protected void RegisterModule<T>(T module) where T : IModule
        {
            this.modules.Add(module);
        }
        #endregion

        #region Public Functions
        public void Run()
        {

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<ILayerManager, LayerManager>();
            serviceCollection.AddSingleton<ILocalizeService, LocalizeService>();
            serviceCollection.AddSingleton<IViewModelInitializer, DefaultViewModelInitializer>();

            ServiceLocator.SetServiceProvider(serviceCollection.CreateContainer());



            var provider = ServiceLocator.GetServiceProvider();
            var layerManager = provider.GetService<ILayerManager>();
            var viewModelMapper = provider.GetService<IViewModelMapper>();

            
            RegisterModules();

            RegisterViewModels(viewModelMapper);

            foreach(var module in modules)
                module.RegisterViewModels(viewModelMapper);

            RegisterDependencies(container);

            foreach(var module in modules)
                module.RegisterDependencies(container);

            ViewMapping(container, layerManager);

            foreach(var module in modules)
                module.ViewMapping(container, layerManager);

            OnStartUp();

            foreach(var module in modules)
                module.OnStartUp();
        }
        #endregion
    }
}
