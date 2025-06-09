using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;

namespace ConvMVVM2.Core.MVVM
{
    public class BatchObservableCollection<T> : ObservableCollection<T>
    {
        public void Update(IEnumerable<T> collection)
        {
            base.Items.Clear();

            foreach (var item in collection)
            {
                base.Items.Add(item);
            }

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public void Update()
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}
