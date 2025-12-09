using System;
using System.Collections.Generic;
using System.Text;

namespace ConvMVVM2.Core.MVVM
{
    public interface IUndoService
    {
        bool CanUndo { get; }
        bool CanRedo { get; }

        void Do(IUndoAction action);
        void Undo();
        void Redo();
    }
}
