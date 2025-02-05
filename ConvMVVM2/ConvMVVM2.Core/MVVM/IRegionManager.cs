using System;

namespace ConvMVVM2.Core.MVVM
{
    public interface IRegionManager
    {
        #region Public Functions
        void Register(string regionName, IRegion region);
        void Add(string regionName, Type viewType);
        void Show(string regionName, Type viewType);
        void Hide(string regionName);
        void Mapping(string regionName, Type viewType);
        #endregion
    }
}
