using CommunityToolkit.Mvvm.ComponentModel;
using YHMIS.AvaloniaUI.ViewModels.Login;

namespace YHMIS.AvaloniaUI.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    public partial string Greeting { get; set; } = "Welcome to Avalonia!";

    [ObservableProperty]
    public partial LoginViewModel Login {get; private set;} = new();
}
