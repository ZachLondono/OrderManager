using MediatR;
using Sales.Implementation.Domain;
using Sales.Implementation.Infrastructure;

namespace Sales.Implementation.Application.Orders;

internal class AddItem {

    public record Command(Guid OrderId, Guid ProductId, string Name, int Qty, Dictionary<string, string> Options) : IRequest<Guid>;

    public class Handler : IRequestHandler<Command, Guid> {

        private readonly OrderRepository _orderRepo;
        private readonly OrderedItemRepository _itemRepo;

        public Handler(OrderRepository orderRepo, OrderedItemRepository itemRepo) {
            _orderRepo = orderRepo;
            _itemRepo = itemRepo;
        }

        public async Task<Guid> Handle(Command request, CancellationToken cancellationToken) {

            var order = await _orderRepo.GetOrderById(request.OrderId);

            var newItem = new OrderedItem(Guid.NewGuid(), request.ProductId, request.Qty, request.Options);

            order.AddItem(newItem);

            await _itemRepo.Save(new(newItem));
            await _orderRepo.Save(order);

            return newItem.Id;

        }
    }

}
