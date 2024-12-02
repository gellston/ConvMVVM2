﻿using System.Configuration;
using System.Data;
using System.Windows;

namespace Lib.Test
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var bootstrapper = new TestBootStrapper();
            bootstrapper.Run();
        }
    }

}
