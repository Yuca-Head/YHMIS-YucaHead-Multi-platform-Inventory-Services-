using System;
using System.Security.Cryptography.X509Certificates;
using Avalonia.Controls.Templates;
using Avalonia.Markup.Xaml.Templates;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YHMIS.AvaloniaUI.ViewModels;
using YHMIS.AvaloniaUI.ViewModels.Login;
using LibHost = Microsoft.Extensions.Hosting;

namespace YHMIS.AvaloniaUI;

public static class HostAppBuilder
{
    public static bool Initialized {get; private set;} = false;
    public static LibHost.IHost Host {get; private set; } = null!;
    private static LibHost.HostApplicationBuilder? Builder {get; set;}
    public static IDataTemplate? ViewLocator{get; set
        {
            if(Initialized)
                return;
            
            field = value;
        }
    }

    public static void CreateBuilder(string[] args)
    => Builder = LibHost.Host.CreateApplicationBuilder(args);
    
    private static void ConfigureServices(IServiceCollection services)
    {
        if(Initialized)
            return;

        //ViewModels
        services.AddTransient<MainViewModel>();
        services.AddTransient<LoginViewModel>();
        
    }

    /// <summary>
    /// Creates external services.
    /// </summary>
    /// <param name="action"></param>
    /// <exception cref="InvalidOperationException">When builder hasn't built yet.</exception>
    public static void RegisterService(Action<IServiceCollection> action)
    {
        if(Builder is null)
            throw new InvalidOperationException("Must create builder to build the app");
    
        action(Builder.Services);
    }


    public static void Build()
    {
        if(Initialized)
            throw new InvalidOperationException("Cannot build the app once built.");
        if(Builder is null)
            throw new InvalidOperationException("Must create builder to build the app");
        
        ConfigureServices(Builder.Services);
        
        
        Host = Builder.Build();
        
        Initialized = true;
    }



}