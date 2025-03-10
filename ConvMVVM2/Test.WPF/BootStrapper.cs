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

            // 모듈 자동서치 기능 on
            this.EnableAutoModuleSearch(true);

            // 모듈 서치할 경로 절대경로 추가 
            this.AddModulePath("C:\\github\\ConvMVVM2\\ConvMVVM2\\Test.ModuleA\\bin\\x64\\Debug\\net8.0-windows");

            // 프로그램 실행 경로기준 모듈 서치할 경로 상대경로 추가
            //this.AddModuleRelativePath("Modules");

            // 프로그램 실행경로를 모듈 서치 경로로 추가 
            this.AddModuleCurrentPath();
            
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
