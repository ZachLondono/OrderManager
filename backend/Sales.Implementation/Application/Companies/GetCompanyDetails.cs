using Dapper;
using MediatR;
using Sales.Contracts;

namespace Sales.Implementation.Application.Companies;

public class GetCompanyDetails {

    public record Query(int Id) : IRequest<CompanyDetails>;

    public class Handler : IRequestHandler<Query, CompanyDetails> {

        private readonly SalesSettings _settings;

        public Handler(SalesSettings settings) {
            _settings = settings;
        }

        public async Task<CompanyDetails> Handle(Query request, CancellationToken cancellationToken) {

            string query = _settings.PersistanceMode switch {

                PersistanceMode.SQLServer => @"SELECT [Id], [Name], [Line1], [Line2], [Line3], [City], [State], [Zip], [Roles]
                                                FROM [Sales].[Companies] WHERE [Id] = @Id;",

                PersistanceMode.SQLite => @"SELECT [Id], [Name], [Line1], [Line2], [Line3], [City], [State], [Zip], [Roles]
                                            FROM [Companies] WHERE [Id] = @Id;",

                _ => throw new InvalidDataException("Invalid persistance mode")

            };

            var company = await _settings.Connection.QuerySingleAsync<CompanyDetails>(query, request);

            string contactQuery = _settings.PersistanceMode switch {

                PersistanceMode.SQLServer => @"SELECT [Id], [Name], [Email], [Phone]
                                            FROM [Sales].[Contacts] WHERE [CompanyId] = @CompanyId;",

                PersistanceMode.SQLite => @"SELECT [Id], [Name], [Email], [Phone]
                                            FROM [Contacts] WHERE [CompanyId] = @CompanyId;",

                _ => throw new InvalidDataException("Invalid persistance mode")

            };

            var contacts = await _settings.Connection.QueryAsync<ContactDetails>(contactQuery, new { CompanyId = request.Id });
            company.Contacts = contacts;

            return company;

        }
    }

}