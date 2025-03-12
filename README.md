
**ConvMVVM2.Core**
[![latest version](https://img.shields.io/nuget/v/ConvMVVM2.Core)](https://www.nuget.org/packages/ConvMVVM2.Core)
[![downloads](https://img.shields.io/nuget/dt/ConvMVVM2.Core)](https://www.nuget.org/packages/ConvMVVM2.Core)

**ConvMVVM2.WPF**
[![latest version](https://img.shields.io/nuget/v/ConvMVVM2.WPF)](https://www.nuget.org/packages/ConvMVVM2.WPF)
[![downloads](https://img.shields.io/nuget/dt/ConvMVVM2.WPF)](https://www.nuget.org/packages/ConvMVVM2.WPF)

<center>

<img src="https://github.com/gellston/ConvMVVM2/blob/main/ConvMVVM2/Icon/convergence.png?raw=true"/>


</center>

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


Property
=======================
```csharp
#region Public Property
[Property]
private string _TestText = "test";
#endregion

#region Event Handler
partial void OnTestTextChanged(string oldValue, string newValue)
{
    
}
partial void OnTestTextChanged(string value)
{
   
}
partial void OnTestTextChanging(string oldValue, string newValue)
{

}
partial void OnTestTextChanging(string value)
{

}
#endregion
```

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




LocalizeService
=======================
```xml
<!-- MainView -->
<UserControl x:Class="LocalizeApp.Views.MainView"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" 
             xmlns:d="http://schemas.microsoft.com/expression/blend/2008" 
             xmlns:local="clr-namespace:LocalizeApp.Views"
             mc:Ignorable="d" 
             d:DesignHeight="450" d:DesignWidth="800"
             xmlns:convMVVM2="https://github.com/gellston/ConvMVVM2">
    <DockPanel Background="Orange">
        <Button DockPanel.Dock="Bottom"
                Height="50"
                Content="Change Local"
                FontSize="35"
                Command="{Binding TestCommand}"></Button>
        <TextBlock Text="{convMVVM2:Localize TestString}"></TextBlock>
    </DockPanel>
</UserControl>
```

```csharp
//MainViewModel
[RelayCommand]
private void Test()
{
    try
    {
        this.switchLocal = !this.switchLocal;

        if (this.switchLocal)
            this.localizeService.ChangeLocal("en");
        else
            this.localizeService.ChangeLocal("kr");
    }
    catch
    {

    }
}
```

```csharp
// BootStrapper
protected override void OnStartUp(IServiceContainer container)
{
    var localizeService = container.GetService<ILocalizeService>();
    localizeService.SetResourceManager(Properties.Resource.ResourceManager);
    
}
```

> it can change local setting easily from resource manager



Module
=======================

```csharp
//BootStrapper
public class BootStrapper : AppBootstrapper
{
    protected override void OnStartUp(IServiceContainer container)
    {

    }

    protected override void RegionMapping(IRegionManager layerManager)
    {

    }

    protected override void RegisterModules()
    {
        this.EnableAutoModuleSearch(true);
        this.AddModuleRelativePath("Modules");
    }

    protected override void RegisterServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<MainWindow>();
    }

    protected override void ViewModelMapping(IViewModelMapper viewModelMapper)
    {
    }
}
```

```csharp
//AModule
public class Module : IModule
{
    public string ModuleName => "AModule";
    public string ModuleVersion => "1.0";
    public void OnStartUp(IServiceContainer container)
    {
    }
    public void RegionMapping(IRegionManager layerManager)
    {
        layerManager.Mapping<AView>("ContentView");
    }
    public void RegisterServices(IServiceCollection container)
    {
        container.AddSingleton<AView>();

        container.AddSingleton<AViewModel>();
    }
    public void ViewModelMapping(IViewModelMapper viewModelMapper)
    {
    }
}
```

```batch
# Post Build Script to move module dll in Modules folder
if not exist "$(SolutionDir)\\ModuleApp\\bin\\$(ConfigurationName)\\net9.0-windows\\Modules" mkdir "$(SolutionDir)\\ModuleApp\\bin\\$(ConfigurationName)\\net9.0-windows\\Modules"
xcopy "$(SolutionDir)\\AModule\\bin\\$(ConfigurationName)\\net9.0-windows\\" "$(SolutionDir)\\ModuleApp\\bin\\$(ConfigurationName)\\net9.0-windows\\Modules\\" /c /i /e /h /y
xcopy "$(SolutionDir)\\BModule\\bin\\$(ConfigurationName)\\net9.0-windows\\" "$(SolutionDir)\\ModuleApp\\bin\\$(ConfigurationName)\\net9.0-windows\\Modules\\" /c /i /e /h /y
xcopy "$(SolutionDir)\\CModule\\bin\\$(ConfigurationName)\\net9.0-windows\\" "$(SolutionDir)\\ModuleApp\\bin\\$(ConfigurationName)\\net9.0-windows\\Modules\\" /c /i /e /h /y
xcopy "$(SolutionDir)\\MainModule\\bin\\$(ConfigurationName)\\net9.0-windows\\" "$(SolutionDir)\\ModuleApp\\bin\\$(ConfigurationName)\\net9.0-windows\\Modules\\" /c /i /e /h /y

```
> ConvMVVM2 also support modulus development

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


[image_ref_q20jjqt0]: data:image/png;base64,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
