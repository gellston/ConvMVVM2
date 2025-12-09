using System;
using System.Collections.Generic;
using System.Text;

namespace ConvMVVM2.Core.MVVM
{
    public class DelegateUndoAction : IUndoAction
    {

        #region Private Property
        private readonly Action _undo;
        private readonly Action _redo;
        #endregion

        #region Constructor
        public DelegateUndoAction(Action undo, Action redo)
        {
            _undo = undo;
            _redo = redo;
        }
        #endregion

        #region Public Functions
        public void Undo()
        {
            _undo.Invoke();
        }
        public void Redo()
        {
            _redo.Invoke();
        }
        #endregion
    }
}
