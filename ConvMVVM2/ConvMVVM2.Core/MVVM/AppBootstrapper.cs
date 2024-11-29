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
        #endregion


        #region Constructor

        protected AppBootstrapper()
        {
            container = new Container();
            layerManager = new LayerManager();
            viewModelMapper = new ViewModelMapper();

            ConfigureContainer();
        }
        #endregion


        #region Private Functions

        private void ConfigureContainer()
        {
            container.RegisterInstance(container);
            container.RegisterInstance(layerManager);
            container.RegisterInstance(viewModelMapper);
            container.RegisterSingleton<IViewModelInitializer, DefaultViewModelInitializer>();
        }
        #endregion

        #region Protected Functions
        protected abstract void RegisterViewModels(IViewModelMapper viewModelMapper);
        protected abstract void RegisterDependencies(IContainer container, ILayerManager layerManager);
        protected abstract void OnStartUp();
        #endregion

        #region Public Functions
        public void Run()
        {
            RegisterViewModels(viewModelMapper);
            RegisterDependencies(container, layerManager);
            OnStartUp();
        }
        #endregion
    }
}
