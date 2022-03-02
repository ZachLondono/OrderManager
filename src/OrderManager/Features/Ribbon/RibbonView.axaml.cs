using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OrderManager.Features.LoadOrders;
using OrderManager.Features.ProductDesigner;
using OrderManager.Features.RemakeOrder;
using OrderManager.Shared;
using ReactiveUI;
using System.Reactive;
using System.Threading.Tasks;

namespace OrderManager.Features.Ribbon;
public partial class RibbonView : ReactiveUserControl<RibbonViewModel> {

    public RibbonView() {
        InitializeComponent();
        DataContext = Program.CreateInstance<RibbonViewModel>();

        this.WhenActivated(d => d(ViewModel!.ShowNewOrderDialog.RegisterHandler(DoShowNewOrderDialogAsync)));
        this.WhenActivated(d => d(ViewModel!.ShowNewProductDialog.RegisterHandler(DoShowNewProductDialogAsync)));
        this.WhenActivated(d => d(ViewModel!.ShowOrderRemakeDialog.RegisterHandler(DoShowOrderRemakeDialogAsync)));
    }

    private async Task DoShowNewOrderDialogAsync(InteractionContext<NewOrderViewModel, Unit> interaction) {
        await CreateDialog<NewOrderViewModel, NewOrderDialog>(interaction);
    }

    private async Task DoShowNewProductDialogAsync(InteractionContext<ProductDesignerViewModel, Unit> interaction) {
        await CreateDialog<ProductDesignerViewModel, NewProductDialog>(interaction);
    }

    private async Task DoShowOrderRemakeDialogAsync(InteractionContext<OrderRemakeViewModel, Unit> interaction) {
        await CreateDialog<OrderRemakeViewModel, OrderRemakeDialog>(interaction);
    }

    private async Task CreateDialog<Tvm, Tv>(InteractionContext<Tvm, Unit> interaction) where Tv : Window where Tvm : ViewModelBase {
        var dialog = Program.CreateInstance<Tv>();
        dialog.DataContext = interaction.Input;
        var window = Program.GetService<MainWindow.MainWindow>();
        await dialog.ShowDialog(window);
        interaction.SetOutput(new Unit());
    }

    private void InitializeComponent() {
        AvaloniaXamlLoader.Load(this);
    }
}
