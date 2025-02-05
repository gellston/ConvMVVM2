using System;
using System.Collections.Generic;
using System.Text;

namespace ConvMVVM2.Core.MVVM
{
    public interface IRegion
    {
        #region Public Property
        object Content { get; set; }
        #endregion
    }
}
