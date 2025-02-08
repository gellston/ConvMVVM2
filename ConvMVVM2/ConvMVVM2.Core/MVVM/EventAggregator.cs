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


        public TDataType GetEvent<TDataType>() where TDataType : EventBase
        {
            if(this._eventCollection.Where(data => data is TDataType).Count() == 0)
            {
                var eventHandler = (TDataType)Activator.CreateInstance(typeof(TDataType));
                this._eventCollection.Add(eventHandler);
            }

            foreach(var eventHandler in this._eventCollection)
            {
                if (eventHandler is TDataType)
                    return (TDataType)eventHandler;
            }

            throw new Exception("Invalid Event Handler");
        }
    }
}
