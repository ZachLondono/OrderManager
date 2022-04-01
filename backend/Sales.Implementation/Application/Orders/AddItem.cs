using MediatR;
using Sales.Implementation.Domain;
using Sales.Implementation.Infrastructure;

namespace Sales.Implementation.Application.Orders;

internal class AddItem {

    public record Command(int OrderId, int ProductId, string Name, int Qty, Dictionary<string, string> Options) : IRequest<int>;

    public class Handler : IRequestHandler<Command, int> {

        private readonly OrderedItemRepository _itemRepo;

        public Handler(OrderedItemRepository itemRepo) {
            _itemRepo = itemRepo;
        }

        public Task<int> Handle(Command request, CancellationToken cancellationToken) {

            throw new NotImplementedException();
            /*var order = await _orderRepo.GetOrderById(request.OrderId);

            var newItem = new OrderedItem(Guid.NewGuid(), request.ProductId, request.Qty, request.Options);

            order.AddItem(newItem);

            await _itemRepo.Save(new(newItem));
            await _orderRepo.Save(order);

            return newItem.Id;*/

        }
    }

}
