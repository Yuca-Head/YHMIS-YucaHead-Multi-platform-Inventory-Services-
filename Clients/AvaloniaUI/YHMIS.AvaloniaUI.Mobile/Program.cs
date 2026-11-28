using System;
using Avalonia;
using Microsoft.Extensions.DependencyInjection;
using YHMIS.AvaloniaUI.Mobile;
using YHMIS.AvaloniaUI.Mobile.Views;
using YHMIS.AvaloniaUI;
using static YHMIS.AvaloniaUI.HostAppBuilder;

namespace YHMIS.AvaloniaUI.Mobile;


/*
FOR PREVIEWER purposes
*/
sealed class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args) 
    { 
        CreateBuilder(args);


        BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);    
    }
    
    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}
