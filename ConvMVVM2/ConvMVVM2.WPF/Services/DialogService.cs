using ConvMVVM2.Core.MVVM;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace ConvMVVM2.WPF.Services
{
    public class DialogService : IDialogService
    {
        #region Private Property
        private readonly IServiceContainer serviceContainer;
        #endregion

        #region Constructor
        public DialogService(IServiceContainer serviceContainer)
        {

            this.serviceContainer = serviceContainer;
        }


        #endregion

        #region Public Functions
        public void Show(string viewName, double width, double height, Core.MVVM.ResizeMode resizeMode = Core.MVVM.ResizeMode.CanResize)
        {
            try
            {
                var view = (FrameworkElement)serviceContainer.GetService(viewName);
                var window = new Window();
                window.Width = width;
                window.Height = height;
                window.ResizeMode = (System.Windows.ResizeMode)resizeMode;
                window.WindowStartupLocation = WindowStartupLocation.CenterScreen;

                window.Content = view;
                window.Activate();
                window.Show();
              
            }
            catch
            {
                throw;
            }
        }

        public ResultTYPE ShowDialog<ResultTYPE>(string viewName, double width, double height, Core.MVVM.ResizeMode resizeMode = Core.MVVM.ResizeMode.CanResize) where ResultTYPE : class
        {
            try
            {
                var view = (FrameworkElement)serviceContainer.GetService(viewName);
                var vm = view.DataContext as IDialogViewModel<ResultTYPE>;
                if (vm == null)
                {
                    throw new Exception("Invalid Dialog ViewModel Interface");
                }

                var window = new Window();

                window.Width = width;
                window.Height = height;
                window.ResizeMode = (System.Windows.ResizeMode)resizeMode;
                window.WindowStartupLocation = WindowStartupLocation.CenterScreen;

                ResultTYPE dialogResult = null;
                Action<ResultTYPE> closeEventHandler = (arg) =>
                {
                    try
                    {
                        window.Close();
                    }
                    catch
                    {

                    }

                    dialogResult = arg;
                };

                vm.CloseEvent += closeEventHandler;
                window.Content = view;
                window.Activate();
                var result = (bool)window.ShowDialog();
                window.Content = null;
                vm.CloseEvent -= closeEventHandler;

                return dialogResult;
            }
            catch
            {
                throw;
            }
        }




        public string OpenFileDialog(string defaultPath, string title, string filter, bool multiselect = true)
        {
            try
            {

                var dialog = new Microsoft.Win32.OpenFileDialog();
                dialog.InitialDirectory = defaultPath;
                dialog.Filter = filter;
                dialog.Multiselect = multiselect;
                dialog.Title = title;
                bool? result = dialog.ShowDialog();
                if(result == false)
                {
                    throw new InvalidOperationException("File is not selected");
                }

                return dialog.FileName;

            }
            catch
            {
                throw;
            }
        }

        public bool SaveFileDialog(string defaultPath, string title, string filter)
        {
            try
            {

                var dialog = new Microsoft.Win32.SaveFileDialog();
                dialog.InitialDirectory = defaultPath;
                dialog.Filter = filter;
                dialog.Title= title;
                bool? result = dialog.ShowDialog();
                return (bool)result;
            }
            catch
            {
                throw;
            }
        }


  
        #endregion


    }
}
