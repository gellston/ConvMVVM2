﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Test.ModuleA.View
{
    /// <summary>
    /// CWindowView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CWindowView : Window
    {
        public CWindowView()
        {
            InitializeComponent();

            var test = Application.Current;
            System.Diagnostics.Debug.WriteLine("test");
        }
    }
}
