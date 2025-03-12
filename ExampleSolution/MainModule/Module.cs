
using ConvMVVM2.Core.MVVM;
using MainModule.ViewModels;
using MainModule.Views;

namespace MainModule
{
    public class Module : ConvMVVM2.Core.MVVM.IModule
    {
        public string ModuleName => "MainModule";

        public string ModuleVersion => "1.0";

        public void OnStartUp(IServiceContainer container)
        {

            
        }

        public void RegionMapping(IRegionManager layerManager)
        {
            layerManager.Mapping<MainView>("MainView");

        }

        public void RegisterServices(IServiceCollection container)
        {
            container.AddSingleton<MainView>();

            container.AddSingleton<MainViewModel>();
        }

        public void ViewModelMapping(IViewModelMapper viewModelMapper)
        {
           
        }
    }

}
