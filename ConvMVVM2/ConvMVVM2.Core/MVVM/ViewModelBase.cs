using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ConvMVVM2.Core.MVVM
{
    public class ViewModelBase : INotifyPropertyChanged, INotifyPropertyChanging
    {
        #region Event
        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangingEventHandler PropertyChanging;
        #endregion

        #region Local Undo Fields

        private readonly Stack<IUndoAction> _UndoStack = new Stack<IUndoAction>();
        private readonly Stack<IUndoAction> _RedoStack = new Stack<IUndoAction>();

        private bool _isUndoEnabled = false;
        private bool _isInUndoOrRedo = false;

        #endregion


        #region Public Property

        public bool IsUndoEnabled
        {
            get => _isUndoEnabled;
            set
            {
                if (_isUndoEnabled == value)
                    return;

                _isUndoEnabled = value;

                if (!value)
                {
                    _UndoStack.Clear();
                    _RedoStack.Clear();
                    OnPropertyChanged(nameof(CanUndo));
                    OnPropertyChanged(nameof(CanRedo));
                }

                OnPropertyChanged();
            }
        }

        public int MaxUndoCount { get; set; } = 100;

        public bool CanUndo => _isUndoEnabled && _UndoStack.Count > 0;
        public bool CanRedo => _isUndoEnabled && _RedoStack.Count > 0;
        #endregion

        #region Public Functions


        public void Undo()
        {
            if (!CanUndo)
                return;

            var action = _UndoStack.Pop();

            _isInUndoOrRedo = true;
            try
            {
                action.Undo();
            }
            finally
            {
                _isInUndoOrRedo = false;
            }

            _RedoStack.Push(action);

            OnPropertyChanged(nameof(CanUndo));
            OnPropertyChanged(nameof(CanRedo));
        }

        public void Redo()
        {
            if (!CanRedo)
                return;

            var action = _RedoStack.Pop();

            _isInUndoOrRedo = true;
            try
            {
                action.Redo();
            }
            finally
            {
                _isInUndoOrRedo = false;
            }

            _UndoStack.Push(action);

            OnPropertyChanged(nameof(CanUndo));
            OnPropertyChanged(nameof(CanRedo));
        }

        public void Do(IUndoAction action)
        {
            if (_isUndoEnabled && !_isInUndoOrRedo)
            {

                if (action == null) throw new ArgumentNullException(nameof(action));

                if (MaxUndoCount > 0 && _UndoStack.Count >= MaxUndoCount)
                {
                    RemoveOldestUndo();
                }

                _UndoStack.Push(action);
                _RedoStack.Clear();

                OnPropertyChanged(nameof(CanUndo));
                OnPropertyChanged(nameof(CanRedo));
            }

        }
        #endregion

        #region Private Functions
        private void RemoveOldestUndo()
        {
            if (_UndoStack.Count == 0)
                return;

            var items = _UndoStack.ToArray();

            _UndoStack.Clear();

            for (int i = 0; i < items.Length - 1; i++)
            {
                _UndoStack.Push(items[i]);
            }
        }
        #endregion




        #region Protected Functions





        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanging([CallerMemberName] string propertyName = null)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
                return false;

            var oldValue = storage;

            OnPropertyChanging(propertyName);

            Do(new PropertyUndoAction<T>(
                target: this,
                propertyName: propertyName,
                oldValue: oldValue,
                newValue: value));

            storage = value;

            OnPropertyChanged(propertyName);
            return true;
        }
        #endregion
    }

}
