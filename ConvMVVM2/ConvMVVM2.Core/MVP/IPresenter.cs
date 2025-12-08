using ConvMVVM2.Core.MVP;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConvMVVM2.Core.MVP
{
    public interface IPresenter : IDisposable, IServiceInitializable, IViewLoaded, IViewShown, IViewClosing, IViewClosed
    {

        IView View { get; }
    }
}
