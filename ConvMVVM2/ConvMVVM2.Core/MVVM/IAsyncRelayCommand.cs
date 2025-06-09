using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConvMVVM2.Core.MVVM
{
    public interface IAsyncRelayCommand : ICommand
    {
        bool IsRunning { get; }
    }
}
