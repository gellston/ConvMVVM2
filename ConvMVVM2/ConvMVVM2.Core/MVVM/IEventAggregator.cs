using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ConvMVVM2.Core.MVVM
{
    public interface IEventAggregator
    {
        #region Public Functions
        public PubSubEvent<TDataType> GetEvent<TDataType>();
        public void Cleanup();
        public void Cleanup<TDataType>();
        #endregion

    }
}
