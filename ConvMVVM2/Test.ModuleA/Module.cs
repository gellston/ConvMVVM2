using ConvMVVM2.Core.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.ModuleA.View;
using Test.ModuleA.ViewModle;

namespace Test.ModuleA
{
    public class Module : IModule
    {
        public string ModuleName => "CModule";
        public string ModuleVersion => "1.0";

        public void OnStartUp()
        {
          
        }

        public void RegionMapping(IRegionManager layerManager)
        {
            layerManager.Mapping<CView>("AView");
        }

        public void RegisterServices(IServiceCollection container)
        {

            container.AddSingleton<CView>();

            container.AddSingleton<CViewModel>();

            container.AddInstance<CWindowView>();
        }

        public void ViewModelMapping(IViewModelMapper viewModelMapper)
        {
   
        }
    }
}
