using MediatR;
using Dapper;
using OrderManager.Shared.Notifications;
using Persistance;
using PluginContracts.Models;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using System;

namespace OrderManager.Features.LoadOrders;

internal class UploadNewOrder {

    public record Command(string OrderProvider) : IRequest;

    public class Handler : IRequestHandler<Command> {

        private readonly IPublisher _publisher;
        private readonly NewOrderProviderFactory _factory;
        private readonly ConnectionStringManager _connectionStringManager;

        public Handler(IPublisher publisher, NewOrderProviderFactory factory, ConnectionStringManager connectionStringManager) {
            _publisher = publisher;
            _factory = factory;
            _connectionStringManager = connectionStringManager;
        }

        public Task<Unit> Handle(Command request, CancellationToken cancellationToken) {

            var provider = _factory.GetProviderByName(request.OrderProvider);
            OrderDto orderData = provider.GetNewOrder();

            using var connection = new SqliteConnection(_connectionStringManager.GetConnectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();

            const string orderQuery = @"INSERT INTO [Orders] ([Number], [Name], [IsPriority], [LastModified], [VendorId], [SupplierId])
                                    VALUES ([@Number], [@Name], [@IsPriority], [@LastModified], [@VendorId], [@SupplierId])
                                    RETURNING Id;";

            int newId = connection.QuerySingle<int>(orderQuery, new {
                orderData.Number,
                orderData.Name,
                IsPriority = false,
                LastModified = DateTime.Now,
                orderData.VendorId,
                orderData.SupplierId,
            });

            SetCustomerId(newId, orderData.Customer, connection);

            transaction.Commit();
            connection.Close();

            _publisher.Publish(new OrderUploadedNotification(newId));

            return Unit.Task;

        }

        /// <summary>
        /// Updates the customer id for the given order. If the customer does not yet exist, it will be created.
        /// </summary>
        private static void SetCustomerId(int orderId, CompanyDto customerData, SqliteConnection connection) {
            const string customerQuery = @"SELECT [Id] FROM [Companies] WHERE [Name] = [@Name];";
            int customerId = connection.QueryFirstOrDefault<int>(customerQuery, new { Name = customerData.Name });

            if (customerId == default) {
                const string customerSql = @"INSERT INTO [Companies] ([Name], [Contact], [Address1], [Address2], [Address3], [City], [State], [Zip])
                                                VALUES ([@Name], [@Contact], [@Address1], [@Address2], [@Address3], [@City], [@State], [@Zip])
                                                RETURNING Id;";

                customerId = connection.QuerySingle<int>(customerSql, customerData);
            }

            const string updateSql = @"UPDATE [Orders] SET [CustomerId] = [@CustomerId] WHERE [Id] = [@Id];";
            connection.Execute(updateSql, new { CustomerId = customerId, Id = orderId });
        }
    }

}
