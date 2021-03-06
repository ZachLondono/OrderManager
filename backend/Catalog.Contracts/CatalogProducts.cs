namespace Catalog.Contracts;

public static class CatalogProducts {

    public delegate Task<IEnumerable<ProductSummary>> GetProducts();

    public delegate Task<ProductDetails> GetProductDetails(int id);

    public delegate Task<ProductClass> GetProductClass(int id);

}
