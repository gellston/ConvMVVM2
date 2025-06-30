using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConvMVVM2.WPF.Behaviors.Base
{
    public abstract class BehaviorBase : Freezable
    {
        #region Public Functions
        public abstract void Attach(DependencyObject associatedObject);
        public abstract void Detach();
        #endregion

        #region Protected Functions
        protected override Freezable CreateInstanceCore()
        {
            throw new NotImplementedException($"{GetType().Name} must override CreateInstanceCore.");
        }
        #endregion

    }

    public abstract class Behavior<T> : BehaviorBase where T : DependencyObject
    {
        #region Public Property
        public T AssociatedObject { get; private set; }
        #endregion

        #region Public Functions

        public override void Attach(DependencyObject associatedObject)
        {
            if (associatedObject is T typed)
            {
                AssociatedObject = typed;
                OnAttached();
            }
        }

        public override void Detach()
        {
            OnDetaching();
            AssociatedObject = null;
        }
        #endregion

        #region Protected Functions
        protected virtual void OnAttached() { }
        protected virtual void OnDetaching() { }
        
        #endregion
    }
}
