using System;
using System.Collections.Generic;
using System.Text;

namespace ConvMVVM2.Core.MVVM
{
    public interface IModule
    {
        public void RegisterViewModels(IViewModelMapper viewModelMapper);

        public void RegisterDependencies(IContainer container, ILayerManager layerManager);
        public void OnStartUp();
    }
}
