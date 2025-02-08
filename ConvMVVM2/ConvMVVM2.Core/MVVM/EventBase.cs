using System;
using System.Collections.Generic;
using System.Text;

namespace ConvMVVM2.Core.MVVM
{

    public enum ThreadOption
    {
        Background,
        Publisher,
        None
    }

    public class EventBase
    {
        #region Private Property
        private ThreadOption threadCallOption = ThreadOption.None;
        #endregion

        #region Consturctor
        public EventBase()
        {

        }
        #endregion

        #region Public Property
        public ThreadOption ThreadOption
        {
            get => threadCallOption;
            protected set => threadCallOption = value;
        }
        #endregion

        
    }
}
