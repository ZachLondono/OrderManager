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

    public async Task<ProductContext> GetProductById(int id) {

        const string query = "SELECT [Id], [Name] FROM [Products] WHERE [Id] = @Id;";
        const string attrQuery = "SELECT [Option] FROM [Attributes] WHERE [ProductId] = @ProductId;";

        var productDto = await _connection.QuerySingleAsync<Persistance.Product>(query, new { Id = id });
        var attributes = await _connection.QueryAsync<string>(attrQuery, new { ProductId = id });

        var product = new Product(productDto.Id, productDto.Name);
        foreach (var attribute in attributes) {
            try {
                product.AddAttribute(attribute);
            } catch (Exception e) {
                _logger.LogWarning("Could not add attribute to product: {Exception}", e);
            }
        }

        return new(product);

    }

    /// <summary>
    /// Adds a new product with the given name
    /// </summary>
    /// <param name="name">The name for the new product</param>
    /// <returns>A ProductContext to track changes to product</returns>
    public async Task<ProductContext> Add(string name) {

        const string query = @"INSERT INTO [Products] ([Name]) VALUES (@Name);
                                SELECT SCOPE_IDENTITY();";

        int newId = await _connection.QuerySingleAsync<int>(query, new {
            Name = name
        });

        var product = new Product(newId, name);

        return new(product);

    }

    /// <summary>
    /// Removes a product and all of its attributes
    /// </summary>
    /// <param name="product">The product to remove</param>
    public async Task Remove(int productId) {

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
