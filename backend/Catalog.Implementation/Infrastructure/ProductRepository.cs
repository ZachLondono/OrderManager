﻿using System.Data;
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

    /// <summary>
    /// Adds a new product with the given name
    /// </summary>
    /// <param name="name">The name for the new product</param>
    /// <returns>A ProductContext to track changes to product</returns>
    public async Task<ProductContext> Add(string name) {

        var newId = Guid.NewGuid();
        var product = new Product(newId, name);

        const string query = "INSERT INTO [Products] ([Id], [Name]) VALUES (@Id, @Name);";

        int rows = await _connection.ExecuteAsync(query, new {
            Id = newId,
            Name = name
        });


        if (rows == 0)
            _logger.LogWarning("No rows affected inserting new product {Name}", name);

        return new(product);

    }

    /// <summary>
    /// Removes a product and all of its attributes
    /// </summary>
    /// <param name="product">The product to remove</param>
    public async Task Remove(Guid productId) {

        const string query1 = "DELETE FROM [Products] WHERE [Id] = @Id";
        const string query2 = "DELETE FROM [ProductAttributes] WHERE [ProductId] = @Id";

        int rows = await _connection.ExecuteAsync(query1, new { productId });
        rows += await _connection.ExecuteAsync(query2, new { productId });

        _logger.LogInformation("Product deleted, {Rows} affected", rows);

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
        int rows = await _connection.ExecuteAsync(query, new {
            ProductId = product.Id,
            attributeRemoved.Name
        }, trx);

        if (rows == 0)
            _logger.LogWarning("No rows affected removing attribute name {Name} from product {ProductId}", attributeRemoved.Name, product.Id);

    }

    private async Task ApplyAttributeAdd(ProductContext product, IDbTransaction trx, AttributeAddedEvent attributeAdded) {
        const string query = @"INSERT INTO [ProductAttributes] ([ProductId], [Name], [Default])
                                VALUES (@ProductId, @Name, @Default);";
        int rows = await _connection.ExecuteAsync(query, new {
            ProductId = product.Id,
            attributeAdded.Name,
            Default = ""
        }, trx);

        if (rows == 0)
            _logger.LogWarning("No rows affected inserting new attribute {Name} into product {ProductId}", attributeAdded.Name, product.Id);

    }

    private async Task ApplyNameChange(ProductContext product, IDbTransaction trx, NameChangeEvent namechange) {
        const string query = @"UPDATE [Products]
                                SET [Name] = @Name
                                WHERE [Id] = @Id;";
        int rows = await _connection.ExecuteAsync(query, new {
            product.Id,
            namechange.Name
        }, trx);

        if (rows == 0)
            _logger.LogWarning("No rows affected setting product name {Name} for product {ProductId}", namechange.Name, product.Id);
    }
}
