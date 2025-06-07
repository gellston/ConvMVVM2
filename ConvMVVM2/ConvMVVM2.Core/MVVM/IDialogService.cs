using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConvMVVM2.Core.MVVM
{
    public interface IDialogService
    {
        #region Public Property

        public ResultTYPE ShowDialog<ResultTYPE>(string windowName, string viewName, double width, double height, Core.MVVM.ResizeMode resizeMode = Core.MVVM.ResizeMode.CanResize, string title = "") where ResultTYPE : class;

        public ResultTYPE ShowDialog<ResultTYPE>(string viewName, double width, double height, Core.MVVM.ResizeMode resizeMode = Core.MVVM.ResizeMode.CanResize, string title = "") where ResultTYPE : class;

        public ResultTYPE ShowDialog<ResultTYPE, ParamType>(string windowName, string viewName, double width, double height, ParamType param, Core.MVVM.ResizeMode resizeMode = Core.MVVM.ResizeMode.CanResize, string title = "") where ResultTYPE : class;

        public ResultTYPE ShowDialog<ResultTYPE, ParamType>(string viewName, double width, double height, ParamType param, Core.MVVM.ResizeMode resizeMode = Core.MVVM.ResizeMode.CanResize, string title = "") where ResultTYPE : class;


        public void Show(string viewName, double width, double height, Core.MVVM.ResizeMode resizeMode = Core.MVVM.ResizeMode.CanResize, string title = "");

        public void Show(string windowName, string viewName, double width, double height, Core.MVVM.ResizeMode resizeMode = Core.MVVM.ResizeMode.CanResize, string title = "");

        public void Show<ParamType>(string viewName, double width, double height, ParamType param, Core.MVVM.ResizeMode resizeMode = Core.MVVM.ResizeMode.CanResize, string title = "");

        public void Show<ParamType>(string windowName, string viewName, double width, double height, ParamType param, Core.MVVM.ResizeMode resizeMode = Core.MVVM.ResizeMode.CanResize, string title = "");


        public string[] OpenFileDialog(string defaultPath, string title, string filter, bool multiselect = true);

        public bool SaveFileDialog(string defaultPath, string title, string filter);



        public string[] OpenFolderDialog(string defaultPath, string title, bool multiselect = false);



        #endregion

    }
}
