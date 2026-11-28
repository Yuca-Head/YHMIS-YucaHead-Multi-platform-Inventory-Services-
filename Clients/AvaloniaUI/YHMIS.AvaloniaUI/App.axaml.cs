using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Templates;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using YHMIS.AvaloniaUI.Abstractions;
using YHMIS.AvaloniaUI.ViewModels;
using YHMIS.AvaloniaUI.ViewModels.Login;


namespace YHMIS.AvaloniaUI;

public partial class App : Application
{

    public override void Initialize()
    {
        
        AvaloniaXamlLoader.Load(this);
#if DEBUG
        this.AttachDeveloperTools();
#endif
    }

    public override void OnFrameworkInitializationCompleted()
    {
        

        var host = HostAppBuilder.Host;

        this.DataTemplates.Add(HostAppBuilder.ViewLocator ?? 
        throw new ArgumentNullException());


        var mainView = host.Services.GetRequiredService<IMainView>() ?? 
        throw new ArgumentNullException();

        mainView.Self.DataContext = host is not null ? 
        host.Services.GetRequiredService<MainViewModel>() :
        throw new InvalidOperationException("Host must be created using HostAppBuilder.Build()");


        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var mainWindow = host.Services.GetRequiredService<IMainWindow>() ?? 
            throw new ArgumentNullException();

            mainWindow.InsertMainView(mainView);

            //System.Console.WriteLine(HostAppBuilder.ViewLocator.Build(new LoginViewModel()));
            
            desktop.MainWindow = mainWindow.Self ??
            throw new InvalidOperationException("When app lifetime is desktop main window cannot be null");
        }
        else if (ApplicationLifetime is IActivityApplicationLifetime singleViewFactoryApplicationLifetime)
        {
            singleViewFactoryApplicationLifetime.MainViewFactory = () => mainView.Self;
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = mainView.Self;
        }
        

        base.OnFrameworkInitializationCompleted();
    }
}