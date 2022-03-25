using System.Data;

namespace Catalog.Implementation.Infrastructure;

public class ProductRepository {

    private readonly IDbConnection _connection;

    public ProductRepository(IDbConnection connection) {
        _connection = connection;
    }

    public Task<ProductContext> GetProductById(Guid Id) => throw new NotImplementedException();

    /// <summary>
    /// Persists all the events which where applied to the product context
    /// </summary>
    /// <param name="product">Context to which events where applied</param>
    public Task Save(ProductContext product) => throw new NotImplementedException();

}
