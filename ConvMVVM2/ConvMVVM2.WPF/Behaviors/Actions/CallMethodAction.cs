using ConvMVVM2.WPF.Behaviors.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TriggerAction = ConvMVVM2.WPF.Behaviors.Base.TriggerAction;


namespace ConvMVVM2.WPF.Behaviors.Actions
{
    public class CallMethodAction : TriggerAction<DependencyObject>
    {
        #region Public Property
        public string MethodName { get; set; }
        #endregion


        #region Dependecy Property
        public static readonly DependencyProperty TargetObjectProperty =  DependencyProperty.Register(nameof(TargetObject), typeof(object), typeof(CallMethodAction), new PropertyMetadata(null));
        public object TargetObject
        {
            get => GetValue(TargetObjectProperty);
            set => SetValue(TargetObjectProperty, value);
        }
        #endregion

        #region Public Function

        protected override void Invoke(object parameter)
        {
            if (string.IsNullOrEmpty(MethodName))
                return;

            object target = TargetObject ?? AssociatedObject;

            MethodInfo method = target?.GetType().GetMethod(MethodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            if (method == null)
            {
                throw new InvalidOperationException($"Method '{MethodName}' not found on {target?.GetType().Name}.");
            }

            method.Invoke(target, method.GetParameters().Length == 0 ? null : new[] { parameter });
        }
        #endregion


    }
}
