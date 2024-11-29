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
        private readonly IContainer _container;
        private readonly IViewModelMapper _viewModelMapper;
        #endregion

        #region Constructor

        public DefaultViewModelInitializer(IContainer container, IViewModelMapper viewModelMapper)
        {
            _container = container;
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
                var constructor = viewModelType.GetConstructors()
                    .OrderByDescending(c => c.GetParameters().Length)
                    .First();

                var parameters = constructor.GetParameters()
                    .Select(p => _container.Resolve(p.ParameterType))
                    .ToArray();

                return constructor.Invoke(parameters);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to create ViewModel of type {viewModelType.Name}.", ex);
            }
        }
        #endregion

    }
}
