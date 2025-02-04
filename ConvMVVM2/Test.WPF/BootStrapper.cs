using ConvMVVM2.Core.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.WPF.ViewModel;

namespace Test.WPF
{
    public class BootStrapper : ConvMVVM2.Core.MVVM.AppBootstrapper
    {
        protected override void OnStartUp()
        {
 
        }

        protected override void RegisterDependencies(IServiceCollection container)
        {


        }

        protected override void RegisterModules()
        {

        }

        protected override void RegisterViewModels(IViewModelMapper viewModelMapper)
        {

        }

        protected override void ViewMapping(IServiceCollection container, ILayerManager layerManager)
        {
   
        }
    }
}
