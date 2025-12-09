using ConvMVVM2.Core.MVP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.WinformTest.Presenter;
using Test.WinformTest.View;

namespace Test.WinformTest
{
    public class BootStrapper : AppBootstrapper
    {
        protected override void OnStartUp(IServiceContainer container)
        {
            System.Diagnostics.Debug.WriteLine("test");
        }

        protected override void RegisterModules()
        {


            this.AddModuleRelativePath("Plugins");


            //Module
            System.Diagnostics.Debug.WriteLine("test");
        }

        protected override void RegisterServices(IServiceCollection serviceCollection)
        {

            System.Diagnostics.Debug.WriteLine("test");



            //View
            serviceCollection.AddSingleton<IMainView, MainView>();



            //Presenter
            serviceCollection.AddSingleton<MainPresenter>();

        }
    }
}
