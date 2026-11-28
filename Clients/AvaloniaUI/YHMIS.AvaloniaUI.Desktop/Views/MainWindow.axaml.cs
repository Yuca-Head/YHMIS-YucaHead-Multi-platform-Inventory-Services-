using Avalonia.Controls;
using YHMIS.AvaloniaUI.Abstractions;

namespace YHMIS.AvaloniaUI.Desktop.Views;

public partial class MainWindow : Window, IMainWindow
{       
    public MainWindow()
    {
        InitializeComponent();
    }

    public Window Self => this;

    public void InsertMainView(IMainView mainView)
    {
        MainView.Content = mainView.Self;
    }
}