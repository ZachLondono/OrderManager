using Avalonia.Controls;

namespace DesktopUI.ViewModels;

public class ToolWindowViewModel : ViewModelBase {

    // TODO: add mechanisim for replacing the content in the window and resizing the window
    public IControl WindowContent { get; init; }

    public ToolWindowViewModel(IControl windowContent) { 
        WindowContent = windowContent;
    }

}