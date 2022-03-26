using MediatR;
using Sales.Contracts;

namespace Sales.Implementation.Application.Orders;

internal class GetOrderDetails {

    public record Query() : IRequest<OrderDetails>;

    public class Handler : IRequestHandler<Query, OrderDetails> {
        public Task<OrderDetails> Handle(Query request, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }
    }

}