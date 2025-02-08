using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConvMVVM2.Core.MVVM
{
    public class EventAggregator : IEventAggregator
    {

        #region Private Property
        private List<object> _eventCollection = new List<object>();
        #endregion


        public PubSubEvent<TDataType> GetEvent<TDataType>()
        {
            if(this._eventCollection.Where(data => data is PubSubEvent<TDataType>).Count() == 0)
            {
                var eventHandler = (PubSubEvent<TDataType>)Activator.CreateInstance(typeof(PubSubEvent<TDataType>));
                this._eventCollection.Add(eventHandler);
            }

            foreach(var eventHandler in this._eventCollection)
            {
                if (eventHandler is PubSubEvent<TDataType>)
                    return (PubSubEvent<TDataType>)eventHandler;
            }

            throw new Exception("Invalid Event Handler");
        }
    }
}
