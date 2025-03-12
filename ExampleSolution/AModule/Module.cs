
using AModule.ViewModels;
using AModule.Views;
using ConvMVVM2.Core.MVVM;

namespace AModule
{
    public class Module : IModule
    {
        public string ModuleName => "AModule";
        public string ModuleVersion => "1.0";
        public void OnStartUp(IServiceContainer container)
        {
        }
        public void RegionMapping(IRegionManager layerManager)
        {
            layerManager.Mapping<AView>("ContentView");
        }
        public void RegisterServices(IServiceCollection container)
        {
            container.AddSingleton<AView>();

            container.AddSingleton<AViewModel>();
        }
        public void ViewModelMapping(IViewModelMapper viewModelMapper)
        {
        }
    }

}
