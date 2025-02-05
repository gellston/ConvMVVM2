using System;
using System.Collections.Generic;
using System.Text;

namespace ConvMVVM2.Core.MVVM
{
    public class ViewModelMapper : IViewModelMapper
    {
        #region Private Property
        private readonly Dictionary<Type, Type> _mappings = new Dictionary<Type, Type>();
        #endregion


        #region Public Functions

        public void Register<TView, TViewModel>() where TView : class where TViewModel : class
        {
            _mappings[typeof(TView)] = typeof(TViewModel);
        }

        public Type GetViewModelType(Type viewType)
        {
            return _mappings.TryGetValue(viewType, out var viewModelType) ? viewModelType : null;
        }
        #endregion
    }

}
