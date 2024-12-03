namespace ConvMVVM2.Core.MVVM
{
    public interface ILayerManager
    {
        #region Public Functions
        void Register(string layerName, ILayer layer);
        void Add(string layerName, IView view);
        void Show(string layerName, IView view);
        void Hide(string layerName);
        void Mapping(string layerName, IView view);
        #endregion
    }
}
