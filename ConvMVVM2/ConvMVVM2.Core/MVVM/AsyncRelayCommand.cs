using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConvMVVM2.Core.MVVM
{
    public class AsyncRelayCommand : IAsyncRelayCommand, INotifyPropertyChanged
    {
        #region Private Property
        private readonly Func<Task> _execute = null;
        private bool _IsRunning = false;
        #endregion


        #region Constructor
        public AsyncRelayCommand(Func<Task> execute)
        {
            _execute = execute;
        }
        #endregion


        #region Event 
        public event EventHandler CanExecuteChanged;
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion


        #region Public Property
        public bool IsRunning
        {
            get => _IsRunning;
            private set
            {
                _IsRunning = value;
                this.OnPropertyChanged();
            }
        }
        #endregion

        #region Private Functions
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); 
            }
        }
        #endregion

        #region Public Functions
        public void InvalidateCommand() => this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        #endregion


        #region Evnet Handler
        public bool CanExecute(object parameter)
        {
            return this._IsRunning;
        }

        public async void Execute(object parameter)
        {
            if (!this.CanExecute(parameter)) return;

            if (_execute != null)
            {
                var task = _execute();
                try
                {
                    IsRunning = true;
                    this.InvalidateCommand();
                    await task;
                }
                finally
                {
                    IsRunning = false;
                    this.InvalidateCommand();
                }
            }
        }
        #endregion
    }


    public class AsyncRelayCommand<T> : IAsyncRelayCommand, INotifyPropertyChanged
    {
        #region Private Property
        private readonly Func<T, Task> _execute = null;
        private readonly Predicate<T> _canExecute = null;
        private bool _IsRunning = false;
        #endregion

        #region Constructor
        public AsyncRelayCommand(Func<T, Task> execute)
        {
            _execute = execute;
        }

        public AsyncRelayCommand(Func<T, Task> execute, Predicate<T> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));
            if (canExecute == null)
                throw new ArgumentNullException(nameof(canExecute));

            _execute = execute;
            _canExecute = canExecute;

        }
        #endregion

        #region Event 
        public event EventHandler CanExecuteChanged;
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Private Functions
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region Public Property
        public bool IsRunning
        {
            get => _IsRunning;
            private set
            {
                _IsRunning = value;
                this.OnPropertyChanged();
            }
        }
        #endregion

        #region Public Functions
        public void InvalidateCommand() => this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        #endregion

        #region Event Handler
        public bool CanExecute(object parameter)
        {

            if (_canExecute == null && !this._IsRunning)
                return true;


            return !this._IsRunning && _canExecute.Invoke((T)parameter);
        }

        public async void Execute(object parameter)
        {

            if (!this.CanExecute(parameter)) return;

            if (_execute != null)
            {
                var task = _execute((T)parameter);

                try
                {
                    IsRunning = true;
                    this.InvalidateCommand();
                    await task;
                }
                finally
                {
                    IsRunning = false;
                    this.InvalidateCommand();
                }
            }

        }
        #endregion
    }
}
