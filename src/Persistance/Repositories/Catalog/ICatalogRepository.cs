namespace Persistance.Repositories.Catalog;

public interface ICatalogRepository {
    public IEnumerable<CatalogProductDAO> GetCatalog();
    public CatalogProductDAO GetProductById(int id);
    public void UpdateProduct(CatalogProductDAO product);
}