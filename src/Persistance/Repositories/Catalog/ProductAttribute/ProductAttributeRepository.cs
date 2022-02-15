namespace Persistance.Repositories.Catalog.ProductAttribute;

public class ProductAttributeRepository : BaseRepository, IProductAttributeRepository {
    public ProductAttributeRepository(ConnectionStringManager connectionStringManager) : base(connectionStringManager) { }

    public ProductAttributeDAO CreateAttribute(int productId, string name) {
        const string query = "INSERT INTO [ProductAttributes] ([ProductId], [Name]) VALUES ([@ProductId], [@Name]) RETURNING Id;";
        int newId = QuerySingleOrDefault<int>(query, new { ProductId = productId, Name = name });
        const string sql = "INSERT INTO [Product_Attribute] ([ProductId], [AttributeId]) VALUES ([@ProductId], [@AttributeId]);";
        Execute(sql, new { ProductId = productId, AttributeId = newId });

        return new() {
            Id = newId,
            ProductId = productId,
            Name = name
        };
    }

    public IEnumerable<ProductAttributeDAO> GetAttributesByProductId(int productId) {
        const string query = @"SELECT [ProductAttributes].[Id], [Product_Attribute].[ProductId], [ProductAttributes].[Name]
                                FROM [ProductAttributes]
                                LEFT JOIN [Product_Attribute]
                                On [Product_Attribute].[AttributeId] = [ProductAttributes].[Id]
                                WHERE [Product_Attribute].[ProductId] = [@ProductId]";
        return Query<ProductAttributeDAO>(query, new { ProductId = productId });
    }

    public void UpdateAttribute(ProductAttributeDAO productAttribute) {
        const string sql = @"UPDATE [ProductAttributes] SET [Name] = [@Name] WHERE [Id] = [@Id];";
        Execute(sql, productAttribute);
    }

    public void RemoveAttributeFromProduct(int productId, int attributeId) {
        const string sql = @"DELETE FROM [Product_Attribute] WHERE [ProductId] = [@ProductId] AND [AttributeId] = [@AttributeId]";
        Execute(sql, new { ProductId = productId, AttributeId = attributeId });
    }

}


