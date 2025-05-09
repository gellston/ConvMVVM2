﻿using ConvMVVM2.Core.Attributes;
using ConvMVVM2.Core.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.WPF.ViewModel
{
    public partial class AViewModel : ViewModelBase, IViewLoadable, IServiceInitializable, INavigateAware, IRenderer, IViewInitializable, IDialogViewModel<DialogResult>
    {
        #region Private Property
        private readonly IRegionManager regionManager;
        private readonly IEventAggregator eventAggregator;


        #endregion

        #region Constructor
        public AViewModel(IRegionManager regionManager,
                          IEventAggregator eventAggregator)
        {
            System.Diagnostics.Debug.WriteLine("test");
            this.regionManager = regionManager;
            this.eventAggregator = eventAggregator;


            this.eventAggregator.GetEvent<string>().Subscribe(this.Test, ThreadOption.Background);
            this.eventAggregator.GetEvent<string>().Subscribe(this.Test, ThreadOption.Background);
            this.eventAggregator.GetEvent<string>().Subscribe(this.Test, ThreadOption.Background);



            this.eventAggregator.GetEvent<string>().Unsubscribe(this.Test);
        }


        #endregion

        #region Public Property


        [Property]
        [PropertyChangedFor("test")]
        private string _Name = "";
        #endregion

        #region Command
        [RelayCommand]
        public void Test()
        {
            try
            {
                var result = new DialogResult()
                {
                    Result = "OK"
                };
                this.CloseEvent(result);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.ToString());
            }

        }
        #endregion

        #region Event
        public event Action<DialogResult> CloseEvent;
        #endregion

        #region Event Handler

        public void Test(string data)
        {


            System.Diagnostics.Debug.WriteLine($"Test {data}");
        }

        public void OnViewLoaded()
        {

        }

        public void OnRendering()
        {
        }

        public void OnServiceInitialized()
        {
            System.Diagnostics.Debug.WriteLine("test");
        }

        public void OnViewInitialized()
        {
            System.Diagnostics.Debug.WriteLine("test");
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
