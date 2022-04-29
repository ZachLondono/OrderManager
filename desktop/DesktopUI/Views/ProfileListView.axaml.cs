using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using DesktopUI.Common;
using DesktopUI.ViewModels;
using ReactiveUI;
using System.Reactive;
using System.Threading.Tasks;

namespace DesktopUI.Views;

public partial class ProfileListView : ReactiveUserControl<ProfileListViewModel> {

    public ProfileListView() {
        InitializeComponent();

        this.WhenActivated(d => d(ViewModel!.ShowDialog.RegisterHandler(DoShowDialogAsync)));
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


    private Window? GetParentWindow() {

        IControl? control = this;
        while (control is not null) {
            if (control is Window window) return window;
            control = control.Parent;
        }

        return null;

    }

}
