using ConvMVVM2.Core.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ConvMVVM2.WPF
{
    public class WPFLayer : ContentControl, ILayer
    {



        #region Private Property
        private bool _isRegistered = false;
        #endregion



        #region Dependency Property
        public static readonly DependencyProperty LayerNameProperty =
        DependencyProperty.Register(nameof(LayerName), typeof(string), typeof(WPFLayer), new PropertyMetadata(null, OnLayerNameChanged));

        public string LayerName
        {
            get => (string)GetValue(LayerNameProperty);
            set => SetValue(LayerNameProperty, value);
        }
        #endregion

        #region Static Constructor
        static WPFLayer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WPFLayer), new FrameworkPropertyMetadata(typeof(WPFLayer)));
        }
        #endregion

        #region Constructor

        public WPFLayer()
        {
            Loaded += UnoLayer_Loaded;
        }
        #endregion


        #region Event Handler
        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);
        }

        private void UnoLayer_Loaded(object sender, RoutedEventArgs e)
        {
            RegisterToLayerManager();
        }


        private static void OnLayerNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                if (d is WPFLayer layer)
                {
                    layer._isRegistered = false;
                    layer.RegisterToLayerManager();
                }
            }
            catch
            {

            }
        }
        #endregion


        #region Private Functions

        private void RegisterToLayerManager()
        {
            if (string.IsNullOrEmpty(LayerName) || _isRegistered)
            {
                return;
            }

            var layerManager = ContainerProvider.GetContainer().Resolve<ILayerManager>();
            if (layerManager != null)
            {
                layerManager.Register(LayerName, this);
                _isRegistered = true;
            }
        }
        #endregion

    }
}
