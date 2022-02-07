using MediatR;
using Dapper;
using OrderManager.ApplicationCore.Domain;
using OrderManager.ApplicationCore.Infrastructure;
using System.Data.OleDb;

namespace OrderManager.ApplicationCore.Features.Statuses;

public class GetAllStatuses {

    public record Query() : IRequest<IEnumerable<Status>>;

    internal class Handler : IRequestHandler<Query, IEnumerable<Status>> {

        private readonly AppConfiguration _config;

        public Handler(AppConfiguration config) {
            _config = config;
        }

        public async Task<IEnumerable<Status>> Handle(Query request, CancellationToken cancellationToken) {

            string query = "SELECT * FROM [Statuses];";

            using var connection = new OleDbConnection(_config.OrderConnectionString);

            connection.Open();

            var statuses = await connection.QueryAsync<Status>(query);

            connection.Close();

            return statuses;

        }
    }

}