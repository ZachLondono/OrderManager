using Dapper;
using MediatR;
using Sales.Contracts;

namespace Sales.Implementation.Application.Orders;

public class GetOrderDetails {

    public record Query(int OrderId) : IRequest<OrderDetails>;

    public class Handler : IRequestHandler<Query, OrderDetails> {

        private readonly SalesSettings _settings;

        public Handler(SalesSettings settings) {
            _settings = settings;
        }

        public async Task<OrderDetails> Handle(Query request, CancellationToken cancellationToken) {

            string query = _settings.PersistanceMode switch {

                PersistanceMode.SQLServer => @"SELECT [Id], [Name], [Number], [Status], [PlacedDate], [ConfirmedDate], [ReleasedDate], [CompletedDate], [LastModifiedDate], [Info],
	                                            [CustomerId] As [Id],
	                                            [Name] = ( SELECT [Name] FROM [Sales].[Companies] WHERE [Id] = [CustomerId] ),
	                                            [Roles] = ( SELECT [Roles] FROM [Sales].[Companies] WHERE [Id] = [CustomerId] ),
	                                            [VendorId] As [Id],
	                                            [Name] = ( SELECT [Name] FROM [Sales].[Companies] WHERE [Id] = [VendorId] ),
	                                            [Roles] = ( SELECT [Roles] FROM [Sales].[Companies] WHERE [Id] = [VendorId] ),
	                                            [SupplierId] As [Id],
	                                            [Name] = ( SELECT [Name] FROM [Sales].[Companies] WHERE [Id] = [SupplierId] ),
	                                            [Roles] = ( SELECT [Roles] FROM [Sales].[Companies] WHERE [Id] = [SupplierId] )
                                            FROM [Sales].[Orders]
                                            WHERE [Sales].[Orders].Id = @OrderId;",

                PersistanceMode.SQLite => @"SELECT [Id], [Name], [Number], [Status], [PlacedDate], [ConfirmedDate], [ReleasedDate], [CompletedDate], [LastModifiedDate], [Info],
	                                            [CustomerId] As [Id],
	                                            [Name] = ( SELECT [Name] FROM [Companies] WHERE [Id] = [CustomerId] ),
	                                            [Roles] = ( SELECT [Roles] FROM [Companies] WHERE [Id] = [CustomerId] ),
	                                            [VendorId] As [Id],
	                                            [Name] = ( SELECT [Name] FROM [Companies] WHERE [Id] = [VendorId] ),
	                                            [Roles] = ( SELECT [Roles] FROM [Companies] WHERE [Id] = [VendorId] ),
	                                            [SupplierId] As [Id],
	                                            [Name] = ( SELECT [Name] FROM [Companies] WHERE [Id] = [SupplierId] ),
	                                            [Roles] = ( SELECT [Roles] FROM [Companies] WHERE [Id] = [SupplierId] )
                                            FROM [Orders]
                                            WHERE [Orders].Id = @OrderId;",

                _ => throw new InvalidDataException("Invalid persistance mode")

            };

            var data = await _settings.Connection.QueryAsync<OrderDetails, CompanySummary, CompanySummary, CompanySummary, OrderDetails>(sql:query, param:request, map:MapOrderCompanies, splitOn:"Id");
            var order = data.First();

            string itemQuery = _settings.PersistanceMode switch {

                PersistanceMode.SQLServer => @"SELECT [Id], [ProductId], [ProductClass], [ProductName], [Qty], [Options]
                                                FROM [Sales].[OrderedItems]
                                                WHERE [OrderId] = @OrderId",

                PersistanceMode.SQLite => @"SELECT [Id], [ProductId], [ProductClass], [ProductName], [Qty], [Options]
                                            FROM [OrderedItems]
                                            WHERE [OrderId] = @OrderId",

                _ => throw new InvalidDataException("Invalid persistance mode")

            };

            var items = await _settings.Connection.QueryAsync<OrderedItemDetails>(itemQuery, request);
            order.OrderedItems = items;

            return order;
            
        }

        private static OrderDetails MapOrderCompanies(OrderDetails order, CompanySummary customer, CompanySummary vendor, CompanySummary supplier) {
            order.Customer = customer;
            order.Vendor = vendor;
            order.Supplier = supplier;
            return order;
        }

    }

}