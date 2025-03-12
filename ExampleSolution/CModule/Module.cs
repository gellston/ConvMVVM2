
using CModule.ViewModels;
using CModule.Views;
using ConvMVVM2.Core.MVVM;

namespace CModule
{
    public class Module : IModule
    {
        public string ModuleName => "CModule";
        public string ModuleVersion => "1.0";
        public void OnStartUp(IServiceContainer container)
        {
        }
        public void RegionMapping(IRegionManager layerManager)
        {
        }
        public void RegisterServices(IServiceCollection container)
        {
            container.AddSingleton<CView>();

            container.AddSingleton<CViewModel>();
        }
        public void ViewModelMapping(IViewModelMapper viewModelMapper)
        {
        }
    }

}
