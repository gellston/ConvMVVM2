using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConvMVVM2.WPF.Behaviors.Base
{
    public abstract class TriggerAction : Freezable
    {
        #region Public Property
        public DependencyObject AssociatedObject { get; private set; }
        #endregion

        #region Public Functions


        public void Attach(DependencyObject associatedObject)
        {
            AssociatedObject = associatedObject;
        }

        public void Detach()
        {
            AssociatedObject = null;
        }

        public abstract void Invoke(object parameter);
        #endregion

        #region Protected Functions

        protected override Freezable CreateInstanceCore()
        {
            return (Freezable)Activator.CreateInstance(GetType());
        }
        #endregion
    }
}
