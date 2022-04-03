using System.Data;
using System.Text.Json;
using Dapper;
using Microsoft.Extensions.Logging;
using Sales.Implementation.Domain;

namespace Sales.Implementation.Infrastructure;

public class OrderedItemRepository {

    private readonly IDbConnection _connection;
    private readonly ILogger<OrderedItemRepository> _logger;

    public OrderedItemRepository(IDbConnection connection, ILogger<OrderedItemRepository> logger) {
        _connection = connection;
        _logger = logger;
    }

    public async Task<OrderedItemContext> GetItemById(int id) {

        const string query = @"SELECT [Id], [OrderId], [ProductId], [ProductName], [Qty], [Options]
                                FROM [Sales].[OrderedItems]
                                WHERE [Id] = @Id;";

        var itemDto = await _connection.QuerySingleAsync<Persistance.OrderedItem>(query, new { Id = id });

        var item = new OrderedItem(id, itemDto.ProductId, itemDto.OrderId);
        item.SetQuantity(itemDto.Qty);

        var options = JsonSerializer.Deserialize<Dictionary<string, string>>(itemDto.Options);
        if (options is not null) { 
            foreach (var key in options.Keys) {
                item[key] = options[key];
            }
        }

        return new(item);

    }

    public async Task Remove(int itemId) {
        const string command = @"DELETE FROM [Sales].[OrderedItems] WHERE [Id] = @Id;";
        await _connection.ExecuteAsync(command, new { Id = itemId });
    }

    public async Task Save(OrderedItemContext item) {

        var trx = _connection.BeginTransaction();

        var events = item.Events;
        foreach (var e in events) {

            if (e is ItemOptionSet itemOptionSet) {
                await ApplyItemOptionSet(trx, item, itemOptionSet);
            } else if (e is ItemQtySet itemQtySet) {
                await ApplyItemQtySet(trx, item, itemQtySet);
            }

        }

        trx.Commit();

    }

    private async Task ApplyItemQtySet(IDbTransaction trx, OrderedItemContext item, ItemQtySet itemQtySet) {
        const string query = @"UPDATE [Sales].[OrderedItems]
                                SET [Qty] = @Qty
                                WHERE [Id] = @Id;";
        await _connection.ExecuteAsync(query, new {
            Id = item.Id,
            Qty = itemQtySet.Qty,
        }, trx);
    }

    private async Task ApplyItemOptionSet(IDbTransaction trx, OrderedItemContext item, ItemOptionSet itemOptionSet) {
        const string query = @"SELECT [Options]
                                FROM [Sales].[OrderedItems]
                                WHERE [Id] = @Id;";

        const string command = @"UPDATE [Sales].[OrderedItems]
                                SET [Options] = @Options 
                                WHERE [Id] = @Id;";
        string json = await _connection.QuerySingleAsync<string>(query, new {
            Id = item.Id
        }, trx);

        var options = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        if (options is not null) {
            options[itemOptionSet.Option] = itemOptionSet.Value;
            json = JsonSerializer.Serialize(options);
            await _connection.ExecuteAsync(command, new { Options = json }, trx);
        }
    }
}