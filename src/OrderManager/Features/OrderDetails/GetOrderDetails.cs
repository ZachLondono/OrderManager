using Dapper;
using MediatR;
using Microsoft.Data.Sqlite;
using OrderManager.Shared;
using Persistance;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace OrderManager.Features.OrderDetails;

public class GetOrderDetails {

    public record Query(Guid Id) : IRequest<QueryResult<OrderDetails>>;

    public class Handler : IRequestHandler<Query, QueryResult<OrderDetails>> {

        private readonly ConnectionStringManager _connectionStringManager;

        public Handler(ConnectionStringManager connectionStringManager) {
            _connectionStringManager = connectionStringManager;
        }

        public Task<QueryResult<OrderDetails>> Handle(Query request, CancellationToken cancellationToken) {

            const string orderQuery = "SELECT * FROM Orders WHERE Id = @Id;";
            const string companyQuery = "SELECT * FROM Companies WHERE Id = @CompanyId;";
            const string orderItemQuery = "SELECT * FROM OrderItems WHERE OrderId = @Id;";
            const string itemOptionsQuery = "SELECT * FROM OrderItemOptions WHERE ItemId = @ItemId;";

            try {

                using var connection = new SqliteConnection(_connectionStringManager.GetConnectionString);

                connection.Open();

                var details = connection.QuerySingle<OrderDetails>(orderQuery, new { Id = request.Id.ToString() });
                details.Customer = connection.QuerySingle<CompanyDetails>(companyQuery, new { CompanyId = details.CustomerId });
                details.Vendor = connection.QuerySingle<CompanyDetails>(companyQuery, new { CompanyId = details.VendorId });
                details.Supplier = connection.QuerySingle<CompanyDetails>(companyQuery, new { CompanyId = details.SupplierId });

                details.OrderedProducts = connection.Query<OrderedProduct>(orderItemQuery, new { Id = request.Id.ToString() });
                foreach (var product in details.OrderedProducts) {
                    product.Options = connection.Query<ProductOption>(itemOptionsQuery, new { ItemId = product.Id });
                }

                connection.Close();

                return Task.FromResult(new QueryResult<OrderDetails>(details));

            } catch (InvalidDataException) {

                return Task.FromResult(new QueryResult<OrderDetails>(new Error("Not Found", $"Could not find order with id '{request.Id}'")));

            } catch (Exception e) {
                return Task.FromResult(new QueryResult<OrderDetails>(new Error("Error", $"Could not find order with id '{request.Id}'\n{e}")));
            }

        }
    }

}
