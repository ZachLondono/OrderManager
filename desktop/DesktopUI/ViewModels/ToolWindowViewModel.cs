using Avalonia.Controls;
using ReactiveUI;
using System;
using System.Threading.Tasks;

namespace DesktopUI.ViewModels;

public class ToolWindowViewModel : ViewModelBase {

    private bool _isLoading = false;
    public bool IsLoading {
        get => _isLoading;
        set => this.RaiseAndSetIfChanged(ref _isLoading, value);
    }

    private Func<Task>? _loadDataFunc;

    // TODO: add mechanisim for replacing the content in the window and resizing the window
    public IControl WindowContent { get; init; }

    public ToolWindowViewModel(IControl windowContent) { 
        WindowContent = windowContent;
    }

    public ToolWindowViewModel(IControl windowContent, Func<Task>? loadDataFunc) : this(windowContent){
        _loadDataFunc = loadDataFunc;
    }

    public async Task LoadData() {

        if (_loadDataFunc is null) return;

        IsLoading = true;
        await _loadDataFunc();
        IsLoading = false;

    }

}