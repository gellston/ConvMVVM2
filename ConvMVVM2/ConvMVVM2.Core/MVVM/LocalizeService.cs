using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Resources;
using System.Text;

namespace ConvMVVM2.Core.MVVM
{
    internal class LocalizeService : ILocalizeService, INotifyPropertyChanged
    {
        #region Private Property
        private ResourceManager _resourceManager = null;
        private CultureInfo _cultureInfo = null;
        #endregion

        #region Event
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Public Property

        #endregion

        #region Public Functions
        public string this[string key]
        {
            get
            {
                if(this._resourceManager == null)
                    return $"[{key}]";

                if(this._cultureInfo == null)
                    return $"[{key}]";

                var res = this._resourceManager.GetString(key, this._cultureInfo);
                return !string.IsNullOrWhiteSpace(res) ? res : $"[{key}]";
            }
        }



        public void ChangeLocal(string local)
        {
            this._cultureInfo = new CultureInfo(local);

            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(string.Empty));
            }
        }

        public void SetResourceManager(ResourceManager resourceManager)
        {
            this._resourceManager = resourceManager;
        }
        #endregion
    }
}
