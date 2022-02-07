using Dapper;
using MediatR;
using OrderManager.ApplicationCore.Domain;
using OrderManager.ApplicationCore.Infrastructure;
using System.Data.OleDb;

namespace OrderManager.ApplicationCore.Features.Priorities;

public class GetAllPriorities {

    public record Query() : IRequest<IEnumerable<Priority>>;

    internal class Handler : IRequestHandler<Query, IEnumerable<Priority>> {

        private readonly AppConfiguration _config;

        public Handler(AppConfiguration config) {
            _config = config;
        }

        public async Task<IEnumerable<Priority>> Handle(Query request, CancellationToken cancellationToken) {

            string query = "SELECT * FROM [Priorities];";

            var connection = new OleDbConnection(_config.OrderConnectionString);

            connection.Open();

            IEnumerable<Priority> priorities = await connection.QueryAsync<Priority>(query);

            connection.Close();

            return priorities;

        }
    }

}