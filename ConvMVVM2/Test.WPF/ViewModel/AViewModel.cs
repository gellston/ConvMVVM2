using ConvMVVM2.Core.Attributes;
using ConvMVVM2.Core.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.WPF.ViewModel
{
    public partial class AViewModel : ViewModelBase
    {

        #region Constructor
        public AViewModel()
        {
            System.Diagnostics.Debug.WriteLine("test");


            this.Name = "there is no cow level";

        }


        #endregion

        #region Public Property
        [Property]
        private string _Name = "";
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
