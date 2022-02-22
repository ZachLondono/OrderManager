using Domain.Entities.OrderAggregate;
using Domain.Services;
using MediatR;
using OrderManager.Shared;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace OrderManager.Features.OrderDetails;

public class GetOrderDetails {

    public record Query(Guid Id) : IRequest<QueryResult<Order>>;

    public class Handler : IRequestHandler<Query, QueryResult<Order>> {

        private readonly OrderService _service;

        public Handler(OrderService service) {
            _service = service;
        }

        public Task<QueryResult<Order>> Handle(Query request, CancellationToken cancellationToken) {

            try { 
                
                Order o = _service.GetOrderById(request.Id);
                return Task.FromResult(new QueryResult<Order>(o));

            } catch (InvalidDataException) {

                return Task.FromResult(new QueryResult<Order>(new Error("Not Found", $"Could not find order with id '{request.Id}'")));

            }

        }
    }

}
