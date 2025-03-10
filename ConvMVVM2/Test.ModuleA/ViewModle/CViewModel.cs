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
        private readonly IDialogService dialogService;
        #endregion

        #region Constructor
        public CViewModel(IDialogService dialogService) { 
        
            this.dialogService = dialogService;
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


                var result = this.dialogService.ShowDialog<DialogResult>("AView", 100, 100);

                System.Diagnostics.Debug.WriteLine("test");
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}
