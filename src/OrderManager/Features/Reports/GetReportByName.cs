using Dapper;
using MediatR;
using OrderManager.ApplicationCore.Domain;
using OrderManager.ApplicationCore.Infrastructure;
using System.Data.OleDb;

namespace OrderManager.ApplicationCore.Features.Reports;

public class GetReportByName {

    public record Query(string ReportName) : IRequest<Report>;

    internal class Handler : IRequestHandler<Query, Report> {

        private readonly AppConfiguration _config;

        public Handler(AppConfiguration config) {
            _config = config;
        }

        public async Task<Report> Handle(Query request, CancellationToken cancellationToken) {

            string query = "SELECT * FROM Reports WHERE ReportName = @ReportName;";

            using var connection = new OleDbConnection(_config.OrderConnectionString);

            connection.Open();

            Report report = await connection.QueryFirstOrDefaultAsync<Report>(query, request);

            connection.Close();

            return report;

        }
    }
}
