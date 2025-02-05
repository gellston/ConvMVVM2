using ConvMVVM2.Core.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.WPF.View;
using Test.WPF.ViewModel;

namespace Test.WPF
{
    public class BootStrapper : ConvMVVM2.Core.MVVM.AppBootstrapper
    {
        protected override void OnStartUp()
        {
 
        }

        protected override void RegisterServices(IServiceCollection container)
        {
            container.AddSingleton<AView>();
            container.AddSingleton<AViewModel>();

        }

        protected override void RegisterModules()
        {

        }

        protected override void ViewModelMapping(IViewModelMapper viewModelMapper)
        {
            viewModelMapper.Register<AView, AViewModel>();
        }

        protected override void RegionMapping(IRegionManager layerManager)
        {
            layerManager.Mapping("AView", typeof(AView));
        }
    }
}
