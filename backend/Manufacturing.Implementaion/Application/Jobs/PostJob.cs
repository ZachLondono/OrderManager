using Dapper;
using Manufacturing.Implementation.Domain;
using MediatR;
using Sales.Contracts;
using System.Data;

namespace Manufacturing.Implementation.Application;

public class PostJob : INotificationHandler<OrderReleasedNotification> {

    private readonly IDbConnection _connection;

    public PostJob(IDbConnection connection) {
        _connection = connection;
    }

    public async Task Handle(OrderReleasedNotification notification, CancellationToken cancellationToken) {

        // Map of product class to quantity ordered
        var classQty = notification.Order
                    .Products
                    .GroupBy(p => p.ProductClass)
                    .ToDictionary(g => g.Key,
                                g => g.Sum(p => p.QtyOrdered));

        const string command = @"INSERT INTO [Manufacturing].[Jobs]
                                ([OrderId], [Name], [Number], [CustomerName], [Status], [ProductQty], [ProductClass])
                                VALUES (@OrderId, @Name, @Number, @Customer, @Status);";

        _connection.Open();
        var trx = _connection.BeginTransaction();

        foreach (var prodClass in classQty) {

            await _connection.ExecuteAsync(command, new {
                OrderId = notification.Order.Id,
                notification.Order.Name,
                notification.Order.Number,
                CustomerName = notification.Order.Customer,
                Status = ManufacturingStatus.Pending,
                ProductQty = prodClass.Value,
                ProductClass = prodClass.Key
            }, trx);

        }

        trx.Commit();
        _connection.Close();

    }

}