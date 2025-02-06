using System;
using System.Collections.Generic;
using System.Text;

namespace ConvMVVM2.Core.MVVM
{
    public interface INavigateAware
    {

        #region Public Functions
        public bool CanNavigate(NavigationContext context);
        public void OnNavigatedTo(NavigationContext context);
        public void OnNavigatedFrom(NavigationContext context);
        #endregion
    }
}
