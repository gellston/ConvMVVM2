using System;
using System.Collections.Generic;
using System.Text;

namespace ConvMVVM2.Core.MVVM
{
    public interface IUndoAction
    {
        void Undo();
        void Redo();
    }
}
