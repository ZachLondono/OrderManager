namespace Catalog.Contracts;

public class ProductDetails {

    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string[] Attributes { get; set; } = Array.Empty<string>();

}
