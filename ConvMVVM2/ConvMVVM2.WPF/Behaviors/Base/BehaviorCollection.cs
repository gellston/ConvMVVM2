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
        protected override Freezable CreateInstanceCore()
        {
            return new BehaviorCollection();
        }
    }
}
