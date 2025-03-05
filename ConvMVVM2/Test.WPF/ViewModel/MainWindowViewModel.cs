using ConvMVVM2.Core.Attributes;
using ConvMVVM2.Core.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.WPF.ViewModel
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        #region Private Property
        private readonly IRegionManager regionManager;
        #endregion

        #region Constructor
        public MainWindowViewModel(IRegionManager regionManager) { 
        
            this.regionManager = regionManager;
        }
        #endregion


        #region Command
        [RelayCommand]
        private void AView()
        {
            try
            {

                this.regionManager.Navigate("AView", "AView");

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }


        [RelayCommand]
        private void BView()
        {
            try
            {

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
        #endregion
    }
}
