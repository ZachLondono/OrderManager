using System.Data;
using System.Text.Json;
using Dapper;
using Microsoft.Extensions.Logging;
using Sales.Contracts;
using Sales.Implementation.Domain;

namespace Sales.Implementation.Infrastructure;

public class OrderedItemRepository {

    private readonly SalesSettings _settings;
    private readonly ILogger<OrderedItemRepository> _logger;

    public OrderedItemRepository(SalesSettings settings, ILogger<OrderedItemRepository> logger) {
        _settings = settings;
        _logger = logger;
    }

    public async Task<OrderedItemContext> GetItemById(int id) {

        _logger.LogInformation("Getting ordered item with ID: {ID}", id);

        string query = _settings.PersistanceMode switch {

            PersistanceMode.SQLServer => @"SELECT [Id], [OrderId], [ProductId], [ProductName], [Qty], [Options]
                                            FROM [Sales].[OrderedItems]
                                            WHERE [Id] = @Id;",

            PersistanceMode.SQLite => @"SELECT [Id], [OrderId], [ProductId], [ProductName], [Qty], [Options]
                                        FROM [OrderedItems]
                                        WHERE [Id] = @Id;",

            _ => throw new InvalidDataException("Invalid persistance mode")

        };

        var itemDto = await _settings.Connection.QuerySingleAsync<Persistance.OrderedItem>(query, new { Id = id });

        var item = new OrderedItem(id, itemDto.ProductId, itemDto.OrderId);
        item.SetQuantity(itemDto.Qty);

        var options = JsonSerializer.Deserialize<Dictionary<string, string>>(itemDto.Options);
        if (options is not null) { 
            foreach (var key in options.Keys) {
                item[key] = options[key];
            }
        }

        _logger.LogInformation("Found ordered item with ID: {ID}, {Item}", id, item);

        return new(item);

    }

    public async Task Remove(int itemId) {
        
        string command = _settings.PersistanceMode switch {

            PersistanceMode.SQLServer => @"DELETE FROM [Sales].[OrderedItems] WHERE [Id] = @Id;",

            PersistanceMode.SQLite => @"DELETE FROM [OrderedItems] WHERE [Id] = @Id;",

            _ => throw new InvalidDataException("Invalid persistance mode")

        };

        await _settings.Connection.ExecuteAsync(command, new { Id = itemId });
    }

    public async Task Save(OrderedItemContext item) {

        if (_settings.Connection is null) return;

        _settings.Connection.Open();
        var trx = _settings.Connection.BeginTransaction();

        var events = item.Events;
        foreach (var e in events) {

            if (e is ItemOptionSet itemOptionSet) {
                await ApplyItemOptionSet(trx, item, itemOptionSet);
            } else if (e is ItemQtySet itemQtySet) {
                await ApplyItemQtySet(trx, item, itemQtySet);
            }

        }

        trx.Commit();
        _settings.Connection.Close();

        _logger.LogInformation("Applied {EventCount} events to ordered item {Item}", events.Count, item);

    }

    private async Task ApplyItemQtySet(IDbTransaction trx, OrderedItemContext item, ItemQtySet itemQtySet) {

        string query = _settings.PersistanceMode switch {

            PersistanceMode.SQLServer => @"UPDATE [Sales].[OrderedItems]
                                            SET [Qty] = @Qty
                                            WHERE [Id] = @Id;",

            PersistanceMode.SQLite => @"UPDATE [OrderedItems]
                                        SET [Qty] = @Qty
                                        WHERE [Id] = @Id;",

            _ => throw new InvalidDataException("Invalid persistance mode")

        };

        await _settings.Connection.ExecuteAsync(query, new {
            item.Id,
            itemQtySet.Qty,
        }, trx);
    }

    private async Task ApplyItemOptionSet(IDbTransaction trx, OrderedItemContext item, ItemOptionSet itemOptionSet) {
        
        string command = _settings.PersistanceMode switch {

            PersistanceMode.SQLServer => @"UPDATE [Sales].[OrderedItems]
                                        SET [Options] = @Options 
                                        WHERE [Id] = @Id;",

            PersistanceMode.SQLite => @"UPDATE [OrderedItems]
                                        SET [Options] = @Options 
                                        WHERE [Id] = @Id;",

            _ => throw new InvalidDataException("Invalid persistance mode")

        };

        string query = _settings.PersistanceMode switch {

            PersistanceMode.SQLServer => @"SELECT [Options]
                                        FROM [Sales].[OrderedItems]
                                        WHERE [Id] = @Id;",

            PersistanceMode.SQLite => @"SELECT [Options]
                                        FROM [OrderedItems]
                                        WHERE [Id] = @Id;",

            _ => throw new InvalidDataException("Invalid persistance mode")

        };

        string json = await _settings.Connection.QuerySingleAsync<string>(query, new {
            item.Id
        }, trx);

        var options = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        if (options is not null) {
            options[itemOptionSet.Option] = itemOptionSet.Value;
            json = JsonSerializer.Serialize(options);
            await _settings.Connection.ExecuteAsync(command, new { Options = json }, trx);
        }
    }
}