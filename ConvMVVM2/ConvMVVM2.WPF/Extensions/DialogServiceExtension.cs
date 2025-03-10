using ConvMVVM2.Core.MVVM;
using ConvMVVM2.WPF.Host;
using ConvMVVM2.WPF.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvMVVM2.WPF.Extensions
{
    public static class DialogServiceExtension
    {

        #region Public Static Functions
        public static ConvMVVM2Host AddWPFDialogService(this ConvMVVM2Host host)
        {
            try
            {
                host.ServiceCollection.AddSingleton<IDialogService, DialogService>();


                return host;
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
