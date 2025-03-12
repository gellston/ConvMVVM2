using ConvMVVM2.Core.MVVM;
using ModuleApp.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleApp
{
    public class BootStrapper : AppBootstrapper
    {
        protected override void OnStartUp(IServiceContainer container)
        {

        }

        protected override void RegionMapping(IRegionManager layerManager)
        {

        }

        protected override void RegisterModules()
        {
            this.EnableAutoModuleSearch(true);
            this.AddModuleRelativePath("Modules");
        }

        protected override void RegisterServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<MainWindow>();
        }

        protected override void ViewModelMapping(IViewModelMapper viewModelMapper)
        {
        }
    }
}
