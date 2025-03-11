using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LocalizeApp
{
    public class Starter
    {

        [STAThread]
        public static void Main(string[] args)
        {
            try
            {

                var host = ConvMVVM2.WPF.Host.ConvMVVM2Host.CreateHost<BootStrapper, Application>(args, "LocalizeApp");
                host.Build()
                    .Popup("MainWindow", dialog: true)
                    .RunApp();
            }
            catch
            {

            }
        }
    }
}
