using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OrderManager.Features.LoadOrders;
using OrderManager.Features.ProductDesigner;
using OrderManager.Shared;
using ReactiveUI;
using System;
using System.Reactive;
using System.Threading.Tasks;

namespace OrderManager.Features.Ribbon;
public partial class RibbonView : ReactiveUserControl<RibbonViewModel> {

    public RibbonView() {
        InitializeComponent();
        DataContext = new RibbonViewModel();

        this.WhenActivated(d => d(ViewModel!.ShowNewOrderDialog.RegisterHandler(DoShowNewOrderDialogAsync)));
        this.WhenActivated(d => d(ViewModel!.ShowNewProductDialog.RegisterHandler(DoShowNewProductDialogAsync)));
    }

    private async Task DoShowNewOrderDialogAsync(InteractionContext<NewOrderViewModel, Unit> interaction) {
        await CreateDialog<NewOrderViewModel, NewOrderDialog>(interaction);
    }

    private async Task DoShowNewProductDialogAsync(InteractionContext<ProductDesignerViewModel, Unit> interaction) {
        await CreateDialog<ProductDesignerViewModel, NewProductDialog>(interaction);
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
