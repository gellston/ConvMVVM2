using ConvMVVM2.Core.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.WPFModule.Local.ViewModels;
using Test.WPFModule.Themes.Views;

namespace Test.WPFModule
{
    public class Module : IModule
    {
        public void OnStartUp()
        {

        }

        public void RegisterDependencies(IContainer container)
        {
            container.RegisterSingleton<IView, AView>();
        }

        public void RegisterViewModels(IViewModelMapper viewModelMapper)
        {
            viewModelMapper.Register<AView, AViewModel>();
        }

        public void ViewMapping(IContainer container, ILayerManager layerManager)
        {

            var view = container.Resolve<AView>();

            layerManager.Mapping("MainView", view);
        }
    }
}
