using Dapper;
using FluentValidation;
using MediatR;
using Sales.Contracts;
using Sales.Implementation.Infrastructure;

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
        private readonly SalesSettings _settings;
        private readonly IPublisher _publisher;

        public Handler(OrderRepository orderRepo, SalesSettings settings, IPublisher publisher) {
            _orderRepo = orderRepo;
            _settings = settings;
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
            
            string orderQuery = _settings.PersistanceMode switch {

                PersistanceMode.SQLServer => @"SELECT [Sales].[Orders].[Id], [Sales].[Orders].[Name], [Sales].[Orders].[Number], [Sales].[Companies].[Name] As Customer
                                                FROM [Sales].[Orders]
                                                INNER JOIN [Sales].[Companies] ON [Sales].[Orders].[CustomerId]=[Sales].[Companies].[Id]
                                                WHERE [Sales].[Orders].[Id] = @OrderId;",

                PersistanceMode.SQLite => @"SELECT [Orders].[Id], [Orders].[Name], [Orders].[Number], [Companies].[Name] As Customer
                                            FROM [Orders]
                                            INNER JOIN [Companies] ON [Orders].[CustomerId]=[Companies].[Id]
                                            WHERE [Orders].[Id] = @OrderId;",

                _ => throw new InvalidDataException("Invalid persistance mode")

            };

            string productQuery = _settings.PersistanceMode switch {

                PersistanceMode.SQLServer => @"SELECT [ProductId], [ProductClass], [Qty] as [QtyOrdered]
                                                FROM [Sales].[OrderedItems]
                                                WHERE [OrderId] = @OrderId;",

                PersistanceMode.SQLite => @"SELECT [ProductId], [ProductClass], [Qty] as [QtyOrdered]
                                            FROM [OrderedItems]
                                            WHERE [OrderId] = @OrderId;",

                _ => throw new InvalidDataException("Invalid persistance mode")

            };

            var releasedOrder = await _settings.Connection.QuerySingleAsync<ReleasedOrder>(orderQuery, new { OrderId = orderId });
            var products = await _settings.Connection.QueryAsync<ProductOrdered>(productQuery, new { OrderId = orderId });

            releasedOrder.Products = products;
            await _publisher.Publish(new OrderReleasedNotification(releasedOrder));

        }
    }

}