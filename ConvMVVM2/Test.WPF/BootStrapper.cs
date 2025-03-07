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
            container.AddSingleton<BView>();
            container.AddSingleton<AViewModel>();
            container.AddSingleton<BViewModel>();
            container.AddInstance<MainWindowView>();
        }

        protected override void RegisterModules()
        {

            this.EnableAutoModuleSearch(true);
            this.AddModulePath("C:\\github\\ConvMVVM2\\ConvMVVM2\\Test.ModuleA\\bin\\x64\\Debug\\net8.0-windows");
            this.AddModuleRelativePath("Modules");

            this.RegisterModule<ModuleA.Module>();
            
        }

        protected override void ViewModelMapping(IViewModelMapper viewModelMapper)
        {
            viewModelMapper.Register<AView, AViewModel>();

        }

        protected override void RegionMapping(IRegionManager layerManager)
        {
        }
    }
}
