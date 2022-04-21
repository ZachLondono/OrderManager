using Avalonia.ReactiveUI;
using DesktopUI.ViewModels;
using ReactiveUI;
using System.Reactive;
using System.Threading.Tasks;
using static DesktopUI.ViewModels.MainWindowViewModel;

namespace DesktopUI.Views;
public partial class MainWindow : ReactiveWindow<MainWindowViewModel> {

    public MainWindow() {
        InitializeComponent();

        this.WhenActivated(d => d(ViewModel!.ShowDialog.RegisterHandler(DoShowDialogAsync)));

    }

    private async Task DoShowDialogAsync(InteractionContext<DialogWindowContent, Unit> interaction) {

        var windowContent = interaction.Input;

        var dialog = new ToolWindow(windowContent.Width, windowContent.Height);
        dialog.DataContext = new ToolWindowViewModel(windowContent.Content);

        var result = await dialog.ShowDialog<Unit>(this);
        interaction.SetOutput(result);

    }

}
