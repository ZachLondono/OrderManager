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

        const string query = @"INSERT INTO [Manufacturing].[Jobs] ([Id], [Name], [Number], [CustomerId], [VendorId], [ItemCount], [Status])
                                VALUES (@Id, @Name, @Number, @CustomerId, @VendorId, @ItemCount, @Status);";

        await _connection.ExecuteAsync(query, new { 
            notification.Order.Id,
            notification.Order.Name,
            notification.Order.Number,
            notification.Order.VendorId,
            notification.Order.CustomerId,
            notification.Order.ItemCount,
            Status = ManufacturingStatus.Pending
        });

    }

}