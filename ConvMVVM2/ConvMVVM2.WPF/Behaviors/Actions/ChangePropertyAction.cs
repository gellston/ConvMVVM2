using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ConvMVVM2.WPF.Behaviors.Base;
using TriggerAction = ConvMVVM2.WPF.Behaviors.Base.TriggerAction;

namespace ConvMVVM2.WPF.Behaviors.Actions
{
    public class ChangePropertyAction : TriggerAction<DependencyObject>
    {
        #region Dependency Property
        public static readonly DependencyProperty PropertyNameProperty =  DependencyProperty.Register(nameof(PropertyName), typeof(string), typeof(ChangePropertyAction), new PropertyMetadata(null));

        public static readonly DependencyProperty ValueProperty =  DependencyProperty.Register(nameof(Value), typeof(object), typeof(ChangePropertyAction), new PropertyMetadata(null));

        public string PropertyName
        {
            get => (string)GetValue(PropertyNameProperty);
            set => SetValue(PropertyNameProperty, value);
        }

        public object Value
        {
            get => GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }
        #endregion

        #region Public Functions
        protected override void Invoke(object parameter)
        {
            if (string.IsNullOrWhiteSpace(PropertyName) || AssociatedObject == null)
                return;

            var propInfo = AssociatedObject.GetType().GetProperty(PropertyName,BindingFlags.Instance | BindingFlags.Public);

            if (propInfo == null || !propInfo.CanWrite)
                throw new InvalidOperationException($"'{PropertyName}' 속성이 {AssociatedObject.GetType().Name}에 없거나 쓰기 불가능합니다.");

            try
            {
                var targetType = propInfo.PropertyType;
                var convertedValue = Convert.ChangeType(Value, targetType);
                propInfo.SetValue(AssociatedObject, convertedValue);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"속성 '{PropertyName}'에 값을 설정하는 중 오류: {ex.Message}", ex);
            }
        }
        #endregion

    }
}
