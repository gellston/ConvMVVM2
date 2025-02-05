using ConvMVVM2.Core.MVVM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace ConvMVVM2.WPF.Controls
{
    public class WPFRegion : ContentControl, IRegion
    {

        #region Private Property
        private bool _isRegistered = false;
        #endregion

        #region Dependency Property
        public static readonly DependencyProperty RegionNameProperty =
        DependencyProperty.Register(nameof(RegionName), typeof(string), typeof(WPFRegion), new PropertyMetadata(null, OnRegionNameChanged));

        public string RegionName
        {
            get => (string)GetValue(RegionNameProperty);
            set => SetValue(RegionNameProperty, value);
        }
        #endregion

        #region Static Constructor
        static WPFRegion()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WPFRegion), new FrameworkPropertyMetadata(typeof(WPFRegion)));
        }
        #endregion

        #region Constructor

        public WPFRegion()
        {
            Loaded += WPFLayer_Loaded;
        }
        #endregion


        #region Event Handler
        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);
        }

        private void WPFLayer_Loaded(object sender, RoutedEventArgs e)
        {
            RegisterToRegionManager();
        }


        private static void OnRegionNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                if (d is WPFRegion layer)
                {
                    layer._isRegistered = false;
                    layer.RegisterToRegionManager();
                }
            }
            catch
            {

            }
        }
        #endregion


        #region Private Functions

        private void RegisterToRegionManager()
        {
            if (string.IsNullOrEmpty(RegionName) || _isRegistered)
            {
                return;
            }

            if (DesignerProperties.GetIsInDesignMode(this)) return;

            var layerManager = ServiceLocator.GetServiceProvider().GetService<IRegionManager>();
            if (layerManager != null)
            {
                layerManager.Register(RegionName, this);
                _isRegistered = true;
            }
        }
        #endregion

    }
}
