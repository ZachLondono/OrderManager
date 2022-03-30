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

    public async Task<OrderContext> GetOrderById(Guid id) {

        const string query = @"SELECT [Id], [Name], [Number], [Status], [Fields], [PlaceDate], [CompletionDate], [ConfirmationDate], [VendorId], [SupplierId], [CustomerId]
                                FROM [Orders] WHERE [Id] = @Id;";

        var orderDto = await _connection.QuerySingleAsync<Persistance.Order>(query, new { Id = id });

        var fields = JsonSerializer.Deserialize<Dictionary<string,string>>(orderDto.OrderFields);
        if (fields is null) fields = new();

        var order = new Order(id, orderDto.Name, orderDto.Number, orderDto.PlacedDate) {
            Fields = fields,
            CompletionDate = orderDto.CompletionDate,
            ConfirmationDate = orderDto.ConfirmationDate,
            //VendorId = orderDto.VendorId,
            //SupplierId = orderDto.SupplierId,
            //CustomerId = orderDto.CustomerId,
            Status = Enum.Parse<OrderStatus>(orderDto.Status)
        };
        
        //TODO: ordereditems in the order should be ids instead of references

        return new(order);

    }

    public async Task Save(OrderContext order) {

        var trx = _connection.BeginTransaction();

        var events = order.Events;
        
        foreach (var e in events) {

            if (e is ItemAddedEvent addedEvent) {
                await ApplyItemAdd(addedEvent, order, trx);
            } else if (e is ItemRemovedEvent removedEvent) {
                await ApplyItemRemove(removedEvent, order, trx);
            } else if (e is OrderPlacedEvent placedEvent) {
                await ApplyOrderPlace(placedEvent, order, trx);
            } else if (e is OrderConfirmedEvent confirmedEvent) {
                await ApplyOrderConfirmation(confirmedEvent, order, trx);
            } else if (e is OrderCompletedEvent completedEvent) {
                await ApplyOrderCompletion(completedEvent, order, trx);
            } else if (e is OrderCanceledEvent canceledEvent) {
                await ApplyOrderCancel(canceledEvent, order, trx);
            }

        }

        trx.Commit();

    }

    private Task ApplyItemAdd(ItemAddedEvent e, OrderContext order, IDbTransaction trx) => throw new NotImplementedException();
    private Task ApplyItemRemove(ItemRemovedEvent e, OrderContext order, IDbTransaction trx) => throw new NotImplementedException();
    private Task ApplyOrderPlace(OrderPlacedEvent e, OrderContext order, IDbTransaction trx) => throw new NotImplementedException();
    private Task ApplyOrderConfirmation(OrderConfirmedEvent e, OrderContext order, IDbTransaction trx) => throw new NotImplementedException();
    private Task ApplyOrderCompletion(OrderCompletedEvent e, OrderContext order, IDbTransaction trx) => throw new NotImplementedException();
    private Task ApplyOrderCancel(OrderCanceledEvent e, OrderContext order, IDbTransaction trx) => throw new NotImplementedException();


}