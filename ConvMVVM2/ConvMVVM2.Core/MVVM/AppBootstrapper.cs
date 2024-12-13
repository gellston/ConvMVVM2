using System;
using System.Collections.Generic;
using System.Text;

namespace ConvMVVM2.Core.MVVM
{
    public abstract class AppBootstrapper
    {
        #region Private Property
        private readonly IContainer container;
        private readonly ILayerManager layerManager;
        private readonly IViewModelMapper viewModelMapper;
        private List<IModule> modules;
        #endregion


        #region Constructor

        protected AppBootstrapper()
        {
            container = ContainerProvider.GetContainer();
            layerManager = container.Resolve<ILayerManager>();
            viewModelMapper = container.Resolve<IViewModelMapper>();


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
        protected abstract void RegisterDependencies(IContainer container, ILayerManager layerManager);
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
            RegisterModules();

            RegisterViewModels(viewModelMapper);

            foreach(var module in modules)
                module.RegisterViewModels(viewModelMapper);

            RegisterDependencies(container, layerManager);

            foreach(var module in modules)
                module.RegisterDependencies(container, layerManager);

            OnStartUp();

            foreach(var module in modules)
                module.OnStartUp();
        }
        #endregion
    }
}
