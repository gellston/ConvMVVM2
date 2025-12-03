using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConvMVVM2.WPF.ViewModels
{
    public class SizeObserverViewModel
    {
        #region Event 
        public event Action<Size> SizeChangedEvent = null;
        #endregion

        #region Public Functions
        public void SizeChanged(Size size) => this.SizeChangedEvent?.Invoke(size);
        #endregion
    }
}
