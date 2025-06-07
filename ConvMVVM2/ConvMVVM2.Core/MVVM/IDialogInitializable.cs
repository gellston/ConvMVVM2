using System;
using System.Collections.Generic;
using System.Text;

namespace ConvMVVM2.Core.MVVM
{
    public interface IDialogInitializable<T>
    {
        public void OnDialogInitialized(T parameter);
    }
}
