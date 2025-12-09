using ConvMVVM2.Core.MVP;
using ConvMVVM2.Winform.MVP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.WinformTest.View;

namespace Test.WinformTest.Presenter
{
    public class MainPresenter : Presenter<IMainView>
    {

        #region Private Property
        private readonly IEventAggregator eventAggregator;
        private readonly ILocalizeService localizeService;
        #endregion

        #region Constructor
        public MainPresenter(IMainView view,
                             IEventAggregator eventAggregator,
                             ILocalizeService localizeService) : base(view) { 
            
            this.eventAggregator = eventAggregator;
            //this.localizeService = localizeService;

           // this.localizeService.SetResourceManager(Test.WinformTest.Properties.Resource.ResourceManager);
        }
        #endregion


        #region Overried Functions
        public override void OnServiceInitialized()
        {


            //this.localizeService.ChangeLocal("kr");

            //var test = this.localizeService["구봉회"];

            

            System.Diagnostics.Debug.WriteLine("test");
        }

        public override void OnViewClosed()
        {
            System.Diagnostics.Debug.WriteLine("test");
        }

        public override void Cleanup()
        {
            System.Diagnostics.Debug.WriteLine("test");

        }

        public override void OnViewClosing()
        {

            System.Diagnostics.Debug.WriteLine("test");
        }

        public override void OnViewLoaded()
        {
            System.Diagnostics.Debug.WriteLine("test");
        }

        public override void OnViewShown()
        {
            System.Diagnostics.Debug.WriteLine("test");
        }


        #endregion

        #region Event Handler
        public void Receive(string message) {

        }
        #endregion

    }
}
