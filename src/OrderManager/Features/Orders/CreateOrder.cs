using MediatR;
using FluentValidation;
using OrderManager.ApplicationCore.Domain;
using OrderManager.ApplicationCore.Infrastructure;
using System.Data.OleDb;
using Dapper;

namespace OrderManager.ApplicationCore.Features.Orders;

public class CreateOrder {
    
    public record Command(string RefNum, DateTime OrderDate) : IRequest<Order?>;

    public class Validator : AbstractValidator<Command> {
        private readonly AppConfiguration _config;

        public Validator(AppConfiguration config) {
            _config = config;

            RuleFor(p => p.RefNum)
                .NotNull()
                .NotEmpty();

            RuleFor(p => p.RefNum)
                .Must(IsNameUnique);

            RuleFor(p => p.OrderDate)
                .NotNull();
        }

        public bool IsNameUnique(string name) {
            const string query = "SELECT COUNT(RefNum) FROM Orders WHERE RefNum = @RefNum;";
            using var connection = new OleDbConnection(_config.ConnectionString);
            connection.Open();
            int count = connection.QuerySingle<int>(query, new { RefNum = name });
            connection.Close();
            return count == 0;
        }

    }

    internal class Handler : IRequestHandler<Command, Order?> {

        private readonly AppConfiguration _config;

        public Handler(AppConfiguration config) {
            _config = config;
        }

        public async Task<Order?> Handle(Command request, CancellationToken cancellationToken) {

            string sql = "INSERT INTO [Orders] (RefNum, OrderDate) VALUES (@RefNum, @OrderDate)";
            string query = "SELECT * FROM [Orders] WHERE RefNum = @RefNum;";

            using var connection = new OleDbConnection(_config.ConnectionString);

            connection.Open();

            int rowsAffected = await connection.ExecuteAsync(sql, request);

            Order? order = null;
            if (rowsAffected > 0) {
                order = await connection.QuerySingleOrDefaultAsync<Order>(query, new { request.RefNum });
            }

            connection.Close();

            return order;
        }
    }

}
