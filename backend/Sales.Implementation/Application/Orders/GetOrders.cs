using MediatR;
using Sales.Contracts;

namespace Sales.Implementation.Application.Orders;

internal class GetOrders {

    public record Query() : IRequest<OrderSummary[]>;

    public class Handler : IRequestHandler<Query, OrderSummary[]> {
        public Task<OrderSummary[]> Handle(Query request, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }
    }

}
