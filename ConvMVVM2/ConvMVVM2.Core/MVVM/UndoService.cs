using System;
using System.Collections.Generic;
using System.Text;

namespace ConvMVVM2.Core.MVVM
{
    internal class UndoService : IUndoService
    {
        #region Private Property
        private readonly Stack<IUndoAction> _undoStack = new Stack<IUndoAction>();
        private readonly Stack<IUndoAction> _redoStack = new Stack<IUndoAction>();
        private GroupUndoAction? _currentGroup;
        #endregion

        #region Public Property

        public int MaxUndoCount { get; set; } = 100;

        public bool CanUndo => _undoStack.Count > 0;

        public bool CanRedo => _redoStack.Count > 0;

        public bool IsGrouping => _currentGroup != null;

        #endregion

        #region Private Functions

        internal void EndGroup()
        {
            if (_currentGroup == null) return;

            if (!_currentGroup.IsEmpty)
            {
                PushUndo(_currentGroup); 
                _redoStack.Clear();
            }

            _currentGroup = null;
        }


        private void PushUndo(IUndoAction action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            if (MaxUndoCount > 0 && _undoStack.Count >= MaxUndoCount)
            {
                RemoveOldestUndo();
            }

            _undoStack.Push(action);
        }

        private void RemoveOldestUndo()
        {
            if (_undoStack.Count == 0)
                return;

            var items = _undoStack.ToArray();

            _undoStack.Clear();

            for (int i = 0; i < items.Length - 1; i++)
            {
                _undoStack.Push(items[i]);
            }
        }

        #endregion

        #region Public Functions

        public IDisposable BeginGroup()
        {
            if (_currentGroup != null)
                throw new InvalidOperationException("Begin group already started");

            _currentGroup = new GroupUndoAction(this);

            return _currentGroup;
        }

        public void Do(IUndoAction action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            _redoStack.Clear();

            if (_currentGroup != null)
            {
                _currentGroup.Add(action);
            }
            else
            {
                PushUndo(action);
            }
        }

        public void Redo()
        {
            if (!CanRedo) return;

            var action = _redoStack.Pop();
            action.Redo();
            PushUndo(action); // Redo된 것도 다시 Undo 대상으로 들어가야 함
        }

        public void Undo()
        {
            if (!CanUndo) return;

            var action = _undoStack.Pop();
            action.Undo();
            _redoStack.Push(action);
        }

        #endregion
    }
}
