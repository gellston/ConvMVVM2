using ConvMVVM2.WPF.Behaviors.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConvMVVM2.WPF.Behaviors.Triggers
{
    public class EventTrigger : TriggerBase<DependencyObject>
    {
        #region Public Property
        public string EventName { get; set; }
        #endregion

        #region Private Property
        private EventInfo _eventInfo;
        private Delegate _handler;
        #endregion

        #region Protected Functions

        protected override void OnAttached()
        {
            if (AssociatedObject == null || string.IsNullOrEmpty(EventName))
                return;

            var type = AssociatedObject.GetType();
            _eventInfo = type.GetEvent(EventName, BindingFlags.Instance | BindingFlags.Public);
            if (_eventInfo == null)
                throw new InvalidOperationException($"Event '{EventName}' not found on {type.Name}.");

            var methodInfo = typeof(EventTrigger).GetMethod(nameof(OnEventRaised), BindingFlags.Instance | BindingFlags.NonPublic);
            _handler = Delegate.CreateDelegate(_eventInfo.EventHandlerType, this, methodInfo);
            _eventInfo.AddEventHandler(AssociatedObject, _handler);
        }

        protected override void OnDetaching()
        {
            if (_eventInfo != null && _handler != null && AssociatedObject != null)
            {
                _eventInfo.RemoveEventHandler(AssociatedObject, _handler);
            }
        }
        #endregion

        #region Private Functions

        private void OnEventRaised(object sender, EventArgs e)
        {
            InvokeActions(e);
        }
        #endregion
    }
}
