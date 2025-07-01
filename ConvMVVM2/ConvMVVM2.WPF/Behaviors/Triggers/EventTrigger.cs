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
        private Delegate eventHandler;
        private EventInfo eventInfo;

        /// <summary>
        /// 이벤트 이름을 지정합니다 (예: "Loaded", "Click" 등)
        /// </summary>
        public string EventName { get; set; }

        protected override void OnAttached()
        {
            base.OnAttached();

            if (AssociatedObject == null || string.IsNullOrEmpty(EventName))
                return;

            // UIElement 또는 FrameworkElement만 허용
            var type = AssociatedObject.GetType();
            eventInfo = type.GetEvent(EventName, BindingFlags.Public | BindingFlags.Instance);

            if (eventInfo == null)
                throw new InvalidOperationException($"이벤트 '{EventName}' 를 '{type.Name}' 에서 찾을 수 없습니다.");

            // Delegate 생성
            var handlerType = eventInfo.EventHandlerType;
            var methodInfo = typeof(EventTrigger).GetMethod(nameof(OnEventRaised), BindingFlags.NonPublic | BindingFlags.Instance);

            eventHandler = Delegate.CreateDelegate(handlerType, this, methodInfo);
            eventInfo.AddEventHandler(AssociatedObject, eventHandler);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            if (eventInfo != null && eventHandler != null)
            {
                eventInfo.RemoveEventHandler(AssociatedObject, eventHandler);
            }

            eventInfo = null;
            eventHandler = null;
        }

        // 이 메서드는 모든 이벤트에서 호출될 수 있도록 설계됨 (EventArgs 무시 가능)
        private void OnEventRaised(object sender, EventArgs e)
        {
            InvokeActions(e);
        }
    }
}
