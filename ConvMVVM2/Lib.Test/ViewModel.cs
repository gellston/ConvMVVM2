

using ConvMVVM2.Core.Attributes;
using ConvMVVM2.Core.MVVM;

namespace Lib.Test
{
    public partial class ViewModel : ViewModelBase
    {
        #region Constructor
        public ViewModel() {

        }
        #endregion

        #region Private Property
        [Property]
        private string _Test = "";


        [RelayCommand]
        void Test2()
        {

        }
        #endregion

        #region Event Handler
        partial void OnTestChanged(string oldValue, string newValue)
        {
           
        }
        partial void OnTestChanging(string oldValue, string newValue)
        {
            
        }
        partial void OnTestChanging(string value)
        {
           
        }
        partial void OnTestChanged(string value)
        {
          
        }
        #endregion
    }
}
