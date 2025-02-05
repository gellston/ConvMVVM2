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


        #region Public Functions

        public void Add(string regionName, Type viewType)
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

        public void Register(string regionName, IRegion region)
        {
            System.Diagnostics.Debug.WriteLine($"RegionManager.Register called with RegionName: {regionName}");

            if (!_regions.ContainsKey(regionName))
            {
                _regions[regionName] = region;
                _layerViews[regionName] = new List<Type>();
                if (_layerViewMappings.TryGetValue(regionName, out var view))
                {
                    Show(regionName, view);
                }
            }
        }

        public void Show(string layerName, Type viewType)
        {
            if (!_regions.TryGetValue(layerName, out var layer))
            {
                throw new InvalidOperationException($"Layer not registered: {layerName}");
            }

            if (viewType == null)
            {
                layer.Content = null;
                return;
            }

            if (!_layerViews[layerName].Contains(viewType))
            {
                Add(layerName, viewType);
            }

            var view = ServiceLocator.GetServiceProvider().GetService(viewType);

            if (view != null)
            {
                layer.Content = view;
            }

            
        }
        #endregion
    }
}
