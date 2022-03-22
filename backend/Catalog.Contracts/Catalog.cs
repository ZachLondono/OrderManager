namespace Catalog.Contracts;

public static class Catalog {

    public delegate ProductSummary[] GetProducts();

    public delegate ProductDetails GetProduct(Guid id);

}
