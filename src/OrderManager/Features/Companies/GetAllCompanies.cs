using Dapper;
using MediatR;
using OrderManager.ApplicationCore.Domain;
using OrderManager.ApplicationCore.Infrastructure;
using System.Data.OleDb;

namespace OrderManager.ApplicationCore.Features.Companies;

public class GetAllCompanies {

    public record Query() : IRequest<IEnumerable<Company>>;

    public class Handler : IRequestHandler<Query, IEnumerable<Company>> {
        private readonly AppConfiguration _config;

        public Handler(AppConfiguration config) {
            _config = config;
        }

        public async Task<IEnumerable<Company>> Handle(Query request, CancellationToken cancellationToken) {

            const string query = "SELECT * FROM Companies;";

            IEnumerable<Company> companies = Enumerable.Empty<Company>();

            using (var connection = new OleDbConnection(_config.ConnectionString)) {
                connection.Open();

                companies = await connection.QueryAsync<Company>(query);

                connection.Close();
            }

            return companies;

        }

    }

}
