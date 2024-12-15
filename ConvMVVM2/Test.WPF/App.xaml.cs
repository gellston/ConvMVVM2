using System.Configuration;
using System.Data;
using System.Windows;

namespace Test.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private BootStrapper bootStrapper = new BootStrapper();

        #region Constructor
        public App()
        {
            
            bootStrapper.Run();
            

        }
        #endregion
    }

}
