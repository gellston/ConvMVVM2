using ConvMVVM2.Core.MVVM;
using LocalizeApp.ViewModels;
using LocalizeApp.Views;
using LocalizeApp.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace LocalizeApp
{
    public class BootStrapper : AppBootstrapper
    {
        protected override void OnStartUp(IServiceContainer container)
        {
            var localizeService = container.GetService<ILocalizeService>();
           
            localizeService.SetResourceManager(Properties.Resource.ResourceManager);
            
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
            serviceCollection.AddSingleton<MainView>();
            serviceCollection.AddSingleton<MainViewModel>();
        }

        protected override void ViewModelMapping(IViewModelMapper viewModelMapper)
        {

        }
    }
}
