using MediatR;

namespace Sales.Implementation.Application.Orders;

internal class ConfirmOrder {

    public record Command(Guid OrderId) : IRequest;

    public class Handler : AsyncRequestHandler<Command> {
        protected override Task Handle(Command request, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }
    }

}