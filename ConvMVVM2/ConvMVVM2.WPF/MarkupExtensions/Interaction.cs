using ConvMVVM2.WPF.Behaviors.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConvMVVM2.WPF.MarkupExtensions
{
    public static class Interaction
    {
        #region Attached Property
        public static readonly DependencyProperty BehaviorsProperty = DependencyProperty.RegisterAttached("Behaviors", typeof(BehaviorCollection), typeof(Interaction), new PropertyMetadata(null, OnBehaviorsChanged));

        public static void SetBehaviors(DependencyObject element, BehaviorCollection value)
        {
            element.SetValue(BehaviorsProperty, value);
        }

        public static BehaviorCollection GetBehaviors(DependencyObject element)
        {

            if (element.GetValue(BehaviorsProperty) is BehaviorCollection collection)
            {
                return collection;
            }

            collection = new BehaviorCollection();
            SetBehaviors(element, collection);
            return collection;
        }

        private static void OnBehaviorsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue is BehaviorCollection oldBehaviors)
            {
                foreach (var behavior in oldBehaviors)
                {
                    behavior.Detach();
                }
                
            }
                
            if (e.NewValue is BehaviorCollection newBehaviors)
            {
                foreach (var behavior in newBehaviors)
                {
                    behavior.Attach(d);
                }

            }
              
        }





        public static readonly DependencyProperty TriggersProperty = DependencyProperty.RegisterAttached("Triggers",typeof(Behaviors.Base.TriggerCollection),typeof(Interaction),new PropertyMetadata(null, OnTriggersChanged));

        public static void SetTriggers(DependencyObject obj, Behaviors.Base.TriggerCollection value)
        {
            obj.SetValue(TriggersProperty, value);
        }

        public static Behaviors.Base.TriggerCollection GetTriggers(DependencyObject obj)
        {
            var collection = (Behaviors.Base.TriggerCollection)obj.GetValue(TriggersProperty);
            if (collection == null)
            {
                collection = new Behaviors.Base.TriggerCollection();
                SetTriggers(obj, collection);
            }
            return collection;
        }

        private static void OnTriggersChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue is Behaviors.Base.TriggerCollection oldCollection)
            {
                foreach (var trigger in oldCollection)
                    trigger.Detach();
            }

            if (e.NewValue is Behaviors.Base.TriggerCollection newCollection)
            {
                foreach (var trigger in newCollection)
                    trigger.Attach(d);
            }
        }
        #endregion
    }
}
