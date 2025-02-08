using System;
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
        #endregion

        #region Consturctor
        public PubSubEvent() { }
        #endregion

        #region Public Functions
        public void Subscribe(Action<TDataType> action, ThreadOption option = ThreadOption.Publisher) {
            lock (this.lockObject)
            {
                if (option == ThreadOption.None) return;
                this.ThreadOption = option;

                OnPublishEvent -= action;
                OnPublishEvent += action;
            }
        }

        public void Unsubscribe(Action<TDataType> action)
        {
            lock (this.lockObject)
            {
                OnPublishEvent -= action;
            }

        }

        public void Publish(TDataType data)
        {
            lock (this.lockObject)
            {
                try
                {
                    switch (this.ThreadOption)
                    {
                        case ThreadOption.None:
                            break;
                        case ThreadOption.Background:
                            {
                                Task.Run(() =>
                                {
                                    try
                                    {
                                        this.OnPublishEvent(data);
                                    }
                                    catch (Exception ex)
                                    {
                                    }
                                });
                                break;
                            }

                        case ThreadOption.Publisher:
                            {
                                this.OnPublishEvent(data);
                                break;
                            }
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

        }
        #endregion

        #region Event
        private event Action<TDataType> OnPublishEvent;
        #endregion
    }
}
