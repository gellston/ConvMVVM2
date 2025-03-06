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
                var bootStrapper = ConvMVVM2Host.CreateStrapper<BootStrapper, Application>();
                bootStrapper.Run("MainWindowView");

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

                
        }
    }
}
