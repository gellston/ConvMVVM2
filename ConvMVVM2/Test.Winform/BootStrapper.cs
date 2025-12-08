using ConvMVVM2.Core.MVP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Winform.Presenters;
using Test.Winform.Views;

namespace Test.Winform
{
    public class BootStrapper : AppBootstrapper
    {
        protected override void OnStartUp(IServiceContainer container)
        {

        }

        protected override void RegisterModules()
        {

        }

        protected override void RegisterServices(IServiceCollection serviceCollection)
        {
            
            //Views
            serviceCollection.AddSingleton<IMainView, MainView>();


            //Presenter
            serviceCollection.AddSingleton<MainPresenter>();
        }
    }
}
