using ConvMVVM2.Core.Attributes;
using ConvMVVM2.Core.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootStrapperApp.ViewModels
{
    public partial class SubViewModel : ViewModelBase
    {
        #region Private Property
        private readonly IRegionManager regionManager;
        #endregion

        #region Constructor
        public SubViewModel(IRegionManager regionManager) { 
            this.regionManager = regionManager;
        }
        #endregion

        #region Command
        [RelayCommand]
        private void Test()
        {
            try
            {
                this.regionManager.Navigate("MainView", "MainView");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
        #endregion
    }
}
