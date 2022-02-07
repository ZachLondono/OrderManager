using Dapper;
using FluentValidation;
using MediatR;
using OrderManager.ApplicationCore.Domain;
using OrderManager.ApplicationCore.Infrastructure;
using System.Data.OleDb;

namespace OrderManager.ApplicationCore.Features.Priorities;

public class CreatePriority {

    public record Command(string Name, int Level) : IRequest<Priority>;

    public class Validator : AbstractValidator<Command> {

        private readonly AppConfiguration _config;

        public Validator(AppConfiguration config) {
            _config = config;

            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .Must(IsNameUnique);

        }

        public bool IsNameUnique(string name) {
            const string query = "SELECT COUNT([Name]) FROM [Priorities] WHERE [Name] = [@Name];";
            using var connection = new OleDbConnection(_config.OrderConnectionString);
            connection.Open();
            int count = connection.QuerySingle<int>(query, new { Name = name });
            connection.Close();
            return count == 0;
        }

    }

    internal class Handler : IRequestHandler<Command, Priority> {

        private readonly AppConfiguration _config;

        public Handler(AppConfiguration config) {
            _config = config;
        }

        public async Task<Priority> Handle(Command request, CancellationToken cancellationToken) {

            string sql = "INSERT INTO [Priorities] ([Name], [Level]) VALUES ([@Name], [@Level]);";
            string query = "SELECT * FROM [Priorities] WHERE [Name] = [@Name];";

            Priority? priority = null;

            using var connection = new OleDbConnection(_config.OrderConnectionString);

            connection.Open();

            int rows = await connection.ExecuteAsync(sql, request);

            if (rows > 0)
                priority = await connection.QueryFirstOrDefaultAsync<Priority>(query, new { request.Name });

            if (priority is null) throw new InvalidOperationException("Could not create new priority level");

            connection.Close();


            return priority;

        }
    }

}
