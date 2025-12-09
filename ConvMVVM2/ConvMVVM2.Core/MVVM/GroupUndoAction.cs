using System;
using System.Collections.Generic;
using System.Text;

namespace ConvMVVM2.Core.MVVM
{
    internal sealed class GroupUndoAction : IUndoAction, IDisposable
    {
        #region Private Property
        private readonly List<IUndoAction> undoActions = new List<IUndoAction>();
        private UndoService? _owner;
        private bool disposed = false;
        #endregion

        #region Constructor
        public GroupUndoAction(UndoService owner) { 
        
            this._owner = owner;
        }
        #endregion

        #region Destructor
        public void Dispose()
        {

            if(this.disposed) return;
            this.disposed = true;

            if (_owner != null)
            {
                _owner.EndGroup();
                _owner = null;
            }

        }
        #endregion

        #region Public Property
        public bool IsEmpty
        {
            get
            {
                return undoActions.Count == 0;
            }
        }
        #endregion

        #region Public Functions
        public void Add(IUndoAction action)
        {
            if (action == null)
                return;

            undoActions.Add(action);
        }


        public void Redo()
        {
            for (int i = 0; i < undoActions.Count; i++)
            {
                undoActions[i].Redo();
            }
        }


        public void Undo()
        {
            for (int i = undoActions.Count - 1; i >= 0; i--)
            {
                undoActions[i].Undo();
            }
        }
        #endregion
    }
}
