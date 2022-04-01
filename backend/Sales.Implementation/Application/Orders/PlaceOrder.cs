using MediatR;

namespace Sales.Implementation.Application.Orders;

internal class PlaceOrder {

    public record Item(int ProductId, string Name, int Qty, Dictionary<string, string> Options);

    public record Command(string Name, string Number, string Customer, string Vendor, Item[] items) : IRequest<int>;

    public class Handler : IRequestHandler<Command, int> {
        public Task<int> Handle(Command request, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }
    }

}