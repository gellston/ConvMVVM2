using ConvMVVM2.Core.Attributes;
using ConvMVVM2.Core.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Test.ViewModel
{
    public partial class MainContentViewModel : ViewModelBase
    {

        #region Private Property
        private readonly IContainer container;
        private readonly ILocalizeService localizeService;

        private bool trigger = false;
        #endregion

        #region Constructor
        public MainContentViewModel(IContainer container,
                                    ILocalizeService localizeService) { 
            this.container = container;
            this.localizeService = localizeService;
        }
        #endregion

        #region Property
        [Property]
        private string _Name = "";


        #endregion

        #region Command


        [RelayCommand]
        private void Test()
        {
            try
            {

                if (this.trigger)
                {
                    this.localizeService.ChangeLocal("kr");
                }
                else
                {

                    this.localizeService.ChangeLocal("en");
                }

                this.trigger = !this.trigger;

            }catch(Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }
        }
        #endregion


        #region Event Handler
        public void Loaded()
        {

            //System.Diagnostics.Debug.WriteLine("test");
            
        }
        #endregion
    }
}
