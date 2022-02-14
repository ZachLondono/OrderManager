namespace Persistance.Repositories.Catalog;

public class CatalogRepository : BaseRepository, ICatalogRepository {
    public CatalogRepository(ConnectionStringManager connectionStringManager) : base(connectionStringManager) { }

    public IEnumerable<CatalogProductDAO> GetCatalog() {
        const string sql = @"SELECT [Id], [Name] FROM [Products];";
        return Query<CatalogProductDAO>(sql);
    }
}