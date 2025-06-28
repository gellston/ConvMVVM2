using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace ConvMVVM2.WPF.Behaviors.Base
{
    [ContentProperty(nameof(Actions))]
    public abstract class TriggerBase : Freezable
    {
        #region Private Property
        private DependencyObject _associatedObject;

        #endregion

        #region Public Property

        public DependencyObject AssociatedObject => _associatedObject;
        #endregion

        #region Collection

        public Collection<TriggerAction> Actions { get; } = new Collection<TriggerAction>();
        #endregion

        #region Public Functions

        public void Attach(DependencyObject associatedObject)
        {
            _associatedObject = associatedObject;
            foreach (var action in Actions)
            {
                action.Attach(associatedObject);
            }
            OnAttached();
        }

        public void Detach()
        {
            OnDetaching();
            foreach (var action in Actions)
            {
                action.Detach();
            }
            _associatedObject = null;
        }
        #endregion


        #region Protected Functions

        protected abstract void OnAttached();
        protected abstract void OnDetaching();


        protected void InvokeActions(object parameter)
        {
            foreach (var action in Actions)
            {
                action.Invoke(parameter);
            }
        }

        protected override Freezable CreateInstanceCore()
        {
            return (Freezable)Activator.CreateInstance(GetType());
        }
        #endregion
    }



    public abstract class TriggerBase<T> : TriggerBase where T : DependencyObject
    {
        #region Public Property
        public new T AssociatedObject => (T)base.AssociatedObject;
        #endregion

        #region Protected Functions

        protected override void OnAttached()
        {
            if (!(base.AssociatedObject is T))
            {
                throw new InvalidOperationException(
                    $"Trigger can only be attached to {typeof(T).Name} or its subclasses.");
            }

            OnAttachedTyped();
        }

        protected override void OnDetaching()
        {
            OnDetachingTyped();
        }

        protected virtual void OnAttachedTyped() { }

        protected virtual void OnDetachingTyped() { }
        #endregion
    }
}
