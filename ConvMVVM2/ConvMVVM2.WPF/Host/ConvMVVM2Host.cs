using ConvMVVM2.Core.MVVM;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace ConvMVVM2.WPF.Host
{
    public class ConvMVVM2Host
    {
        #region Private Property
        private AppBootstrapper bootStrapper = null;
        private string[] arg = null;
        private IServiceCollection serviceCollection = null;
        private string hostName = "";
        #endregion

        #region Constructor
        internal ConvMVVM2Host(string[] arg, string hostName, AppBootstrapper bootStrapper) { 
            this.arg = arg;
            this.bootStrapper = bootStrapper;
            this.serviceCollection = bootStrapper.ServiceCollection;
            this.hostName = hostName;


        }
        #endregion

        #region Create Host
        public static ConvMVVM2Host CreateHost<BOOTSTRAP, APP>(string[] arg, string hostName) where BOOTSTRAP : AppBootstrapper where APP : Application
        {
            try
            {
                var bootStrapper = Activator.CreateInstance(typeof(BOOTSTRAP)) as BOOTSTRAP;
                bootStrapper.ServiceCollection.AddSingleton<Application, APP>();
                var host = new ConvMVVM2Host(arg, hostName, bootStrapper);
                return host;
            }
            catch
            {
                throw;
            }
        }

        public static ConvMVVM2Host CreateHost<BOOTSTRAP>(string[] arg, string hostName) where BOOTSTRAP : AppBootstrapper
        {
            try
            {
                var bootStrapper = Activator.CreateInstance(typeof(BOOTSTRAP)) as BOOTSTRAP;
                var host = new ConvMVVM2Host(arg, hostName, bootStrapper);
                return host;
            }
            catch
            {
                throw;
            }
        }
        #endregion



        #region Public Property

        public IServiceCollection ServiceCollection
        {
            get => this.serviceCollection;
        }
        #endregion


        #region Public Functions
        public ConvMVVM2Host Build()
        {


            try
            {
                this.bootStrapper.Run();

                return this;
            }
            catch
            {
                throw;
            }

        }

        public ConvMVVM2Host Popup<WINDOW>(bool dialog=true) where WINDOW : Window
        {

            try
            {
                var app = (Application)ServiceLocator.GetServiceProvider().GetService<Application>();
                var window = ServiceLocator.GetServiceProvider().GetService<WINDOW>();

                if (dialog == true)
                    window.ShowDialog();
                else
                    window.Show();

                return this;
            }
            catch
            {
                throw;
            }
        }

        public  ConvMVVM2Host Popup(string name, bool dialog = true)
        {

            try
            {
                var app = (Application)ServiceLocator.GetServiceProvider().GetService<Application>();
                var window = (Window)ServiceLocator.GetServiceProvider().GetService(name);
                if (dialog == true)
                    window.ShowDialog();
                else
                    window.Show();
                return this;
            }
            catch
            {
                throw;
            }
        }

        public ConvMVVM2Host ShutdownMode(ShutdownMode mode)
        {
            try
            {
                var app = (Application)ServiceLocator.GetServiceProvider().GetService<Application>();
                app.ShutdownMode = mode;

                return this;
            }
            catch
            {
                throw;
            }
        }

        public ConvMVVM2Host Shutdown()
        {
            try
            {
                var app = (Application)ServiceLocator.GetServiceProvider().GetService<Application>();
                app.Shutdown();

                return this;
            }
            catch
            {
                throw;
            }
        }

        public int RunApp()
        {

            try
            {
                var app = (Application)ServiceLocator.GetServiceProvider().GetService<Application>();
                return app.Run();
            }
            catch
            {
                throw;
            }
        }

        public int RunApp<WINDOW>() where WINDOW : Window
        {

            try
            {
                var app = (Application)ServiceLocator.GetServiceProvider().GetService<Application>();
                var window = ServiceLocator.GetServiceProvider().GetService<WINDOW>();
                return app.Run(window);
            }
            catch
            {
                throw;
            }
        }

        public int RunApp(string name)
        {

            try
            {
                var app = (Application)ServiceLocator.GetServiceProvider().GetService<Application>();
                var window = (Window)ServiceLocator.GetServiceProvider().GetService(name);
                return app.Run(window);
            }
            catch
            {
                throw;
            }
        }

        public void Run()
        {
            try
            {
                throw new InvalidOperationException("Its not implemented yet");
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
