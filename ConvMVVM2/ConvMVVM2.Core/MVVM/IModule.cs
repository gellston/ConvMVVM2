using System;
using System.Collections.Generic;
using System.Runtime;
using System.Text;

namespace ConvMVVM2.Core.MVVM
{
    public interface IModule
    {
        #region Public Functions
        public void ViewModelMapping(IViewModelMapper viewModelMapper);

        public void RegisterServices(IServiceCollection container);
        public void RegionMapping(IRegionManager layerManager);
        public void OnStartUp();

        public string ModuleName { get; }
        public string ModuleVersion { get; }

       
        #endregion
    }
}
