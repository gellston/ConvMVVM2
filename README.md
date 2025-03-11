
**ConvMVVM2.Core**
[![latest version](https://img.shields.io/nuget/v/ConvMVVM2.Core)](https://www.nuget.org/packages/ConvMVVM2.Core)
[![downloads](https://img.shields.io/nuget/dt/ConvMVVM2.Core)](https://www.nuget.org/packages/ConvMVVM2.Core)

**ConvMVVM2.WPF**
[![latest version](https://img.shields.io/nuget/v/ConvMVVM2.WPF)](https://www.nuget.org/packages/ConvMVVM2.WPF)
[![downloads](https://img.shields.io/nuget/dt/ConvMVVM2.WPF)](https://www.nuget.org/packages/ConvMVVM2.WPF)

<center> 
<img src="https://github.com/gellston/ConvMVVM2/blob/main/ConvMVVM2/Icon/convergence.png?raw=true?raw=true" width=200 /> </center> 

ConvMVVM2
=======================
ConvMVVM2 (Convergence MVVM2) is free MVVM library inspired by Community Toolkit library and Prism frameworks.


Supported .NET Runtime
=======================
 - **.NET Framework 4.8**
 - **.NET Framework 4.8.1**
 - **.NET 6**
 - **.NET 7**
 - **.NET 8**
 - **.NET 9**
 

Overview
=======================

Host Template
=======================

```csharp
[STAThread]
public static void Main(string[] args)
{
    var host = ConvMVVM2.WPF.Host.ConvMVVM2Host.CreateHost<BootStrapper, Application>(args, "HostTemplate");
    host.Build()
        .ShutdownMode(ShutdownMode.OnMainWindowClose)
        .Popup<MainWindow>(dialog: true)
        //.Popup("MainWindow")
        .RunApp();

}
```
> It support host object creation with generic template and also popup window selectively


BootStrapper
=======================
```csharp
public class BootStrapper : AppBootstrapper
{
    protected override void OnStartUp()
    {
      
    }

    protected override void RegionMapping(IRegionManager layerManager)
    {
        layerManager.Mapping<MainView>("MainView");
    }

    protected override void RegisterModules()
    {
    
    }

    protected override void RegisterServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<MainWindow>();

        serviceCollection.AddSingleton<MainViewModel>();
        serviceCollection.AddSingleton<SubViewModel>();

        serviceCollection.AddSingleton<MainView>();
        serviceCollection.AddSingleton<SubView>();
    }

    protected override void ViewModelMapping(IViewModelMapper viewModelMapper)
    {
        
    }
}
```
> Bootstrapper support ioc, module, viewmodel mapping and region mapping.


RelayCommand
=======================
```csharp
[RelayCommand]
private void Test()
{
    try
    {

    }
    catch (Exception ex)
    {
        System.Diagnostics.Debug.WriteLine(ex.ToString());
    }
}

[AsyncRelayCommand]
private async Task Test2()
{
    try
    {

    }
    catch (Exception ex)
    {
        System.Diagnostics.Debug.WriteLine(ex.ToString());
    }
}
```
> It support RelayCommand and AsyncRelayCommand also support source generator for them


ViewModelLocator
=======================
```xml
<UserControl x:Class="BootStrapperApp.Views.MainView"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" 
             xmlns:d="http://schemas.microsoft.com/expression/blend/2008" 
             xmlns:local="clr-namespace:BootStrapperApp.Views"
             mc:Ignorable="d" 
             d:DesignHeight="450" d:DesignWidth="800"
             xmlns:convMVVM2="https://github.com/gellston/ConvMVVM2"
             convMVVM2:ViewModelLocator.AutoWireViewModel="True"
             convMVVM2:ViewModelLocator.UseNamePatternMapper="True"
             convMVVM2:ViewModelLocator.UseViewModelMapper="False">
    
</UserControl>
```
> ViewModelLocator support autowire viewmodel with using name pattern mapping and manual mapping


RegionManager
=======================
```xml
<Window x:Class="BootStrapperApp.Windows.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:BootStrapperApp.Windows"
        mc:Ignorable="d"
        Title="MainWindow" Height="450" Width="800"
        xmlns:convMVVM2="https://github.com/gellston/ConvMVVM2"
        >

    <convMVVM2:WPFRegion RegionName="MainView"></convMVVM2:WPFRegion>
</Window>
```

```csharp
[RelayCommand]
private void Test()
{
    try
    {
        this.regionManager.Navigate("MainView", "SubView");
    }
    catch (Exception ex)
    {
        System.Diagnostics.Debug.WriteLine(ex.ToString());
    }
}
```

> To use regionManager, it need to place RegionControl on somewhere

License
=======================

```
The MIT License (MIT)

Copyright (c) 2022-present ConvMVVM Development Team

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```
<div style="text-align: right; margin-right:30px;"> 

[TOP](#convmvvm) 



</div>
