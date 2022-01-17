using MediatR;
using FluentValidation;
using OrderManager.ApplicationCore.Domain;
using System.Data.OleDb;
using OrderManager.ApplicationCore.Infrastructure;
using Dapper;

namespace OrderManager.ApplicationCore.Features.Products;

public class CreateProduct { 
    
    public record Command(string ProductName, string ProductDescription, IEnumerable<string> Attributes) : IRequest<Product?>;

    public class Validator : AbstractValidator<Command> {
        private readonly AppConfiguration _config;

        public Validator(AppConfiguration config) {
            _config = config;

            RuleFor(p => p.ProductName)
                .NotNull()
                .NotEmpty()
                .WithMessage("Product Name must not be empty");

            RuleFor(p => p.ProductName).Must(IsNameUnique);

            RuleFor(p => p.ProductDescription)
                .NotNull()
                .NotEmpty()
                .WithMessage("Product Description must not be empty");
            
            RuleFor(p => p.Attributes)
                .NotNull()
                .WithMessage("Product Attributes must not be null");
        }

        public bool IsNameUnique(string name) {
            const string query = "SELECT COUNT(ProductName) FROM Products WHERE ProductName = @ProductName;";
            using var connection = new OleDbConnection(_config.ConnectionString);
            connection.Open();
            int count = connection.QuerySingle<int>(query, new { ProductName = name });
            connection.Close();
            return count == 0;
        }

    }

    internal class Handler : IRequestHandler<Command, Product?> {

        private readonly AppConfiguration _config;

        public Handler(AppConfiguration config) {
            _config = config;
        }

        public async Task<Product?> Handle(Command request, CancellationToken cancellationToken) {

            string sql = "INSERT INTO [Products] (ProductName, ProductDescription) VALUES (@ProductName, @ProductDescription);";
            string query = "SELECT * FROM [Products] WHERE ProductName = @ProductName;";

            using var connection = new OleDbConnection(_config.ConnectionString);
            connection.Open();

            int rowsAffected = await connection.ExecuteAsync(sql, request);

            Product? product = null;
            if (rowsAffected > 0) {
                product = await connection.QuerySingleAsync<Product>(query, new { request.ProductName });

                string attributeSql = "INSERT INTO [ProductAttributes] (AttributeName, ProductId) VALUES (@AttributeName, @ProductId);";

                List<object> parameters = new();
                foreach (string attribute in request.Attributes) {
                    parameters.Add(new {
                        AttributeName = attribute,
                        product.ProductId
                    });
                }

                rowsAffected = await connection.ExecuteAsync(attributeSql, parameters);
            }

            connection.Close();

            return product;
        }

    }
}