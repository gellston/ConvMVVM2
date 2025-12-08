using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Test.WinformTest.Presenter;
using Test.WinformTest.View;

namespace Test.WinformTest
{
    internal static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            var host = ConvMVVM2.Winform.Host.ConvMVVM2Host.CreateHost<BootStrapper>(args, "App");
            host.Build()
                .RunApp<MainPresenter>();



            //Application.Run(new MainView());
        }
    }
}
