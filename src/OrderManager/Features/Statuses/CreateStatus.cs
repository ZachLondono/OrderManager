using Dapper;
using FluentValidation;
using MediatR;
using OrderManager.ApplicationCore.Domain;
using OrderManager.ApplicationCore.Infrastructure;
using System.Data.OleDb;

namespace OrderManager.ApplicationCore.Features.Statuses;

public class CreateStatus {

    public record Command(string Name, int Level) : IRequest<Status?>;

    public class Validator : AbstractValidator<Command> {
        private readonly AppConfiguration _config;

        public Validator(AppConfiguration config) {
            _config = config;

            RuleFor(s => s.Name)
                .NotNull()
                .NotEmpty();

        }

        public bool IsNameUnique(string name) {
            const string query = "SELECT COUNT([Name]) FROM Statuses WHERE [Name] = [@Name];";
            using var connection = new OleDbConnection(_config.OrderConnectionString);
            connection.Open();
            int count = connection.QuerySingle<int>(query, new { RefNum = name });
            connection.Close();
            return count == 0;
        }

    }

    internal class Handler : IRequestHandler<Command, Status?> {

        private readonly AppConfiguration _config;

        public Handler(AppConfiguration config) {
            _config = config;
        }

        public async Task<Status?> Handle(Command request, CancellationToken cancellationToken) {

            string sql = "INSERT INTO [Statuses] ([Name], [Level]) VALUES ([@Name], [@Level]);";
            string query = "SELECT * FROM [Statuses] WHERE [Name] = [@Name];";

            using var connection = new OleDbConnection(_config.OrderConnectionString);

            connection.Open();
            
            Status? status = null;

            int rows = await connection.ExecuteAsync(sql, request);

            if (rows > 0) {
                status = await connection.QueryFirstOrDefaultAsync<Status>(query, request);
            }

            connection.Close();

            return status;

        }
    }

}
