using Dapper;
using MediatR;
using OrderManager.ApplicationCore.Domain;
using OrderManager.ApplicationCore.Infrastructure;
using System.Data.OleDb;

namespace OrderManager.ApplicationCore.Features.Reports;

public class GetAllReports {

    public record Query() :IRequest<IEnumerable<Report>>;

    internal class Handler : IRequestHandler<Query, IEnumerable<Report>> {

        private readonly AppConfiguration _config;

        public Handler(AppConfiguration config) {
            _config = config;
        }

        public async Task<IEnumerable<Report>> Handle(Query request, CancellationToken cancellationToken) {

            string query = "SELECT * FROM Reports;";

            using var connection = new OleDbConnection(_config.OrderConnectionString);

            connection.Open();

            IEnumerable<Report> reports = await connection.QueryAsync<Report>(query);

            connection.Close();

            return reports;

        }
    }
}
