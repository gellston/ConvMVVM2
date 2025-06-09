using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ConvMVVM2.Core.MVVM
{
    public class Progress<T> : IProgress<T>
    {
        private readonly SynchronizationContext _syncContext;
        private readonly Action<T> _handler;

        public Progress(Action<T> handler)
        {
            _syncContext = SynchronizationContext.Current;
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
        }

        public void Report(T value)
        {
            if (_syncContext != null)
                _syncContext.Post(_ => _handler((T)_), value);
            else
                _handler(value);
        }
    }
}
