using ConvMVVM2.Core.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Security;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ConvMVVM2.WPF.Host
{
    public static class ConvMVVM2Host
    {
        #region Static Private Property
        private static AppBootstrapper bootStrapper = null;
        #endregion


        #region Extension Functions
        public static AppBootstrapper CreateStrapper<BOOTSTRAP, APP>(ShutdownMode shutdownMode = ShutdownMode.OnMainWindowClose) where BOOTSTRAP : AppBootstrapper where APP : Application
        {
            try
            {
                if (ConvMVVM2Host.bootStrapper != null)
                {
                    return ConvMVVM2Host.bootStrapper;
                }

                var bootStrapper = Activator.CreateInstance(typeof(BOOTSTRAP)) as BOOTSTRAP;
                bootStrapper.ServiceCollection.AddSingleton<Application, APP>();
                bootStrapper.Run();


                var serviceLocator = ServiceLocator.GetServiceProvider().GetService<Application>();
                serviceLocator.ShutdownMode = shutdownMode;

                ConvMVVM2Host.bootStrapper = bootStrapper;
                return bootStrapper;
            }
            catch
            {
                throw;
            }

        }

        public static AppBootstrapper Shutdown(this AppBootstrapper bootStrapper)
        {
            try
            {
                var app = ServiceLocator.GetServiceProvider().GetService<Application>();
                app.Shutdown();
            }
            catch
            {
                throw;
            }

            return bootStrapper;
        }

        public static AppBootstrapper Shutdown(this AppBootstrapper bootStrapper, int exitCode)
        {
            try
            {
                var app = ServiceLocator.GetServiceProvider().GetService<Application>();
                app.Shutdown(exitCode);
            }
            catch
            {
                throw;
            }

            return bootStrapper;
        }

        public static AppBootstrapper RunApp(this AppBootstrapper bootStrapper)
        {
            try
            {
                var app = ServiceLocator.GetServiceProvider().GetService<Application>();
                app.Run();
            }
            catch
            {
                throw;
            }

            return bootStrapper;
        }

        public static AppBootstrapper Run<WINDOW>(this AppBootstrapper bootStrapper) where WINDOW : Window
        {
            
            try
            {

                var window = ServiceLocator.GetServiceProvider().GetService<WINDOW>();
                window.ShowDialog();

            }
            catch 
            {
                throw;
            }

            return bootStrapper;
        }

        public static AppBootstrapper Run(this AppBootstrapper bootStrapper, string windowName)
        {
            try
            {
                var window = (Window)ServiceLocator.GetServiceProvider().GetService(windowName);
                window.ShowDialog();
            }
            catch
            {
                throw;
            }

            return bootStrapper;
        }

        public static AppBootstrapper RunAsync<WINDOW>(this AppBootstrapper bootStrapper) where WINDOW : Window
        {

            try
            {

                var window = ServiceLocator.GetServiceProvider().GetService<WINDOW>();
                window.Show();

            }
            catch
            {
                throw;
            }

            return bootStrapper;
        }

        public static AppBootstrapper RunAsync(this AppBootstrapper bootStrapper, string windowName)
        {
            try
            {
                var window = (Window)ServiceLocator.GetServiceProvider().GetService(windowName);
                window.Show();
            }
            catch
            {
                throw;
            }

            return bootStrapper;
        }
        #endregion
    }
}
