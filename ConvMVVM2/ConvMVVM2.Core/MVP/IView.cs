using ConvMVVM2.Core.MVP;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConvMVVM2.Core.MVP
{
    public interface IView
    {
        #region Public Property
        public IPresenter Presenter { get; set; }
        #endregion


        #region Event Handler
        public event Action OnViewLoadedEvent;
        public event Action OnViewShownEvent;
        public event Action OnViewClosingEvent;
        public event Action OnViewClosedEvent;
        #endregion
    }
}
