using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvMVVM2.Core.MVVM.Messenger
{
    public class WeakReferenceMessenger
    {
        #region Static Property
        public static WeakReferenceMessenger Default { get; } = new WeakReferenceMessenger();
        #endregion

        #region Private Property
        private Dictionary<Type, List<IMessageHandler>> receivers = new Dictionary<Type, List<IMessageHandler>>();
        private Dictionary<Type, List<IAsyncMessageHandler>> asyncReceivers = new Dictionary<Type, List<IAsyncMessageHandler>>();
        #endregion

        #region Public Functions

        public void UnRegisterAll<TReceiver>(TReceiver receiver)
        {
            if (receivers.ContainsKey(typeof(TReceiver)) == true)
            {
                var sameHandlers = receivers[typeof(TReceiver)].Where(handler => handler.Comapre(receiver)).ToList();
                foreach (var handler in sameHandlers)
                    receivers[typeof(TReceiver)].Remove(handler);
            }
        }

        public void Register<TReceiver, TMessage>(TReceiver receiver, Action<TReceiver, TMessage> callback) where TReceiver : class
        {
            foreach (var keyPair in receivers)
            {
                var deadHandlers = keyPair.Value.Where(handler => handler.IsAlive() == false).ToList();
                foreach (var handler in deadHandlers)
                    keyPair.Value.Remove(handler);
            }

            var _handler = new MessageHandler<TReceiver, TMessage>(callback, receiver);
            if (receivers.ContainsKey(typeof(TReceiver)) == false)
            {
                receivers[typeof(TReceiver)] = new List<IMessageHandler>();
            }
            receivers[typeof(TReceiver)].Add(_handler);
        }

        public void Send<TMessage>(TMessage message)
        {

            foreach (var keyPair in receivers)
            {
                var deadHandlers = keyPair.Value.Where(handler => handler.IsAlive() == false).ToList();
                foreach (var handler in deadHandlers)
                    keyPair.Value.Remove(handler);
            }

            List<IMessageHandler> messageHandlers = new List<IMessageHandler>();

            foreach (var receiver in receivers)
            {
                foreach (var handler in receiver.Value)
                {
                    if (handler.MessageType() == typeof(TMessage))
                    {
                        messageHandlers.Add(handler);
                    }
                }
            }
            foreach (var handler in messageHandlers)
                handler.Callback(message);
        }

        public void Send<TMessage, TReceiver>(TMessage message) where TReceiver : class
        {

            foreach (var keyPair in receivers)
            {
                var deadHandlers = keyPair.Value.Where(handler => handler.IsAlive() == false).ToList();
                foreach (var handler in deadHandlers)
                    keyPair.Value.Remove(handler);
            }

            if (receivers.ContainsKey(typeof(TReceiver)) == false)
            {
                throw new InvalidOperationException("There is no proper receiver type");
            }

            var handlers = receivers[typeof(TReceiver)];

            foreach (var handler in handlers)
            {
                handler.Callback(message);
            }
        }


        public void AsyncRegister<TReceiver, TMessage>(TReceiver receiver, Func<TReceiver, TMessage, Task> callback) where TReceiver : class
        {
            foreach (var keyPair in asyncReceivers)
            {
                var deadHandlers = keyPair.Value.Where(handler => handler.IsAlive() == false).ToList();
                foreach (var handler in deadHandlers)
                    keyPair.Value.Remove(handler);
            }

            var _handler = new AsyncMessageHandler<TReceiver, TMessage>(callback, receiver);
            if (asyncReceivers.ContainsKey(typeof(TReceiver)) == false)
            {
                asyncReceivers[typeof(TReceiver)] = new List<IAsyncMessageHandler>();
            }
            asyncReceivers[typeof(TReceiver)].Add(_handler);
        }

        public async Task AsyncSend<TMessage>(TMessage message)
        {
            foreach (var keyPair in asyncReceivers)
            {
                var deadHandlers = keyPair.Value.Where(handler => handler.IsAlive() == false).ToList();
                foreach (var handler in deadHandlers)
                    keyPair.Value.Remove(handler);
            }

            List<IAsyncMessageHandler> messageHandlers = new List<IAsyncMessageHandler>();

            foreach (var receiver in asyncReceivers)
            {
                foreach (var handler in receiver.Value)
                {
                    if (handler.MessageType() == typeof(TMessage))
                    {
                        messageHandlers.Add(handler);
                    }
                }
            }
            foreach (var handler in messageHandlers)
                await handler.Callback(message);
        }

        public async Task AsyncSend<TMessage, TReceiver>(TMessage message) where TReceiver : Type
        {
            foreach (var keyPair in asyncReceivers)
            {
                var deadHandlers = keyPair.Value.Where(handler => handler.IsAlive() == false).ToList();
                foreach (var handler in deadHandlers)
                    keyPair.Value.Remove(handler);
            }

            if (asyncReceivers.ContainsKey(typeof(TReceiver)) == false)
            {
                throw new InvalidOperationException("There is no proper receiver type");
            }

            var handlers = asyncReceivers[typeof(TReceiver)];

            foreach (var handler in handlers)
            {
                await handler.Callback(message);
            }

        }

        #endregion



    }
}
