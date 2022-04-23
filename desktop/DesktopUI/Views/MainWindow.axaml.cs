using Avalonia.Controls;
using Avalonia.ReactiveUI;
using DesktopUI.Common;
using DesktopUI.ViewModels;
using ReactiveUI;
using System.Reactive;
using System.Threading.Tasks;

namespace DesktopUI.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel> {

    public MainWindow() {
        InitializeComponent();

        this.WhenActivated(d => d(ViewModel!.ShowDialog.RegisterHandler(DoShowDialogAsync)));
        this.WhenActivated(d => d(ViewModel!.ShowFileDialogAndReturnPath.RegisterHandler(DoShowFileDialogAndReturnPathAsync)));

    }

    private async Task DoShowDialogAsync(InteractionContext<DialogWindowContent, Unit> interaction) {

        var windowContent = interaction.Input;

        var dialog = new ToolWindow(windowContent.Title, windowContent.Width, windowContent.Height) {
            DataContext = new ToolWindowViewModel(windowContent.Content)
        };

        var result = await dialog.ShowDialog<Unit>(this);
        interaction.SetOutput(result);

    }

    private async Task DoShowFileDialogAndReturnPathAsync(InteractionContext<Unit, string?> interaction) {

        OpenFileDialog dialog = new();
        dialog.Filters.Add(new FileDialogFilter() { Name = "Label", Extensions = { "label" } });

        string[]? result = await dialog.ShowAsync(this);
        if (result is null || result.Length == 0) {
            interaction.SetOutput(null);
        } else interaction.SetOutput(result[0]);

    }

}
