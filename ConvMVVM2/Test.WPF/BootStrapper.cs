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


        protected override void RegisterServices(IServiceCollection container)
        {

            container.AddInstance<MainWindowView>();
            container.AddInstance<MainWindowViewModel>();
        }

        protected override void RegisterModules()
        {

            // 모듈 자동서치 기능 on
            this.EnableAutoModuleSearch(true);


            // 특정 모듈을 로드하지 안도록 벤
            //this.RejectModule("CModule");


            this.AddModuleCurrentPath();



            // 프로그램 실행 경로기준 모듈 서치할 경로 상대경로 추가
            //this.AddModuleRelativePath("Modules");

            // 프로그램 실행경로를 모듈 서치 경로로 추가 
            this.AddModuleCurrentPath();
            
        }

        protected override void ViewModelMapping(IViewModelMapper viewModelMapper)
        {


        }

        protected override void RegionMapping(IRegionManager layerManager)
        {
        }

        protected override void OnStartUp(IServiceContainer container)
        {

        }
    }
}
