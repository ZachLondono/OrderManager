using Dapper;
using FluentValidation;
using MediatR;
using OrderManager.ApplicationCore.Domain;
using OrderManager.ApplicationCore.Infrastructure;
using System.Data.OleDb;

namespace OrderManager.ApplicationCore.Features.Companies;

public class GetCompanyByName {

    public record Query(string CompanyName) : IRequest<Company?>;

    public class Validator : AbstractValidator<Query> {
        public Validator() {
            RuleFor(q => q.CompanyName).NotEmpty().NotNull();
        }
    }

    public class Handler : IRequestHandler<Query, Company?> {
        private readonly AppConfiguration _config;

        public Handler(AppConfiguration config) {
            _config = config;
        }

        public async Task<Company?> Handle(Query request, CancellationToken cancellationToken) {

            const string query = "SELECT * FROM Companies WHERE [Name] = [@Name];";

            Company? company = null;

            using (var connection = new OleDbConnection(_config.OrderConnectionString)) {
                connection.Open();

                company = await connection.QuerySingleAsync<Company>(query, request);

                connection.Close();
            }

            return company;

        }

    }

}
