using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;

namespace ConvMVVM2.Core.MVP
{
    public class ServiceCollection : IServiceCollection
    {
        #region Private Property
        private readonly Dictionary<Type, Tuple<Type, bool, object, object>> Types = new Dictionary<Type, Tuple<Type, bool, object, object>>();
        private readonly Dictionary<string, Type> KeyTypes = new Dictionary<string, Type>();
        #endregion

        #region Constructor
        internal ServiceCollection()
        {

        }
        #endregion

        #region Static Functions
        public static IServiceCollection Create()
        {
            return new ServiceCollection();
        }
        #endregion

        #region Private Functions
        private Tuple<Type, bool, object, object> CreateTypeInfo(Type type, bool cache, object cacheObject, object callback)
        {
            return new Tuple<Type, bool, object, object>(type, cache, cacheObject, callback);
        }
        #endregion

        #region Public Functions
        public void AddSingleton<TInterface, TImplementation>() where TImplementation : TInterface
        {
            this.KeyTypes[typeof(TInterface).Name] = typeof(TInterface);
            this.Types[typeof(TInterface)] = this.CreateTypeInfo(typeof(TImplementation), true, null, null);
        }

        public void AddSingleton<TImplementation>() where TImplementation : class
        {
            this.KeyTypes[typeof(TImplementation).Name] = typeof(TImplementation);
            this.Types[typeof(TImplementation)] = this.CreateTypeInfo(typeof(TImplementation), true, null, null);
        }

        public void AddSingleton<TImplementation>(TImplementation implementation) where TImplementation : class
        {
            this.KeyTypes[typeof(TImplementation).Name] = typeof(TImplementation);
            this.Types[typeof(TImplementation)] = this.CreateTypeInfo(typeof(TImplementation), true, implementation, null);
        }

        public void AddInstance<TInterface, TImplementation>() where TImplementation : TInterface
        {
            this.KeyTypes[typeof(TInterface).Name] = typeof(TInterface);
            this.Types[typeof(TInterface)] = this.CreateTypeInfo(typeof(TImplementation), false, null, null);
        }

        public void AddInstance<TImplementation>() where TImplementation : class
        {
            this.KeyTypes[typeof(TImplementation).Name] = typeof(TImplementation);
            this.Types[typeof(TImplementation)] = this.CreateTypeInfo(typeof(TImplementation), false, null, null);
        }

        public void AddSingleton<TInterface, TImplementation>(TImplementation implementation) where TImplementation : TInterface
        {
            this.KeyTypes[typeof(TInterface).Name] = typeof(TInterface);
            this.Types[typeof(TInterface)] = this.CreateTypeInfo(typeof(TImplementation), true, implementation, null);
        }

        public void AddSingleton<TInterface, TImplementation>(Func<IServiceContainer, TInterface> factory) where TImplementation : TInterface
        {
            this.KeyTypes[typeof(TInterface).Name] = typeof(TInterface);
            this.Types[typeof(TInterface)] = this.CreateTypeInfo(typeof(TImplementation), true, null, factory);
        }

        public void AddInstance<TInterface, TImplementation>(Func<IServiceContainer, TInterface> factory) where TImplementation : TInterface
        {
            this.KeyTypes[typeof(TInterface).Name] = typeof(TInterface);
            this.Types[typeof(TInterface)] = this.CreateTypeInfo(typeof(TImplementation), false, null, factory);
        }

        public void AddSingleton<TImplementation>(Func<IServiceContainer, TImplementation> factory) where TImplementation : class
        {
            this.KeyTypes[typeof(TImplementation).Name] = typeof(TImplementation);
            this.Types[typeof(TImplementation)] = this.CreateTypeInfo(typeof(TImplementation), true, null, factory);
        }
        public void AddInstance<TImplementation>(Func<IServiceContainer, TImplementation> factory) where TImplementation : class
        {
            this.KeyTypes[typeof(TImplementation).Name] = typeof(TImplementation);
            this.Types[typeof(TImplementation)] = this.CreateTypeInfo(typeof(TImplementation), false, null, factory);
        }

        public Type KeyType(string name)
        {
            if (!this.KeyTypes.ContainsKey(name)) return null;
            else return this.KeyTypes[name];
        }

        public bool CheckType(Type type)
        {
            if (this.Types.ContainsKey(type)) return true;
            else return false;
        }

        public IServiceContainer CreateContainer()
        {
            return new ServiceContainer(this);
        }

        public Tuple<Type, bool, object, object> GetType(Type type)
        {
            if (!CheckType(type))
            {
                throw new InvalidOperationException("Invalid key type");
            }

            return this.Types[type];
        }


        #endregion
    }
}
