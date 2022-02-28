using Dapper;
using MediatR;
using Microsoft.Data.Sqlite;
using OrderManager.Shared;
using Persistance;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace OrderManager.Features.OrderList;

public class GetOrders {

    public record Query() : IRequest<Either<OrderList, Error>>;

    public record OrderList(IEnumerable<OrderListItem> Orders);

    public class OrderListItem {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public DateTime LastModified { get; set; }
        public bool IsPriority { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string VendorName { get; set; } = string.Empty;
        public string SupplierName { get; set; } = string.Empty;
    }

    public class Handler : IRequestHandler<Query, Either<OrderList, Error>> {

        private readonly ConnectionStringManager _connectionStringManager;

        public Handler(ConnectionStringManager connectionStringManager) {
            _connectionStringManager = connectionStringManager;
        }

        public Task<Either<OrderList, Error>> Handle(Query request, CancellationToken cancellationToken) {

            const string query = @"SELECT Orders.Id, Orders.IsPriority, Orders.LastModified, Orders.Number, Orders.Name, Customer.Name as CustomerName, Vendor.Name as VendorName, Supplier.Name as SupplierName
                                FROM Orders
                                LEFT JOIN Companies AS Customer
                                ON CustomerId = Customer.Id
                                LEFT JOIN Companies AS Vendor
                                ON VendorId = Vendor.Id
                                LEFT JOIN Companies AS Supplier
                                ON SupplierId = Supplier.Id";

            try {

                IEnumerable<OrderListItem> items;

                using var connection = new SqliteConnection(_connectionStringManager.GetConnectionString);
                connection.Open();

                items = connection.Query<OrderListItem>(query);

                connection.Close();

                return Task.FromResult(new Either<OrderList, Error>(new OrderList(items)));
            } catch (Exception e) {
                Debug.WriteLine(e);
            }

            return Task.FromResult(new Either<OrderList, Error>(new Error("No Data", "Could not connect to database with connection string 'Data Source=C:/'")));

        }

    }

}
