using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace ConvMVVM2.Core.MVVM
{

    public class PubSubEvent<TDataType> : EventBase
    {
        #region Private Property
        private Object lockObject = new Object();
        private HashSet<CallbackDelegate> callbackHash = new HashSet<CallbackDelegate>();
        private Hashtable callbackOptionTable = new Hashtable();
        #endregion

        #region Consturctor
        public PubSubEvent() { 
            
        }
        #endregion

        #region Public Property
        public delegate void CallbackDelegate(TDataType arg);
        #endregion

        #region Public Functions
        public void Subscribe(CallbackDelegate callback, ThreadOption option = ThreadOption.Publisher) {
            lock (this.lockObject)
            {
                if (callback == null)
                    throw new NullReferenceException("Callback null!");

                if (callbackHash.Add(callback))
                {
                    callbackOptionTable.Add(callback, option);
                }
            }
        }

        public void Unsubscribe(CallbackDelegate callback)
        {
            lock (this.lockObject)
            {
                if (callback == null)
                    throw new NullReferenceException("Callback null!");

                this.callbackHash.Remove(callback);
                this.callbackOptionTable.Remove(callback);
            }
        }

        public void Publish(TDataType data)
        {
            lock (this.lockObject)
            {
                try
                {

                    foreach(var callback in this.callbackHash)
                    {
                        ThreadOption option = (ThreadOption)this.callbackOptionTable[callback];
                        switch (option)
                        {
                            case ThreadOption.Background:
                                {
                                    Task.Run(() =>
                                    {
                                        try
                                        {
                                            callback(data);
                                        }
                                        catch
                                        {

                                        }
                                    });
                                    break;
                                }

                            case ThreadOption.Publisher:
                                {
                                    try
                                    {
                                        callback(data);
                                    }
                                    catch
                                    {

                                    }
                                    break;
                                }
                        }
                    }
                }
                catch
                {
                    throw;
                }
            }

        }
        #endregion

    }
}
