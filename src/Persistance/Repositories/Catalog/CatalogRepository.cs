namespace Persistance.Repositories.Catalog;

public class CatalogRepository : BaseRepository, ICatalogRepository {
    public CatalogRepository(ConnectionStringManager connectionStringManager) : base(connectionStringManager) { }

    public IEnumerable<CatalogProductDAO> GetCatalog() {
        const string sql = @"SELECT [Id], [Name] FROM [Products];";
        return Query<CatalogProductDAO>(sql);
    }

    public CatalogProductDAO GetProductById(int id) {
        const string query = @"SELECT [Id], [Name] FROM [Products] WHERE [Id] = [@Id];";
        return QuerySingleOrDefault<CatalogProductDAO>(query, new { Id = id });
    }

    public void UpdateProduct(CatalogProductDAO product) {
        const string sql = @"UPDATE [Products] SET [Name] = [@Name] WHERE [Id] = [@Id];";
        Execute(sql, product);
    }
}