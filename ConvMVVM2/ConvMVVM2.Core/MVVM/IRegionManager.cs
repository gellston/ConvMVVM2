using System;

namespace ConvMVVM2.Core.MVVM
{
    public interface IRegionManager
    {
        #region Public Functions
        void Cleanup(string regionName);
        void Register(string regionName, IRegion region);


        void Navigate(string regionName, string viewName);
        void Navigate<TView>(string regionName) where TView : class;
        void Navigate<TView>(string regionName, NavigationContext context) where TView : class;
        void Navigate(string regionName, string viewName, NavigationContext context);
        void Navigate(string regionName, Type viewType, NavigationContext navigationContext = null);


        void Hide(string regionName);
        void Mapping(string regionName, Type viewType);
        void Mapping<TView>(string regionName) where TView : class;



        #endregion
    }
}
