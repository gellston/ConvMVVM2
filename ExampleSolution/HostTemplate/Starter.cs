using HostTemplate.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HostTemplate
{

    public class Starter
    {

        [STAThread]
        public static void Main(string[] args)
        {
            var host = ConvMVVM2.WPF.Host.ConvMVVM2Host.CreateHost<BootStrapper, Application>(args, "HostTemplate");
            host.Build()
                .ShutdownMode(ShutdownMode.OnMainWindowClose)
                .Popup<MainWindow>(dialog: true)
                //.Popup("MainWindow")
                .RunApp();

        }
    }
}
