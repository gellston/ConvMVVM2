using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConvMVVM2.Core.MVVM
{
    public interface IAsyncRelayCommand : ICommand
    {
        #region Public Property
        bool IsRunning { get; }
        #endregion


        #region Public Functions

        public void InvalidateCommand();

        #endregion
    }
}
