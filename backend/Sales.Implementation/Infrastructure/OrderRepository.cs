using Dapper;
using Microsoft.Extensions.Logging;
using Sales.Implementation.Domain;
using System.Data;
using System.Text.Json;

namespace Sales.Implementation.Infrastructure;

public class OrderRepository {

    private readonly IDbConnection _connection;
    private readonly ILogger<OrderRepository> _logger;

    public OrderRepository(IDbConnection connection, ILogger<OrderRepository> logger) {
        _connection = connection;
        _logger = logger;
    }

    public async Task<OrderContext> GetOrderById(int id) {

        const string query = @"SELECT [Id], [Name], [Number], [Status], [Fields], [PlaceDate], [CompletionDate], [ConfirmationDate], [VendorId], [SupplierId], [CustomerId]
                                FROM [Orders] WHERE [Id] = @Id;";

        var orderDto = await _connection.QuerySingleAsync<Persistance.Order>(query, new { Id = id });

        var fields = JsonSerializer.Deserialize<Dictionary<string,string>>(orderDto.OrderFields);
        if (fields is null) fields = new();

        const string itemQuery = @"SELECT [Id] FROM [OrderedItems] WHERE [OrderId] = @OrderId;";
        var items = await _connection.QueryAsync<int>(itemQuery, new { OrderId = id });

        var status = Enum.Parse<OrderStatus>(orderDto.Status);

        var order = new Order(id, orderDto.Name, orderDto.Number, items.ToArray(), status, orderDto.PlacedDate) {
            Fields = fields,
            CompletionDate = orderDto.CompletionDate,
            ConfirmationDate = orderDto.ConfirmationDate,
            VendorId = orderDto.VendorId,
            SupplierId = orderDto.SupplierId,
            CustomerId = orderDto.CustomerId
        };

        return new(order);

    }

    public async Task Save(OrderContext order) {

        var trx = _connection.BeginTransaction();

        var events = order.Events;
        
        foreach (var e in events) {

            if (e is OrderConfirmedEvent confirmedEvent) {
                await ApplyOrderConfirmation(confirmedEvent, order, trx);
            } else if (e is OrderCompletedEvent completedEvent) {
                await ApplyOrderCompletion(completedEvent, order, trx);
            } else if (e is OrderCanceledEvent canceledEvent) {
                await ApplyOrderCancel(canceledEvent, order, trx);
            }

        }

        trx.Commit();

    }

    private async Task ApplyOrderConfirmation(OrderConfirmedEvent e, OrderContext order, IDbTransaction trx) {
        const string command = "UPDATE [Orders] SET [Status] = @Status, [ConfirmedDate] = @TimeStamp WHERE [Id] = @Id;";
        await _connection.ExecuteAsync(command, new {
            Id = order.Id,
            Status = OrderStatus.Confirmed.ToString(),
            e.TimeStamp
        }, trx);
    }

    private async Task ApplyOrderCompletion(OrderCompletedEvent e, OrderContext order, IDbTransaction trx) {
        const string command = "UPDATE [Orders] SET [Status] = @Status, [CompletedDate] = @TimeStamp WHERE [Id] = @Id;";
        await _connection.ExecuteAsync(command, new {
            Id = order.Id,
            Status = OrderStatus.Completed.ToString(),
            e.TimeStamp
        }, trx);
    }

    private async Task ApplyOrderCancel(OrderCanceledEvent e, OrderContext order, IDbTransaction trx) {
        const string command = "UPDATE [Orders] SET [Status] = @Status, [CanceledDate] = @TimeStamp WHERE [Id] = @Id;";
        await _connection.ExecuteAsync(command, new {
            Id = order.Id,
            Status = OrderStatus.Void.ToString(),
            e.TimeStamp
        }, trx);
    }

}