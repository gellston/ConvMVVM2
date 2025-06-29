using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using ConvMVVM2.WPF.Behaviors.Base;
using TriggerAction = ConvMVVM2.WPF.Behaviors.Base.TriggerAction;

namespace ConvMVVM2.WPF.Behaviors.Actions
{
    public class GoToStateAction : TriggerAction
    {
        #region Dependency Property
        public static readonly DependencyProperty StateNameProperty = DependencyProperty.Register(nameof(StateName), typeof(string), typeof(GoToStateAction), new PropertyMetadata(null));

        public static readonly DependencyProperty TargetNameProperty = DependencyProperty.Register(nameof(TargetName), typeof(string), typeof(GoToStateAction), new PropertyMetadata(null));

        public string StateName
        {
            get => (string)GetValue(StateNameProperty);
            set => SetValue(StateNameProperty, value);
        }

        public string TargetName
        {
            get => (string)GetValue(TargetNameProperty);
            set => SetValue(TargetNameProperty, value);
        }
        #endregion

        #region Public Functions

        public override void Invoke(object parameter)
        {
            if (string.IsNullOrWhiteSpace(StateName) || AssociatedObject == null)
                return;

            FrameworkElement element = AssociatedObject as FrameworkElement;

            if (!string.IsNullOrEmpty(TargetName))
            {
                element = FindChildByName(AssociatedObject, TargetName);
            }

            if (element != null)
            {
                VisualStateManager.GoToState(element, StateName, true);
            }
        }

        private FrameworkElement FindChildByName(DependencyObject parent, string name)
        {
            if (parent is FrameworkElement fe && fe.Name == name)
                return fe;

            int count = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < count; i++)
            {
                var result = FindChildByName(VisualTreeHelper.GetChild(parent, i), name);
                if (result != null)
                    return result;
            }

            return null;
        }
        #endregion

        #region Protected Functions
        protected override Freezable CreateInstanceCore()
        {
            return new GoToStateAction();
        }
        #endregion
    }
}
