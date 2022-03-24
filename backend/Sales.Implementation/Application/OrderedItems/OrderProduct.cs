using MediatR;

namespace Sales.Implementation.Application.OrderedItems;

internal class OrderProduct {

    public record Command(Guid ProductId, int Qty, Dictionary<string, string> Options) : IRequest<Guid>;

    public class Handler : IRequestHandler<Command, Guid> {
        public Task<Guid> Handle(Command request, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }
    }

}
