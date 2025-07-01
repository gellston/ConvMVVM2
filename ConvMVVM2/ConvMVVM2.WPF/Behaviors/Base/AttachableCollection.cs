﻿using ConvMVVM2.WPF.MarkupExtensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConvMVVM2.WPF.Behaviors.Base
{
    public abstract class AttachableCollection<T> : FreezableCollection<T>, IAttachedObject where T : DependencyObject, IAttachedObject
    {
        private Collection<T> snapshot;
        private DependencyObject associatedObject;

        /// <summary>
        /// The object on which the collection is hosted.
        /// </summary>
        protected DependencyObject AssociatedObject
        {
            get
            {
                this.ReadPreamble();
                return this.associatedObject;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttachableCollection&lt;T&gt;"/> class.
        /// </summary>
        /// <remarks>Internal, because this should not be inherited outside this assembly.</remarks>
        internal AttachableCollection()
        {
            INotifyCollectionChanged notifyCollectionChanged = (INotifyCollectionChanged)this;
            notifyCollectionChanged.CollectionChanged += new NotifyCollectionChangedEventHandler(OnCollectionChanged);

            this.snapshot = new Collection<T>();
        }

        /// <summary>
        /// Called immediately after the collection is attached to an AssociatedObject.
        /// </summary>
        protected abstract void OnAttached();

        /// <summary>
        /// Called when the collection is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        protected abstract void OnDetaching();

        /// <summary>
        /// Called when a new item is added to the collection.
        /// </summary>
        /// <param name="item">The new item.</param>
        internal abstract void ItemAdded(T item);

        /// <summary>
        /// Called when an item is removed from the collection.
        /// </summary>
        /// <param name="item">The removed item.</param>
        internal abstract void ItemRemoved(T item);

        [Conditional("DEBUG")]
        private void VerifySnapshotIntegrity()
        {
            bool isValid = (this.Count == this.snapshot.Count);
            if (isValid)
            {
                for (int i = 0; i < this.Count; i++)
                {
                    if (this[i] != this.snapshot[i])
                    {
                        isValid = false;
                        break;
                    }
                }
            }
            Debug.Assert(isValid, "ReferentialCollection integrity has been compromised.");
        }

        /// <exception cref="InvalidOperationException">Cannot add the instance to a collection more than once.</exception>
        private void VerifyAdd(T item)
        {
            if (this.snapshot.Contains(item))
            {
                string message = string.Format(CultureInfo.CurrentCulture,"Cannot add the same instance of '{0}' to the '{1}' more than once.",typeof(T).Name, this.GetType().Name);

                throw new InvalidOperationException(message);
            }
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                // We fix the snapshot to mirror the structure, even if an exception is thrown. This may not be desirable.
                case NotifyCollectionChangedAction.Add:
                    foreach (T item in e.NewItems)
                    {
                        try
                        {
                            this.VerifyAdd(item);
                            this.ItemAdded(item);
                        }
                        finally
                        {
                            this.snapshot.Insert(this.IndexOf(item), item);
                        }
                    }
                    break;

                case NotifyCollectionChangedAction.Replace:
                    foreach (T item in e.OldItems)
                    {
                        this.ItemRemoved(item);
                        this.snapshot.Remove(item);
                    }
                    foreach (T item in e.NewItems)
                    {
                        try
                        {
                            this.VerifyAdd(item);
                            this.ItemAdded(item);
                        }
                        finally
                        {
                            this.snapshot.Insert(this.IndexOf(item), item);
                        }
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (T item in e.OldItems)
                    {
                        this.ItemRemoved(item);
                        this.snapshot.Remove(item);
                    }
                    break;

                case NotifyCollectionChangedAction.Reset:
                    foreach (T item in this.snapshot)
                    {
                        this.ItemRemoved(item);
                    }
                    this.snapshot = new Collection<T>();
                    foreach (T item in this)
                    {
                        this.VerifyAdd(item);
                        this.ItemAdded(item);
                    }
                    break;
                case NotifyCollectionChangedAction.Move:
                default:
                    Debug.Fail("Unsupported collection operation attempted.");
                    break;
            }
#if DEBUG
			this.VerifySnapshotIntegrity();
#endif
        }

        #region IAttachedObject Members

        /// <summary>
        /// Gets the associated object.
        /// </summary>
        /// <value>The associated object.</value>
        DependencyObject IAttachedObject.AssociatedObject
        {
            get
            {
                return this.AssociatedObject;
            }
        }

        /// <summary>
        /// Attaches to the specified object.
        /// </summary>
        /// <param name="dependencyObject">The object to attach to.</param>
        /// <exception cref="InvalidOperationException">The IAttachedObject is already attached to a different object.</exception>
        public void Attach(DependencyObject dependencyObject)
        {
            if (dependencyObject != this.AssociatedObject)
            {
                if (this.AssociatedObject != null)
                {
                    throw new InvalidOperationException();
                }

                if (Interaction.ShouldRunInDesignMode || !(bool)this.GetValue(DesignerProperties.IsInDesignModeProperty))
                {
                    this.WritePreamble();
                    this.associatedObject = dependencyObject;
                    this.WritePostscript();
                }
                this.OnAttached();
            }
        }

        /// <summary>
        /// Detaches this instance from its associated object.
        /// </summary>
        public void Detach()
        {
            this.OnDetaching();
            this.WritePreamble();
            this.associatedObject = null;
            this.WritePostscript();
        }

        #endregion
    }
}
