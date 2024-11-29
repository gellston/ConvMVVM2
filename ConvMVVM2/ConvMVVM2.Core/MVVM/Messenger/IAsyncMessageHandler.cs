using System;
using System.Threading.Tasks;

namespace ConvMVVM2.Core.MVVM.Messenger
{
    public interface IAsyncMessageHandler
    {
        #region Public Functions
        public Type MessageType();
        public Type ReceiverType();

        public Task Callback(object message);

        public bool IsAlive();

        #endregion
    }
}
