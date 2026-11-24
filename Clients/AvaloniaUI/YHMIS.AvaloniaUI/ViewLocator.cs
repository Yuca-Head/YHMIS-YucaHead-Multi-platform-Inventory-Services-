using System;
using System.Diagnostics.CodeAnalysis;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using YHMIS.AvaloniaUI.ViewModels;

namespace YHMIS.AvaloniaUI;

/// <summary>
/// Given a view model, returns the corresponding view if possible.
/// </summary>
[RequiresUnreferencedCode(
    "Default implementation of ViewLocator involves reflection which may be trimmed away.",
    Url = "https://docs.avaloniaui.net/docs/concepts/view-locator")]
public class ViewLocator : IDataTemplate
{
    public Control? Build(object? param)
    {
        if (param is null)
            return null;



        var name = string.Empty;

        if (OperatingSystem.IsAndroid())
        {
            System.Console.WriteLine("We're on Android");
            name = param.GetType().FullName!.Replace("YHMIS.AvaloniaUI","YHMIS.AvaloniaUI.Android", StringComparison.Ordinal)
            .Replace("ViewModel", "View", StringComparison.Ordinal);
        }
        else if (OperatingSystem.IsWindows())
        {
            System.Console.WriteLine("We're on Windows");
            name = param.GetType().FullName!.Replace("ViewModel", "View", StringComparison.Ordinal);
        }
        var type = Type.GetType(name);
        
        System.Console.WriteLine(name);
        
        if (type != null)
        {
            return (Control)Activator.CreateInstance(type)!;
        }

        return new TextBlock { Text = "Not Found: " + name };
    }

    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }
}