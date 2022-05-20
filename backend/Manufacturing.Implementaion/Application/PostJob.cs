using Dapper;
using Manufacturing.Implementation.Domain;
using MediatR;
using Sales.Contracts;
using System.Data;

namespace Manufacturing.Implementation.Application;

public class PostJob : INotificationHandler<OrderConfirmedNotification> {

    private readonly IDbConnection _connection;

    public PostJob(IDbConnection connection) {
        _connection = connection;
    }

    public async Task Handle(OrderConfirmedNotification notification, CancellationToken cancellationToken) {

        const string query = @"INSERT INTO [Manufacturing].[Jobs] ([OrderId], [Name], [Number], [CustomerName], [Status])
                                VALUES (@OrderId, @Name, @Number, @Customer, @Status);
                                SELECT SCOPE_IDENTITY();";

        const string itemQuery = @"INSERT INTO [Manufacturing].[JobProducts] ([JobId], [ProductId], [QtyOrdered])
                                VALUES (@JobId, @ProductId, @QtyOrdered )";

        var trx = _connection.BeginTransaction();

        _connection.Open();

        int jobId = await _connection.QuerySingleAsync(query, new { 
            OrderId = notification.Order.Id,
            notification.Order.Name,
            notification.Order.Number,
            notification.Order.Customer,
            Status = ManufacturingStatus.Pending
        }, trx);

        Dictionary<int, int> products = new();
        foreach (var product in notification.Order.Products) {
            if (!products.ContainsKey(product.ProductId)) products[product.ProductId] = 0;
            products[product.ProductId] += product.QtyOrdered;
        }

        foreach (var product in products) {
            await _connection.ExecuteAsync(itemQuery, new {
                JobId = jobId,
                ProductId = product.Key,
                QtyOrdered = product.Value
            }, trx);
        }

        _connection.Close();

    }

}