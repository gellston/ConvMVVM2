using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConvMVVM2.Core.MVVM.Messenger
{
    internal class AsyncMessageHandler<TReceiver, TMessage> : IAsyncMessageHandler where TReceiver : class
    {
        #region Private Property
        private readonly Func<TReceiver, TMessage, Task> handler;
        private WeakReference<TReceiver> receiver;
        #endregion

        #region Constructor
        public AsyncMessageHandler(Func<TReceiver, TMessage, Task> handler, TReceiver _receiver)
        {
            this.handler = handler;
            receiver = new WeakReference<TReceiver>(_receiver);
        }
        #endregion


        #region Functions
        public async Task Callback(object message)
        {
            if (handler != null)
            {

                TReceiver receiver;
                if (this.receiver.TryGetTarget(out receiver))
                {
                    await handler(receiver, (TMessage)message);
                }
            }
        }

        public Type MessageType()
        {
            return typeof(TMessage);
        }

        public Type ReceiverType()
        {
            return typeof(TReceiver);
        }

        public bool IsAlive()
        {
            TReceiver receiver = null;
            this.receiver.TryGetTarget(out receiver);

            return receiver != null;
        }
        #endregion
    }
}
