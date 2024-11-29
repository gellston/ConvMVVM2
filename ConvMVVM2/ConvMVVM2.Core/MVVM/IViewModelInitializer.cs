namespace ConvMVVM2.Core.MVVM
{
    public interface IViewModelInitializer
    {
        #region Public Functions
        void InitializeViewModel(IView view);
        #endregion
    }
}
