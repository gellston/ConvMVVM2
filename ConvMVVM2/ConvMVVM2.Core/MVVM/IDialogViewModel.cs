using System;
using System.Collections.Generic;
using System.Text;

namespace ConvMVVM2.Core.MVVM
{
    public interface IDialogViewModel<RType>  where RType : class
    {
        #region Public Functions
        public event Action<RType> CloseEvent;
        #endregion
    }
}
