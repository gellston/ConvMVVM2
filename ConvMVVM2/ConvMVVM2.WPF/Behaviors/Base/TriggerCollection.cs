using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConvMVVM2.WPF.Behaviors.Base
{
    public class TriggerCollection : FreezableCollection<TriggerBase>
    {
        protected override Freezable CreateInstanceCore()
        {
            return new TriggerCollection();
        }
    }
}
