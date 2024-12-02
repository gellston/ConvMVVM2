using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ConvMVVM2.Core.MVVM
{
    public static class ContainerProvider
    {
        #region Private Static Property
        private static IContainer _container = new Container();
        #endregion

        #region Constructor
        static ContainerProvider()
        {
            _container = new Container();
            _container.RegisterInstance<IContainer>(_container);
            _container.RegisterInstance<ILayerManager>(new LayerManager());
            _container.RegisterInstance<IViewModelMapper>(new ViewModelMapper());
            _container.RegisterSingleton<IViewModelInitializer, DefaultViewModelInitializer>();
        }
        #endregion


        #region Public Static Functions

        //public static void SetContainer(IContainer container)
        //{
        //    _container = container;
        //}

        public static IContainer GetContainer()
        {
            if (_container == null)
            {
                throw new InvalidOperationException("IContainer has not been set. Make sure to call ContainerProvider.SetContainer in your App class.");
            }
            return _container;
        }
        #endregion


    }
}
