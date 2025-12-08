using System;
using System.Collections.Generic;
using System.Runtime;
using System.Text;

namespace ConvMVVM2.Core.MVP
{
    public interface IModule
    {
        #region Public Functions

        public void RegisterServices(IServiceCollection container);
        public void OnStartUp(IServiceContainer container);

        public string ModuleName { get; }
        public string ModuleVersion { get; }

       
        #endregion
    }
}
