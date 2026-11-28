using Avalonia.Controls;

namespace YHMIS.AvaloniaUI.Abstractions;

public interface IMainWindow
{
    Window Self {get;}
    void InsertMainView(IMainView mainView);
}