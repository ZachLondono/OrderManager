using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using DesktopUI.Common;
using DesktopUI.ViewModels;
using ReactiveUI;
using System.Reactive;
using System.Threading.Tasks;

namespace DesktopUI.Views;

public partial class LabelListView : ReactiveUserControl<LabelListViewModel> {

    public LabelListView() {
        InitializeComponent();

        this.WhenActivated(d => 
            d(ViewModel!.ShowDialog.RegisterHandler(DoShowDialogAsync))
        );

        this.WhenActivated(d =>
            d(ViewModel!.ShowFileDialogAndReturnPath.RegisterHandler(DoShowFileDialogAndReturnPathAsync))
        );

    }

    private void InitializeComponent() {
        AvaloniaXamlLoader.Load(this);
    }

    private async Task DoShowDialogAsync(InteractionContext<DialogWindowContent, Unit> interaction) {

        Window? window = GetParentWindow();
        if (window is null) return;

        var windowContent = interaction.Input;

        var dialog = new ToolWindow(windowContent.Title, windowContent.Width, windowContent.Height) {
            DataContext = new ToolWindowViewModel(windowContent.Content)
        };

        var result = await dialog.ShowDialog<Unit>(window);
        interaction.SetOutput(result);

    }

    private async Task DoShowFileDialogAndReturnPathAsync(InteractionContext<Unit, string?> interaction) {

        Window? window = GetParentWindow();
        if (window is null) return;

        OpenFileDialog dialog = new();
        dialog.Filters.Add(new FileDialogFilter() { Name = "Label", Extensions = { "label" } });

        string[]? result = await dialog.ShowAsync(window);
        if (result is null || result.Length == 0) {
            interaction.SetOutput(null);
        } else interaction.SetOutput(result[0]);

    }

    private Window? GetParentWindow() {

        IControl? control = this;
        while (control is not null) {
            if (control is Window window) return window;
            control = control.Parent;
        }

        return null;

    }

}
