
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
var collection = ConvMVVM.Core.DI.ServiceCollection.Create();
//it suport constructor injection 
collection.RegisterCache<AModel>();
collection.RegisterCache<IBModel, BModel>();

//it support lambda creation 
collection.RegisterCache<CModel>((container) =>{
    var aModel = container.GetService<AModel>();
    var bModel = container.GetService<IBModel>();
    var model = CModel(aModel, bModel);
    return model;
})

collection.RegisterCache<DModel>(new DModel());

//ioc container creation
var container = collection.CreateContainer();
var aModel1 = container.GetService<AModel>();
var bModel1 = container.GetService<IBModel>();
var dModel = container.GetService<DModel>();
```

> it support constructor injection and lambda creation routine


Property 
=======================
```csharp

partial class AViewModel : NotifyObject
{
    public AViewModel() { 
    }

    //it support code generator
    [Property]
    private string _Test1 = "";

    private string _Test2 = "";
    public string Test2
    {
        get => _Test2;
        set => Property(ref _Test2, value);
    }
}
```
> it support property changed notification and source generator for property


RelayCommand
=======================
```csharp
partial class MainWindowViewModel : NotifyObject
{
    public MainWindowViewModel() { 
    }

    [RelayCommand]
    private void Test()
    {
        System.Diagnostics.Debug.WriteLine("no delay!!");
    }
    
    [AsyncRelayCommand]
    private async Task AsyncTest()
    {
        await Task.Delay(10000);
        System.Diagnostics.Debug.WriteLine("delay!!");
    }

}
```
> it support RelayCommand and AsyncRelayCommand also support source generator for them


ViewModelLocator
=======================
```xml
<Window x:Class="RelayCommandExample.View.MainWindowView"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:RelayCommandExample"
        mc:Ignorable="d"
        Title="MainWindow" Height="450" Width="800"
        xmlns:convMVVM="https://github.com/gellston/ConvMVVM"
        convMVVM:ViewModelLocator.AutoWireViewModel="True"> 
        //Auto Wire ViewModel//
</Window>
```



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
