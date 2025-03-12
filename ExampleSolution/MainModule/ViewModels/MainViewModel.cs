using ConvMVVM2.Core.Attributes;
using ConvMVVM2.Core.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainModule.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        #region Private Property
        private readonly IRegionManager regionManager;
        #endregion

        #region Constructor
        public MainViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;

        }
        #endregion

        #region Command
        [RelayCommand]
        private void AView()
        {
            try
            {
                this.regionManager.Navigate("MainView", "AView");
            }
            catch
            {

            }
        }

        [RelayCommand]
        private void BView()
        {
            try
            {
                this.regionManager.Navigate("MainView", "BView");
            }
            catch
            {

            }
        }

        [RelayCommand]
        private void CView()
        {
            try
            {
                this.regionManager.Navigate("MainView", "CView");
            }
            catch
            {

            }
        }
        #endregion
    }
}
