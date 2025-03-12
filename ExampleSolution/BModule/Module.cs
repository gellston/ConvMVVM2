
using BModule.ViewModels;
using BModule.Views;
using ConvMVVM2.Core.MVVM;

namespace BModule
{
    public class Module : IModule
    {
        public string ModuleName => "BModule";
        public string ModuleVersion => "1.0";
        public void OnStartUp(IServiceContainer container)
        {
        }
        public void RegionMapping(IRegionManager layerManager)
        {
        }
        public void RegisterServices(IServiceCollection container)
        {
            container.AddSingleton<BView>();

            container.AddSingleton<BViewModel>();
        }
        public void ViewModelMapping(IViewModelMapper viewModelMapper)
        {
        }
    }

}
