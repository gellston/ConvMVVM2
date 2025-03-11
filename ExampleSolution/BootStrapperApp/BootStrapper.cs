using BootStrapperApp.ViewModel;
using BootStrapperApp.ViewModels;
using BootStrapperApp.Views;
using BootStrapperApp.Windows;
using ConvMVVM2.Core.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootStrapperApp
{
    public class BootStrapper : AppBootstrapper
    {
        protected override void OnStartUp(IServiceContainer container)
        {
          
        }

        protected override void RegionMapping(IRegionManager layerManager)
        {
            layerManager.Mapping<MainView>("MainView");
        }

        protected override void RegisterModules()
        {
        
        }

        protected override void RegisterServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<MainWindow>();

            serviceCollection.AddSingleton<MainViewModel>();
            serviceCollection.AddSingleton<SubViewModel>();

            serviceCollection.AddSingleton<MainView>();
            serviceCollection.AddSingleton<SubView>();
        }

        protected override void ViewModelMapping(IViewModelMapper viewModelMapper)
        {
            
        }
    }
}
