using System;


namespace ConvMVVM2.Core.MVVM.Messenger
{
    internal class MessageHandler<TReceiver, TMessage> : IMessageHandler where TReceiver : class
    {
        #region Private Property
        private readonly Action<TReceiver, TMessage> handler;
        private WeakReference<TReceiver> receiver;
        #endregion

        #region Constructor
        public MessageHandler(Action<TReceiver, TMessage> handler, TReceiver _receiver)
        {
            this.handler = handler;
            receiver = new WeakReference<TReceiver>(_receiver);
        }
        #endregion

        #region Functions
        public void Callback(object message)
        {
            if (handler != null && MessageType() == message.GetType())
            {

                TReceiver receiver;
                if (this.receiver.TryGetTarget(out receiver))
                {
                    handler(receiver, (TMessage)message);
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

        public bool Comapre(object _receiver)
        {
            TReceiver receiver = null;
            this.receiver.TryGetTarget(out receiver);

            return receiver == _receiver;
        }
        #endregion
    }
}
