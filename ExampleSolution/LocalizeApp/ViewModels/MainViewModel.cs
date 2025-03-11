using ConvMVVM2.Core.Attributes;
using ConvMVVM2.Core.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalizeApp.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        #region Private Property
        private readonly ILocalizeService localizeService;
        private bool switchLocal = false;
        #endregion

        #region Constructor
        public MainViewModel(ILocalizeService localizeService) { 
        
            this.localizeService = localizeService;
        }
        #endregion

        #region Command
        [RelayCommand]
        private void Test()
        {
            try
            {

                this.switchLocal = !this.switchLocal;

                if (this.switchLocal)
                    this.localizeService.ChangeLocal("en");
                else
                    this.localizeService.ChangeLocal("kr");
               
            }
            catch
            {

            }
        }
        #endregion


    }
}
