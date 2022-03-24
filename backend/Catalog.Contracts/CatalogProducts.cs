namespace Catalog.Contracts;

public static class CatalogProducts {

    public delegate Task<ProductSummary[]> GetProducts();

    public delegate Task<ProductDetails> GetProductDetails(Guid id);

}
