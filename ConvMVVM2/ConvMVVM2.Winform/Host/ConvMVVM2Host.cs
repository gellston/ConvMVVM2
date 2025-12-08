
using ConvMVVM2.Core.MVP;
using ConvMVVM2.Winform.Extension;
using System;
using System.Windows.Forms;


namespace ConvMVVM2.Winform.Host
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
        internal ConvMVVM2Host(string[] arg, string hostName, AppBootstrapper bootStrapper)
        {
            this.arg = arg;
            this.bootStrapper = bootStrapper;
            this.serviceCollection = bootStrapper.ServiceCollection;
            this.hostName = hostName;


            this.bootStrapper.OnModuleAddEvent += BootStrapper_OnModuleAddEvent;

        }


        #endregion

        #region Create Host

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


        public void RunApp<PRESENTER>() where PRESENTER : IPresenter
        {

            try
            {
                var presenter = ServiceLocator.GetServiceProvider().GetPresenter<PRESENTER>();
                Application.Run((Form)presenter.View);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Event
        public event Action<string, string> OnModuleAddEvent;
        #endregion

        #region Event Handler
        private void BootStrapper_OnModuleAddEvent(string arg1, string arg2)
        {

            this.OnModuleAddEvent?.Invoke(arg1, arg2);
        }
        #endregion
    }
}
