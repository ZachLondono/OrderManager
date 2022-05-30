using System.Data;
using Dapper;
using MediatR;
using Sales.Contracts;

namespace Sales.Implementation.Application.Orders;

public class GetOrderDetails {

    public record Query(int OrderId) : IRequest<OrderDetails>;

    public class Handler : IRequestHandler<Query, OrderDetails> {

        private readonly IDbConnection _connection;

        public Handler(IDbConnection connection) {
            _connection = connection;
        }

        public async Task<OrderDetails> Handle(Query request, CancellationToken cancellationToken) {

            const string query = @"SELECT [Id], [Name], [Number], [Status], [PlacedDate], [ConfirmedDate], [ReleasedDate], [CompletedDate], [LastModifiedDate], [Info],
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
                                    WHERE [Sales].[Orders].Id = @OrderId;";

            var data = await _connection.QueryAsync<OrderDetails, CompanySummary, CompanySummary, CompanySummary, OrderDetails>(sql:query, param:request, map:MapOrderCompanies, splitOn:"Id");
            var order = data.First();

            const string itemQuery = @"SELECT [Id], [ProductId], [ProductClass], [ProductName], [Qty], [Options]
                                        FROM [Sales].[OrderedItems]
                                        WHERE [OrderId] = @OrderId";

            var items = await _connection.QueryAsync<OrderedItemDetails>(itemQuery, request);
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