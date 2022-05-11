namespace OrderManager.Domain.Catalog;

public class Product {
    
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public IEnumerable<ProductAttribute> Attributes { get; set; } = Enumerable.Empty<ProductAttribute>();

}