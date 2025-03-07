using ConvMVVM2.Core.Attributes;
using ConvMVVM2.Core.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.ModuleA.ViewModle
{
    public partial class CViewModel : ViewModelBase
    {
        #region Private Property
        private readonly IRegionManager regionManager;

        #endregion

        #region Constructor
        public CViewModel() { 
        
        }
        #endregion

        #region Public Property
        [Property]
        private string _TestC = "";
        #endregion

        #region Command


        [RelayCommand]
        private void Test()
        {
            try
            {
                this.TestC = "There is no cow level";

            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}
