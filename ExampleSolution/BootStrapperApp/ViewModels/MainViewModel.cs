using ConvMVVM2.Core.Attributes;
using ConvMVVM2.Core.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootStrapperApp.ViewModel
{
    public partial class MainViewModel : ViewModelBase
    {
        #region Private Property
        private readonly IRegionManager regionManager;
        #endregion

        #region Constructor
        public MainViewModel(IRegionManager regionManager) { 

            this.regionManager = regionManager;

            
        }
        #endregion

        #region Public Property
        [Property]
        private string _TestText = "test";
        #endregion

        #region Event Handler
        partial void OnTestTextChanged(string oldValue, string newValue)
        {
            
        }
        partial void OnTestTextChanged(string value)
        {
           
        }
        partial void OnTestTextChanging(string oldValue, string newValue)
        {
  
        }
        partial void OnTestTextChanging(string value)
        {

        }
        #endregion

        #region Command

        [RelayCommand]
        private void Test()
        {
            try
            {
                this.regionManager.Navigate("MainView", "SubView");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        [RelayCommand]
        private void Change()
        {
            this.TestText = "there is no cow level";
        }
        #endregion
    }
}
