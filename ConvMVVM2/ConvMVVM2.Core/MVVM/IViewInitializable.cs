using System;
using System.Collections.Generic;
using System.Text;

namespace ConvMVVM2.Core.MVVM
{
    public interface IViewInitializable
    {
        #region Public Functions
        void OnViewInitialized();
        #endregion
    }
}
