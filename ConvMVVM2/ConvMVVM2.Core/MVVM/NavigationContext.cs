using System;
using System.Collections.Generic;
using System.Text;

namespace ConvMVVM2.Core.MVVM
{
    public class NavigationContext
    {
        #region Private Property
        private readonly Dictionary<string, object> _parameters;
        #endregion

        #region Constructor
        public NavigationContext()
        {

            this._parameters = new Dictionary<string, object>();
        }

        public NavigationContext(IDictionary<string, object> parameters)
        {
            this._parameters = new Dictionary<string, object>(parameters);
        }
        #endregion

        #region Public Property
        public IReadOnlyDictionary<string, object> Parameters => _parameters;
        #endregion

        #region Public Functions
        public void Add(string key, object value)
        {

            _parameters[key] = value;
        }

        public bool Contains(string key)
        {

            return _parameters.ContainsKey(key);
        }

        public T GetValue<T>(string key)
        {
            if (_parameters.TryGetValue(key, out var value))
            {
                if (value is T typedValue)
                    return typedValue;

                try
                {
                    return (T)System.Convert.ChangeType(value, typeof(T));
                }
                catch
                {
                    return default!;
                }
            }

            return default!;
        }
        #endregion
    }
}
