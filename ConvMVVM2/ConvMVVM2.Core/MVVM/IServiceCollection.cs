using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;

namespace ConvMVVM2.Core.MVVM
{
    public interface IServiceCollection
    {
        #region Public Functions
        public void AddSingleton<TInterface, TImplementation>() where TImplementation : TInterface;
        public void AddInstance<TInterface, TImplementation>() where TImplementation : TInterface;

        public void AddSingleton<TImplementation>() where TImplementation : class;
        public void AddSingleton<TImplementation>(TImplementation implementation) where TImplementation : class;
        public void AddInstance<TImplementation>() where TImplementation : class;

        public void AddSingleton<TInterface, TImplementation>(TImplementation implementation) where TImplementation : TInterface;

        public void AddSingleton<TInterface, TImplementation>(Func<IServiceContainer, TInterface> factory) where TImplementation : TInterface;
        public void AddInstance<TInterface, TImplementation>(Func<IServiceContainer, TInterface> factory) where TImplementation : TInterface;

        public void AddSingleton<TImplementation>(Func<IServiceContainer, TImplementation> factory) where TImplementation : class;
        public void AddInstance<TImplementation>(Func<IServiceContainer, TImplementation> factory) where TImplementation : class;


        public bool CheckType(Type type);
        public Type KeyType(string name);

        public Tuple<Type, bool, object, object> GetType(Type type);

        public IServiceContainer CreateContainer();
        #endregion
    }
}
