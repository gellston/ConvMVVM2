﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ConvMVVM2.Core.MVVM
{
    public interface IServiceInitializable
    {
        #region Public Functions
        void OnServiceInitialized();
        #endregion
    }
}
