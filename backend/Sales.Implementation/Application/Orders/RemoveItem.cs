using MediatR;
using Sales.Implementation.Infrastructure;

namespace Sales.Implementation.Application.Orders;

internal class RemoveItem {

    public record Command(int OrderId, int ItemId) : IRequest;

    public class Handler : AsyncRequestHandler<Command> {

        private readonly OrderRepository _orderRepo;
        private readonly OrderedItemRepository _itemRepo;

        public Handler(OrderRepository orderRepo, OrderedItemRepository itemRepo) {
            _orderRepo = orderRepo;
            _itemRepo = itemRepo;
        }

        protected async override Task Handle(Command request, CancellationToken cancellationToken) {

            var order = await _orderRepo.GetOrderById(request.OrderId);
            order.RemoveItem(request.ItemId);

            await _itemRepo.Delete(request.ItemId);

        }
    }

}
