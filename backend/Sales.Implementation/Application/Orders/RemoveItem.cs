using MediatR;

namespace Sales.Implementation.Application.Orders;

internal class RemoveItem {

    public record Command(Guid OrderId, Guid ItemId) : IRequest;

    public class Handler : AsyncRequestHandler<Command> {
        protected override Task Handle(Command request, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }
    }

}
