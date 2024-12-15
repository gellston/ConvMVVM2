using ConvMVVM2.Core.Attributes;
using ConvMVVM2.Core.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.WPFModule.Local.ViewModels
{
    public partial class AViewModel : ViewModelBase
    {

        #region Private Property
        private readonly ILayerManager layerManager;
        #endregion

        #region Constructor
        public AViewModel(ILayerManager layerManager)
        {
            this.layerManager = layerManager;
            
        }
        #endregion

        #region Public Property
        [Property]
        private string _Test = "";
        #endregion
    }
}
