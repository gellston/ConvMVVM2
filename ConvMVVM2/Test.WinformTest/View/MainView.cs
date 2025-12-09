using ConvMVVM2.Core.MVP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Test.WinformTest.View;

namespace Test.WinformTest
{
    public partial class MainView : Form, IMainView
    {
        public MainView()
        {
            InitializeComponent();

            this.Load += MainView_Load;
            this.Shown += MainView_Shown;
        }


        #region Public Property
        public IPresenter Presenter { get; set; }
        #endregion

        #region Protected Functions
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            this.OnViewClosedEvent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            this.OnViewClosingEvent();
        }
        #endregion

        #region Event Handler

        private void MainView_Shown(object sender, EventArgs e)
        {
            this.OnViewShownEvent();
        }

        private void MainView_Load(object sender, EventArgs e)
        {
            this.OnViewLoadedEvent();
        }

        #endregion

        #region Event
        public event Action OnViewLoadedEvent;
        public event Action OnViewShownEvent;
        public event Action OnViewClosingEvent;
        public event Action OnViewClosedEvent;
        #endregion
    }
}
