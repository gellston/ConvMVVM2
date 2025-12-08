using ConvMVVM2.Core.MVP;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConvMVVM2.Winform.MVP
{
    public class Presenter<TView> : IPresenter, ICleanup where TView : class , IView
    {
        #region Private Property
        private bool _disposed;
        private IView view = null;
        #endregion

        #region Constructor
        public Presenter(IView view){
            this.view = view;

            this.view.OnViewLoadedEvent += View_OnViewLoadedEvent;
            this.view.OnViewShownEvent += View_OnViewShownEvent;
            this.view.OnViewClosedEvent += View_OnViewClosedEvent;
            this.view.OnViewClosingEvent += View_OnViewClosingEvent;
            
        }



        #endregion

        #region Finalizer
        ~Presenter() {
            this.Dispose(true);
        }
        #endregion


        #region Destructor
        public void Dispose()
        {
            this.Dispose(false);
        }


        protected virtual void Dispose(bool disposing)
        {
            if (this._disposed) return;
            this._disposed = true;

            if (disposing)
            {
                //Managed

                this.view = null;
                
            }
        }
        #endregion

        #region Public Property

        public IView View
        {
            get => this.view;
        }

        public TView TypeView
        {
            get => (TView)this.view;
        }
        #endregion

        #region Virtual Functions
        public virtual void OnServiceInitialized()
        {


        }

        public virtual void OnViewLoaded()
        {

        }

        public virtual void OnViewShown()
        {

        }

        public virtual void OnViewClosing()
        {

        }

        public virtual void OnViewClosed()
        {

        }
        #endregion


        #region Event Handler



        public virtual void Cleanup()
        {
            this.view.OnViewLoadedEvent -= View_OnViewLoadedEvent;
            this.view.OnViewShownEvent -= View_OnViewShownEvent;
    
        }

        private void View_OnViewShownEvent()
        {
            this.OnViewShown();
        }


        private void View_OnViewLoadedEvent()
        {
            this.OnViewLoaded();
        }


        private void View_OnViewClosingEvent()
        {
            this.OnViewClosing();

        }

        private void View_OnViewClosedEvent()
        {
            this.OnViewClosed();
        }
        #endregion

    }
}
