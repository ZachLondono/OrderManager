using Avalonia.Controls;

namespace DesktopUI.ViewModels;

public class ToolWindowViewModel : ViewModelBase {

    public IControl WindowContent { get; init; }

    public ToolWindowViewModel(IControl windowContent) { 
        WindowContent = windowContent;
    }

}