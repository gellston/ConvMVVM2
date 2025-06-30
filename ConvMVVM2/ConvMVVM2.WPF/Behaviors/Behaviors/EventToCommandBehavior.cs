using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
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
    public class EventToCommandBehavior : Behavior<FrameworkElement>
    {

        #region Private Property
        Delegate eventHandler;
        #endregion

        #region Dependecy Property
        public static readonly DependencyProperty EventNameProperty = DependencyProperty.Register("EventName", typeof(string), typeof(EventToCommandBehavior), new PropertyMetadata("", OnEventNameChanged));
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(EventToCommandBehavior), new PropertyMetadata(null));
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(EventToCommandBehavior), new PropertyMetadata(null));
        public static readonly DependencyProperty InputConverterProperty = DependencyProperty.Register("Converter", typeof(IValueConverter), typeof(EventToCommandBehavior), new PropertyMetadata(null));
        #endregion



        #region Public Property
        public string EventName
        {
            get => (string)GetValue(EventNameProperty);
            set => SetValue(EventNameProperty, value);
        }


        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public IValueConverter Converter
        {
            get => (IValueConverter)GetValue(InputConverterProperty);
            set => SetValue(InputConverterProperty, value);
        }

        #endregion



        #region Event Handler
        protected override void OnAttached()
        {

            RegisterEvent(EventName);

        }


        protected override void OnDetaching()
        {
            DeregisterEvent(EventName);
        }


        private static void OnEventNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var behavior = (EventToCommandBehavior)d;
            if (behavior.AssociatedObject == null)
            {
                return;
            }



            string oldEventName = (string)e.OldValue;
            string newEventName = (string)e.NewValue;

            behavior.DeregisterEvent(oldEventName);
            behavior.RegisterEvent(newEventName);
        }

        #endregion

        #region Public Functions
        public void RegisterEvent(string eventName)
        {
            if (string.IsNullOrWhiteSpace(eventName))
            {
                return;
            }

            EventInfo eventInfo = AssociatedObject.GetType().GetRuntimeEvent(eventName);
            if (eventInfo == null)
            {
                throw new ArgumentException("Invalid event name : " + eventName);
            }

            MethodInfo methodInfo = typeof(EventToCommandBehavior).GetTypeInfo().GetDeclaredMethod("OnEvent");
            eventHandler = methodInfo.CreateDelegate(eventInfo.EventHandlerType, this);
            eventInfo.AddEventHandler(AssociatedObject, eventHandler);

        }

        public void DeregisterEvent(string eventName)
        {
            if (string.IsNullOrWhiteSpace(eventName))
            {
                return;
            }

            if (eventHandler == null)
            {
                return;
            }

            EventInfo eventInfo = AssociatedObject.GetType().GetRuntimeEvent(eventName);
            if (eventInfo == null)
            {
                throw new ArgumentException("Invalid event name : " + eventName);
            }
            eventInfo.RemoveEventHandler(AssociatedObject, eventHandler);
            eventHandler = null;
        }

        public void OnEvent(object sender, object eventArgs)
        {
            if (Command == null)
            {
                return;
            }

            object resolvedParameter = null;

            if (CommandParameter != null)
            {
                resolvedParameter = CommandParameter;
            }
            else if (Converter != null)
            {
                resolvedParameter = Converter.Convert(eventArgs, typeof(object), null, null);
            }
            else
            {
                resolvedParameter = eventArgs;
            }

            if (Command.CanExecute(resolvedParameter))
            {
                Command.Execute(resolvedParameter);
            }

        }
        #endregion

    }
}
