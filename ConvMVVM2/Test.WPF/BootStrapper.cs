using ConvMVVM2.Core.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.WPF
{
    public class BootStrapper : AppBootstrapper
    {
        protected override void OnStartUp()
        {

        }

        protected override void RegisterDependencies(IContainer container)
        {

        }

        protected override void RegisterModules()
        {
            this.RegisterModule<IModule>(new Test.WPFModule.Module());
        }

        protected override void RegisterViewModels(IViewModelMapper viewModelMapper)
        {

        }

        protected override void ViewMapping(IContainer container, ILayerManager layerManager)
        {

        }
    }
}
