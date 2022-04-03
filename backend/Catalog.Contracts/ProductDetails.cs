namespace Catalog.Contracts;

public class ProductDetails {

    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public IEnumerable<ProductAttribute> Attributes { get; set; } = Enumerable.Empty<ProductAttribute>();

}
