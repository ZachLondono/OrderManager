using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OrderManager.Features.LoadOrders;
using ReactiveUI;
using System.Reactive;
using System.Threading.Tasks;

namespace OrderManager.Features.Ribbon;
public partial class RibbonView : ReactiveUserControl<RibbonViewModel> {

    public RibbonView() {
        InitializeComponent();
        DataContext = new RibbonViewModel();

        this.WhenActivated(d => d(ViewModel!.ShowDialog.RegisterHandler(DoShowDialog)));
    }

    private async Task DoShowDialog(InteractionContext<NewOrderViewModel, Unit> interaction) {
        var dialog = new NewOrderDialog();
        dialog.DataContext = interaction.Input;
        // This seems wrong...
        // TODO: Figure out how to correctly create the dialog
        var window = Program.GetService<MainWindow.MainWindow>();
        await dialog.ShowDialog(window);
        interaction.SetOutput(new Unit());
    }

    private void InitializeComponent() {
        AvaloniaXamlLoader.Load(this);
    }
}
