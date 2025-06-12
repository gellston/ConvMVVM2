using System;
using System.Collections.Generic;
using System.Text;

namespace ConvMVVM2.Core.MVVM
{
    public class RegionManager : IRegionManager
    {
        #region Private Property
        private readonly Dictionary<string, IRegion> _regions = new Dictionary<string, IRegion>();
        private readonly Dictionary<string, List<Type>> _layerViews = new Dictionary<string, List<Type>>();
        private readonly Dictionary<string, Type> _layerViewMappings = new Dictionary<string, Type>();
        #endregion

        #region Private Property
        private void Add(string regionName, Type viewType)
        {
            if (!_regions.ContainsKey(regionName))
            {
                throw new InvalidOperationException($"Region not registered: {regionName}");
            }

            if (!_layerViews.ContainsKey(regionName))
            {
                _layerViews[regionName] = new List<Type>();
            }

            if (!_layerViews[regionName].Contains(viewType))
            {
                _layerViews[regionName].Add(viewType);
            }
        }
        #endregion


        #region Public Functions



        public void Hide(string regionName)
        {
            if (_regions.TryGetValue(regionName, out var layer))
            {
                layer.Content = null;
            }
        }

        public void Mapping(string regionName, Type viewType)
        {
            _layerViewMappings[regionName] = viewType;
        }

        public void Mapping<TView>(string regionName) where TView : class
        {
            this.Mapping(regionName, typeof(TView));
        }

        public void Cleanup(string regionName)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"RegionManager.Cleanup called with RegionName: {regionName}");

                if (_regions.ContainsKey(regionName))
                {
                    _regions.Remove(regionName);
                }


            }
            catch
            {
                throw;
            }

        }

        public void Register(string regionName, IRegion region)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"RegionManager.Register called with RegionName: {regionName}");

                if (!_regions.ContainsKey(regionName))
                {
                    _regions[regionName] = region;
                }

                if (!_layerViews.ContainsKey(regionName))
                {
                    _layerViews[regionName] = new List<Type>();
                }

                if (_layerViewMappings.TryGetValue(regionName, out var view))
                {
                    Navigate(regionName, view);
                }

            }
            catch
            {
                throw;
            }

        }

        public void Navigate(string regionName, Type viewType, NavigationContext navigationContext = null)
        {
            try
            {
                if (!_regions.TryGetValue(regionName, out var layer))
                {
                    throw new InvalidOperationException($"Layer not registered: {regionName}");
                }

                if (viewType == null)
                {
                    layer.Content = null;
                    return;
                }

                if (!_layerViews[regionName].Contains(viewType))
                {
                    Add(regionName, viewType);
                }


                var next = ServiceLocator.GetServiceProvider().GetService(viewType);
                if (next == null) return;

                var prev = layer.Content;
                object prevDataContext = null;
                object nextDataContext = null;

                if (prev != null)
                {
                    prevDataContext = prev.GetType().GetProperty("DataContext").GetValue(prev);
                }

                nextDataContext = next.GetType().GetProperty("DataContext").GetValue(next);


                if(navigationContext == null)
                    navigationContext = new NavigationContext();
                

                if (prevDataContext is INavigateAware prevNavigationAware)
                {
                    prevNavigationAware.OnNavigatedFrom(navigationContext);
                }

                if (nextDataContext is INavigateAware nextNavigationAware)
                {
                    if (!nextNavigationAware.CanNavigate(navigationContext)) return;

                    nextNavigationAware.OnNavigatedTo(navigationContext);
                    layer.Content = next;

                    if (_layerViewMappings.ContainsKey(regionName))
                    {
                        _layerViewMappings[regionName] = viewType;
                    }
                }
                else
                {
                    layer.Content = next;

                    if (_layerViewMappings.ContainsKey(regionName))
                    {
                        _layerViewMappings[regionName] = viewType;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public void Navigate<TView>(string regionName) where TView : class
        {
            try
            {
                this.Navigate(regionName, typeof(TView));
            }
            catch
            {
                throw;
            }
        }

        public void Navigate(string regionName, string viewName)
        {
            try
            {
                var viewType = ServiceLocator.GetServiceProvider().KeyType(viewName);
                if (viewType == null)
                {
                    throw new InvalidOperationException($"Can't find view info: {viewName}");
                }

                this.Navigate(regionName, viewType);
            }
            catch
            {
                throw;
            }
        }

        public void Navigate(string regionName, string viewName, NavigationContext context)
        {
            try
            {
                var viewType = ServiceLocator.GetServiceProvider().KeyType(viewName);
                if (viewType == null)
                {
                    throw new InvalidOperationException($"Can't find view info: {viewName}");
                }

                this.Navigate(regionName, viewType, context);
            }
            catch
            {
                throw;
            }
        }

        public void Navigate<TView>(string regionName, NavigationContext context) where TView : class
        {
            try
            {
                this.Navigate(regionName, typeof(TView), context);
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
