using System;

namespace ConvMVVM2.Core.MVVM
{
    public interface IRegionManager
    {
        #region Public Functions
        void Register(string regionName, IRegion region);
        void Show(string regionName, Type viewType);
        void Show(string regionName, string viewName);
        void Show<TView>(string regionName) where TView : class;
        void Hide(string regionName);
        void Mapping(string regionName, Type viewType);
        void Mapping<TView>(string regionName) where TView : class;
        #endregion
    }
}
