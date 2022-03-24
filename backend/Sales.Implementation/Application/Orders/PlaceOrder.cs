using MediatR;

namespace Sales.Implementation.Application.Orders;

internal class PlaceOrder {

    public record Item(Guid ProductId, string Name, int Qty, Dictionary<string, string> Options);

    public record Command(string Name, string Number, string Customer, string Vendor, Item[] items) : IRequest<Guid>;

    public class Handler : IRequestHandler<Command, Guid> {
        public Task<Guid> Handle(Command request, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }
    }

}