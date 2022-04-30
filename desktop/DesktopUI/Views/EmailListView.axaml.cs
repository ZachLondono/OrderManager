using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using DesktopUI.Common;
using DesktopUI.ViewModels;
using ReactiveUI;
using System.Reactive;
using System.Threading.Tasks;

namespace DesktopUI.Views;

public partial class EmailListView : ReactiveUserControl<EmailListViewModel> {

    public EmailListView() {
        InitializeComponent();

        this.WhenActivated(d => d(ViewModel!.ShowDialog.RegisterHandler(DoShowDialogAsync)));
        this.WhenActivated(d => d(ViewModel!.ShowFileDialogAndReturnPath.RegisterHandler(DoShowFileDialogAndReturnPathAsync)));

    }

    private void InitializeComponent() {
        AvaloniaXamlLoader.Load(this);
    }
    
    private async Task DoShowDialogAsync(InteractionContext<ToolWindowContent, Unit> interaction) {
        await ToolWindowOpener.OpenDialog(interaction.Input, this);
        interaction.SetOutput(Unit.Default);
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
