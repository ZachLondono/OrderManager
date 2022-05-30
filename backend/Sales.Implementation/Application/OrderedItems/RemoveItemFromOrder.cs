using MediatR;
using Sales.Implementation.Infrastructure;

namespace Sales.Implementation.Application.OrderedItems;

public class RemoveItemFromOrder {

    public record Command(int ItemId) : IRequest;

    public class Handler : AsyncRequestHandler<Command> {

        private readonly OrderedItemRepository _repo;

        public Handler(OrderedItemRepository repo) {
            _repo = repo;
        }

        protected override async Task Handle(Command request, CancellationToken cancellationToken) {
            await _repo.Remove(request.ItemId);
        }
    }
}
