using MediatR;
using OrderManager.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OrderManager.Features.OrderDetails;

public class GetOrderDetails {

    public record Query(int Id) : IRequest<QueryResult<OrderModel>>;

    public class Handler : IRequestHandler<Query, QueryResult<OrderModel>> {
        public async Task<QueryResult<OrderModel>> Handle(Query request, CancellationToken cancellationToken) {
            OrderModel o = new() {
                Id = request.Id,
                Number = "IDK",
                ItemCount = new Random().Next(100)
            };

            await Task.Delay(5);

            //return new QueryResult<OrderModel>(new Error("Not Found", $"Could not find order with id '{request.Id}'"));
            return new QueryResult<OrderModel>(o);

        }
    }

}
