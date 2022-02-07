using Dapper;
using FluentValidation;
using MediatR;
using OrderManager.ApplicationCore.Domain;
using OrderManager.ApplicationCore.Infrastructure;
using System.Data.OleDb;

namespace OrderManager.ApplicationCore.Features.Priorities;

public class UpdatePriority {

    public record Command(Priority Priority) : IRequest;

    public class Validator : AbstractValidator<Command> {

        private readonly AppConfiguration _config;

        public Validator(AppConfiguration config) {
            _config = config;

            RuleFor(p => p.Priority.PriorityName)
                .NotNull()
                .NotEmpty();

            RuleFor(p => p.Priority)
                .Must(IsNameUnique);


        }

        public bool IsNameUnique(Priority priority) {
            const string query = "SELECT COUNT([Name]) FROM [Priorities] WHERE [Name] = [@Name] AND NOT [Id] = [@Id];";
            using var connection = new OleDbConnection(_config.OrderConnectionString);
            connection.Open();
            int count = connection.QuerySingleOrDefault<int>(query, priority);
            connection.Close();
            return count == 0;
        }

    }

    internal class Handler : IRequestHandler<Command> {

        private readonly AppConfiguration _config;

        public Handler(AppConfiguration config) {
            _config = config;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken) {

            string sql = "UPDATE [Priorities] SET [Name] = [@Name], [Level] = [@Level] WHERE [Id] = [@Id];";

            var connection = new OleDbConnection(_config.OrderConnectionString);

            connection.Open();
            
            // when updating using OleDb, paramaters must be added in the same order they where used in the sql statment
            DynamicParameters param = new();
            param.Add("@Name", request.Priority.PriorityName);
            param.Add("@Level", request.Priority.Level);
            param.Add("@Id", request.Priority.Id);

            int rows = await connection.ExecuteAsync(sql, param);

            connection.Close();

            if (rows == 0) throw new InvalidOperationException("Failed to update priority");

            return new Unit();

        }

    }

}