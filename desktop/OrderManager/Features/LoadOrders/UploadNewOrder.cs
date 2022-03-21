using MediatR;
using Dapper;
using PluginContracts.Models;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using System;
using Domain;
using Microsoft.Extensions.Logging;
using PluginContracts.Interfaces;
using Avalonia.Controls;

namespace OrderManager.Features.LoadOrders;

internal class UploadNewOrder {

    public record Command(string OrderProvider) : IRequest<Guid>;

    public class Handler : IRequestHandler<Command, Guid> {

        private readonly ILogger<Handler> _logger;
        private readonly NewOrderProviderFactory _factory;
        private readonly ConnectionStringManager _connectionStringManager;

        public Handler(ILogger<Handler> logger, NewOrderProviderFactory factory, ConnectionStringManager connectionStringManager) {
            _logger = logger;
            _factory = factory;
            _connectionStringManager = connectionStringManager;
        }

        public async Task<Guid> Handle(Command request, CancellationToken cancellationToken) {

            _logger.LogTrace("Getting new order provider with name {0}", request.OrderProvider);

            var plugin = _factory.GetProviderByName(request.OrderProvider);

            OrderDto? orderData = null;

            if (plugin is INewOrderFromFileProvider fileprovider) {

                OpenFileDialog dialog = new OpenFileDialog();
                dialog.AllowMultiple = false;
                var result = await dialog.ShowAsync(Program.GetService<MainWindow.MainWindow>());

                if (result is null || result.Length != 1) throw new InvalidOperationException();

                orderData = fileprovider.GetNewOrder(result[0]);

            } else if (plugin is INewOrderProvider provider) {
                orderData =  provider.GetNewOrder();
            }

            if (orderData is null) throw new InvalidOperationException();

            _logger.LogTrace("New order data recieved {Order}", orderData);

            using var connection = new SqliteConnection(_connectionStringManager.GetConnectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();

            const string orderQuery = @"INSERT INTO [Orders] ([Id], [Number], [Name], [IsPriority], [LastModified], [VendorId], [SupplierId])
                                    VALUES (@Id, @Number, @Name, @IsPriority, @LastModified, @VendorId, @SupplierId);";

            Guid newId = Guid.NewGuid();

            _logger.LogDebug("Creating new order with ID '{OrderId}'", newId);
            connection.Execute(orderQuery, new {
                Id = newId.ToString(),
                orderData.Number,
                orderData.Name,
                IsPriority = false,
                LastModified = DateTime.Now,
                orderData.VendorId,
                orderData.SupplierId,
            });

            SetCustomerId(newId, orderData.Customer, connection);

            const string productNameQuery = @"SELECT [Name] FROM [Products] WHERE [Id] = @Id;";

            const string productQuery = @"INSERT INTO [OrderItems]
                                            ([Qty], [LineNumber], [ProductId], [OrderId], [ProductName])
                                            VALUES
                                            (@Qty, @LineNumber, @ProductId, @OrderId, @ProductName)
                                            RETURNING Id;";

            const string productOptionQuery = @"INSERT INTO [OrderItemOptions]
                                                ([ItemId], [Key], [Value])
                                                VALUES
                                                (@ItemId, @Key, @Value)
                                                RETURNING Id;";

            foreach (var product in orderData.Products) {

                try { 

                    string productName = connection.QuerySingle<string>(productNameQuery, new { Id = product.ProductId });

                    _logger.LogTrace("Query for name of product with id '{ProductId}' returned '{ProductName}'", product.ProductId, productName);

                    int itemId = connection.QuerySingle<int>(productQuery, new {
                        product.Qty,
                        product.LineNumber,
                        product.ProductId,
                        OrderId = newId.ToString(),
                        ProductName = productName
                    });

                    _logger.LogTrace("New ordered item id inserted '{0}'", itemId);

                    foreach (var option in product.Options) {

                        int optionId = connection.QuerySingle<int>(productOptionQuery, new {
                            ItemId = itemId,
                            option.Key,
                            option.Value
                        });

                        _logger.LogTrace("New ordered item option id inserted '{OptionId}'", optionId);

                    }

                } catch (Exception e) {

                    _logger.LogError("Could not insert product options\n{exception}", e);

                }

            }

            transaction.Commit();
            connection.Close();

            _logger.LogInformation("New order created with ID '{0}'", newId);

            return newId;

        }

        /// <summary>
        /// Updates the customer id for the given order. If the customer does not yet exist, it will be created.
        /// </summary>
        private void SetCustomerId(Guid orderId, CompanyDto customerData, SqliteConnection connection) {
            const string customerQuery = @"SELECT [Id] FROM [Companies] WHERE [Name] = @Name;";
            int customerId = connection.QueryFirstOrDefault<int>(customerQuery, new { Name = customerData.Name });

            if (customerId == default) {

                const string customerSql = @"INSERT INTO [Companies] ([Name], [Contact], [Address1], [Address2], [Address3], [City], [State], [Zip])
                                                VALUES (@Name, @Contact, @Address1, @Address2, @Address3, @City, @State, @Zip)
                                                RETURNING Id;";

                customerId = connection.QuerySingle<int>(customerSql, customerData);

                _logger.LogDebug("New customer created with ID '{0}'", customerId);

            }

            _logger.LogDebug("Setting customerId '{0}' for order ID '{1}'", customerId, orderId);

            const string updateSql = @"UPDATE [Orders] SET [CustomerId] = @CustomerId WHERE [Id] = @Id;";
            connection.Execute(updateSql, new { CustomerId = customerId, Id = orderId.ToString() });
        }
    }

}
