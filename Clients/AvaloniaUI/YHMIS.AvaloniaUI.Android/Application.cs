using Android.App;
using Android.Runtime;
using Avalonia;
using Android.Util;
using Avalonia.Android;
using System;
using System.Diagnostics;
using Microsoft.Extensions.Hosting;
using YHMIS.AvaloniaUI.Mobile;
using Microsoft.Extensions.DependencyInjection;
using YHMIS.AvaloniaUI.Mobile.Views;
using YHMIS.AvaloniaUI.Abstractions;
using Host = YHMIS.AvaloniaUI.HostAppBuilder;

namespace YHMIS.AvaloniaUI.Android
{
    [Application]
    public class Application : AvaloniaAndroidApplication<App>
    {
        protected Application(nint javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            Host.ViewLocator = new ViewLocator();
            Host.CreateBuilder([]);
            Host.RegisterService(x => x.AddTransient<IMainView, MainView>());
            Host.Build();
        }

        protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
        {
            return base.CustomizeAppBuilder(builder)
                .WithInterFont();;
        }
    }
}
