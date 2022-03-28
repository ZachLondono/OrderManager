using Catalog.Contracts;
using System.Data;
using Dapper;
using Catalog.Implementation.Domain;
using Microsoft.Extensions.Logging;

namespace Catalog.Implementation.Infrastructure;

public class ProductRepository {

    private readonly IDbConnection _connection;
    private readonly ILogger<ProductRepository> _logger;

    public ProductRepository(IDbConnection connection, ILogger<ProductRepository> logger) {
        _connection = connection;
        _logger = logger;
    }

    public async Task<ProductContext> GetProductById(Guid Id) {

        const string query = "SELECT [Id], [Name] FROM [Products] WHERE [Id] = @Id;";
        const string attrQuery = "SELECT [Option] FROM [Attributes] WHERE [ProductId] = @ProductId;";

        var productDto = await _connection.QuerySingleAsync<Persistance.Product>(query, new { Id = Id });
        var attributes = await _connection.QueryAsync<string>(attrQuery, new { ProductId = Id });

        var product = new Product(productDto.Id, productDto.Name);
        foreach (var attribute in attributes) {
            try { 
                product.AddAttribute(attribute);
            } catch(Exception e) {
                _logger.LogWarning("Could not add attribute to product: {Exception}", e);
            }
        }

        return new(product);
    }

    public async Task<ProductSummary[]> GetProducts() {

        var productDtos = await _connection.QueryAsync<Persistance.Product>("SELECT [Id], [Name] FROM [Products];");

        List<ProductSummary> products = new();

        foreach (var product in productDtos) {
            products.Add(new(product.Id, product.Name));
        }

        return products.ToArray();

    }

    /// <summary>
    /// Persists all the events which where applied to the product context
    /// </summary>
    /// <param name="product">Context to which events where applied</param>
    public async Task Save(ProductContext product) {

        var trx = _connection.BeginTransaction();

        var events = product.Events;
        foreach (var e in events) {

            if (e is NameChangeEvent namechange) {
                await ApplyNameChange(product, trx, namechange);
            } else if (e is AttributeAddedEvent attributeAdded) {
                await ApplyAttributeAdd(product, trx, attributeAdded);
            } else if (e is AttributeRemovedEvent attributeRemoved) {
                await ApplyAttributeRemove(product, trx, attributeRemoved);
            }

        }

        trx.Commit();

    }

    private async Task ApplyAttributeRemove(ProductContext product, IDbTransaction trx, AttributeRemovedEvent attributeRemoved) {
        const string query = @"DELETE FROM [ProductAttributes]
                                WHERE [ProductId] = @ProductId And [Name] = @Name;";
        await _connection.ExecuteAsync(query, new {
            ProductId = product.Id,
            attributeRemoved.Name
        }, trx);
    }

    private async Task ApplyAttributeAdd(ProductContext product, IDbTransaction trx, AttributeAddedEvent attributeAdded) {
        const string query = @"INSERT INTO [ProductAttributes] ([ProductId], [Name], [Default])
                                VALUES (@ProductId, @Name, @Default);";
        await _connection.ExecuteAsync(query, new {
            ProductId = product.Id,
            attributeAdded.Name,
            Default = ""
        }, trx);
    }

    private async Task ApplyNameChange(ProductContext product, IDbTransaction trx, NameChangeEvent namechange) {
        const string query = @"UPDATE [Products]
                                SET [Name] = @Name
                                WHERE [Id] = @Id;";
        await _connection.ExecuteAsync(query, new {
            product.Id,
            namechange.Name
        }, trx);
    }
}
