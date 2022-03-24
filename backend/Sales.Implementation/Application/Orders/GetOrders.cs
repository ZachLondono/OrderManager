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

internal class GetOrderDetails {

    public record Query() : IRequest<OrderDetails>;

    public class Handler : IRequestHandler<Query, OrderDetails> {
        public Task<OrderDetails> Handle(Query request, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }
    }

}