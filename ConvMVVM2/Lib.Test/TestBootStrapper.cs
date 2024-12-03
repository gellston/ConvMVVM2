using ConvMVVM2.Core.MVVM;
using Lib.Test.Properties;
using Lib.Test.UI.Views;
using Lib.Test.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Test
{
    public class TestBootStrapper : AppBootstrapper
    {
        #region Protected Functions
        protected override void RegisterViewModels(IViewModelMapper viewModelMapper)
        {
            viewModelMapper.Register<MainContent, MainContentViewModel>();
        }

        protected override void RegisterDependencies(IContainer container, ILayerManager layerManager)
        {

            container.RegisterSingleton<IView, MainContent>("MainContent");



            IView mainContent = container.Resolve<MainContent>();



            layerManager.Mapping("MainContent", mainContent);



            //language
            ILocalizeService localService = container.Resolve<ILocalizeService>();

            localService.SetResourceManager(Lib.Test.Properties.Resource.ResourceManager);

            localService.ChangeLocal("kr");

            
            //localService.SetResourceManager(Properties.Resource);

           
        }

        protected override void OnStartUp()
        {
           
        }
        #endregion
    }
}
