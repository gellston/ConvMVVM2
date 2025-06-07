using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConvMVVM2.Core.MVVM
{
    public interface IDialogService
    {
        #region Public Property
        public ResultTYPE ShowDialog<ResultTYPE>(string viewName, double width, double height, Core.MVVM.ResizeMode resizeMode = Core.MVVM.ResizeMode.CanResize) where ResultTYPE : class;

        public void Show(string viewName, double width, double height, Core.MVVM.ResizeMode resizeMode = Core.MVVM.ResizeMode.CanResize);

        public string[] OpenFileDialog(string defaultPath, string title, string filter, bool multiselect = true);

        public bool SaveFileDialog(string defaultPath, string title, string filter);



        public string[] OpenFolderDialog(string defaultPath, string title);



        #endregion

    }
}
