using ConvMVVM2.Core.MVVM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConvMVVM2.WPF.Extensions
{
    public class ViewModelLocator
    {
        #region Dependency Property
        public static readonly DependencyProperty AutoWireViewModelProperty = DependencyProperty.RegisterAttached("AutoWireViewModel",typeof(bool),typeof(ViewModelLocator), new PropertyMetadata(false, OnAutoWireViewModelChanged));

        public static bool GetAutoWireViewModel(DependencyObject d)
        {
            return (bool)d.GetValue(AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(DependencyObject d, bool value)
        {
            d.SetValue(AutoWireViewModelProperty, value);
        }




        public static readonly DependencyProperty UseViewModelMapperProperty = DependencyProperty.RegisterAttached("UseViewModelMapper", typeof(bool), typeof(ViewModelLocator), new PropertyMetadata(false));

        public static bool GetUseViewModelMapper(DependencyObject d)
        {
            return (bool)d.GetValue(UseViewModelMapperProperty);
        }

        public static void SetUseViewModelMapper(DependencyObject d, bool value)
        {
            d.SetValue(UseViewModelMapperProperty, value);
        }



        public static readonly DependencyProperty UseNamePatternMapperProperty = DependencyProperty.RegisterAttached("UseNamePatternMapper", typeof(bool), typeof(ViewModelLocator), new PropertyMetadata(false));

        public static bool GetUseNamePatternMapper(DependencyObject d)
        {
            return (bool)d.GetValue(UseNamePatternMapperProperty);
        }

        public static void SetUseNamePatternMapper(DependencyObject d, bool value)
        {
            d.SetValue(UseNamePatternMapperProperty, value);
        }




        public static readonly DependencyProperty ViewModelNameProperty = DependencyProperty.RegisterAttached("ViewModelName", typeof(string), typeof(ViewModelLocator), new PropertyMetadata(""));

        public static string GetViewModelName(DependencyObject d)
        {
            return (string)d.GetValue(ViewModelNameProperty);
        }

        public static void SetViewModelName(DependencyObject d, string value)
        {
            d.SetValue(ViewModelNameProperty, value);
        }
        #endregion




        #region Event Handler
        private static void OnAutoWireViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FrameworkElement frameworkElement && (bool)e.NewValue)
            {

                frameworkElement.Initialized -= FrameworkElement_Initialized;
                frameworkElement.Initialized += FrameworkElement_Initialized;

                frameworkElement.Loaded -= FrameworkElement_Loaded;
                frameworkElement.Loaded += FrameworkElement_Loaded;
            }
      
        }

        private static void FrameworkElement_Loaded(object sender, RoutedEventArgs e)
        {
            if (!(sender is FrameworkElement frameworkElement))
                return;

            if (DesignerProperties.GetIsInDesignMode(frameworkElement)) return;

            if (frameworkElement.DataContext == null) return;

            if (frameworkElement.DataContext is IViewLoadable loadable)
            {
                loadable.OnViewLoaded();
            }
        }

        private static void FrameworkElement_Initialized(object sender, EventArgs e)
        {
            if (!(sender is FrameworkElement frameworkElement))
                return;



            if (DesignerProperties.GetIsInDesignMode(frameworkElement)) return;


            var useViewModelMapper = GetUseViewModelMapper(frameworkElement);
            var useNamePatternMapper = GetUseNamePatternMapper(frameworkElement);
            var dpViewModelName = GetViewModelName(frameworkElement);


            if (useViewModelMapper == true)
            {
                // 특정 인터페이스를 구현하는지 확인
                var viewModelMapper = ServiceLocator.GetServiceProvider().GetService<IViewModelMapper>();
                var viewModelType = viewModelMapper.GetViewModelType(sender.GetType());
                frameworkElement.DataContext = ServiceLocator.GetServiceProvider().GetService(viewModelType);
            }


            if (useNamePatternMapper == true)
            {
                // 특정 인터페이스를 구현하는지 확인
                var viewName = sender.GetType().Name;
                var viewModelName = viewName.Replace("View", "ViewModel");

                frameworkElement.DataContext = ServiceLocator.GetServiceProvider().GetService(viewModelName);
            }


            if (dpViewModelName != "")
            {
                frameworkElement.DataContext = ServiceLocator.GetServiceProvider().GetService(dpViewModelName);
            }


            if (frameworkElement.DataContext is IViewInitializable initializer)
            {
                initializer.OnViewInitialized();
            }

            frameworkElement.Initialized -= FrameworkElement_Initialized;
        }


        #endregion


    }
}
