using ConvMVVM2.WPF.Extensions;
using ConvMVVM2.WPF.Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Test.WPF.View;

namespace Test.WPF
{
    public class TestApp
    {

        [STAThread]
        public static void Main(string[] args)
        {
            try
            {


                //일반 싱글 메인 윈도우 앱
                var host1 = ConvMVVM2Host.CreateHost<BootStrapper, Application>(args, "App");
                host1.OnModuleAddEvent += Host1_OnModuleAddEvent;
                host1.AddWPFDialogService()
                     .Build()
                     .ShutdownMode(ShutdownMode.OnExplicitShutdown)
                     .Popup("MainWindowView");
                host1.Shutdown();

               



                System.Diagnostics.Debug.WriteLine("test");

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

                
        }

        private static void Host1_OnModuleAddEvent(string version, string name)
        {
            System.Diagnostics.Debug.WriteLine("name = " + name + ",  version=" + version);
        }
    }
}
