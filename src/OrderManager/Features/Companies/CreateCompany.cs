using Dapper;
using FluentValidation;
using MediatR;
using OrderManager.ApplicationCore.Domain;
using OrderManager.ApplicationCore.Infrastructure;
using System.Data.OleDb;

namespace OrderManager.ApplicationCore.Features.Companies;

public class CreateCompany {

    public record Command(
            string CompanyName,
            string ContactName,
            string ContactEmail,
            string ContactPhone,
            string AddressLine1,
            string AddressLine2,
            string AddressLine3,
            string City,
            string State,
            string PostalCode) : IRequest<Company?>;

    public class Validator : AbstractValidator<Command> {
        
        private readonly AppConfiguration _config;

        public Validator(AppConfiguration config) {
            _config = config;


            RuleFor(c => c.CompanyName)
                .NotEmpty()
                .NotNull()
                .Must(IsNameUnique);

            RuleFor(c => c.ContactName).NotNull();
            RuleFor(c => c.ContactEmail).NotNull();
            RuleFor(c => c.ContactPhone).NotNull();
            RuleFor(c => c.AddressLine1).NotNull();
            RuleFor(c => c.AddressLine2).NotNull();
            RuleFor(c => c.AddressLine3).NotNull();
            RuleFor(c => c.City).NotNull();
            RuleFor(c => c.State).NotNull();
            RuleFor(c => c.PostalCode).NotNull();

        }

        public bool IsNameUnique(string name) {
            const string query = "SELECT COUNT(CompanyName) FROM Companies WHERE CompanyName = @CompanyName;";
            using var connection = new OleDbConnection(_config.ConnectionString);
            connection.Open();
            int count = connection.QuerySingle<int>(query, new { CompanyName = name });
            connection.Close();
            return count == 0;
        }

    }

    public class Handler : IRequestHandler<Command, Company?> {

        private readonly AppConfiguration _config;

        public Handler(AppConfiguration config) {
            _config = config;
        }

        public async Task<Company?> Handle(Command request, CancellationToken cancellationToken) {

            const string sql = @"INSERT INTO [Companies] 
                                (CompanyName, ContactName, ContactEmail, ContactPhone, AddressLine1, AddressLine2, AddressLine3, City, State, PostalCode)
                                VALUES (@CompanyName, @ContactName, @ContactEmail, @ContactPhone, @AddressLine1, @AddressLine2, @AddressLine3, @City, @State, @PostalCode);";

            const string query = "SELECT * FROM Companies WHERE CompanyName = @CompanyName;";

            using var connection = new OleDbConnection(_config.ConnectionString);
            connection.Open();

            int rowsAffected = await connection.ExecuteAsync(sql, request);

            Company? company = null;
            if (rowsAffected > 0) {
                company = await connection.QuerySingleAsync<Company>(query, new { request.CompanyName });
            }

            connection.Close();

            return company;

        }

    }

}
