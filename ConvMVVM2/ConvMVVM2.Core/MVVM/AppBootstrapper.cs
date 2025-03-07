using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConvMVVM2.Core.MVVM
{
    public abstract class AppBootstrapper
    {
        #region Private Property
        private List<IModule> modules;
        private ServiceCollection serviceCollection = new ServiceCollection();
        private HashSet<string> moduleLoadPaths = new HashSet<string>();
        private bool enableAutoModuleSearch = false;
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


        private void LoadAssemblyStart()
        {
            try
            {

                try
                {

                    // 현재 경로에서 플러그인 찾기
                    var modulePath = AppDomain.CurrentDomain.BaseDirectory;
                    var moduleFiles = Directory.GetFiles(modulePath, "*.dll");

                    foreach (var moduleFile in moduleFiles)
                    {
                        try
                        {
                            var assembly = Assembly.LoadFrom(moduleFile);
                            var pluginTypes = assembly.GetTypes().Where(t => typeof(IModule).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

                            foreach (var pluginType in pluginTypes)
                            {
                                var plugin = (IModule)Activator.CreateInstance(pluginType);

                                this.modules.Add(plugin);
                            }
                        }
                        catch
                        {

                        }

                    }

                }
                catch(Exception ex)
                {

                }



                foreach(var modulePath in this.moduleLoadPaths)
                {
                    var moduleFiles = Directory.GetFiles(modulePath, "*.dll");

                    foreach (var moduleFile in moduleFiles)
                    {

                        try
                        {
                            var assembly = Assembly.LoadFrom(moduleFile);
                            var pluginTypes = assembly.GetTypes().Where(t => typeof(IModule).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

                            foreach (var pluginType in pluginTypes)
                            {
                                var plugin = (IModule)Activator.CreateInstance(pluginType);

                                this.modules.Add(plugin);
                            }
                        }
                        catch
                        {

                        }
                    }
                }
            }
            catch
            {

            }
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

        protected void RegisterModule<T>() where T : IModule
        {
            this.modules.Add((IModule)Activator.CreateInstance(typeof(T)));
        }

        protected void AddModulePath(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                    throw new InvalidOperationException("Path is not exists");

                this.moduleLoadPaths.Add(path);
            }
            catch
            {
                throw;
            }
        }

        protected void EnableAutoModuleSearch(bool enable)
        {
            this.enableAutoModuleSearch = enable;
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

            if(this.enableAutoModuleSearch)
                LoadAssemblyStart();


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


        #region Event Handler


        #endregion



    }
}
