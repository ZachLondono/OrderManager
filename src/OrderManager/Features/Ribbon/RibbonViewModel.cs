using Avalonia.Interactivity;
using OrderManager.Features.LoadOrders;
using OrderManager.Features.ProductDesigner;
using OrderManager.Shared;
using ReactiveUI;
using System;
using System.Reactive;
using System.Reactive.Linq;

namespace OrderManager.Features.Ribbon;

public class RibbonViewModel : ViewModelBase {

    public Interaction<NewOrderViewModel, Unit> ShowNewOrderDialog { get; }
    public ReactiveCommand<Unit, Unit> OpenNewOrderDialogCommand { get; }

    public Interaction<ProductDesignerViewModel, Unit> ShowNewProductDialog { get; }
    public ReactiveCommand<Unit, Unit> OpenProductDesignerDialogCommand { get; }

    public RibbonViewModel() {

        ShowNewOrderDialog = new();
        OpenNewOrderDialogCommand = ReactiveCommand.CreateFromTask(async () => {
            var vm = Program.CreateInstance<NewOrderViewModel>();
            await ShowNewOrderDialog.Handle(vm);
        });

        ShowNewProductDialog = new();
        OpenProductDesignerDialogCommand = ReactiveCommand.CreateFromTask(async () => {
            var vm = new ProductDesignerViewModel() {
                Attributes = {
                    new() { Name = "Height", DefaultValue = "4.125"},
                    new() { Name = "Width", DefaultValue = "21"},
                    new() { Name = "Depth", DefaultValue = "21"}
                }
            };
            await ShowNewProductDialog.Handle(vm);
        });

    }

}
