using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ConvMVVM2.Core.MVP
{
    public static class ServiceLocator
    {
        #region Private Static Property
        private static IServiceContainer _container = null;
        #endregion

        #region Constructor
        static ServiceLocator()
        {
            //_container = new Container();
            //_container.AddSingleton<IServiceCollection>(_container);
            //_container.RegisterInstance<ILayerManager>(new LayerManager());
            //_container.RegisterInstance<IViewModelMapper>(new ViewModelMapper());
            //_container.RegisterSingleton<IViewModelInitializer, DefaultViewModelInitializer>();
            //_container.RegisterSingleton<ILocalizeService, LocalizeService>();
        }
        #endregion


        #region Public Static Functions
        public static IServiceContainer GetServiceProvider()
        {
            if (_container == null)
            {
                throw new InvalidOperationException("IContainer has not been set. Make sure to call ServiceProvider.SetContainer in your App class.");
            }
            return _container;
        }
        #endregion

        #region Internal Static Functions
        internal static void SetServiceProvider(IServiceContainer container)
        {
            _container = container;
        }
        #endregion
    }
}
