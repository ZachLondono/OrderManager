using Dapper;
using Manufacturing.Contracts;
using Manufacturing.Implementation.Domain;
using MediatR;
using Sales.Contracts;

namespace Manufacturing.Implementation.Application;

public class PostJob : INotificationHandler<OrderReleasedNotification> {

    private readonly ManufacturingSettings _settings;

    public PostJob(ManufacturingSettings settings) {
        _settings = settings;
    }

    public async Task Handle(OrderReleasedNotification notification, CancellationToken cancellationToken) {

        if (_settings.Connection is null) return;

        // Map of product class to quantity ordered
        var classQty = notification.Order
                    .Products
                    .GroupBy(p => p.ProductClass)
                    .ToDictionary(g => g.Key,
                                g => g.Sum(p => p.QtyOrdered));

        string command = _settings.PersistanceMode switch {

            Contracts.PersistanceMode.SQLServer => @"INSERT INTO [Manufacturing].[Jobs]
                                        ([OrderId], [Name], [Number], [CustomerName], [Status], [ProductQty], [ProductClass], [ReleasedDate])
                                        VALUES (@OrderId, @Name, @Number, @Customer, @Status, @ReleasedDate);",

            Contracts.PersistanceMode.SQLite => @"INSERT INTO [Jobs]
                                        ([OrderId], [Name], [Number], [CustomerName], [Status], [ProductQty], [ProductClass], [ReleasedDate])
                                        VALUES (@OrderId, @Name, @Number, @Customer, @Status, @ReleasedDate);",

            _ => throw new InvalidDataException("Invalid DataBase mode")

        };

        _settings.Connection.Open();
        var trx = _settings.Connection.BeginTransaction();

        foreach (var prodClass in classQty) {

            await _settings.Connection.ExecuteAsync(command, new {
                OrderId = notification.Order.Id,
                notification.Order.Name,
                notification.Order.Number,
                CustomerName = notification.Order.Customer,
                Status = ManufacturingStatus.Pending,
                ProductQty = prodClass.Value,
                ProductClass = prodClass.Key,
                ReleasedDate = DateTime.Now
            }, trx);

        }

        trx.Commit();
        _settings.Connection.Close();

    }

}