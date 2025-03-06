using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ConvMVVM2.Core.MVVM
{
    public class RelayCommand : ICommand
    {
        #region Private Property
        readonly Action _execute = null;
        #endregion


        #region Constructor
        public RelayCommand(Action execute)
        {
            _execute = execute;
        }
        #endregion


        #region Event 
        public event EventHandler CanExecuteChanged;
        #endregion

        #region Public Functions
        public void InvalidateCommand() => this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        #endregion


        #region Evnet Handler
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (_execute != null)
                _execute();
        }
        #endregion
    }


    public class RelayCommand<T> : ICommand
    {
        #region Private Property
        readonly Action<T> _execute = null;
        readonly Predicate<T> _canExecute = null;
        #endregion

        #region Constructor
        public RelayCommand(Action<T> execute)
        {
            _execute = execute;
        }

        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
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

        public void Execute(object parameter)
        {
            if (_execute != null)
                _execute((T)parameter);

        }
        #endregion
    }

}
