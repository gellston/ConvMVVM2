﻿using ConvMVVM2.Core.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace ConvMVVM2.WPF.MarkupExtensions
{
    public class LocalizeExtension : Binding
    {

        #region Constructor
        public LocalizeExtension(string path) : base("[" + path + "]")
        {
            try
            {
                this.Mode = BindingMode.OneWay;
                this.Source = ServiceLocator.GetServiceProvider().GetService<ILocalizeService>();
            }
            catch { 
            
            }

        }
        #endregion
    }
}
