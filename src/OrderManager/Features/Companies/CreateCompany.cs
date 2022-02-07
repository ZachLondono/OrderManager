using Dapper;
using FluentValidation;
using MediatR;
using OrderManager.ApplicationCore.Domain;
using OrderManager.ApplicationCore.Infrastructure;
using System.Data.OleDb;

namespace OrderManager.ApplicationCore.Features.Companies;

public class CreateCompany {

    public record Command(
            string Name,
            string Contact,
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


            RuleFor(c => c.Name)
                .NotEmpty()
                .NotNull()
                .Must(IsNameUnique);

            RuleFor(c => c.Contact).NotNull();
            RuleFor(c => c.AddressLine1).NotNull();
            RuleFor(c => c.AddressLine2).NotNull();
            RuleFor(c => c.AddressLine3).NotNull();
            RuleFor(c => c.City).NotNull();
            RuleFor(c => c.State).NotNull();
            RuleFor(c => c.PostalCode).NotNull();

        }

        public bool IsNameUnique(string name) {
            const string query = "SELECT COUNT([Name]) FROM Companies WHERE [Name] = [@Name];";
            using var connection = new OleDbConnection(_config.OrderConnectionString);
            connection.Open();
            int count = connection.QuerySingle<int>(query, new { Name = name });
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
                                ([Name], [Contact], [AddressLine1], [AddressLine2], [AddressLine3], [City], [State], [PostalCode])
                                VALUES ([@Name], [@Contact], [@AddressLine1], [@AddressLine2], [@AddressLine3], [@City], [@State], [@PostalCode]);";

            const string query = "SELECT * FROM Companies WHERE [Name] = [@Name];";

            using var connection = new OleDbConnection(_config.OrderConnectionString);
            connection.Open();

            int rowsAffected = await connection.ExecuteAsync(sql, request);

            Company? company = null;
            if (rowsAffected > 0) {
                company = await connection.QuerySingleAsync<Company>(query, new { request.Name });
            }

            connection.Close();

            return company;

        }

    }

}
