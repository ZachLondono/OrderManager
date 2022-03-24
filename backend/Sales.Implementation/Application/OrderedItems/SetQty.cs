using MediatR;

namespace Sales.Implementation.Application.OrderedItems;

internal class SetQty {

    public record Command(Guid ItemId, int Qty) : IRequest;

    public class Handler : AsyncRequestHandler<Command> {
        protected override Task Handle(Command request, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }
    }

}
