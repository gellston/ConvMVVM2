using System;
using System.Collections.Generic;
using System.Text;

namespace ConvMVVM2.Core.MVVM
{
    public static class ContainerProvider
    {
        #region Private Static Property
        private static IContainer _container;
        #endregion


        #region Public Static Functions

        public static void SetContainer(IContainer container)
        {
            _container = container;
        }

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
