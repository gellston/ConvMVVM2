using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ConvMVVM2.Core.MVVM
{
    public class DefaultViewModelInitializer : IViewModelInitializer
    {
        #region Private Property
        private readonly IServiceProvider _serviceProvider;
        private readonly IViewModelMapper _viewModelMapper;
        #endregion

        #region Constructor

        public DefaultViewModelInitializer(IServiceProvider serviceProvider, IViewModelMapper viewModelMapper)
        {
            _serviceProvider = serviceProvider;
            _viewModelMapper = viewModelMapper;
        }
        #endregion

        #region Public Functions

        public void InitializeViewModel(IView view)
        {
            var viewType = view.GetType();
            var viewModelType = _viewModelMapper.GetViewModelType(viewType);

            if (viewModelType != null)
            {
                var viewModel = CreateViewModel(viewModelType);
                view.DataContext = viewModel;
            }
        }

        private object CreateViewModel(Type viewModelType)
        {
            try
            {
                var serviceProvider = _serviceProvider.GetService(viewModelType);

                return serviceProvider;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to create ViewModel of type {viewModelType.Name}.", ex);
            }
        }
        #endregion

    }
}
