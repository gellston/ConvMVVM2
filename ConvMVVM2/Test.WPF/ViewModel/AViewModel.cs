using ConvMVVM2.Core.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.WPF.ViewModel
{
    public class AViewModel : ViewModelBase, IViewLoadable, IServiceInitializable
    {

        #region Constructor
        public AViewModel()
        {
            System.Diagnostics.Debug.WriteLine("test");

        }


        #endregion

        #region Event Handler
        public void OnViewLoaded()
        {

        }

        public void OnServiceInitialized()
        {

        }
        #endregion
    }
}
