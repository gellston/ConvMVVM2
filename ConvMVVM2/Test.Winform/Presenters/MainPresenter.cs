using ConvMVVM2.Core.MVP;
using ConvMVVM2.Winform.MVP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Winform.Views;

namespace Test.Winform.Presenters
{
    public class MainPresenter : Presenter<IMainView>
    {
        #region Private Property

        #endregion

        #region Constructor
        public MainPresenter(IMainView view) : base(view){ 

        }
        #endregion

        #region Virtual Functions
        public override void OnServiceInitialized()
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

        public override void OnViewClosed()
        {
            System.Diagnostics.Debug.WriteLine("test");
        }

        public override void OnViewClosing()
        {
            System.Diagnostics.Debug.WriteLine("test");
        }
        #endregion
    }
}
