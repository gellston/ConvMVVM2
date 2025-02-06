using ConvMVVM2.Core.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.WPF.ViewModel
{
    public class BViewModel : ViewModelBase, INavigateAware
    {
        #region Constructor
        public BViewModel() { }


        #endregion

        public bool CanNavigate(NavigationContext context)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext context)
        {
            System.Diagnostics.Debug.WriteLine("test");
        }

        public void OnNavigatedTo(NavigationContext context)
        {
            System.Diagnostics.Debug.WriteLine("test");
        }
    }
}
