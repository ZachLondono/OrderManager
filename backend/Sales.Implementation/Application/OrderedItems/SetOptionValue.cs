using MediatR;
using Sales.Implementation.Infrastructure;

namespace Sales.Implementation.Application.OrderedItems;

internal class SetOptionValue {

    public record Command(int ItemId, string Option, string Value) : IRequest;

    public class Handler : AsyncRequestHandler<Command> {

        private readonly OrderedItemRepository _itemRepo;

        public Handler(OrderedItemRepository itemRepo) {
            _itemRepo = itemRepo;
        }

        protected override async Task Handle(Command request, CancellationToken cancellationToken) {

            var item = await _itemRepo.GetItemById(request.ItemId);
            item.SetItemOption(request.Option, request.Value);

            await _itemRepo.Save(item);

        }
    }

}