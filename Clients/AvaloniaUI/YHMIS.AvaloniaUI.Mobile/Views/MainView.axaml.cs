using Avalonia.Controls;
using YHMIS.AvaloniaUI.Abstractions;

namespace YHMIS.AvaloniaUI.Mobile.Views;

public partial class MainView : UserControl, IMainView
{
    public MainView()
    {
        InitializeComponent();
    }

    public UserControl Self => this;
}