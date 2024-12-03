using System;
using System.Collections.Generic;
using System.Resources;
using System.Text;

namespace ConvMVVM2.Core.MVVM
{
    public interface ILocalizeService
    {

        #region Public Functions

        public string this[string key] { get; }

        public void ChangeLocal(string local);

        public void SetResourceManager(ResourceManager resourceManager);
        #endregion
    }
}
