﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConvMVVM2.WPF.Behaviors.Base
{
    public abstract class Behavior<T> : DependencyObject where T : DependencyObject
    {
        #region Public Property
        public T AssociatedObject { get; private set; }
        #endregion


        #region Public Functions

        public void Attach(T associatedObject)
        {
            AssociatedObject = associatedObject;
            OnAttached();
        }

        public void Detach()
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
