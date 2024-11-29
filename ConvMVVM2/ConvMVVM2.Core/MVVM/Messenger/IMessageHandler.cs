using System;


namespace ConvMVVM2.Core.MVVM.Messenger
{
    public interface IMessageHandler
    {
        #region Public Functions
        public Type MessageType();
        public Type ReceiverType();

        public void Callback(object message);

        public bool IsAlive();

        public bool Comapre(object _receiver);

        #endregion
    }
}
