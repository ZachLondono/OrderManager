using Dapper;
using Domain;
using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OrderManager.Features.Ribbon.DeleteOrder;

public class DeleteOrderById {

    public record Command(Guid Id) : IRequest;

    public class Handler : IRequestHandler<Command> {

        private readonly ILogger<Handler> _logger;
        private readonly ConnectionStringManager _manager;

        public Handler(ILogger<Handler> logger, ConnectionStringManager manager) {
            _logger = logger;
            _manager = manager;
        }

        public Task<Unit> Handle(Command request, CancellationToken cancellationToken) {

            using var connection = new SqliteConnection(_manager.GetConnectionString);

            connection.Open();

            using var transaction = connection.BeginTransaction();

            const string orderItemsQuery = @"SELECT [Id] FROM [OrderItems] WHERE [OrderId] = @OrderId;";

            const string deleteOptionCommand = @"DELETE FROM [OrderItemOptions] WHERE [ItemId] = @ItemId;";
            const string deleteItemCommand = @"DELETE FROM [OrderItems] WHERE [OrderId] = @OrderId;";
            const string deleteOrderCommand = @"DELETE FROM [Orders] WHERE [Id] = @Id;";

            IEnumerable<int> itemIds = connection.Query<int>(orderItemsQuery, new { OrderId = request.Id });
            
            int rows = 0;
            foreach (var itemId in itemIds) {
                rows = connection.Execute(deleteOptionCommand, new { ItemId = itemId });
                _logger.LogTrace("Deleting order item options for item with id '{0}', rows updated {1}", itemId, rows);
            }

            rows = connection.Execute(deleteItemCommand, new { OrderId = request.Id.ToString() });
            _logger.LogTrace("Deleting order items for order with id '{0}', rows updated {1}", request.Id, rows);

            rows = connection.Execute(deleteOrderCommand, new { Id = request.Id.ToString() });
            _logger.LogTrace("Deleting order '{0}', rows updated {1}", request.Id, rows);

            transaction.Commit();
            connection.Close();

            _logger.LogInformation("Order deleted '{0}'", request.Id);

            return Task.FromResult(Unit.Value);

        }

    }

}
