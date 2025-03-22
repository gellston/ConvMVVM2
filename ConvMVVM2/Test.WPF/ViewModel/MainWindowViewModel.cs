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

        #region Public Property

        [Property]
        [PropertyChangedFor("Test2")]
        [PropertyChangedFor("Test3")]
        private string _Test = "test";

        [Property]
        private string _Test2 = "test";

        public string Test3 { get; set; } = "";

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

                this._Test2 = "there is no cow level!@#!#@";
                this.Test3 = "there is no cow level";
                this.Test = "there is no cow  level";

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
        #endregion
    }
}
