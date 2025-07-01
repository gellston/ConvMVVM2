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
        #region Static Property
        internal static bool ShouldRunInDesignMode
        {
            get;
            set;
        }
        #endregion

        #region Attached Property




        public static readonly DependencyProperty BehaviorsProperty = DependencyProperty.RegisterAttached("ShadowBehaviors", typeof(BehaviorCollection), typeof(Interaction), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnBehaviorsChanged)));

        public static void SetBehaviors(DependencyObject element, BehaviorCollection value)
        {
            element.SetValue(BehaviorsProperty, value);
        }

        public static BehaviorCollection GetBehaviors(DependencyObject element)
        {

            var behaviors = (BehaviorCollection)element.GetValue(BehaviorsProperty);
            if (behaviors == null)
            {
                behaviors = new BehaviorCollection();
                SetBehaviors(element, behaviors);
            }
            return behaviors;
        }

        private static void OnBehaviorsChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            BehaviorCollection oldCollection = (BehaviorCollection)args.OldValue;
            BehaviorCollection newCollection = (BehaviorCollection)args.NewValue;

            if (oldCollection != newCollection)
            {
                if (oldCollection != null && ((IAttachedObject)oldCollection).AssociatedObject != null)
                {
                    oldCollection.Detach();
                }

                if (newCollection != null && obj != null)
                {
                    if (((IAttachedObject)newCollection).AssociatedObject != null)
                    {
                        throw new InvalidOperationException("There is no associatedObject");
                    }

                    newCollection.Attach(obj);
                }
            }
        }





        public static readonly DependencyProperty TriggersProperty = DependencyProperty.RegisterAttached("ShadowTriggers", typeof(Behaviors.Base.TriggerCollection),typeof(Interaction), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnTriggersChanged)));

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
                obj.SetValue(Interaction.TriggersProperty, collection);
            }
            return collection;
        }

        private static void OnTriggersChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            Behaviors.Base.TriggerCollection oldCollection = args.OldValue as Behaviors.Base.TriggerCollection;
            Behaviors.Base.TriggerCollection newCollection = args.NewValue as Behaviors.Base.TriggerCollection;

            if (oldCollection != newCollection)
            {
                if (oldCollection != null && ((IAttachedObject)oldCollection).AssociatedObject != null)
                {
                    oldCollection.Detach();
                }

                if (newCollection != null && obj != null)
                {
                    if (((IAttachedObject)newCollection).AssociatedObject != null)
                    {
                        throw new InvalidOperationException("there is no associatedObject");
                    }

                    newCollection.Attach(obj);
                }
            }
        }
        #endregion
    }
}
