using MediatR;
using OrderManager.Features.OrderDetails.CompanyDisplay;
using OrderManager.Shared;
using ReactiveUI;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using Unit = System.Reactive.Unit;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Threading;

namespace OrderManager.Features.OrderDetails.FilledDetails;

public record ProductGroup(string ProductName, List<OrderedProduct> Products);

public class FilledOrderDetailsViewModel : ViewModelBase {

    public OrderDetailsEventDomain Details { get; set; }
    public CompanyDisplayViewModel Customer { get; set; }
    public CompanyDisplayViewModel Vendor { get; set; }
    public CompanyDisplayViewModel Supplier { get; set; }
    public List<ProductGroup> GroupedProducts { get; set; }

    public ReactiveCommand<Unit, Unit> SaveOrder { get; }

    public ReactiveCommand<Guid, Unit> ReleaseOrder { get; }

    public ReactiveCommand<Guid, Unit> RemakeOrder { get; }

    private readonly ApplicationContext _context;
    private readonly ILogger<FilledOrderDetailsViewModel> _logger;
    private readonly ISender _sender;

    public FilledOrderDetailsViewModel(ApplicationContext context, OrderDetails order) {

        _context = context;
        _logger = Program.GetService<ILogger<FilledOrderDetailsViewModel>>();
        _sender = Program.GetService<ISender>();

        Details = new(order);

        GroupedProducts = order.OrderedProducts
                                    .GroupBy(p => p.ProductName)
                                    .Select(g => new ProductGroup(g.Key, g.ToList()))
                                    .ToList();

        Customer = new() { Company = order.Customer };
        Vendor = new() { Company = order.Vendor };
        Supplier = new() { Company = order.Supplier };

        SaveOrder = ReactiveCommand.CreateFromTask(OnOrderSave);
        ReleaseOrder = ReactiveCommand.Create<Guid>(OnOrderRelease);
        RemakeOrder = ReactiveCommand.Create<Guid>(OnOrderRemake);
    }

    public async Task OnOrderSave() {
        _logger.LogTrace("Order save command triggered");
        await _sender.Send(new UpdateOrder.Command(Details));
    }

    public static void OnOrderRelease(Guid orderId) {
        Debug.WriteLine($"Releasing order {orderId}");
    }

    public async void OnOrderRemake(Guid orderId) {
        Debug.WriteLine($"Remaking order {orderId}");

        var sender = Program.GetService<IMediator>();
        try {

            Guid newId = await sender.Send(new CreateRemake.Command(orderId, new() {
                new(1, 10) // TODO: open dialog to choose quantities of order to remake
            }));

            _context.AddOrder(newId);

        } catch(Exception e) {
            Debug.WriteLine(e);
        }
    }

}

public class UpdateOrder {

    public record Command(OrderDetailsEventDomain Order) : IRequest;

    public class Handler : IRequestHandler<Command> {

        private readonly ILogger<Handler> _logger;
        private readonly OrderDetailsRepository _repository;

        public Handler(ILogger<Handler> logger, OrderDetailsRepository repository) {
            _logger = logger;
            _repository = repository;
        }
        
        public Task<MediatR.Unit> Handle(Command request, CancellationToken cancellationToken) {

            _logger.LogInformation("Handling request to save order");
            _repository.Save(request.Order);

            return Task.FromResult(MediatR.Unit.Value);

        }

    }

}