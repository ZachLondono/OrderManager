using MediatR;
using Microsoft.Extensions.Logging;
using OrderManager.Features.DeleteOrder;
using OrderManager.Features.LoadOrders;
using OrderManager.Features.ProductDesigner;
using OrderManager.Features.RemakeOrder;
using OrderManager.Shared;
using ReactiveUI;
using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Unit = System.Reactive.Unit;

namespace OrderManager.Features.Ribbon;

public class RibbonViewModel : ViewModelBase {

    public Interaction<NewOrderViewModel, Unit> ShowNewOrderDialog { get; }
    public ReactiveCommand<Unit, Unit> OpenNewOrderDialogCommand { get; }

    public Interaction<ProductDesignerViewModel, Unit> ShowNewProductDialog { get; }
    public ReactiveCommand<Unit, Unit> OpenProductDesignerDialogCommand { get; }

    public Interaction<OrderRemakeViewModel, Unit> ShowOrderRemakeDialog { get; }
    public ReactiveCommand<Unit, Unit> RemakeCurrentOrderCommand { get; }

    public ReactiveCommand<Unit, Unit> ReleaseCurrentOrderCommand { get; }

    public ReactiveCommand<Unit, Unit> DeleteCurrentOrderCommand { get; }

    public ReactiveCommand<Unit, Unit> ArchiveCurrentOrderCommand { get; }

    private bool _isOrderSelected;
    private bool IsOrderSelected {
        get => _isOrderSelected;
        set => this.RaiseAndSetIfChanged(ref _isOrderSelected, value);
    }

    private readonly ILogger<RibbonViewModel> _logger;
    private readonly ApplicationContext _context;

    public RibbonViewModel(ILogger<RibbonViewModel> logger, ApplicationContext context, ISender sender) {

        _logger = logger;
        _context = context;
        _context.OrderSelectedEvent += (a, b) => {
            return Task.FromResult(
                IsOrderSelected = true
            );
        };

        ShowNewOrderDialog = new();
        OpenNewOrderDialogCommand = ReactiveCommand.CreateFromTask(async () => {
            _logger.LogTrace("OpenNewOrder command triggered");
            var vm = Program.CreateInstance<NewOrderViewModel>();
            await ShowNewOrderDialog.Handle(vm);
        });

        ShowNewProductDialog = new();
        OpenProductDesignerDialogCommand = ReactiveCommand.CreateFromTask(async () => {
            _logger.LogTrace("OpenProductDesigner command triggered");
            var vm = new ProductDesignerViewModel() {
                Attributes = {
                    new() { Name = "Height", DefaultValue = "4.125"},
                    new() { Name = "Width", DefaultValue = "21"},
                    new() { Name = "Depth", DefaultValue = "21"}
                }
            };
            await ShowNewProductDialog.Handle(vm);
        });

        var orderSelected = this.WhenAnyValue(x => x.IsOrderSelected);

        ShowOrderRemakeDialog = new();
        RemakeCurrentOrderCommand = ReactiveCommand.CreateFromTask(async () => {
            _logger.LogTrace("Remake command triggered");
            var vm = new OrderRemakeViewModel();
            await ShowOrderRemakeDialog.Handle(vm);
        }, canExecute: orderSelected);

        ReleaseCurrentOrderCommand = ReactiveCommand.Create(() => {
            _logger.LogTrace("Release command triggered");
        }, canExecute: orderSelected);

        DeleteCurrentOrderCommand = ReactiveCommand.Create(() => {
            _logger.LogTrace("Delete command triggered");

            try {
                if (_context.SelectedOrderId is null) return;

                Guid idToDelete = (Guid)_context.SelectedOrderId;

                sender.Send(new DeleteOrderById.Command(idToDelete));

                context.RemoveOrder(idToDelete);
            } catch (Exception ex) {
                _logger.LogError("Could not delete order \n{1}", ex);
            }

        }, canExecute: orderSelected);

        ArchiveCurrentOrderCommand = ReactiveCommand.Create(() => {
            _logger.LogTrace("Archive command triggered");
        }, canExecute: orderSelected);
    }

}
