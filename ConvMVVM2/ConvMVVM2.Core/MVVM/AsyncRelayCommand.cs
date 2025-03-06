using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConvMVVM2.Core.MVVM
{
    public class AsyncRelayCommand : ICommand
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
        #endregion


        #region Public Property
        public bool IsRunning
        {
            get => _IsRunning;
            private set
            {
                _IsRunning = value;
            }
        }
        #endregion

        #region Public Functions
        public void InvalidateCommand() => this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        #endregion


        #region Evnet Handler
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            if (_execute != null)
            {
                var task = _execute();
                IsRunning = true;
                await task;
                IsRunning = false;
            }
        }
        #endregion
    }


    public class AsyncRelayCommand<T> : ICommand
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
        #endregion

        #region Public Property
        public bool IsRunning
        {
            get => _IsRunning;
            private set
            {
                _IsRunning = value;
            }
        }
        #endregion

        #region Public Functions
        public void InvalidateCommand() => this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        #endregion

        #region Event Handler
        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return true;

            return _canExecute.Invoke((T)parameter);
        }

        public async void Execute(object parameter)
        {
            if (_execute != null)
            {
                var task = _execute((T)parameter);
                IsRunning = true;
                await task;
                IsRunning = false;
            }

        }
        #endregion
    }
}
