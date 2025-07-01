
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using ConvMVVM2.WPF.Behaviors.Base;

/* 'ConvMVVM2.WPF (net9.0-windows)' 프로젝트에서 병합되지 않은 변경 내용
추가됨:
using ConvMVVM2;
using ConvMVVM2.WPF;
using ConvMVVM2.WPF.Behaviors;
using ConvMVVM2.WPF.Behaviors.Behaviors;
*/

namespace ConvMVVM2.WPF.Behaviors.Behaviors
{
    public class SelectedItemsBehavior : Behavior<ListBox>
    {
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.Register(
                nameof(SelectedItems),
                typeof(IList),
                typeof(SelectedItemsBehavior),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public IList SelectedItems
        {
            get => (IList)GetValue(SelectedItemsProperty);
            set => SetValue(SelectedItemsProperty, value);
        }


        protected override void OnAttached()
        {
            base.OnAttached();

            if (AssociatedObject != null)
            {
                AssociatedObject.SelectionChanged += OnSelectionChanged;
                UpdateSelectedItems(); // 초기 반영
            }
        }

        protected override void OnDetaching()
        {
            if (AssociatedObject != null)
            {
                AssociatedObject.SelectionChanged -= OnSelectionChanged;
            }

            base.OnDetaching();
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            UpdateSelectedItems();
        }

        private void UpdateSelectedItems()
        {
            if (AssociatedObject == null)
                return;

            try
            {

                // 항상 새로운 ArrayList로 대체
                SetValue(SelectedItemsProperty, new ArrayList(AssociatedObject.SelectedItems));
            }
            finally
            {

            }
        }
    }
}
