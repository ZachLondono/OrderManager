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

            string query = "SELECT * FROM [Orders];";

            using var connection = new OleDbConnection(_config.ConnectionString);

            connection.Open();

            IEnumerable<Order> orders = await connection.QueryAsync<Order>(query);

            connection.Close();

            return orders;

        }
    }

}
