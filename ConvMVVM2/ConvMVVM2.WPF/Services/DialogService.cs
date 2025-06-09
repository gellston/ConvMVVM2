using ConvMVVM2.Core.MVVM;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
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



        public void Show(string viewName, double width, double height, Core.MVVM.ResizeMode resizeMode = Core.MVVM.ResizeMode.CanResize, string title = "")
        {
            try
            {
                var view = (FrameworkElement)serviceContainer.GetService(viewName);
                var window = new Window();
                window.Width = width;
                window.Height = height;
                window.ResizeMode = (System.Windows.ResizeMode)resizeMode;
                window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                window.Title = title;

                window.Content = view;
                window.Activate();
                window.Show();
              
            }
            catch
            {
                throw;
            }
        }

        public void Show(string windowName, string viewName, double width, double height, Core.MVVM.ResizeMode resizeMode = Core.MVVM.ResizeMode.CanResize, string title = "")
        {
            try
            {
                var view = (FrameworkElement)serviceContainer.GetService(viewName);
                var window = serviceContainer.GetService(windowName) as Window;
                if(window == null)
                {
                    throw new InvalidOperationException("Invalid window type");
                }
                window.Width = width;
                window.Height = height;
                window.ResizeMode = (System.Windows.ResizeMode)resizeMode;
                window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                window.Title = title;

                window.Content = view;
                window.Activate();
                window.Show();

            }
            catch
            {
                throw;
            }
        }

        public void Show<ParamType>(string viewName, double width, double height, ParamType param, Core.MVVM.ResizeMode resizeMode = Core.MVVM.ResizeMode.CanResize, string title = "")
        {
            try
            {
                var view = (FrameworkElement)serviceContainer.GetService(viewName);
                if(view.DataContext is IDialogInitializable<ParamType> init)
                {
                    init.OnDialogInitialized(param);
                }

                var window = new Window();
                window.Width = width;
                window.Height = height;
                window.ResizeMode = (System.Windows.ResizeMode)resizeMode;
                window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                window.Title = title;

                window.Content = view;
                window.Activate();
                window.Show();

            }
            catch
            {
                throw;
            }
        }

        public void Show<ParamType>(string windowName, string viewName, double width, double height, ParamType param, Core.MVVM.ResizeMode resizeMode = Core.MVVM.ResizeMode.CanResize, string title = "")
        {
            try
            {
                var view = (FrameworkElement)serviceContainer.GetService(viewName);
                if (view.DataContext is IDialogInitializable<ParamType> init)
                {
                    init.OnDialogInitialized(param);
                }

                var window = serviceContainer.GetService(windowName) as Window;
                if (window == null)
                {
                    throw new InvalidOperationException("Invalid window type");
                }
                window.Width = width;
                window.Height = height;
                window.ResizeMode = (System.Windows.ResizeMode)resizeMode;
                window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                window.Title = title;

                window.Content = view;
                window.Activate();
                window.Show();

            }
            catch
            {
                throw;
            }
        }



        public ResultTYPE ShowDialog<ResultTYPE>(string windowName, string viewName, double width, double height, Core.MVVM.ResizeMode resizeMode = Core.MVVM.ResizeMode.CanResize, string title = "")
        {
            try
            {
                var view = (FrameworkElement)serviceContainer.GetService(viewName);
                var vm = view.DataContext as IDialogViewModel<ResultTYPE>;
                if (vm == null)
                {
                    throw new Exception("Invalid Dialog ViewModel Interface");
                }

                var window = serviceContainer.GetService(windowName) as Window;
                if (window == null)
                {
                    throw new InvalidOperationException("Invalid window type");
                }

                window.Width = width;
                window.Height = height;
                window.ResizeMode = (System.Windows.ResizeMode)resizeMode;
                window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                window.Title = title;


                ResultTYPE dialogResult = default;
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

        public ResultTYPE ShowDialog<ResultTYPE>(string viewName, double width, double height, Core.MVVM.ResizeMode resizeMode = Core.MVVM.ResizeMode.CanResize, string title = "")
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
                window.Title = title;

                ResultTYPE dialogResult = default;
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



        public ResultTYPE ShowDialog<ResultTYPE, ParamType>(string windowName, string viewName, double width, double height, ParamType param, Core.MVVM.ResizeMode resizeMode = Core.MVVM.ResizeMode.CanResize, string title = "")
        {
            try
            {
                var view = (FrameworkElement)serviceContainer.GetService(viewName);
                if (view.DataContext is IDialogInitializable<ParamType> init)
                {
                    init.OnDialogInitialized(param);
                }


                var vm = view.DataContext as IDialogViewModel<ResultTYPE>;
                if (vm == null)
                {
                    throw new Exception("Invalid Dialog ViewModel Interface");
                }

                var window = serviceContainer.GetService(windowName) as Window;
                if (window == null)
                {
                    throw new InvalidOperationException("Invalid window type");
                }

                window.Width = width;
                window.Height = height;
                window.ResizeMode = (System.Windows.ResizeMode)resizeMode;
                window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                window.Title = title;


                ResultTYPE dialogResult = default;
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

        public ResultTYPE ShowDialog<ResultTYPE, ParamType>(string viewName, double width, double height, ParamType param, Core.MVVM.ResizeMode resizeMode = Core.MVVM.ResizeMode.CanResize, string title = "")
        {
            try
            {
                var view = (FrameworkElement)serviceContainer.GetService(viewName);
                if (view.DataContext is IDialogInitializable<ParamType> init)
                {
                    init.OnDialogInitialized(param);
                }

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
                window.Title = title;

                ResultTYPE dialogResult = default;
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


        public string[] OpenFolderDialog(string defaultPath, string title, bool multiselect = false)
        {


            try
            {
                var dlg = new CommonOpenFileDialog
                {
                    IsFolderPicker = true,
                    Title = title,
                    InitialDirectory = defaultPath,
                    Multiselect = multiselect
                };


                var result = dlg.ShowDialog();
                if (result != CommonFileDialogResult.Ok)
                {
                    throw new InvalidOperationException("Folder is not selected");
                }

                return dlg.FileNames.ToArray();

            }
            catch
            {
                throw;
            }
        }


        private static void AddFilterFromString(CommonOpenFileDialog dialog, string filterString)
        {
            var parts = filterString.Split('|');
            for (int i = 0; i + 1 < parts.Length; i += 2)
            {
                string displayName = parts[i];
                string pattern = parts[i + 1];
                dialog.Filters.Add(new CommonFileDialogFilter(displayName, pattern));
            }
        }

        public string[] OpenFileDialog(string defaultPath, string title, string filter, bool multiselect = true)
        {
            try
            {


                var dlg = new CommonOpenFileDialog
                {
                    IsFolderPicker = false,
                    Title = title,
                    InitialDirectory = defaultPath,
                    Multiselect = multiselect,
                };

                AddFilterFromString(dlg, filter);


                var result = dlg.ShowDialog();
                if (result != CommonFileDialogResult.Ok)
                {
                    throw new InvalidOperationException("File is not selected");
                }

                return dlg.FileNames.ToArray();

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
