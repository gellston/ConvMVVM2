using ConvMVVM2.Core.Attributes;
using ConvMVVM2.Core.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.WPF.ViewModel
{
    public partial class BViewModel : ViewModelBase, INavigateAware
    {

        #region Private Property
        private readonly IEventAggregator eventAggregator;
        #endregion

        #region Constructor
        public BViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            this.eventAggregator.GetEvent<string>().Subscribe(this.Test, ThreadOption.Background);
        }


        #endregion

        [RelayCommand]
        public void Test()
        {
            try
            {
                this.eventAggregator.GetEvent<string>().Publish("there is no cow level");

            }
            catch (Exception ex)
            {

            }
        }

        public void Test(string data)
        {


            System.Diagnostics.Debug.WriteLine($"Test {data}");
        }


        public bool CanNavigate(NavigationContext context)
        {


            
            return true;
        }

        public void OnNavigatedFrom(NavigationContext context)
        {
            System.Diagnostics.Debug.WriteLine("test");
        }

        public void OnNavigatedTo(NavigationContext context)
        {
            System.Diagnostics.Debug.WriteLine("test");
        }
    }
}
