using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConvMVVM2.WPF.Behaviors.Base
{
    public class TriggerActionCollection : AttachableCollection<TriggerAction>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TriggerActionCollection"/> class.
        /// </summary>
        /// <remarks>Internal, because this should not be inherited outside this assembly.</remarks>
        internal TriggerActionCollection()
        {
        }

        /// <summary>
        /// Called immediately after the collection is attached to an AssociatedObject.
        /// </summary>
        protected override void OnAttached()
        {
            foreach (TriggerAction action in this)
            {
                Debug.Assert(action.IsHosted, "Action must be hosted if it is in the collection.");
                action.Attach(this.AssociatedObject);
            }
        }

        /// <summary>
        /// Called when the collection is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        protected override void OnDetaching()
        {
            foreach (TriggerAction action in this)
            {
                Debug.Assert(action.IsHosted, "Action must be hosted if it is in the collection.");
                action.Detach();
            }
        }

        /// <summary>
        /// Called when a new item is added to the collection.
        /// </summary>
        /// <param name="item">The new item.</param>
        internal override void ItemAdded(TriggerAction item)
        {
            if (item.IsHosted)
            {
                throw new InvalidOperationException("Its hosted action");
            }
            if (this.AssociatedObject != null)
            {
                item.Attach(this.AssociatedObject);
            }
            item.IsHosted = true;
        }

        /// <summary>
        /// Called when an item is removed from the collection.
        /// </summary>
        /// <param name="item">The removed item.</param>
        internal override void ItemRemoved(TriggerAction item)
        {
            Debug.Assert(item.IsHosted, "Item should hosted if it is being removed from a TriggerCollection.");
            if (((IAttachedObject)item).AssociatedObject != null)
            {
                item.Detach();
            }
            item.IsHosted = false;
        }

        /// <summary>
        /// Creates a new instance of the TriggerActionCollection.
        /// </summary>
        /// <returns>The new instance.</returns>
        protected override Freezable CreateInstanceCore()
        {
            return new TriggerActionCollection();
        }
    }
}
