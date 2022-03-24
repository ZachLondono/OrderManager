using MediatR;

namespace Sales.Implementation.Application.Orders;

internal class AddItem {

    public record Command(Guid OrderId, Guid ProductId, string Name, int Qty, Dictionary<string, string> Options) : IRequest<Guid>;

    public class Handler : IRequestHandler<Command, Guid> {
        public Task<Guid> Handle(Command request, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }
    }

}
