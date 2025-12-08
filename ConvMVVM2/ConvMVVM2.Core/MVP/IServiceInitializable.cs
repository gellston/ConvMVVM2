using System;
using System.Collections.Generic;
using System.Text;

namespace ConvMVVM2.Core.MVP
{
    public interface IServiceInitializable
    {
        #region Public Functions
        void OnServiceInitialized();
        #endregion
    }
}
