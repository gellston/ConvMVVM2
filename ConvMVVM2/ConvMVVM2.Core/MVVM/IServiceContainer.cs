using System;
using System.Collections.Generic;
using System.Text;

namespace ConvMVVM2.Core.MVVM
{
    public interface IServiceContainer
    {

        #region Public Functions
        public TInterface GetService<TInterface>() where TInterface : class;
        public object GetService(Type serviceType);
        public object GetService(string typeName);
        public Type KeyType(string key);
        #endregion 
    }
}
