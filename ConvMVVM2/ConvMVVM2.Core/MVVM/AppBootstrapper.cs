using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConvMVVM2.Core.MVVM
{
    public abstract class AppBootstrapper
    {
        #region Private Property
        private List<IModule> modules;
        private ServiceCollection serviceCollection = new ServiceCollection();
        #endregion


        #region Constructor

        protected AppBootstrapper()
        {
            ConfigureModule();
        }
        #endregion

        #region Public Property
        public IServiceCollection ServiceCollection
        {
            get => this.serviceCollection;
        }
        #endregion


        #region Private Functions

        private void ConfigureModule()
        {
            modules = new List<IModule>();

        }
        #endregion

        #region Protected Functions
        protected abstract void ViewModelMapping(IViewModelMapper viewModelMapper);
        protected abstract void RegisterServices(IServiceCollection serviceCollection);
        protected abstract void RegionMapping(IRegionManager layerManager);
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

            serviceCollection.AddSingleton<IRegionManager, RegionManager>();
            serviceCollection.AddSingleton<ILocalizeService, LocalizeService>();
            serviceCollection.AddSingleton<IViewModelMapper, ViewModelMapper>();
            serviceCollection.AddSingleton<IEventAggregator, EventAggregator>();

            var container = serviceCollection.CreateContainer();
            ServiceLocator.SetServiceProvider(container);
            var regionManager = container.GetService<IRegionManager>();
            var viewModelMapper = container.GetService<IViewModelMapper>();

            
            RegisterModules();

            ViewModelMapping(viewModelMapper);

            foreach(var module in modules)
                module.ViewModelMapping(viewModelMapper);

            RegisterServices(serviceCollection);

            foreach(var module in modules)
                module.RegisterServices(serviceCollection);

            RegionMapping(regionManager);

            foreach(var module in modules)
                module.RegionMapping(regionManager);

            OnStartUp();

            foreach(var module in modules)
                module.OnStartUp();
        }
        #endregion


    }
}
