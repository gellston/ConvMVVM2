using ConvMVVM2.Core.Attributes;
using ConvMVVM2.Core.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.WPF.ViewModel
{
    public partial class AViewModel : ViewModelBase, IViewLoadable, IServiceInitializable, INavigateAware
    {
        #region Private Property
        private readonly IRegionManager regionManager;
        #endregion

        #region Constructor
        public AViewModel(IRegionManager regionManager)
        {
            System.Diagnostics.Debug.WriteLine("test");
            this.regionManager = regionManager;


            
            
        }


        #endregion

        #region Command
        [RelayCommand]
        public void Test()
        {
            try
            {
                this.regionManager.Navigate("AView", "BView");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.ToString());
            }

        }
        #endregion

        #region Event Handler
        public void OnViewLoaded()
        {

        }

        public void OnServiceInitialized()
        {

        }

        public bool CanNavigate(NavigationContext context)
        {
            return true;
        }

        public void OnNavigatedTo(NavigationContext context)
        {
            System.Diagnostics.Debug.WriteLine("test");
        }

        public void OnNavigatedFrom(NavigationContext context)
        {
            System.Diagnostics.Debug.WriteLine("test");
        }
        #endregion
    }
}
