using ConvMVVM2.WPF.Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
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
                var bootStrapper = ConvMVVM2Host.CreateStrapper<BootStrapper>();
                bootStrapper.Run("MainWindowView");
                    
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

                
        }
    }
}
