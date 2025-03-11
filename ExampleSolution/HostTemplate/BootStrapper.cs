using ConvMVVM2.Core.MVVM;
using HostTemplate.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostTemplate
{
    public class BootStrapper : ConvMVVM2.Core.MVVM.AppBootstrapper
    {
        protected override void OnStartUp()
        {

        }

        protected override void RegionMapping(IRegionManager layerManager)
        {

        }

        protected override void RegisterModules()
        {

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
