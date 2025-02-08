using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ConvMVVM2.Core.MVVM
{
    public interface IEventAggregator
    {
        public PubSubEvent<TDataType> GetEvent<TDataType>();
    }
}
