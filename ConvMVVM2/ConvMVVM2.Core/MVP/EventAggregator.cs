using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConvMVVM2.Core.MVP
{
    public class EventAggregator : IEventAggregator
    {

        #region Private Property
  
        private Hashtable _observerCollection = new Hashtable();
        #endregion


        #region Public Functions
        public PubSubEvent<TDataType> GetEvent<TDataType>()
        {
            if (!_observerCollection.ContainsKey(typeof(TDataType)))
            {
                var eventHandler = (PubSubEvent<TDataType>)Activator.CreateInstance(typeof(PubSubEvent<TDataType>));
                this._observerCollection.Add(typeof(TDataType), eventHandler);
            }
            return (PubSubEvent<TDataType>)this._observerCollection[typeof(TDataType)];
        }

        public void Cleanup()
        {
            _observerCollection.Clear();
        }
        public void Cleanup<TDataType>()
        {
            this._observerCollection.Remove(typeof(TDataType));
        }

        #endregion
    }
}
