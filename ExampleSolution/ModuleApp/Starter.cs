using ModuleApp.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ModuleApp
{
    public class Starter
    {

        [STAThread]
        public static void Main(string[] args)
        {
            var host = ConvMVVM2.WPF.Host.ConvMVVM2Host.CreateHost<BootStrapper, Application>(args, "ModuleApp");

            host.Build()
                .Popup<MainWindow>(dialog:true)
                .RunApp();
        }
    }
}
