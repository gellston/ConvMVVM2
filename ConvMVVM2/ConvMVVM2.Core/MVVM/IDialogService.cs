using System;
using System.Collections.Generic;
using System.Text;

namespace ConvMVVM2.Core.MVVM
{
    public interface IDialogService
    {
        #region Public Property
        public DialogResult ShowDialog(string viewName, double width, double height, Core.MVVM.ResizeMode resizeMode = Core.MVVM.ResizeMode.CanResize);
        public void Show(string viewName, double width, double height, Core.MVVM.ResizeMode resizeMode = Core.MVVM.ResizeMode.CanResize);

        public string OpenFileDialog(string defaultPath, string title, string filter, bool multiselect = true);

        public bool SaveFileDialog(string defaultPath, string title, string filter);
        #endregion

    }
}
