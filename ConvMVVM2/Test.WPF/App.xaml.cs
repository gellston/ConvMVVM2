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
        private readonly BootStrapper bootstrapper = new BootStrapper();


        #region Constructor
        public App()
        {

            bootstrapper.Run();
        }
        #endregion
    }

}
