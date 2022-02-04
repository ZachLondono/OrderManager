using Dapper;
using FluentValidation;
using MediatR;
using OrderManager.ApplicationCore.Domain;
using OrderManager.ApplicationCore.Infrastructure;
using System.Data.OleDb;

namespace OrderManager.ApplicationCore.Features.Emails;

public class CreateEmail {
    
    public record Command(string Name, string To, string Cc, string Bcc, string Subject, string Body) : IRequest<Email?>;

    public class Validator : AbstractValidator<Command> {
        private readonly AppConfiguration _config;

        public Validator(AppConfiguration config) {
            _config = config;

            RuleFor(c => c.Name)
                .NotEmpty()
                .NotNull()
                .Must(IsNameUnique);

        }

        public bool IsNameUnique(string name) {
            const string query = "SELECT COUNT([Name]) FROM [Emails] WHERE [Name] = [@Name];";
            using var connection = new OleDbConnection(_config.OrderConnectionString);
            connection.Open();
            int count = connection.QuerySingle<int>(query, new { Name = name });
            connection.Close();
            return count == 0;
        }

    }

    internal class Handler : IRequestHandler<Command, Email?> {

        private readonly AppConfiguration _config;

        public Handler(AppConfiguration config) {
            _config = config;
        }
        
        public async Task<Email?> Handle(Command request, CancellationToken cancellationToken) {

            string sql = @"INSERT INTO [Emails]
                            ([Name], [To], [Cc], [Bcc], [Subject], [Body])
                            VALUES ([@Name], [@To], [@Cc], [@Bcc], [@Subject], [@Body]);";

            string query = "SELECT * FROM [Emails] WHERE [Name] = [@Name];";

            using var connection = new OleDbConnection(_config.OrderConnectionString);

            connection.Open();

            int rows = await connection.ExecuteAsync(sql, request);

            Email? newEmail = null;
            if (rows > 0) {
                newEmail = await connection.QueryFirstOrDefaultAsync<Email>(query, request);
            } 

            connection.Close();

            return newEmail;

        }
    }

}
