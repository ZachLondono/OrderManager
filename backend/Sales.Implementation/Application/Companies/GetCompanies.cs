using Dapper;
using MediatR;
using Sales.Contracts;

namespace Sales.Implementation.Application.Companies;

public class GetCompanies {

    public record Query() : IRequest<IEnumerable<CompanySummary>>;

    public class Handler : IRequestHandler<Query, IEnumerable<CompanySummary>> {

        private readonly SalesSettings _settings;

        public Handler(SalesSettings settings) {
            _settings = settings;
        }

        public async Task<IEnumerable<CompanySummary>> Handle(Query request, CancellationToken cancellationToken) {

            string query = _settings.PersistanceMode switch {

                PersistanceMode.SQLServer => "SELECT [Id], [Name], [Roles] FROM [Sales].[Companies];",

                PersistanceMode.SQLite => "SELECT [Id], [Name], [Roles] FROM [Companies];",

                _ => throw new InvalidDataException("Invalid persistance mode")

            };

            var companies = await _settings.Connection.QueryAsync<CompanySummary>(query);

            return companies;

        }
    }

}