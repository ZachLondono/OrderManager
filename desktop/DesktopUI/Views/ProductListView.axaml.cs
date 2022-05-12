using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using DesktopUI.Common;
using DesktopUI.ViewModels;
using ReactiveUI;
using System.Reactive;
using System.Threading.Tasks;

namespace DesktopUI.Views;
public partial class ProductListView : ReactiveUserControl<ProductListViewModel> {
    public ProductListView() {
        InitializeComponent();

        this.WhenActivated(d =>
            d(ViewModel!.ShowDialog.RegisterHandler(DoShowDialogAsync))
        );

    }

    private void InitializeComponent() {
        AvaloniaXamlLoader.Load(this);
    }

    private async Task DoShowDialogAsync(InteractionContext<ToolWindowContent, Unit> interaction) {
        await ToolWindowOpener.OpenDialog(interaction.Input, this);
        interaction.SetOutput(Unit.Default);
    }

}
