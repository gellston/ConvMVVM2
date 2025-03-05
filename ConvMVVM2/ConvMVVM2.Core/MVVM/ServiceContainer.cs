using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConvMVVM2.Core.MVVM
{
    public class ServiceContainer : IServiceContainer
    {
        #region Private Property
        private readonly IServiceCollection serviceCollection;
        private readonly Dictionary<Type, object> cacheObjects = new Dictionary<Type, object>();
        #endregion//

        #region Constructor
        public ServiceContainer(IServiceCollection services)
        {
            if (services == null)
            {
                throw new InvalidOperationException("Invalid service collection");
            }
            this.serviceCollection = services;
        }
        #endregion


        #region Private Functions
        public object Create(Type type)
        {
            if (!this.serviceCollection.CheckType(type))
            {
                throw new InvalidOperationException("There is no service key");
            }
            try
            {

                var typeInfo = this.serviceCollection.GetType(type);

                var objectType = typeInfo.Item1;
                var cacheOrNot = typeInfo.Item2;
                var cacheObject = typeInfo.Item3;
                var callback = typeInfo.Item4;
                var defaultConstructors = objectType.GetConstructors();
                if (defaultConstructors.Count() <= 0 && cacheObject == null)
                {
                    throw new InvalidOperationException("There is no constructor");
                }

                if (cacheOrNot == false && callback == null && cacheObject == null)
                {
                    var defaultConstructor = defaultConstructors[0];
                    var defaultParams = defaultConstructor.GetParameters();
                    var parameters = defaultParams.Select(param => Create(param.ParameterType)).ToArray();
                    var service = defaultConstructor.Invoke(parameters);
                    if (service is IServiceInitializable)
                    {
                        var _service = service as IServiceInitializable;
                        _service.OnServiceInitialized();
                    }
                    return service;
                }

                if (cacheOrNot == false && callback != null && cacheObject == null)
                {
                    var methodInfo = callback.GetType().GetMethod("Invoke");
                    var service = methodInfo.Invoke(callback, new[] { this });
                    if (service is IServiceInitializable)
                    {
                        var _service = service as IServiceInitializable;
                        _service.OnServiceInitialized();
                    }
                    return service;
                }

                if (cacheOrNot == true && callback == null && cacheObject == null)
                {
                    if (this.cacheObjects.ContainsKey(type) == true)
                        return this.cacheObjects[type];

                    var defaultConstructor = defaultConstructors[0];
                    var defaultParams = defaultConstructor.GetParameters();
                    var parameters = defaultParams.Select(param => Create(param.ParameterType)).ToArray();
                    var service = defaultConstructor.Invoke(parameters);
                    this.cacheObjects[type] = service;
                    if (service is IServiceInitializable)
                    {
                        var _service = service as IServiceInitializable;
                        _service.OnServiceInitialized();
                    }
                    return service;
                }

                if (cacheOrNot == true && callback != null && cacheObject == null)
                {
                    if (this.cacheObjects.ContainsKey(type) == true)
                        return this.cacheObjects[type];

                    var methodInfo = callback.GetType().GetMethod("Invoke");
                    var service = methodInfo.Invoke(callback, new[] { this });
                    this.cacheObjects[type] = service;
                    if (service is IServiceInitializable)
                    {
                        var _service = service as IServiceInitializable;
                        _service.OnServiceInitialized();
                    }
                    return service;
                }

                if (cacheOrNot == true && callback == null && cacheObject != null)
                {
                    if (this.cacheObjects.ContainsKey(type) == true)
                        return this.cacheObjects[type];

                    this.cacheObjects[type] = cacheObject;
                    if (cacheObject is IServiceInitializable)
                    {
                        var _service = cacheObject as IServiceInitializable;
                        _service.OnServiceInitialized();
                    }
                    return cacheObject;
                }

                throw new InvalidOperationException("Invalid service collection infomation");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Unknown", ex);
            }
        }
        #endregion

        #region Functions

        public TInterface GetService<TInterface>() where TInterface : class
        {
            try
            {
                return (TInterface)Create(typeof(TInterface));
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(String.Format("Can't instantiate {0}", typeof(TInterface)), ex);
            }
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return Create(serviceType);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(String.Format("Can't instantiate {0}", serviceType), ex);
            }
        }


        public object GetService(string typeName)
        {
            try
            {
                var serviceType = this.KeyType(typeName);
                return Create(serviceType);

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(String.Format("Can't instantiate {0}", typeName), ex);

            }
        }


        public Type KeyType(string key)
        {
            return this.serviceCollection.KeyType(key);
        }
        #endregion
    }
}
