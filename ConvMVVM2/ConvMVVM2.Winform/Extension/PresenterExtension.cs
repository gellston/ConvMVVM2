using ConvMVVM2.Core.MVP;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvMVVM2.Winform.Extension
{
    public static class PresenterExtension
    {

        #region Puiblic Static Functions
        public static IPresenter GetPresenter(this IServiceContainer serviceProvider, Type type)
        {

            try
            {

                var presenter = (IPresenter)serviceProvider.GetService(type);
                var view = presenter.View;
                view.Presenter = presenter;


                return presenter;

            }catch {
                throw;
            }

        }

        public static T GetPresenter<T>(this IServiceContainer serviceProvider) where T : IPresenter
        {
            try
            {
                var presenter = GetPresenter(serviceProvider, typeof(T));

                return (T)presenter;
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
