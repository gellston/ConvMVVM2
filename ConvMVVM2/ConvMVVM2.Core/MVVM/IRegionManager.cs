using System;

namespace ConvMVVM2.Core.MVVM
{
    public interface IRegionManager
    {
        #region Public Functions
        void Cleanup(string regionName);
        void Register(string regionName, IRegion region);
        void Navigate(string regionName, Type viewType);
        void Navigate(string regionName, string viewName);
        void Navigate<TView>(string regionName) where TView : class;
        void Hide(string regionName);
        void Mapping(string regionName, Type viewType);
        void Mapping<TView>(string regionName) where TView : class;
        #endregion
    }
}
