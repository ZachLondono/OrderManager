using System.Data;
using Dapper;
using MediatR;
using Sales.Contracts;

namespace Sales.Implementation.Application.Companies;

public class GetCompanyDetails {

    public record Query(int Id) : IRequest<CompanyDetails>;

    public class Handler : IRequestHandler<Query, CompanyDetails> {

        private readonly IDbConnection _connection;

        public Handler(IDbConnection connection) {
            _connection = connection;
        }

        public async Task<CompanyDetails> Handle(Query request, CancellationToken cancellationToken) {

            const string query = @"SELECT [Id], [Name], [Line1], [Line2], [Line3], [City], [State], [Zip], [Roles]
                                    FROM [Sales].[Companies] WHERE [Id] = @Id;";

            var company = await _connection.QuerySingleAsync<CompanyDetails>(query, request);

            const string contactQuery = @"SELECT [Id], [Name], [Email], [Phone]
                                            FROM [Sales].[Contacts] WHERE [CompanyId] = @CompanyId;";

            var contacts = await _connection.QueryAsync<ContactDetails>(contactQuery, new { CompanyId = request.Id });
            company.Contacts = contacts;

            return company;

        }
    }

}