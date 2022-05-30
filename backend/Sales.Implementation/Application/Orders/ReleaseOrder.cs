using Dapper;
using FluentValidation;
using MediatR;
using Sales.Contracts;
using Sales.Implementation.Infrastructure;
using System.Data;

namespace Sales.Implementation.Application.Orders;

public class ReleaseOrder {

    public record Command(int OrderId) : IRequest;

    public class Validation : AbstractValidator<Command> {

        public Validation() {

            RuleFor(x => x.OrderId)
                .NotEqual(0)
                .WithMessage("Invalid order id");

        }

    }

    public class Handler : AsyncRequestHandler<Command> {

        private readonly OrderRepository _orderRepo;
        private readonly IDbConnection _connection;
        private readonly IPublisher _publisher;

        public Handler(OrderRepository orderRepo, IDbConnection connection, IPublisher publisher) {
            _orderRepo = orderRepo;
            _connection = connection;
            _publisher = publisher;
        }

        /// <summary>
        /// Marks the order as released, then saves the canges to the repo. Publishes a notification that an order has been released.
        /// </summary>
        protected override async Task Handle(Command request, CancellationToken cancellationToken) {
            
            var order = await _orderRepo.GetOrderById(request.OrderId);
            order.ReleaseOrder();
            await _orderRepo.Save(order);

            await PublishOrderReleasedNotification(request.OrderId);

        }

        /// <summary>
        /// Queries the necessar data to publish an OrderReleasedNotification to be consumed by notification handlers
        /// </summary>
        private async Task PublishOrderReleasedNotification(int orderId) {
            
            const string orderQuery = @"SELECT [Sales].[Orders].[Id], [Sales].[Orders].[Name], [Sales].[Orders].[Number], [Sales].[Companies].[Name] As Customer
                                        FROM [Sales].[Orders]
                                        INNER JOIN [Sales].[Companies] ON [Sales].[Orders].[CustomerId]=[Sales].[Companies].[Id]
                                        WHERE [Sales].[Orders].[Id] = @OrderId;";

            const string productQuery = @"SELECT [ProductId], [ProductClass], [Qty] as [QtyOrdered]
                                        FROM [Sales].[OrderedItems]
                                        WHERE [OrderId] = @OrderId;";

            var releasedOrder = await _connection.QuerySingleAsync<ReleasedOrder>(orderQuery, new { OrderId = orderId });
            var products = await _connection.QueryAsync<ProductOrdered>(productQuery, new { OrderId = orderId });

            releasedOrder.Products = products;
            await _publisher.Publish(new OrderReleasedNotification(releasedOrder));

        }
    }

}