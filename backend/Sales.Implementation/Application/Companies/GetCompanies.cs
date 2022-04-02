using System.Data;
using Dapper;
using MediatR;
using Sales.Contracts;

namespace Sales.Implementation.Application.Companies;

internal class GetCompanies {

    public record Query() : IRequest<IEnumerable<CompanySummary>>;

    public class Handler : IRequestHandler<Query, IEnumerable<CompanySummary>> {

        private readonly IDbConnection _connection;

        public Handler(IDbConnection connection) {
            _connection = connection;
        }

        public async Task<IEnumerable<CompanySummary>> Handle(Query request, CancellationToken cancellationToken) {

            const string query = "SELECT [Id], [Name], [Roles] FROM [Companies];";

            var companies = await _connection.QueryAsync<CompanySummary>(query);

            return companies;

        }
    }

}