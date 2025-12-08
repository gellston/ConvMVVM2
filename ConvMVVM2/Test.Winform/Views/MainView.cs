using ConvMVVM2.Core.MVP;
using ConvMVVM2.Winform.MVP;
using System.ComponentModel;
using Test.Winform.Views;

namespace Test.Winform
{
    public partial class MainView : Form , IMainView
    {
        public MainView()
        {
            InitializeComponent();

            this.Shown += MainView_Shown;
            this.Load += MainView_Load;
     
        }

        #region Public Property
        public IPresenter Presenter
        {
            get; set;
        }

        #endregion

        #region Protected Functions
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            this.OnViewClosedEvent?.Invoke();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            this.OnViewClosingEvent?.Invoke();
        }
        #endregion

        #region Event
        public event Action OnViewInitializedEvent;
        public event Action OnViewLoadedEvent;
        public event Action OnViewShownEvent;
        public event Action OnViewClosingEvent;
        public event Action OnViewClosedEvent;
        #endregion

        #region Event Handler
        private void MainView_Shown(object? sender, EventArgs e)
        {
            this.OnViewShownEvent?.Invoke();
        }

        private void MainView_Load(object? sender, EventArgs e)
        {
            this.OnViewLoadedEvent?.Invoke();
        }
        #endregion


    }
}
