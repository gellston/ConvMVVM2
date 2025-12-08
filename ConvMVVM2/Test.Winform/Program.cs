using ConvMVVM2.Winform.Host;
using Test.Winform.Presenters;

namespace Test.Winform
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] arg)
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            var host = ConvMVVM2Host.CreateHost<BootStrapper>(arg, "App");
            host.OnModuleAddEvent += Program_OnModuleAddEvent;
            host
                .Build()
                .RunApp<MainPresenter>();
            

        }

        private static void Program_OnModuleAddEvent(string arg1, string arg2)
        {
            System.Diagnostics.Debug.WriteLine("test");
        }
    }
}