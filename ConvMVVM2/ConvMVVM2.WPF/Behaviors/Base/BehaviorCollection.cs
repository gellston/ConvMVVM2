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
    
    public class BehaviorCollection : FreezableCollection<BehaviorBase>
    {
        #region Public Property
        public DependencyObject AssociatedObject { get; private set; }
        #endregion

        #region Public Functions

        public void Attach(DependencyObject associatedObject)
        {
            AssociatedObject = associatedObject;
            
            foreach (var behavior in this)
            {
                var method = behavior.GetType().GetMethod("Attach");
                method?.Invoke(behavior, new object[] { associatedObject });
            }
        }

        public void Detach()
        {
            foreach (var behavior in this)
            {
                var method = behavior.GetType().GetMethod("Detach");
                method?.Invoke(behavior, null);
            }

            AssociatedObject = null;
        }
        #endregion
    }
}
