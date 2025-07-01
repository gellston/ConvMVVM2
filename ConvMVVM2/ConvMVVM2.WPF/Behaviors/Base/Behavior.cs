using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace ConvMVVM2.WPF.Behaviors.Base
{
    public abstract class Behavior<T> : Behavior where T : DependencyObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Behavior&lt;T&gt;"/> class.
        /// </summary>
        protected Behavior()
            : base(typeof(T))
        {
        }

        /// <summary>
        /// Gets the object to which this <see cref="Behavior&lt;T&gt;"/> is attached.
        /// </summary>
        protected new T AssociatedObject
        {
            get { return (T)base.AssociatedObject; }
        }
    }

    /// <summary>
    /// Encapsulates state information and zero or more ICommands into an attachable object.
    /// </summary>
    /// <remarks>This is an infrastructure class. Behavior authors should derive from Behavior&lt;T&gt; instead of from this class.</remarks>
    public abstract class Behavior :
        Animatable,
        IAttachedObject
    {
        private Type associatedType;
        private DependencyObject associatedObject;

        internal event EventHandler AssociatedObjectChanged;

        /// <summary>
        /// The type to which this behavior can be attached.
        /// </summary>
        protected Type AssociatedType
        {
            get
            {
                this.ReadPreamble();
                return this.associatedType;
            }
        }

        /// <summary>
        /// Gets the object to which this behavior is attached.
        /// </summary>
        protected DependencyObject AssociatedObject
        {
            get
            {
                this.ReadPreamble();
                return this.associatedObject;
            }
        }

        internal Behavior(Type associatedType)
        {
            this.associatedType = associatedType;
        }

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>Override this to hook up functionality to the AssociatedObject.</remarks>
        protected virtual void OnAttached()
        {
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        /// <remarks>Override this to unhook functionality from the AssociatedObject.</remarks>
        protected virtual void OnDetaching()
        {
        }

        protected override Freezable CreateInstanceCore()
        {
            Type classType = this.GetType();
            return (Freezable)Activator.CreateInstance(classType);
        }

        private void OnAssociatedObjectChanged()
        {
            if (this.AssociatedObjectChanged != null)
            {
                this.AssociatedObjectChanged(this, new EventArgs());
            }
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
        /// <exception cref="InvalidOperationException">The Behavior is already hosted on a different element.</exception>
        /// <exception cref="InvalidOperationException">dependencyObject does not satisfy the Behavior type constraint.</exception>
        public void Attach(DependencyObject dependencyObject)
        {
            if (dependencyObject != this.AssociatedObject)
            {
                if (this.AssociatedObject != null)
                {
                    throw new InvalidOperationException("There is no associate object");
                }

                // todo jekelly: what do we do if dependencyObject is null?

                // Ensure the type constraint is met
                if (dependencyObject != null && !this.AssociatedType.IsAssignableFrom(dependencyObject.GetType()))
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture,
                                                                        "There is no associate object",
                                                                        this.GetType().Name,
                                                                        dependencyObject.GetType().Name,
                                                                        this.AssociatedType.Name));
                }

                this.WritePreamble();
                this.associatedObject = dependencyObject;
                this.WritePostscript();
                this.OnAssociatedObjectChanged();

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
            this.OnAssociatedObjectChanged();
        }

        #endregion
    }

}
