using System;
using System.Collections.Generic;
using System.Text;

namespace ConvMVVM2.Core.MVVM
{
    public class LayerManager : ILayerManager
    {
        #region Private Property
        private readonly Dictionary<string, ILayer> _layers = new Dictionary<string, ILayer>();
        private readonly Dictionary<string, List<IView>> _layerViews = new Dictionary<string, List<IView>>();
        private readonly Dictionary<string, IView> _layerViewMappings = new Dictionary<string, IView>();
        #endregion


        #region Public Functions

        public void Add(string layerName, IView view)
        {
            if (!_layers.ContainsKey(layerName))
            {
                throw new InvalidOperationException($"Layer not registered: {layerName}");
            }

            if (!_layerViews.ContainsKey(layerName))
            {
                _layerViews[layerName] = new List<IView>();
            }

            if (!_layerViews[layerName].Contains(view))
            {
                _layerViews[layerName].Add(view);
            }
        }

        public void Hide(string layerName)
        {
            if (_layers.TryGetValue(layerName, out var layer))
            {
                layer.Content = null;
            }
        }

        public void Mapping(string layerName, IView view)
        {
            _layerViewMappings[layerName] = view;
        }

        public void Register(string layerName, ILayer layer)
        {
            System.Diagnostics.Debug.WriteLine($"LayerManager.Register called with layerName: {layerName}");

            if (!_layers.ContainsKey(layerName))
            {
                _layers[layerName] = layer;
                _layerViews[layerName] = new List<IView>();
                if (_layerViewMappings.TryGetValue(layerName, out var view))
                {
                    Show(layerName, view);
                }
            }
        }

        public void Show(string layerName, IView view)
        {
            if (!_layers.TryGetValue(layerName, out var layer))
            {
                throw new InvalidOperationException($"Layer not registered: {layerName}");
            }

            if (view == null)
            {
                layer.Content = null;
                return;
            }

            if (!_layerViews[layerName].Contains(view))
            {
                Add(layerName, view);
            }

            layer.Content = view;
        }
        #endregion
    }
}
