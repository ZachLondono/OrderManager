using MediatR;
using OrderManager.ApplicationCore.Domain;
using OrderManager.ApplicationCore.Infrastructure;
using System.Data.OleDb;
using Dapper;

namespace OrderManager.ApplicationCore.Features.Orders;

public class GetAllOrders {

    public record Query() : IRequest<IEnumerable<Order>>;

    public class Handler : IRequestHandler<Query, IEnumerable<Order>> {

        private readonly AppConfiguration _config;

        public Handler(AppConfiguration config) {
            _config = config;
        }

        public async Task<IEnumerable<Order>> Handle(Query request, CancellationToken cancellationToken) {

            string query = @"SELECT Orders.[Id], Orders.[Number], Orders.[Name], Orders.[SupplierId], Orders.[VendorId], Orders.[CustomerId], Orders.[Notes], Statuses.[Id] as StatusId, Statuses.[Name] As StatusName, Statuses.[Level], Priorities.[Id] as PriorityId, Priorities.[Name] As PriorityName, Priorities.[Level]
                            FROM  ([Orders] 
                                LEFT JOIN [Statuses]
                                ON Orders.[StatusId] = Statuses.[Id])
                            LEFT JOIN [Priorities] 
                            ON Orders.[PriorityId] = Priorities.[Id];";

            using var connection = new OleDbConnection(_config.OrderConnectionString);

            connection.Open();

            IEnumerable<Order> orders = await connection.QueryAsync<Order, Status, Priority, Order>(query, (o, s, p) => {

                o.StatusId = s.Id;
                o.Status = s;
                o.PriorityId = p.Id;
                o.Priority = p;

                return o;

            }, splitOn:"StatusId,PriorityId");

            connection.Close();

            return orders;

        }
    }

}
