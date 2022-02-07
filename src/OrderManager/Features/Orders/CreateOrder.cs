using MediatR;
using FluentValidation;
using OrderManager.ApplicationCore.Domain;
using OrderManager.ApplicationCore.Infrastructure;
using System.Data.OleDb;
using Dapper;

namespace OrderManager.ApplicationCore.Features.Orders;

public class CreateOrder {
    
    public record Command(string Number,
                        string Name,
                        int SupplierId,
                        int VendorId,
                        int CustomerId,
                        int StatusId,
                        int PriorityId,
                        string Notes) : IRequest<Order?>;

    public class Validator : AbstractValidator<Command> {
        private readonly AppConfiguration _config;

        public Validator(AppConfiguration config) {
            _config = config;

            RuleFor(p => p.Number)
                .NotNull()
                .NotEmpty();

            RuleFor(p => p.Number)
                .Must(IsNumUnique)
                .WithMessage("Order number must be unique");


            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty();

        }

        public bool IsNumUnique(string number) {
            const string query = "SELECT COUNT([Number]) FROM Orders WHERE [Number] = [@Number];";
            using var connection = new OleDbConnection(_config.OrderConnectionString);
            connection.Open();
            int count = connection.QuerySingle<int>(query, new { Number = number });
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

            string sql = @"INSERT INTO [Orders] ([Number], [Name], [SupplierId], [VendorId], [CustomerId], [StatusId], [PriorityId], [Notes])
                        VALUES ([@Number], [@Name], [@SupplierId], [@VendorId], [@CustomerId], [@StatusId], [@PriorityId], [@Notes]);";
            
            string query = "SELECT * FROM [Orders] WHERE [Number] = [@Number];";

            using var connection = new OleDbConnection(_config.OrderConnectionString);

            connection.Open();

            int rowsAffected = await connection.ExecuteAsync(sql, request);

            Order? order = null;
            if (rowsAffected > 0) {
                order = await connection.QuerySingleOrDefaultAsync<Order>(query, new { request.Number });
            }

            connection.Close();

            return order;
        }

    }

}