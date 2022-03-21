using MediatR;
using Microsoft.Extensions.Logging;
using OrderManager.Features.Ribbon.DeleteOrder;
using OrderManager.Features.LoadOrders;
using OrderManager.Features.ProductDesigner;
using OrderManager.Features.RemakeOrder;
using OrderManager.Shared;
using ReactiveUI;
using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Unit = System.Reactive.Unit;
using OrderManager.Features.Ribbon.ReleaseProfiles;

namespace OrderManager.Features.Ribbon;

public class RibbonViewModel : ViewModelBase {

    public Interaction<NewOrderViewModel, Unit> ShowNewOrderDialog { get; }
    public ReactiveCommand<Unit, Unit> OpenNewOrderDialogCommand { get; }

    public Interaction<ProductDesignerViewModel, Unit> ShowNewProductDialog { get; }
    public ReactiveCommand<Unit, Unit> OpenProductDesignerDialogCommand { get; }

    public Interaction<OrderRemakeViewModel, Unit> ShowOrderRemakeDialog { get; }
    public ReactiveCommand<Unit, Unit> RemakeCurrentOrderCommand { get; }

    public ReactiveCommand<Unit, Unit> ReleaseCurrentOrderCommand { get; }

    public Interaction<DeleteOrderViewModel, bool> ShowOrderDeleteDialog { get; }
    public ReactiveCommand<Unit, Unit> DeleteCurrentOrderCommand { get; }

    public ReactiveCommand<Unit, Unit> ArchiveCurrentOrderCommand { get; }

    public Interaction<ProfileEditorViewModel, Unit> ShowProfileEditorDialog { get; }
    public ReactiveCommand<Unit, Unit> NewReleaseProfileCommand { get; }

    public Interaction<ProfileManagerViewModel, Unit> ShowProfileManagerDialog { get; }
    public ReactiveCommand<Unit, Unit> ReleaseProfileManagerCommand { get; }

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

        ShowOrderDeleteDialog = new();
        DeleteCurrentOrderCommand = ReactiveCommand.CreateFromTask(async () => {
            _logger.LogTrace("Delete command triggered");

            if (_context.SelectedOrderId is null) return;

            var vm = new DeleteOrderViewModel();
            vm.OrderId = ((Guid)_context.SelectedOrderId).ToString();
            bool doDelete = await ShowOrderDeleteDialog.Handle(vm);

            if (!doDelete) return;

            try {
                if (_context.SelectedOrderId is null) return;

                Guid idToDelete = (Guid)_context.SelectedOrderId;

                await sender.Send(new DeleteOrderById.Command(idToDelete));

                context.RemoveOrder(idToDelete);
            } catch (Exception ex) {
                _logger.LogError("Could not delete order \n{1}", ex);
            }

        }, canExecute: orderSelected);

        ArchiveCurrentOrderCommand = ReactiveCommand.Create(() => {
            _logger.LogTrace("Archive command triggered");
            var dialog = new DeleteOrderDialog();
            dialog.Show();

        }, canExecute: orderSelected);

        ShowProfileEditorDialog = new();
        NewReleaseProfileCommand = ReactiveCommand.CreateFromTask(async () => {
            _logger.LogTrace("New release profile command triggered");
            try {

                var logger = Program.GetService<ILogger<ProfileEditorViewModel>>();
                var sender = Program.GetService<ISender>();

                var profile = await sender.Send(new CreateNewReleaseProfile.Command("Profile A"));

                var vm = new ProfileEditorViewModel(logger, sender, profile);
                await ShowProfileEditorDialog.Handle(vm);
            } catch (Exception e) {
                _logger.LogError("Could not edit profile {exception}", e);
            }
        });

        ShowProfileManagerDialog = new();
        ReleaseProfileManagerCommand = ReactiveCommand.CreateFromTask(async () => {
            _logger.LogTrace("Release Profile manager command triggered");

            var vm = Program.CreateInstance<ProfileManagerViewModel>();
            await ShowProfileManagerDialog.Handle(vm);
        });

    }

}
