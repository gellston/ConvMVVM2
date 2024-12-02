using ConvMVVM2.Core.Attributes;
using ConvMVVM2.Core.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Test.ViewModel
{
    public partial class MainContentViewModel : ViewModelBase, IViewLoadable
    {

        #region Private Property
        private readonly IContainer container;
        #endregion

        #region Constructor
        public MainContentViewModel(IContainer container) { 
            this.container = container;
        }
        #endregion

        #region Property
        [Property]
        private string _Name = "";


        #endregion


        #region Event Handler
        public void Loaded()
        {

            System.Diagnostics.Debug.WriteLine("test");
        }
        #endregion
    }
}
