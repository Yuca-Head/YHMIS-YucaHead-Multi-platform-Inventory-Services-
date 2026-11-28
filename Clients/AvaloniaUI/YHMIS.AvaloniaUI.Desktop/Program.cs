using System;
using Avalonia;
using Microsoft.Extensions.DependencyInjection;
using YHMIS.AvaloniaUI.Abstractions;
using YHMIS.AvaloniaUI.Desktop.Views;
using static YHMIS.AvaloniaUI.HostAppBuilder;

namespace YHMIS.AvaloniaUI.Desktop;

sealed class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        HostAppBuilder.ViewLocator = new ViewLocator();
        CreateBuilder(args);

        RegisterServices();

        Build();

        BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);
    }

    private static void RegisterServices()
    {
        RegisterService(x => x.AddTransient<IMainView, MainView>());
        RegisterService(x => x.AddTransient<IMainWindow, MainWindow>());
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}
