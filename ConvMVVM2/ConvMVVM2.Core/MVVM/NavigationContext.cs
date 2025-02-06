using System;
using System.Collections.Generic;
using System.Text;

namespace ConvMVVM2.Core.MVVM
{
    public class NavigationContext
    {
        #region Private Property

        private object _PrevData = null;
        private object _NextData = null;
        #endregion

        #region Constructor
        public NavigationContext(object prevContext, object nextContext)
        {
            this.PrevContext = prevContext;
            this.NextContext = nextContext;
        }
        #endregion

        #region Public Property
        public bool FirstNavigation
        {
            get => this.PrevContext == null;
        }
        public object PrevData
        {
            get => this._PrevData;
            set=> this._PrevData = value;
        }
        public object PrevContext { get; } = null;
        public bool HasPrevData
        {
            get => this._PrevData != null;
        }

        public object NextData
        {
            get => this._NextData;
            set => this._NextData = value;
        }
        public object NextContext { get; } = null;
        public bool HasNextData
        {
            get => this._NextData != null;
        }
        #endregion
    }
}
