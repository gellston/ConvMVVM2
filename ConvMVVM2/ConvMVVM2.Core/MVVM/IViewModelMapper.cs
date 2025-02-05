using System;

namespace ConvMVVM2.Core.MVVM
{
    public interface IViewModelMapper
    {
        #region Public Functions
        void Register<TView, TViewModel>() where TView : class where TViewModel : class;
        Type GetViewModelType(Type viewType);
        #endregion
    }
}
