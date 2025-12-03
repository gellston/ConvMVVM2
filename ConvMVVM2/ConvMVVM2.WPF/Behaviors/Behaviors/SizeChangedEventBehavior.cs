using ConvMVVM2.WPF.Behaviors.Base;
using ConvMVVM2.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConvMVVM2.WPF.Behaviors.Behaviors
{
    public class SizeChangedEventBehavior : Behavior<FrameworkElement>
    {
        #region Protected Functions
        protected override void OnAttached()
        {
            AssociatedObject.SizeChanged += AssociatedObject_SizeChanged;
            AssociatedObject.Loaded += AssociatedObject_Loaded;
        }



        protected override void OnDetaching()
        {
            AssociatedObject.SizeChanged -= AssociatedObject_SizeChanged;
            AssociatedObject.Loaded -= AssociatedObject_Loaded;
        }
        #endregion

        #region Dependency Property
        public static DependencyProperty SizeObserverViewModelPropewrty = DependencyProperty.Register("SizeObserverViewModel", typeof(SizeObserverViewModel), typeof(SizeChangedEventBehavior));
        public SizeObserverViewModel SizeObserverViewModel
        {
            get => (SizeObserverViewModel)GetValue(SizeObserverViewModelPropewrty);
            set => SetValue(SizeObserverViewModelPropewrty, value);
        }
        #endregion

        #region Event Handler
        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            if (SizeObserverViewModel == null) return;

            SizeObserverViewModel.SizeChanged(new Size(this.AssociatedObject.Width, this.AssociatedObject.Height));
        }


        private void AssociatedObject_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            if (SizeObserverViewModel == null) return;

            SizeObserverViewModel.SizeChanged(e.NewSize);
        }
        #endregion
    }
}
